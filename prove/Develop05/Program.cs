using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public string Description { get; set; }

    [JsonConstructor]
    protected Goal() { }

    protected Goal(string name, int points, string description)
    {
        Name = name;
        Points = points;
        Description = description;
    }

    public abstract void Record();
}

class SimpleGoal : Goal
{
    public bool IsComplete { get; private set; }

    [JsonConstructor]
    public SimpleGoal() { }

    public SimpleGoal(string name, int points, string description) : base(name, points, description) { }

    public override void Record()
    {
        IsComplete = true;
    }
}

class EternalGoal : Goal
{
    [JsonConstructor]
    public EternalGoal() { }

    public EternalGoal(string name, int points, string description) : base(name, points, description) { }

    public override void Record()
    {
        // Implementation for EternalGoal's Record method
    }
}

class ChecklistGoal : Goal
{
    public int Times { get; set; }
    public int Target { get; set; }
    public int Bonus { get; set; }

    [JsonConstructor]
    public ChecklistGoal() { }

    public ChecklistGoal(string name, int points, string description, int target, int bonus) : base(name, points, description)
    {
        Target = target;
        Bonus = bonus;
    }

    public override void Record()
    {
        Times++;
    }
}

class GoalConverter : JsonConverter<Goal>
{
    public override Goal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;
            if (root.TryGetProperty(nameof(SimpleGoal.IsComplete), out _))
                return JsonSerializer.Deserialize<SimpleGoal>(root.GetRawText(), options);
            if (root.TryGetProperty(nameof(ChecklistGoal.Times), out _))
                return JsonSerializer.Deserialize<ChecklistGoal>(root.GetRawText(), options);
            if (root.TryGetProperty(nameof(EternalGoal), out _))
                return JsonSerializer.Deserialize<EternalGoal>(root.GetRawText(), options);

            throw new JsonException("Unknown type of Goal.");
        }
    }

    public override void Write(Utf8JsonWriter writer, Goal value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Create New Goals\n2. List Goals\n3. Save Goals\n4. Load Goals\n5. Record Events\n6. Quit");
            Console.Write("Choose an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    try
                    {
                        SaveGoals();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error saving goals: {e.Message}");
                    }
                    break;
                case "4":
                    try
                    {
                        LoadGoals();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error loading goals: {e.Message}");
                    }
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Type of Goals: ");
        Console.WriteLine("1. Simple Goals");
        Console.WriteLine("2. Eternal Goals");
        Console.WriteLine("3. Checklist Goals");

        Console.Write("What type of goal would you like to create? ");
        var goalType = int.Parse(Console.ReadLine());

        Console.Write("What is the name of your goal? ");
        var name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        var description = Console.ReadLine();

        Console.Write("What is the amount of points associated with it? ");
        var points = int.Parse(Console.ReadLine());

        switch (goalType)
        {
            case 1:
                goals.Add(new SimpleGoal(name, points, description));
                break;
            case 2:
                goals.Add(new EternalGoal(name, points, description));
                break;
            case 3:
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                var target = int.Parse(Console.ReadLine());

                Console.Write("What is the bonus for accomplishing it many times? ");
                var bonus = int.Parse(Console.ReadLine());

                goals.Add(new ChecklistGoal(name, points, description, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type. Please try again.");
                break;
        }
    }

    static void ListGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine($"{goal.Name} - {goal.Description} - Points: {goal.Points}");
        }
    }

    static void SaveGoals()
    {
        Console.Write("Enter the name of the file to save: ");
        string fileName = Console.ReadLine();

        string json = JsonSerializer.Serialize(goals);
        File.WriteAllText(fileName, json);

        Console.WriteLine($"Goals saved to {fileName}.");
    }

    static void LoadGoals()
    {
        Console.Write("Enter the name of the file to load: ");
        string fileName = Console.ReadLine();

        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new GoalConverter());
            goals = JsonSerializer.Deserialize<List<Goal>>(json, options);
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    static void RecordEvent()
    {
        Console.Write("Enter goal name: ");
        var name = Console.ReadLine();

        var goal = goals.Find(g => g.Name == name);
        if (goal != null)
        {
            goal.Record();
            UpdateScore();
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }

    static void UpdateScore()
    {
        score = 0;
        foreach (var goal in goals)
        {
            score += goal.Points;
        }
    }
}
