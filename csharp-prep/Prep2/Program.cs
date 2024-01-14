using System;

class Program
{
    static void Main(string[] args)
    {
       Console.Write("Enter your grade percentage: ");
        double grade = Convert.ToDouble(Console.ReadLine());
        string letter;

        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine("Your letter grade is: " + letter);

        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't be discouraged. Study harder and Better luck next time!");
        }
    
    }
}