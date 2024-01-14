using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
               // Step 1: Ask the user for a series of numbers, and append each one to a list. Stop when they enter 0.
        List<int> numbers = new List<int>();
        int number;
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {
            Console.Write("Enter number: ");
            number = Convert.ToInt32(Console.ReadLine());
            if (number != 0)
            {
                numbers.Add(number);
            }
        } while (number != 0);

        // Step 2: Compute the sum, or total, of the numbers in the list.
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }
        Console.WriteLine("The sum is: " + sum);

        // Step 3: Compute the average of the numbers in the list.
        double average = (double)sum / numbers.Count;
        Console.WriteLine("The average is: " + average);

        // Step 4: Find the maximum, or largest, number in the list.
        int max = numbers[0];
        foreach (int num in numbers)
        {
            if (num > max)
            {
                max = num;
            }
        }
        Console.WriteLine("The largest number is: " + max);
    }   
  
}