using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public enum ShoeCategory
{
    Casual,
    Sports,
    Formal,
    Others
}

public enum ShoeStatus
{
    Bought,
    Sold
}

public abstract class Shoe
{
    public string Model { get; set; }
    public string Brand { get; set; }
    public double Amount { get; set; }
    public ShoeCategory Category { get; set; }
    public string Description { get; set; }
    public ShoeStatus Status { get; set; }

    public abstract double EstimateResaleValue();
}

public class JordanShoe : Shoe
{
    public override double EstimateResaleValue()
    {
        // For Jordan brand, there is 20% appreciation
        return Amount * 1.2;
    }
}

public class OtherBrandShoe : Shoe
{
    public override double EstimateResaleValue()
    {
        // For other brands, there is 20% depreciation
        return Amount * 0.8;
    }
}

public class ShoeCollection
{
    private List<Shoe> shoes;

    public ShoeCollection()
    {
        shoes = new List<Shoe>();
    }

    public void AddShoe(Shoe shoe)
    {
        shoes.Add(shoe);
    }

    public List<Shoe> GetFilteredShoes(string filterOption, string filterValue)
    {
        switch (filterOption.ToLower())
        {
            case "brand":
                return shoes.Where(shoe => shoe.Brand.Equals(filterValue, StringComparison.OrdinalIgnoreCase)).ToList();
            case "category":
                return shoes.Where(shoe => shoe.Category.ToString().Equals(filterValue, StringComparison.OrdinalIgnoreCase)).ToList();
            case "status":
                return shoes.Where(shoe => shoe.Status.ToString().Equals(filterValue, StringComparison.OrdinalIgnoreCase)).ToList();
            default:
                return shoes;
        }
    }

    public void ListShoes(string filterOption, string filterValue)
    {
        var filteredShoes = GetFilteredShoes(filterOption, filterValue);
        if (filteredShoes.Any())
        {
            foreach (var shoe in filteredShoes)
            {
                Console.WriteLine($"Model: {shoe.Model}, Brand: {shoe.Brand}, Amount: ${shoe.Amount}, Category: {shoe.Category}, Description: {shoe.Description}, Status: {shoe.Status}");
            }
        }
        else
        {
            Console.WriteLine("No shoes found matching the criteria.");
        }
    }

    public double CalculateTotalSpent()
    {
        return shoes.Sum(shoe => shoe.Amount);
    }

    public double CalculateTotalResaleValue()
    {
        return shoes.Sum(shoe => shoe.EstimateResaleValue());
    }

    public double CalculateValueDifference()
    {
        return CalculateTotalResaleValue() - CalculateTotalSpent();
    }

    public void SaveToFile(string fileName)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine("\nShoe collection saved to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError saving shoe collection: {ex.Message}");
        }
    }

    public static ShoeCollection LoadFromFile(string fileName)
    {
        try
        {
            var jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<ShoeCollection>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError loading shoe collection from file: {ex.Message}");
            return new ShoeCollection();
        }
    }

    public int ShoeCount => shoes.Count;
}

class Program
{
    static void Main(string[] args)
    {
        ShoeCollection myShoes;

        if (File.Exists("shoes.json"))
        {
            myShoes = ShoeCollection.LoadFromFile("shoes.json");
        }
        else
        {
            myShoes = new ShoeCollection();
        }

        while (true)
        {
            Console.WriteLine($"\n\nCurrent number of shoe entries: {myShoes.ShoeCount}");
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Add new Shoes entry");
            Console.WriteLine("2. List all shoes");
            Console.WriteLine("3. Filter shoes");
            Console.WriteLine("4. Calculate total spent on shoes");
            Console.WriteLine("5. Calculate total resale value of shoes");
            Console.WriteLine("6. Calculate difference between total appreciated value and depreciated value");
            Console.WriteLine("7. Save shoe collection to file");
            Console.WriteLine("8. Open a shoe collection file");
            Console.WriteLine("9. Exit");
            
            Console.Write("Please enter option here: ");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    // Add new shoe entry
                    // Code for adding a new shoe entry
                    break;
                case 2:
                    myShoes.ListShoes("", "");
                    break;
                case 3:
                    // Filter shoes
                    // Code for filtering shoes
                    break;
                case 4:
                    Console.WriteLine("\nTotal spent on shoes: $" + myShoes.CalculateTotalSpent());
                    break;
                case 5:
                    Console.WriteLine("\nTotal resale value of shoes: $" + myShoes.CalculateTotalResaleValue());
                    break;
                case 6:
                    Console.WriteLine("\nDifference between total appreciated value and depreciated value: $" + myShoes.CalculateValueDifference());
                    break;
                case 7:
                    Console.WriteLine("\nEnter file name to save:");
                    string saveFileName = Console.ReadLine();
                    myShoes.SaveToFile(saveFileName);
                    break;
                case 8:
                    Console.WriteLine("\nEnter file name to open:");
                    string openFileName = Console.ReadLine();
                    myShoes = ShoeCollection.LoadFromFile(openFileName);
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
