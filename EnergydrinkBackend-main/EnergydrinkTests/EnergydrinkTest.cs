using EnergydrinkAPI.Models;

namespace EnergydrinkTests;

[TestClass]
public class EnergydrinkTests
{
    [TestMethod]
    [DataRow(1, "PowerBlast",  EnergyType.Caffeinated,  12.95)]
    [DataRow(2, "MaxEnergy",  EnergyType.Regular,  13.95)]
    public void Validate_ValidProperties_ShouldPass(int id, string name,  EnergyType type, double price)
    {
        // Arrange
        var drink = new Energydrink
        {
            Id = id,
            Name = name,
            Type = type,
            Price = price
        };

        // Act & Assert
        Assert.IsTrue(drink.Validate());
    }

    [TestMethod]
    [DataRow(0, "ValidName",  EnergyType.Caffeinated,  12.95)]
    [DataRow(-1, "ValidName",  EnergyType.Regular,  13.95)]
    public void Validate_InvalidId_ShouldThrowArgumentException(int id, string name,  EnergyType type, double price)
    {
        // Arrange
        var drink = new Energydrink
        {
            Id = id,
            Name = name,
            Type = type,
            Price = price
        };

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => drink.Validate(), "Id must be greater than 0.");
    }

    [TestMethod]
    [DataRow(1, "",  EnergyType.Caffeinated,  12.95)]
    [DataRow(2, null,  EnergyType.Regular,  13.95)]
    public void Validate_InvalidName_ShouldThrowArgumentException(int id, string name,  EnergyType type, double price)
    {
        // Arrange
        var drink = new Energydrink
        {
            Id = id,
            Name = name,
            Type = type,
            Price = price
        };

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => drink.Validate(), "Name is required.");
    }

    [TestMethod]
    [DataRow(1, "ValidName",  EnergyType.Caffeinated, -1.0)]
    public void Validate_InvalidPrice_ShouldThrowArgumentException(int id, string name,  EnergyType type, double price)
    {
        // Arrange
        var drink = new Energydrink
        {
            Id = id,
            Name = name,
            Type = type,
            Price = price
        };

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => drink.Validate(), "Price cannot be negative.");
    }

    [TestMethod]
    [DataRow(1, "ValidName",  EnergyType.Caffeinated,  12.95)]
    public void ToString_ShouldReturnCorrectFormat(int id, string name,  EnergyType type, double price)
    {
        // Arrange
        var drink = new Energydrink
        {
            Id = id,
            Name = name,
            Type = type,
            Price = price
        };
        var expected = $"Energydrink: {id}, {name}, {type}, ${price}";

        // Act
        var result = drink.ToString();

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Equals_SameProperties_ShouldReturnTrue()
    {
        // Arrange
        var drink1 = new Energydrink { Id = 1, Name = "PowerBlast", Type =  EnergyType.Caffeinated, Price =  12.95 };
        var drink2 = new Energydrink { Id = 1, Name = "PowerBlast", Type =  EnergyType.Caffeinated, Price =  12.95 };

        // Act & Assert
        Assert.IsTrue(drink1.Equals(drink2));
    }

    [TestMethod]
    public void Equals_DifferentProperties_ShouldReturnFalse()
    {
        // Arrange
        var drink1 = new Energydrink { Id = 1, Name = "PowerBlast", Type =  EnergyType.Caffeinated, Price =  12.95 };
        var drink2 = new Energydrink { Id = 2, Name = "MaxEnergy", Type =  EnergyType.Regular, Price =  13.95 };

        // Act & Assert
        Assert.IsFalse(drink1.Equals(drink2));
    }

    [TestMethod]
    public void GetHashCode_SameProperties_ShouldReturnSameHash()
    {
        // Arrange
        var drink1 = new Energydrink { Id = 1, Name = "PowerBlast", Type =  EnergyType.Caffeinated, Price =  12.95 };
        var drink2 = new Energydrink { Id = 1, Name = "PowerBlast", Type =  EnergyType.Caffeinated, Price =  12.95 };

        // Act
        var hash1 = drink1.GetHashCode();
        var hash2 = drink2.GetHashCode();

        // Assert
        Assert.AreEqual(hash1, hash2);
    }

    [TestMethod]
    public void GetHashCode_DifferentProperties_ShouldReturnDifferentHash()
    {
        // Arrange
        var drink1 = new Energydrink { Id = 1, Name = "PowerBlast", Type =  EnergyType.Caffeinated, Price =  12.95 };
        var drink2 = new Energydrink { Id = 2, Name = "MaxEnergy", Type =  EnergyType.Regular, Price =  13.95 };

        // Act
        var hash1 = drink1.GetHashCode();
        var hash2 = drink2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hash1, hash2);
    }
}