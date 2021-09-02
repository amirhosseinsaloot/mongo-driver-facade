using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test")]
namespace MongoDriver.Facade
{
    internal class ReflectionHelper
    {
        /// <summary>
        /// Get all defined collection with their configuration.
        /// </summary>
        /// <returns>Key value pairs that key represents type of collection and value represents configuration of collection</returns>
        public Dictionary<string, CreateCollectionOptions> GetCollections()
        {
            var collectionWithConfiguration = new Dictionary<string, CreateCollectionOptions>();

            var collectionConfigurationType = typeof(ICollectionConfiguration<>);
            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(p => p.GetTypes());
            var mongoDbCollectionType = typeof(MongoDbCollection);


            var configureMethodName = nameof(ICollectionConfiguration<MongoDbCollection>.Configure);

            // List of all collections configuration
            var collectionConfigurations = (from x in allTypes
                                            from z in x.GetInterfaces()

                                            let y = x.BaseType

                                            where
                                            (y != null && y.IsGenericType && collectionConfigurationType.IsAssignableFrom(y.GetGenericTypeDefinition()))
                                            ||
                                            (z.IsGenericType && collectionConfigurationType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                                            select x);

            // List of all defined collection without considering whether they have configuration or not.
            var definedCollections = allTypes
                                     .Where(p =>
                                            mongoDbCollectionType.IsAssignableFrom(p)
                                            && p.IsClass && p != mongoDbCollectionType)
                                     .Select(p => p.Name);


            // Get generic arguments from collection configuration
            // and execute configure method for getting CreateCollectionOptions.
            // Finally add into collectionWithConfiguration.
            foreach (var item in collectionConfigurations)
            {
                if (collectionWithConfiguration.ContainsKey(item.Name))
                {
                    throw new Exception($"Collection type {item.Name} has more than one configuration.");
                }

                var instance = Activator.CreateInstance(item);
                MethodInfo methodInfo = item.GetMethod(configureMethodName);

                // Execute Configure method for getting CreateCollectionOptions
                var createCollectionOptions = (CreateCollectionOptions)methodInfo.Invoke(instance, null);

                var collectionName = item
                                     .GetInterface(collectionConfigurationType.Name)
                                     .GetGenericArguments()
                                     .FirstOrDefault()
                                     .Name;

                collectionWithConfiguration.Add(collectionName, createCollectionOptions);
            }

            // List of collections that have not configuration
            var collectionsWithoutConfiguration = definedCollections
                                                  .Except(collectionConfigurations
                                                  .Select(p =>
                                                          p.GetInterface(collectionConfigurationType.Name)
                                                          .GetGenericArguments()
                                                          .FirstOrDefault().Name));

            foreach (var item in collectionsWithoutConfiguration)
            {
                collectionWithConfiguration.Add(item, null);
            }

            return collectionWithConfiguration;
        }
    }
}
