using System;

class Program
{
    static void Main(string[] args)
    {
        // Core Requirement 1:
        // Ask the user for the magic number and compare it with their guess.
        //
        // Core Requirements 1 and 2 originally used:
        // Console.Write("What is the magic number? ");
        // int magicNumber = int.Parse(Console.ReadLine());

        // Core Requirement 3:
        // Instead of asking the user for the magic number,
        // generate a random number from 1 to 100.
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int guess = -1;

        // Core Requirement 2:
        // Keep asking for guesses until the magic number is guessed.
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            // Core Requirement 1:
            // Tell the user whether to guess higher or lower.
            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
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