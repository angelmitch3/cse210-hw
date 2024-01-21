using System;
using System.Text.Json;
using System.IO;

public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public JournalEntry() { }

    public JournalEntry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

public class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void AddEntry(JournalEntry entry)
    {
        entries.Add(entry);
    }

    public void AddEntry()
    {
        var random = new Random();
        int index = random.Next(prompts.Count);
        Console.WriteLine(prompts[index]);
        string response = Console.ReadLine();
        AddEntry(new JournalEntry(prompts[index], response));
    }

    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public string SaveJournal(string filename)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    var json = JsonSerializer.Serialize(entry);
                    sw.WriteLine(json);
                }
            }
            return "Journal saved successfully.\n";
        }
        catch (Exception ex)
        {
            return $"Failed to write to file: {filename}. Error: {ex.Message}";
        }
    }

    public string LoadJournal(string filename)
    {
        if (!File.Exists(filename))
        {
            return $"File not found: {filename}";
        }

        entries.Clear();
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        var entry = JsonSerializer.Deserialize<JournalEntry>(line);
                        entries.Add(entry);
                    }
                    catch (JsonException ex)
                    {
                        return $"Failed to deserialize line: {line}. Error: {ex.Message}";
                    }
                }
            }
            return "Journal loaded successfully. Please select Option 2 to display the loaded journal entry.\n";
        }
        catch (Exception ex)
        {
            return $"Failed to read from file: {filename}. Error: {ex.Message}";
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nPlease select one of the following choices:\n"); 
            Console.WriteLine("1. Write a new entry\n2. Display the journal\n3. Save the journal to a file\n4. Load the journal from a file\n5. Exit");
            Console.Write("What would you like to do? ");
            Console.WriteLine("");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.WriteLine("Enter a filename:");
                    string saveFilename = Console.ReadLine();
                    string saveResult = journal.SaveJournal(saveFilename);
                    Console.WriteLine(saveResult);
                    break;
                case "4":
                    Console.WriteLine("Enter a filename:");
                    string loadFilename = Console.ReadLine();
                    string loadResult = journal.LoadJournal(loadFilename);
                    Console.WriteLine(loadResult);
                    break;
                case "5":
                    running = false;
                    break;
            }
        }
    }
}
