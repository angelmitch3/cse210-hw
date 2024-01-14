using System;

class Program
{
    static void Main(string[] args)
    {

        // Step 1: Ask the user for the magic number.
        Console.Write("What is the magic number? ");
        int magicNumber = int.Parse(Console.ReadLine());

        // Step 2: Ask the user for a guess.
        Console.Write("What is your guess? ");
        int userGuess = int.Parse(Console.ReadLine());

        // Step 3: Determine if the user needs to guess higher or lower next time, or tell them if they guessed it.
        if (userGuess < magicNumber)
        {
            Console.WriteLine("Higher");
        }
        else if (userGuess > magicNumber)
        {
            Console.WriteLine("Lower");
        }
        else
        {
            Console.WriteLine("You guessed it!");
        }

        // Step 4: Add a loop that keeps looping as long as the guess does not match the magic number.
        while (userGuess != magicNumber)
        {
            Console.Write("What is your guess? ");
            userGuess = Convert.ToInt32(Console.ReadLine());

            if (userGuess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (userGuess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }

        // Step 5: Instead of having the user supply the magic number, generate a random number from 1 to 100.
        Random rand = new Random();
        magicNumber = rand.Next(1, 101);

        Console.WriteLine("A new magic number has been generated. Let's play again!");
        userGuess = -1; // Reset the user guess.

        while (userGuess != magicNumber)
        {
            Console.Write("What is your guess? ");
            userGuess = Convert.ToInt32(Console.ReadLine());

            if (userGuess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (userGuess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }
    


    }
}