using System;

class Program
{
    static void Main(string[] args)
    {
        // Example scripture with multiple verses
        string referenceText = "Doctine and Convenant 4:2";
        string scriptureText = "Therefore O ye that embark in the service of God see that ye serve him with all your" +
        "heart, might, mind and strength, that ye may stand blameless before God at the last day.";

        // Create Scripture object
        Scripture scripture = new Scripture(referenceText, scriptureText);

        while (true)
        {
            Console.Clear();
            scripture.Display();

            Console.WriteLine("\nPress Enter to hide some words or type 'quit' to exit.");
            string input = Console.ReadLine().ToLower();

            if (input == "quit")
            {
                break;
            }

            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("\nAll words are hidden. Well done!");
                break;
            }

            scripture.HideRandomWords(3); // Hide 3 random words each time
        }
    }
}
