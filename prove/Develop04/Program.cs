using System;
using System.Threading;
using System.Collections.Generic;

abstract class Activity
{
    protected int duration;

    public Activity(int duration)
    {
        this.duration = duration;
    }

    public abstract void Start();

    protected void End(string name)
    {
        Console.WriteLine($"Good job! You have completed the {name}. It lasted for {duration} seconds.");
        Thread.Sleep(1000); // Pause for 5 seconds
        LoadingAnimation(5); // Loading animation
    }

    protected void Countdown(int countdownNumber)
    {
        for (int i = countdownNumber; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000); // Pause for 1 second
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }

    protected void LoadingAnimation(int seconds)
    {
        List<string> animationStrings = new List<string>() { "|", "/", "-", "\\", "|", "/", "-", "\\", "|","/", "-", "\\", "|"};
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(animationStrings[i % animationStrings.Count]);
            Thread.Sleep(500); // Pause for 1 second
            Console.Write("\b \b");
        }
    }
}

class BreathingActivity : Activity
{
    private int breathInCountdown;
    private int breathOutCountdown;

    public BreathingActivity(int duration, int breathInCountdown, int breathOutCountdown) : base(duration)
    {
        this.breathInCountdown = breathInCountdown;
        this.breathOutCountdown = breathOutCountdown;
    }

    public override void Start()
    {
        Console.Clear();
        Console.WriteLine("\nWelcome to the Breathing Activity.\n\nThis activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.\n");
        Console.WriteLine($"The activity will last for {duration} seconds.");
        Console.WriteLine($"Get Ready...");
        Thread.Sleep(1000); // Pause for 5 seconds
        LoadingAnimation(6); // Loading animation

        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Breathe in...");
            Countdown(breathInCountdown);
            Console.WriteLine("Breathe out...");
            Countdown(breathOutCountdown);
        }
        End("Breathing Activity");
    }
}
class ReflectionActivity : Activity
{
    public ReflectionActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        Console.Clear();
        Console.WriteLine("\nWelcome to the Reflection Activity.\n\nThis activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.\n");
        Console.WriteLine($"The activity will last for {duration} seconds.\n");
        Console.WriteLine($"Get Ready...");
        Thread.Sleep(1000); // Pause for 5 seconds
        LoadingAnimation(6); // Loading animation

        string[] prompts = new string[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        string[] questions = new string[]
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            string question = questions[random.Next(questions.Length)];
            Console.WriteLine(question);
            Console.ReadLine();
            Console.WriteLine("Please wait for next question...");
            //Console.ReadLine();
            //Thread.Sleep(5000); // Pause for 5 seconds
            LoadingAnimation(6); // Loading animation
        }
        End("Reflection Activity");
    }
}

class ListingActivity : Activity
{
    public ListingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        Console.Clear();
        Console.WriteLine("\nWelcome to the Listing Activity.\n\nThis activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.\n");
        Console.WriteLine($"The activity will last for {duration} seconds.");
        Console.WriteLine($"Get Ready...");
        Thread.Sleep(1000); // Pause for 5 seconds
        LoadingAnimation(6); // Loading animation

        string[] prompts = new string[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        List<string> items = new List<string>();
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Enter an item:");
            string item = Console.ReadLine();
            items.Add(item);
            Console.ReadLine();
            Thread.Sleep(1000); // Pause for 1 seconds
            LoadingAnimation(5); // Loading animation3
            Console.WriteLine("Please keep writing more items...");
            
        }

        Console.WriteLine($"You have listed {items.Count} items.");
        End("Listing Activity");
    }
}

class GratitudeActivity : Activity
{
    public GratitudeActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {   
        Console.Clear();
        Console.WriteLine("\nWelcome to the Gratitude Activity.\n\nThis activity will help you reflect on the things you are grateful for in your life. Try to list as many things as you can.\n");
        Console.WriteLine($"The activity will last for {duration} seconds.\n");
        Console.WriteLine($"Get Ready...\n");
        Thread.Sleep(1000); // Pause for 5 seconds
        LoadingAnimation(6); // Loading animation

        List<string> items = new List<string>();
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Enter an item:");
            string item = Console.ReadLine();
            items.Add(item);
            Console.ReadLine();
            Console.WriteLine("Please wait for next question...");
            Thread.Sleep(1000); // Pause for 5 seconds
            LoadingAnimation(6); // Loading animation
        }

        Console.WriteLine($"You have listed {items.Count} items.");
        End("Gratitude Activity");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nChoose an activity:\n1. Breathing Activity\n2. Reflection Activity\n3. Listing Activity\n4. Gratitude Activity\n5. Exit\nPlease enter your choice below:");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 5)
                break;

            Console.WriteLine("Enter the duration of the activity in seconds:");
            int duration = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the countdown for breath in:");
                    int breathInCountdown = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the countdown for breath out:");
                    int breathOutCountdown = Convert.ToInt32(Console.ReadLine());
                    Activity activity = new BreathingActivity(duration, breathInCountdown, breathOutCountdown);
                    activity.Start();
                    break;
                case 2:
                    activity = new ReflectionActivity(duration);
                    activity.Start();
                    break;
                case 3:
                    activity = new ListingActivity(duration);
                    activity.Start();
                    break;
                case 4:
                    activity = new GratitudeActivity(duration);
                    activity.Start();
                    break;
            }
        }
    }
}
