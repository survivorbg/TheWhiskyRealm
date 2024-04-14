using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.City;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class CityServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IEnumerable<City> cities;
    private IRepository repository;
    private ICityService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);


        repository = new Repository(dbContext);
        service = new CityService(repository);
        var country = new Country { Id = 1, Name = "Bulgaria" };
        await dbContext.AddAsync(country);

        var city = new City { Id = 1, Name = "Test City", CountryId = 1, ZipCode = "1000" };
        await dbContext.AddAsync(city);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddCityAsync_WithValidData_ShouldAddCityAndReturnId()
    {
        // Arrange
        var cityName = "Sofia";
        var countryId = 1;
        var zipCode = "1000";

        // Act
        var addedCityId = await service.AddCityAsync(cityName, countryId, zipCode);

        // Assert
        Assert.That(addedCityId, Is.GreaterThan(1));
        Assert.IsTrue(await repository.AllReadOnly<City>().AnyAsync(c => c.Id == addedCityId));
    }
    [Test]
    public async Task CityExistAsync_WithExistingCityId_ShouldReturnTrue()
    {
        // Arrange
        var existingCityId = 1;

        // Act
        var cityExists = await service.CityExistAsync(existingCityId);

        // Assert
        Assert.IsTrue(cityExists);
    }


    [Test]
    public async Task CityExistAsync_WithNonExistingCityId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingCityId = 999; 

        // Act
        var cityExists = await service.CityExistAsync(nonExistingCityId);

        // Assert
        Assert.IsFalse(cityExists);
    }

    [Test]
    public async Task CityWithThisNameAndCountryExistsAsync_WithExistingCityNameAndCountryId_ShouldReturnTrue()
    {
        // Arrange
        var existingCityName = "Test City";
        var existingCountryId = 1;

        // Act
        var cityExists = await service.CityWithThisNameAndCountryExistsAsync(existingCityName, existingCountryId);

        // Assert
        Assert.IsTrue(cityExists);
    }

    [Test]
    public async Task CityWithThisNameAndCountryExistsAsync_WithNonExistingCityNameAndCountryId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingCityName = "NonExistingCity";
        var nonExistingCountryId = 999; 

        // Act
        var cityExists = await service.CityWithThisNameAndCountryExistsAsync(nonExistingCityName, nonExistingCountryId);

        // Assert
        Assert.IsFalse(cityExists);
    }

    [Test]
    public async Task CityWithThisNameAndCountryExistsAsync_WithExistingCityNameAndCountryId_ShouldReturnFalse()
    {
        // Arrange
        var existingCityName = "Test City";
        var existingCountryId = 1;
        var existingCityId = 1;

        // Act
        var cityExists = await service.CityWithThisNameAndCountryExistsAsync(existingCityName, existingCountryId, existingCityId);

        // Assert
        Assert.IsFalse(cityExists);
    }

    [Test]
    public async Task CityWithThisNameAndCountryExistsAsync_WithExistingCityNameAndCountryIdAndDifferentCityId_ShouldReturnTrue()
    {
        // Arrange
        var existingCityName = "Test City";
        var existingCountryId = 1;
        var existingCityId = 2;

        // Act
        var cityExists = await service.CityWithThisNameAndCountryExistsAsync(existingCityName, existingCountryId, existingCityId);

        // Assert
        Assert.IsTrue(cityExists);
    }

    [Test]
    public async Task EditCityAsync_WithValidData_ShouldEditCity()
    {
        // Arrange
        var cityId = 1;
        var model = new CityFormViewModel
        {
            Id = cityId,
            Name = "New City Name",
            CountryId = 1, 
            Zip = "New Zip Code"
        };

        // Act
        await service.EditCityAsync(model);

        // Assert
        var editedCity = await repository.GetByIdAsync<City>(cityId);
        Assert.IsNotNull(editedCity);
        Assert.AreEqual(model.Name, editedCity.Name);
        Assert.AreEqual(model.CountryId, editedCity.CountryId);
        Assert.AreEqual(model.Zip, editedCity.ZipCode);
    }

    [Test]
    public async Task EditCityAsync_WithInvalidId_ShouldNotEditCity()
    {
        // Arrange
        var invalidCityId = 999; 
        var model = new CityFormViewModel
        {
            Id = invalidCityId,
            Name = "New City Name",
            CountryId = 1, 
            Zip = "New Zip Code"
        };

        // Act
        await service.EditCityAsync(model);

        // Assert
        var editedCity = await repository.GetByIdAsync<City>(invalidCityId);
        Assert.IsNull(editedCity);
    }

    [Test]
    public async Task EditCityAsync_WithNonExistingCity_ShouldNotEditCity()
    {
        // Arrange
        var nonExistingCityId = 999; 
        var model = new CityFormViewModel
        {
            Id = nonExistingCityId,
            Name = "New City Name",
            CountryId = 1, 
            Zip = "New Zip Code"
        };

        // Act
        await service.EditCityAsync(model);

        // Assert
        var editedCity = await repository.GetByIdAsync<City>(nonExistingCityId);
        Assert.IsNull(editedCity);
    }

    [Test]
    public async Task GetAllCitiesAsync_ShouldReturnOneCity()
    {
        // Arrange
        var pageSize = 5;
        var currentPage = 1;

        // Act
        var cities = await service.GetAllCitiesAsync(currentPage, pageSize);

        // Assert
        Assert.AreEqual(1, cities.Count());
    }

    [Test]
    public async Task GetAllCitiesAsync_ShouldReturnCorrectCityData()
    {
        // Arrange
        var pageSize = 5;
        var currentPage = 1;
        var expectedCityData = new List<CityViewModel>
    {
        new CityViewModel { Id = 1, Name = "Test City", Country = "Bulgaria", Zip = "1000" }
    }; 

        // Act
        var cities = await service.GetAllCitiesAsync(currentPage, pageSize);

        // Assert
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Id), cities.Select(c => c.Id));
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Name), cities.Select(c => c.Name));
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Country), cities.Select(c => c.Country));
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Zip), cities.Select(c => c.Zip));
    }
    [Test]
    public async Task GetAllCitiesAsync_NoParameters_ShouldReturnOneCity()
    {
        // Act
        var cities = await service.GetAllCitiesAsync();

        // Assert
        Assert.AreEqual(1, cities.Count());
    }

    [Test]
    public async Task GetAllCitiesAsync_NoParameters_ShouldReturnCorrectCityData()
    {
        // Arrange
        var expectedCityData = new List<CityViewModel>
    {
        new CityViewModel { Id = 1, Name = "Test City", Country = "Bulgaria", Zip = "1000" }
    }; 

        // Act
        var cities = await service.GetAllCitiesAsync();

        // Assert
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Id), cities.Select(c => c.Id));
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Name), cities.Select(c => c.Name));
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Country), cities.Select(c => c.Country));
        CollectionAssert.AreEqual(expectedCityData.Select(c => c.Zip), cities.Select(c => c.Zip));
    }
    [Test]
    public async Task GetCityByIdAsync_WithExistingId_ShouldReturnCorrectCityData()
    {
        // Arrange
        var existingCityId = 1; 
        var expectedCityData = new CityFormViewModel { Id = 1, Name = "Test City", CountryId = 1, Zip = "1000" }; 

        // Act
        var city = await service.GetCityByIdAsync(existingCityId);

        // Assert
        Assert.IsNotNull(city);
        Assert.AreEqual(expectedCityData.Id, city.Id);
        Assert.AreEqual(expectedCityData.Name, city.Name);
        Assert.AreEqual(expectedCityData.CountryId, city.CountryId);
        Assert.AreEqual(expectedCityData.Zip, city.Zip);
    }

    [Test]
    public async Task GetCityByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingCityId = 999;

        // Act
        var city = await service.GetCityByIdAsync(nonExistingCityId);

        // Assert
        Assert.IsNull(city);
    }
    [Test]
    public async Task GetTotalCitiesAsync_ShouldReturnCorrectCount()
    {
        // Arrange
        var expectedCount = 1; 

        // Act
        var totalCities = await service.GetTotalCitiesAsync();

        // Assert
        Assert.AreEqual(expectedCount, totalCities);
    }

}
