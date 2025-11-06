using EnergydrinkAPI.Models;

namespace EnergydrinkAPI.Repositories;

public class EnergydrinkRepository
{
    private readonly List<Energydrink> _energyDrinks;

    public EnergydrinkRepository()
    {
        // Initialize with sample data
        _energyDrinks = new List<Energydrink>
        {
            new Energydrink { Id = 1, Name = "PowerBlast", Type = EnergyType.Caffeinated, Price = 10.95 },
            new Energydrink { Id = 2, Name = "MaxEnergy", Type = EnergyType.Regular, Price = 11.95 },
            new Energydrink { Id = 3, Name = "TurboCharge", Type = EnergyType.SugarFree, Price = 12.95 },
            new Energydrink { Id = 4, Name = "Revitalize", Type = EnergyType.Decaffeinated, Price = 13.50 },
            new Energydrink { Id = 5, Name = "UltraFuel", Type = EnergyType.Caffeinated, Price = 13.95 },
            new Energydrink { Id = 6, Name = "EnergyRush", Type = EnergyType.Regular, Price = 14.95 },
            new Energydrink { Id = 7, Name = "NightBoost", Type = EnergyType.SugarFree, Price = 14.95 },
            new Energydrink { Id = 8, Name = "MorningKick", Type = EnergyType.Caffeinated, Price = 15.95 },
            new Energydrink { Id = 9, Name = "EndurancePro", Type = EnergyType.Regular, Price = 16.95 },
            new Energydrink { Id = 10, Name = "PowerLite", Type = EnergyType.Decaffeinated, Price = 17.95 }
        };
    }

    // Get all Energydrinks
    public IEnumerable<Energydrink> GetAll()
    {
        return _energyDrinks;
    }

    // Get a single Energydrink by Id
    public Energydrink? GetById(int id)
    {
        return _energyDrinks.FirstOrDefault(e => e.Id == id);
    }

    // Add a new Energydrink
    public void Add(Energydrink newDrink)
    {
        if (GetById(newDrink.Id) == null)
        {
            newDrink.Id = GetNextId();
            _energyDrinks.Add(newDrink);
        }
        else
        {
            Update(newDrink);
        }
    }

    // Update an existing Energydrink
    public bool Update(Energydrink updatedDrink)
    {
        var existingDrink = GetById(updatedDrink.Id);
        if (existingDrink == null)
            return false;

        existingDrink.Name = updatedDrink.Name;
        existingDrink.Type = updatedDrink.Type;
        existingDrink.Price = updatedDrink.Price;
        return true;
    }

    // Remove an Energydrink by Id
    public bool Delete(int id)
    {
        var drink = GetById(id);
        if (drink == null)
            return false;

        _energyDrinks.Remove(drink);
        return true;
    }

    private int GetNextId()
    {
        return _energyDrinks.Max(e => e.Id) + 1;
    }
}
