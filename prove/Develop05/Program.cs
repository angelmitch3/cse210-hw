using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public DateTime LastUpdated { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        LastUpdated = DateTime.Now;
    }

    public abstract void Record();

    public bool IsAging()
    {
        return (DateTime.Now - LastUpdated).TotalDays > 7;
    }
}

class SimpleGoal : Goal
{
    public bool IsComplete { get; private set; }

    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void Record()
    {
        IsComplete = true;
        LastUpdated = DateTime.Now;
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void Record()
    {
        LastUpdated = DateTime.Now;
    }
}

class ChecklistGoal : Goal
{
    public int Times { get; set; }
    public int Target { get; set; }
    public int Bonus { get; set; }

    public ChecklistGoal(string name, int points, int target, int bonus) : base(name, points)
    {
        Target = target;
        Bonus = bonus;
    }

    public override void Record()
    {
        Times++;
        if (Times == Target)
        {
            Points += Bonus;
        }
        LastUpdated = DateTime.Now;
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
        Console.Write("Enter goal name: ");
        var name = Console.ReadLine();

        Console.Write("Enter goal points: ");
        var points = int.Parse(Console.ReadLine());

        Console.WriteLine("Choose goal type:\n1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal");
        Console.Write("Choose an option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                goals.Add(new SimpleGoal(name, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, points));
                break;
            case "3":
                Console.Write("Enter target times: ");
                var target = int.Parse(Console.ReadLine());

                Console.Write("Enter bonus points: ");
                var bonus = int.Parse(Console.ReadLine());

                goals.Add(new ChecklistGoal(name, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    static void ListGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine($"{goal.Name} - {(goal is SimpleGoal ? ((SimpleGoal)goal).IsComplete ? "[X]" : "[ ]" : "")} {(goal is ChecklistGoal ? $"Completed {((ChecklistGoal)goal).Times}/{((ChecklistGoal)goal).Target} times" : "")}");
        }
    }

    static void SaveGoals()
    {
        using (Stream stream = File.Open("goals.dat", FileMode.Create))
        {
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream, goals);
        }
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.dat"))
        {
            using (Stream stream = File.Open("goals.dat", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                goals = (List<Goal>)bin.Deserialize(stream);
            }
        }
        else
        {
            Console.WriteLine("No saved goals found.");
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
            if (goal is SimpleGoal && ((SimpleGoal)goal).IsComplete || goal is ChecklistGoal && ((ChecklistGoal)goal).Times > 0 || goal is EternalGoal)
            {
                score += goal.Points;
            }
        }
    }
}
