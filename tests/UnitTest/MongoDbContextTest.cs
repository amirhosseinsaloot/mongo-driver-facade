using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDriver.Facade;
using MongoDriver.Facade.TestApi.Models;
using Moq;
using System.Collections.Generic;
using Xunit;


namespace Test;

public class MongoDbContextTest
{
    private readonly Mock<IConfigurationSection> _configurationSectionConnectionStringMock;

    private readonly Mock<IConfigurationSection> _configurationSectionDatabaseMock;

    private readonly Mock<IConfiguration> _configurationMock;

    public MongoDbContextTest()
    {
        _configurationSectionConnectionStringMock = new Mock<IConfigurationSection>();
        _configurationSectionDatabaseMock = new Mock<IConfigurationSection>();
        _configurationMock = new Mock<IConfiguration>();
    }

    [Fact]
    public void CollectionCreationTest()
    {
        // Arrange
        _configurationSectionConnectionStringMock.Setup(p => p.Value).Returns("mongodb://localhost:27017");
        _configurationSectionDatabaseMock.Setup(p => p.Value).Returns("TestDb");
        _configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);
        _configurationMock.Setup(a => a.GetSection("MongoSettings:ConnectionString")).Returns(_configurationSectionConnectionStringMock.Object);
        _configurationMock.Setup(a => a.GetSection("MongoSettings:Database")).Returns(_configurationSectionDatabaseMock.Object);

        // Collections
        var expected = new List<string> { nameof(Book), nameof(Food), nameof(Location), nameof(Person), nameof(Post), nameof(Product) };

        // Drop current database 
        var client = new MongoClient(_configurationSectionConnectionStringMock.Object.Value);
        client.DropDatabase(_configurationSectionDatabaseMock.Object.Value);


        // Act
        var mongoDbContext = new MongoDbContext(_configurationMock.Object);
        var database = client.GetDatabase(_configurationSectionDatabaseMock.Object.Value);
        var actual = database.ListCollectionNames().ToList();

        // Assert
        actual.Should().BeEquivalentTo(expected);

    }
}
