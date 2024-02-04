using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:\n1. Breathing Activity\n2. Reflection Activity\n3. Listing Activity\n4. Gratitude Activity\n5. Exit");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 5)
                break;

            Console.WriteLine("Enter the duration of the activity in seconds:");
            int duration = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the countdown number for each breath:");
                    int countdownNumber = Convert.ToInt32(Console.ReadLine());
                    StartActivity("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.", duration);
                    BreathingActivity(duration, countdownNumber);
                    break;
                case 2:
                    StartActivity("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.", duration);
                    ReflectionActivity(duration);
                    break;
                case 3:
                    StartActivity("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", duration);
                    ListingActivity(duration);
                    break;
                case 4:
                    StartActivity("Gratitude Activity", "This activity will help you reflect on the things you are grateful for in your life. Try to list as many things as you can.", duration);
                    GratitudeActivity(duration);
                    break;
            }
        }
    }

    static void StartActivity(string name, string description, int duration)
    {
        Console.WriteLine($"Starting the {name}. {description}");
        Console.WriteLine($"The activity will last for {duration} seconds. Prepare to begin...");
        Thread.Sleep(5000); // Pause for 5 seconds
        LoadingAnimation(5); // Loading animation
    }

    static void EndActivity(string name, int duration)
    {
        Console.WriteLine("Good job! You have completed the activity.");
        Thread.Sleep(5000); // Pause for 5 seconds
        LoadingAnimation(5); // Loading animation
        Console.WriteLine($"You have completed the {name}. It lasted for {duration} seconds.");
        Thread.Sleep(5000); // Pause for 5 seconds
        LoadingAnimation(5); // Loading animation
    }

    static void BreathingActivity(int duration, int countdownNumber)
    {
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Breathe in...");
            Countdown(countdownNumber);
            Console.WriteLine("Breathe out...");
            Countdown(countdownNumber);
        }
        EndActivity("Breathing Activity", duration);
    }

    static void ReflectionActivity(int duration)
    {
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
            Thread.Sleep(5000); // Pause for 5 seconds
            LoadingAnimation(5); // Loading animation
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        EndActivity("Reflection Activity", duration);
    }

    static void ListingActivity(int duration)
    {
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
            Thread.Sleep(5000); // Pause for 5 seconds
            LoadingAnimation(5); // Loading animation
        }

        Console.WriteLine($"You have listed {items.Count} items.");
        EndActivity("Listing Activity", duration);
    }

    static void GratitudeActivity(int duration)
    {
        Console.WriteLine("List the things you are grateful for:");

        List<string> items = new List<string>();
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Enter an item:");
            string item = Console.ReadLine();
            items.Add(item);
            Thread.Sleep(5000); // Pause for 5 seconds
            LoadingAnimation(5); // Loading animation
        }

        Console.WriteLine($"You have listed {items.Count} items.");
        EndActivity("Gratitude Activity", duration);
    }

    static void Countdown(int countdownNumber)
    {
        for (int i = countdownNumber; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000); // Pause for 1 second
            LoadingAnimation(1); // Loading animation
        }
        Console.WriteLine();
    }

    static void LoadingAnimation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000); // Pause for 1 second
        }
    }
}
