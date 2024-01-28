using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static List<Scripture> scriptures = new List<Scripture>();

    public static void Main()
    {
        // Initialize with some scriptures
        AddScripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
        AddScripture(new Reference("Moses", 1, 39), "For behold, this is my work and my glory—to bring to pass the immortality and eternal life of man.");
        AddScripture(new Reference("Moses", 7, 18), "And the Lord called his people Zion, because they were of one heart and one mind, and dwelt in righteousness; and there was no poor among them.");
        AddScripture(new Reference("Abraham", 3, 22, 23), "And God saw these souls that they were good, and he stood in the midst of them, and he said: These I will make my rulers; for he stood among those that were spirits, and he saw that they were good; and he said unto me: Abraham, thou art one of them; thou wast chosen before thou wast born.");

        while (true)
        {
            Console.WriteLine("\nType 'add' to add a new verse, 'select' to select a verse, 'random' to pick a random verse, 'practice' to practice, or 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }
            else if (input.ToLower() == "add")
            {
                AddNewVerse();
            }
            else if (input.ToLower() == "select")
            {
                SelectVerse();
            }
            else if (input.ToLower() == "random")
            {
                RandomVerse();
            }
            else if (input.ToLower() == "practice")
            {
                PracticeVerse();
            }
        }
    }

    public static void AddScripture(Reference reference, string text)
    {
        scriptures.Add(new Scripture(reference, text));
    }

    public static void AddNewVerse()
    {
        Console.WriteLine("Enter the book name:");
        string book = Console.ReadLine();

        Console.WriteLine("Enter the chapter number:");
        int chapter = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the starting verse number:");
        int verseStart = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the ending verse number (optional):");
        string verseEndStr = Console.ReadLine();
        int? verseEnd = string.IsNullOrEmpty(verseEndStr) ? (int?)null : int.Parse(verseEndStr);

        Console.WriteLine("Enter the verse text:");
        string text = Console.ReadLine();

        AddScripture(new Reference(book, chapter, verseStart, verseEnd), text);
    }

    public static void SelectVerse()
    {
        Console.WriteLine("Here are the available verses:");
        for (int i = 0; i < scriptures.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptures[i].Reference}");
        }

        Console.WriteLine("Enter the number of the verse you want to select:");
        int index = int.Parse(Console.ReadLine()) - 1;

        PracticeVerse(scriptures[index]);
    }

    public static void RandomVerse()
    {
        Random random = new Random();
        int index = random.Next(scriptures.Count);
        PracticeVerse(scriptures[index]);
    }

    public static void PracticeVerse()
    {
        Random random = new Random();
        int index = random.Next(scriptures.Count);
        PracticeVerse(scriptures[index]);
    }

    public static void PracticeVerse(Scripture scripture)
    {
        scripture.Display();

        while (true)
        {
            Console.WriteLine("\nPress enter to hide a word or type 'back' to go back.");
            string input = Console.ReadLine();

            if (input.ToLower() == "back")
            {
                break;
            }

            scripture.HideRandomWord();
            Console.Clear();
            scripture.Display();
        }
    }
}

public class Scripture
{
    public Reference Reference { get; set; }
    public List<Word> Words { get; set; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void Display()
    {
        Console.WriteLine(Reference);
        Console.WriteLine(string.Join(" ", Words));
    }

    public void HideRandomWord()
    {
        Random random = new Random();
        int index = random.Next(Words.Count);
        Words[index].Hide();
    }
}

public class Reference
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int VerseStart { get; set; }
    public int? VerseEnd { get; set; }

    public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    public override string ToString()
    {
        return VerseEnd.HasValue ? $"{Book} {Chapter}:{VerseStart}-{VerseEnd}" : $"{Book} {Chapter}:{VerseStart}";
    }
}

public class Word
{
    private string text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        this.text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? "_____" : text;
    }
}
