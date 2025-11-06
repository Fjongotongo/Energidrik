using EnergydrinkAPI.Models;
using EnergydrinkAPI.Repositories;

namespace EnergydrinkTests;
[TestClass]
public class EnergydrinkRepositoryTests
{
    private EnergydrinkRepository _repository = new EnergydrinkRepository();

    [TestInitialize]
    public void Setup()
    {
        // Initialize repository before each test
        _repository = new EnergydrinkRepository();
    }

    [TestMethod]
    public void GetAll_ShouldReturnInitialData()
    {
        // Act
        var allDrinks = _repository.GetAll().ToList();

        // Assert
        Assert.AreEqual(10, allDrinks.Count, "Initial data should contain 10 Energydrinks.");
    }

    [TestMethod]
    public void GetById_ValidId_ShouldReturnCorrectDrink()
    {
        // Act
        var drink = _repository.GetById(1);

        // Assert
        Assert.IsNotNull(drink);
        Assert.AreEqual(1, drink?.Id);
        Assert.AreEqual("PowerBlast", drink?.Name);
    }

    [TestMethod]
    public void GetById_InvalidId_ShouldReturnNull()
    {
        // Act
        var drink = _repository.GetById(999);

        // Assert
        Assert.IsNull(drink, "Should return null if the ID does not exist.");
    }

    [TestMethod]
    public void Add_NewDrink_ShouldAssignIdAndIncreaseCount()
    {
        // Arrange
        var newDrink = new Energydrink { Name = "EnergyMax", Type = EnergyType.SugarFree, Price = 3.50 };

        // Act
        _repository.Add(newDrink);
        var allDrinks = _repository.GetAll().ToList();

        // Assert
        Assert.AreEqual(11, allDrinks.Count, "Count should be 11 after adding a new drink.");
        Assert.IsTrue(newDrink.Id > 0, "New drink should have a positive Id assigned.");
        Assert.IsTrue(allDrinks.Any(d => d.Name == "EnergyMax" && d.Id == newDrink.Id));
    }

    [TestMethod]
    public void Add_DuplicateId_ShouldUpdateExistingDrink()
    {
        // Arrange
        var duplicateDrink = new Energydrink { Id = 1, Name = "UpdatedDrink", Type = EnergyType.Caffeinated, Price = 5.00 };

        // Act
        _repository.Add(duplicateDrink);
        var drink = _repository.GetById(1);

        // Assert
        Assert.IsNotNull(drink);
        Assert.AreEqual("UpdatedDrink", drink?.Name);
        Assert.AreEqual(EnergyType.Caffeinated, drink?.Type);
        Assert.AreEqual(5.00, drink?.Price);
    }

    [TestMethod]
    public void Update_ValidId_ShouldUpdateDrink()
    {
        // Arrange
        var updatedDrink = new Energydrink { Id = 1, Name = "UpdatedDrink", Type = EnergyType.Regular, Price = 4.00 };

        // Act
        var result = _repository.Update(updatedDrink);
        var drink = _repository.GetById(1);

        // Assert
        Assert.IsTrue(result, "Update should return true for a valid Id.");
        Assert.AreEqual("UpdatedDrink", drink?.Name);
        Assert.AreEqual(EnergyType.Regular, drink?.Type);
        Assert.AreEqual(4.00, drink?.Price);
    }

    [TestMethod]
    public void Update_InvalidId_ShouldReturnFalse()
    {
        // Arrange
        var updatedDrink = new Energydrink { Id = 999, Name = "NonExistentDrink", Type = EnergyType.SugarFree, Price = 3.99 };

        // Act
        var result = _repository.Update(updatedDrink);

        // Assert
        Assert.IsFalse(result, "Update should return false for an invalid Id.");
    }

    [TestMethod]
    public void Delete_ValidId_ShouldDecreaseCount()
    {
        // Act
        var result = _repository.Delete(1);
        var allDrinks = _repository.GetAll().ToList();

        // Assert
        Assert.IsTrue(result, "Delete should return true for a valid Id.");
        Assert.AreEqual(9, allDrinks.Count, "Count should be 9 after deleting one drink.");
        Assert.IsNull(_repository.GetById(1), "Deleted drink should no longer exist in the repository.");
    }

    [TestMethod]
    public void Delete_InvalidId_ShouldReturnFalse()
    {
        // Act
        var result = _repository.Delete(999);
        var allDrinks = _repository.GetAll().ToList();

        // Assert
        Assert.IsFalse(result, "Delete should return false for an invalid Id.");
        Assert.AreEqual(10, allDrinks.Count, "Count should remain 10 if deleting a non-existent Id.");
    }
}