namespace EnergydrinkAPI.Models;

public class Energydrink
{
    // Properties
    public int Id { get; set; }
    public string? Name { get; set; }
    public EnergyType Type { get; set; } = EnergyType.Undefined;
    public double Price { get; set; }

    // Validation method
    public bool Validate()
    {
        if (Id <= 0)
            throw new ArgumentException("Id must be greater than 0.");
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Name is required.");
        if (Price < 0)
            throw new ArgumentException("Price cannot be negative.");
        return true;
    }

    // Override ToString method
    public override string ToString()
    {
        return $"Energydrink: {Id}, {Name}, {Type}, ${Price}";
    }

    // Override Equals method
    public override bool Equals(object? obj)
    {
        if (obj is Energydrink other)
        {
            return Id == other.Id &&
                   Name == other.Name &&
                   Type == other.Type &&
                   Price == other.Price;
        }
        return false;
    }

    // Override GetHashCode method
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Type, Price);
    }
}
