using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;
using TheWhiskyRealm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Core.Models.AdminArea.Country;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class CountryServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IEnumerable<Country> countries;
    private IRepository repository;
    private ICountryService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);

        

        
        repository = new Repository(dbContext);
        service = new CountryService(repository);
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task GetTotalCountriesAsync_ReturnsCorrectCount()
    {
        // Arrange
        var expectedCount = 3; 
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Name = "Bulgaria" },
        new Country { Name = "United Kingdom" },
        new Country { Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        // Act
        var actualCount = await service.GetTotalCountriesAsync();

        // Assert
        Assert.That(actualCount, Is.EqualTo(expectedCount));
    }

    [Test]
    public async Task CountryExistsAsync_WithExistingCountryId_ReturnsTrue()
    {
        // Arrange
        var existingCountryId = 1;
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Id = existingCountryId, Name = "Bulgaria" },
        new Country { Id = 2, Name = "United Kingdom" },
        new Country { Id = 3, Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        // Act
        var exists = await service.CountryExistsAsync(existingCountryId);

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task CountryExistsAsync_WithNonExistingCountryId_ReturnsFalse()
    {
        // Arrange
        var nonExistingCountryId = 100; 
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Id = 1, Name = "Bulgaria" },
        new Country { Id = 2, Name = "United Kingdom" },
        new Country { Id = 3, Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        // Act
        var exists = await service.CountryExistsAsync(nonExistingCountryId);

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task CountryWithNameExistsAsync_WithExistingCountryNameAndDifferentId_ReturnsTrue()
    {
        // Arrange
        var existingCountryName = "Bulgaria";
        var existingCountryId = 1;
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Id = existingCountryId, Name = existingCountryName },
        new Country { Id = 2, Name = "United Kingdom" },
        new Country { Id = 3, Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        // Act
        var exists = await service.CountryWithNameExistsAsync(existingCountryName, existingCountryId + 1);

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task CountryWithNameExistsAsync_WithExistingCountryNameAndSameId_ReturnsFalse()
    {
        // Arrange
        var existingCountryName = "Bulgaria";
        var existingCountryId = 1;
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Id = existingCountryId, Name = existingCountryName },
        new Country { Id = 2, Name = "United Kingdom" },
        new Country { Id = 3, Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        // Act
        var exists = await service.CountryWithNameExistsAsync(existingCountryName, existingCountryId); 

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task CountryWithNameExistsAsync_WithNonExistingCountryName_ReturnsFalse()
    {
        // Arrange
        var nonExistingCountryName = "NonExistingCountry"; 
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Id = 1, Name = "Bulgaria" },
        new Country { Id = 2, Name = "United Kingdom" },
        new Country { Id = 3, Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        // Act
        var exists = await service.CountryWithNameExistsAsync(nonExistingCountryName);

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task AddCountryAsync_WithValidName_AddsCountryToDatabase()
    {
        // Arrange
        var countryName = "France"; 

        // Act
        await service.AddCountryAsync(countryName);
        var countryExists = await dbContext.Countries.AnyAsync(c => c.Name == countryName);

        // Assert
        Assert.That(countryExists, Is.True);
    }

    [Test]
    public async Task EditAsync_WithValidModel_EditsCountryName()
    {
        // Arrange
        var existingCountry = new Country { Name = "Italy" }; 
        dbContext.Add(existingCountry);
        await dbContext.SaveChangesAsync();

        var updatedName = "Spain"; 

        var model = new CountryViewModel
        {
            Id = existingCountry.Id,
            Name = updatedName
        };

        // Act
        await service.EditAsync(model);
        var editedCountry = await dbContext.Countries.FindAsync(existingCountry.Id);

        // Assert
        Assert.IsNotNull(editedCountry);
        Assert.That(editedCountry.Name, Is.EqualTo(updatedName));
    }

    [Test]
    public async Task GetAllCountriesAsync_ReturnsCorrectNumberOfCountries()
    {
        // Arrange
        var expectedCount = 2; 
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Name = "Bulgaria" },
        new Country { Name = "United Kingdom" },
        new Country { Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        var currentPage = 1;
        var pageSize = 2; 

        // Act
        var countries = await service.GetAllCountriesAsync(currentPage, pageSize);

        // Assert
        Assert.That(countries.Count, Is.EqualTo(expectedCount));
    }

    [Test]
    public async Task GetAllCountriesAsync_ReturnsAllCountries()
    {
        // Arrange
        var expectedCount = 3; 
        await dbContext.AddRangeAsync(new List<Country>
    {
        new Country { Name = "Bulgaria" },
        new Country { Name = "United Kingdom" },
        new Country { Name = "Germany" }
    });
        await dbContext.SaveChangesAsync();

        // Act
        var countries = await service.GetAllCountriesAsync();

        // Assert
        Assert.That(countries.Count, Is.EqualTo(expectedCount));
    }
    [Test]
    public async Task GetByIdAsync_WithValidId_ReturnsCountryViewModel()
    {
        // Arrange
        var expectedId = 1;
        var expectedName = "Bulgaria";
        var country = new Country { Id = expectedId, Name = expectedName };
        await dbContext.AddAsync(country);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await service.GetByIdAsync(expectedId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(expectedId));
        Assert.That(result.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var result = await service.GetByIdAsync(invalidId);

        // Assert
        Assert.That(result, Is.Null);
    }

}

