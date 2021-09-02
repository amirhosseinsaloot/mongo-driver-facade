using FluentAssertions;
using MongoDB.Driver;
using MongoDriver.Facade;
using MongoDriver.Facade.TestApi.Models;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class ReflectionHelperTest
    {
        [Fact]
        public void GetCollectionTest()
        {
            // Arrange
            var expected = new Dictionary<string, CreateCollectionOptions>();

            // Book
            var bookConfigration = new BookConfigration().Configure();
            expected.Add(nameof(Book), bookConfigration);

            // Food
            expected.Add(nameof(Food), null);

            // Location
            expected.Add(nameof(Location), null);

            // Person
            expected.Add(nameof(Person), null);

            // Post
            expected.Add(nameof(Post), null);

            // Product
            var productConfigration = new ProductConfigration().Configure();
            expected.Add(nameof(Product), productConfigration);

            // Act
            var actual = new ReflectionHelper().GetCollections();

            // Assert
            actual.Should().BeEquivalentTo(expected);

        }
    }
}