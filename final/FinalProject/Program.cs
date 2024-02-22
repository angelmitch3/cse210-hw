using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// Defining different categories of shoes
public enum ShoeCategory
{
    Casual,
    Sports,
    Formal,
    Others
}

// Defining the status of a shoe (whether it's bought or sold)
public enum ShoeStatus
{
    InStock, // Modified from Bought
    Sold
}

// Abstract class representing a shoe with common properties and methods
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

// Class representing a shoe from the Jordan brand
public class JordanShoe : Shoe
{
    public override double EstimateResaleValue()
    {
        // For Jordan brand, there is 20% appreciation
        return Amount * 1.2;
    }
}

// Class representing a shoe from other brands
public class OtherBrandShoe : Shoe
{
    public override double EstimateResaleValue()
    {
        // For other brands, there is 20% depreciation
        return Amount * 0.8;
    }
}

// Class representing a collection of shoes with various operations
public class ShoeCollection
{
    private List<Shoe> shoes;

    public ShoeCollection()  // Initialize a new shoe collection
    {
        shoes = new List<Shoe>();
    }

    public void AddShoe(Shoe shoe) // Add a new shoe to the collection
    {
        shoes.Add(shoe);
    }

    public List<Shoe> GetFilteredShoes(string filterOption, string filterValue)  // Filter shoes based on a given criteria
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

    public void ListShoes(string filterOption, string filterValue) // List to store the shoes in the collection
    {
        var filteredShoes = GetFilteredShoes(filterOption, filterValue);
        Console.WriteLine("\nList of Shoes:");
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

    public double CalculateTotalSpent() // Calculate the total amount spent on all shoes in the collection
    {
        return shoes.Sum(shoe => shoe.Amount);
    }

    public double CalculateTotalResaleValue() // Calculate the total resale value of all shoes in the collection
    {
        return shoes.Sum(shoe => shoe.EstimateResaleValue());
    }

    public double CalculateValueDifference() // Calculate the difference between the total appreciated and depreciated value of all shoes
    {
        return CalculateTotalResaleValue() - CalculateTotalSpent();
    }

    // Save the shoe collection to a JSON file
    public void SaveToFile(string fileName)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(this, options); 
        File.WriteAllText(fileName, jsonString);
    }

    // Load a shoe collection from a JSON file
    public static ShoeCollection LoadFromFile(string fileName)
    {
        var jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<ShoeCollection>(jsonString);
    }

    public int ShoeCount => shoes.Count; // Total count of shoes in the collection

    public Shoe GetShoe(string model) // Get a shoe from the collection by its model
    {
        return shoes.FirstOrDefault(shoe => shoe.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
    }

    // Update a shoe's information in the collection
    public void UpdateShoe(string model, string fieldToUpdate, string newValue)
    {
        var shoeToUpdate = GetShoe(model);
        if (shoeToUpdate != null)
        {
            switch (fieldToUpdate.ToLower())
            {
                case "model":
                    shoeToUpdate.Model = newValue;
                    break;
                case "brand":
                    shoeToUpdate.Brand = newValue;
                    break;
                case "amount":
                    shoeToUpdate.Amount = Convert.ToDouble(newValue);
                    break;
                case "category":
                    shoeToUpdate.Category = (ShoeCategory)Enum.Parse(typeof(ShoeCategory), newValue, true);
                    break;
                case "description":
                    shoeToUpdate.Description = newValue;
                    break;
                case "status":
                    shoeToUpdate.Status = (ShoeStatus)Enum.Parse(typeof(ShoeStatus), newValue, true);
                    break;
                default:
                    Console.WriteLine("Invalid field to update.");
                    break;
            }
            Console.WriteLine("Shoe record updated successfully.");
        }
        else
        {
            Console.WriteLine("Shoe not found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string directoryPath = @"C:\temp\"; // Change the directory path as needed
        string filePath = Path.Combine(directoryPath, "shoes.json");

        ShoeCollection myShoes;

        if (File.Exists(filePath))
        {
            myShoes = ShoeCollection.LoadFromFile(filePath);
        }
        else
        {
            myShoes = new ShoeCollection();
        }

        while (true)
        {
             Console.WriteLine("\n\nWELCOME TO MY SHOE COLLECTION MANAGEMENT SYSTEM");
            Console.WriteLine($"\nCurrent number of shoe entries: {myShoes.ShoeCount}");
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Add new Shoes entry");
            Console.WriteLine("2. List all shoes");
            Console.WriteLine("3. Filter shoes");
            Console.WriteLine("4. Calculate total spent on shoes");
            Console.WriteLine("5. Calculate total resale value of shoes");
            Console.WriteLine("6. Calculate difference between total appreciated value and depreciated value");
            Console.WriteLine("7. Exit");

            Console.Write("Please enter option here: ");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("\nSelect brand:");
                    Console.WriteLine("1. Jordan");
                    Console.WriteLine("2. Nike");
                    Console.WriteLine("3. Adidas");
                    Console.WriteLine("4. Puma");
                    Console.WriteLine("5. Other brand");
                    Console.Write("Please enter number option here: ");
                    int brandOption = Convert.ToInt32(Console.ReadLine());
                    string brand;
                    switch (brandOption)
                    {
                        case 1:
                            brand = "Jordan";
                            break;
                        case 2:
                            brand = "Nike";
                            break;
                        case 3:
                            brand = "Adidas";
                            break;
                        case 4:
                            brand = "Puma";
                            break;
                        case 5:
                            brand = "Other brand";
                            break;
                        default:
                            Console.WriteLine("Invalid brand option.");
                            continue;
                    }

                    Shoe newShoe = null;
                    if (brand.ToLower() == "jordan")
                    {
                        newShoe = new JordanShoe();
                    }
                    else
                    {
                        newShoe = new OtherBrandShoe();
                    }
                    Console.WriteLine("\nEnter model:");
                    newShoe.Model = Console.ReadLine();
                    Console.WriteLine("Enter amount in USD:");
                    newShoe.Amount = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter category (0 for Casual, 1 for Sports, 2 for Formal, 3 for Others):");
                    newShoe.Category = (ShoeCategory)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter description:");
                    newShoe.Description = Console.ReadLine();
                    newShoe.Brand = brand;
                    newShoe.Status = ShoeStatus.InStock; // Modified from Bought
                    myShoes.AddShoe(newShoe);
                    Console.WriteLine("\nShoe record has been created.");
                    break;
                case 2:
                    myShoes.ListShoes("", "");
                    break;
                case 3:
                    if (myShoes == null)
                    {
                        Console.WriteLine("Error: Shoe collection is not initialized.");
                        break;
                    }

                    Console.WriteLine("\nSelect filter option:");
                    Console.WriteLine("1. Brand");
                    Console.WriteLine("2. Category");
                    Console.WriteLine("3. Status");
                    Console.Write("Please enter option here: ");
                    int filterOption = Convert.ToInt32(Console.ReadLine());
                    string filterValue = "";
                    switch (filterOption)
                    {
                        case 1:
                            Console.WriteLine("\nEnter brand:");
                            filterValue = Console.ReadLine();
                            myShoes.ListShoes("brand", filterValue);
                            break;
                        case 2:
                            Console.WriteLine("\nEnter category (Casual, Sports, Formal, Others):");
                            filterValue = Console.ReadLine();
                            myShoes.ListShoes("category", filterValue);
                            break;
                        case 3:
                            Console.WriteLine("\nEnter status (InStock, Sold):");
                            filterValue = Console.ReadLine();
                            myShoes.ListShoes("status", filterValue);
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
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
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
