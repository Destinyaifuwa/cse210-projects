using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessProgram
{
    // Base class with shared behavior and properties
    abstract class Activity
    {
        protected string _activityName;
        protected string _description;
        protected int _durationSeconds;

        public Activity(string name, string description)
        {
            _activityName = name;
            _description = description;
        }

        // Common starting message + get duration
        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"--- {_activityName} ---");
            Console.WriteLine(_description);
            Console.WriteLine();
            _durationSeconds = GetDurationFromUser();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }

        // Common ending message
        public void End()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(3);
            Console.WriteLine($"You have completed the {_activityName} for {_durationSeconds} seconds.");
            ShowSpinner(3);
            Console.WriteLine();
        }

        // Abstract method to be implemented by derived classes
        public abstract void Run();

        // Helper: Get duration input safely
        private int GetDurationFromUser()
        {
            int duration = 0;
            while (true)
            {
                Console.Write("Enter duration in seconds: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out duration) && duration > 0)
                    break;
                Console.WriteLine("Please enter a positive integer.");
            }
            return duration;
        }

        // Helper: Spinner animation for given seconds
        protected void ShowSpinner(int seconds)
        {
            string[] spinner = { "/", "-", "\\", "|" };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            while (DateTime.Now < endTime)
            {
                Console.Write(spinner[i % spinner.Length]);
                Thread.Sleep(250);
                Console.Write("\b");
                i++;
            }
        }

        // Helper: Countdown timer animation (counts down from seconds)
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
    }

    // Breathing Activity class
    class BreathingActivity : Activity
    {
        public BreathingActivity() 
            : base("Breathing Activity", 
                   "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        public override void Run()
        {
            Start();

            int elapsed = 0;
            int breatheInSeconds = 4;
            int breatheOutSeconds = 6;

            while (elapsed < _durationSeconds)
            {
                Console.WriteLine("Breathe in...");
                ShowCountdown(breatheInSeconds);
                elapsed += breatheInSeconds;
                if (elapsed >= _durationSeconds) break;

                Console.WriteLine("Breathe out...");
                ShowCountdown(breatheOutSeconds);
                elapsed += breatheOutSeconds;
            }

            End();
        }
    }

    // Reflection Activity class
    class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
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

        private Random _random = new Random();

        public ReflectionActivity()
            : base("Reflection Activity",
                   "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        { }

        public override void Run()
        {
            Start();

            // Show random prompt
            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine("When you have something in mind, press Enter to continue.");
            Console.ReadLine();

            DateTime endTime = DateTime.Now.AddSeconds(_durationSeconds);
            int questionIndex = 0;

            while (DateTime.Now < endTime)
            {
                string question = _questions[questionIndex % _questions.Count];
                Console.WriteLine();
                Console.WriteLine(question);
                ShowSpinner(5);  // pause with spinner for 5 seconds
                questionIndex++;
            }

            End();
        }
    }

    // Listing Activity class
    class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private Random _random = new Random();

        public ListingActivity()
            : base("Listing Activity",
                   "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }

        public override void Run()
        {
            Start();

            // Show random prompt
            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine("You may begin listing items after the countdown.");
            ShowCountdown(5);

            DateTime endTime = DateTime.Now.AddSeconds(_durationSeconds);

            List<string> items = new List<string>();
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    items.Add(item.Trim());
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {items.Count} items!");
            End();
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("Please select an activity:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Enter your choice (1-4): ");

                string choice = Console.ReadLine();

                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        Thread.Sleep(1500);
                        continue;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        continue;
                }

                if (activity != null)
                {
                    activity.Run();
                    Console.WriteLine("Press Enter to return to the menu.");
                    Console.ReadLine();
                }
            }
        }
    }
}
