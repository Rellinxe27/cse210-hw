using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int number = -1;

        // Keep asking for numbers until the user enters 0.
        while (number != 0)
        {
            Console.Write("Enter number: ");
            number = int.Parse(Console.ReadLine());

            if (number != 0)
            {
                numbers.Add(number);
            }
        }

        // Core Requirement 1:
        // Compute the sum of all the numbers.
        int sum = 0;

        foreach (int value in numbers)
        {
            sum += value;
        }

        Console.WriteLine($"The sum is: {sum}");

        // Core Requirement 2:
        // Compute the average of all the numbers.
        double average = (double)sum / numbers.Count;

        Console.WriteLine($"The average is: {average}");

        // Core Requirement 3:
        // Find the largest number in the list.
        int largest = numbers[0];

        foreach (int value in numbers)
        {
            if (value > largest)
            {
                largest = value;
            }
        }

        Console.WriteLine($"The largest number is: {largest}");
    }
}