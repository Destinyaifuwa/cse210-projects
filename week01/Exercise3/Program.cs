using System;

class Program
{
    static void Main(string[] args)
    {
        // Step 1 generate a random number between 1 and 100
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101); // 101 is not included in the list of numbers

        int guess = -1; // This will initialize the value that cant't be the answer


        Console.WriteLine("Welcome to the guess my number game!");

        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            string input = Console.ReadLine();
            guess = int.Parse(input);

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