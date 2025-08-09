using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base abstract class
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        public abstract int RecordEvent();

        public abstract bool IsComplete();

        public virtual string GetStatus()
        {
            return "[ ]";
        }

        public virtual string GetDetailsString()
        {
            return $"{_name} ({_description})";
        }

        public virtual string Serialize()
        {
            // Format: ClassName|name|description|points|... (extra fields overridden in derived classes)
            return $"{GetType().Name}|{_name}|{_description}|{_points}";
        }

        public static Goal Deserialize(string line)
        {
            // Parse string and create appropriate Goal subclass
            string[] parts = line.Split('|');
            string type = parts[0];
            string name = parts[1];
            string desc = parts[2];
            int points = int.Parse(parts[3]);

            switch (type)
            {
                case "SimpleGoal":
                    bool isComplete = bool.Parse(parts[4]);
                    var sg = new SimpleGoal(name, desc, points);
                    sg.SetComplete(isComplete);
                    return sg;
                case "EternalGoal":
                    return new EternalGoal(name, desc, points);
                case "ChecklistGoal":
                    int timesCompleted = int.Parse(parts[4]);
                    int timesRequired = int.Parse(parts[5]);
                    int bonusPoints = int.Parse(parts[6]);
                    var cg = new ChecklistGoal(name, desc, points, timesRequired, bonusPoints);
                    cg.SetTimesCompleted(timesCompleted);
                    return cg;
                default:
                    throw new Exception("Unknown goal type");
            }
        }
    }

    // SimpleGoal class
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points) : base(name, description, points)
        {
            _isComplete = false;
        }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return _points;
            }
            return 0;
        }

        public override bool IsComplete() => _isComplete;

        public override string GetStatus() => _isComplete ? "[X]" : "[ ]";

        public void SetComplete(bool val)
        {
            _isComplete = val;
        }

        public override string Serialize()
        {
            return $"{base.Serialize()}|{_isComplete}";
        }
    }

    // EternalGoal class
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points) : base(name, description, points) { }

        public override int RecordEvent()
        {
            return _points;
        }

        public override bool IsComplete() => false;

        public override string GetStatus() => "[∞]";
    }

    // ChecklistGoal class
    public class ChecklistGoal : Goal
    {
        private int _timesCompleted;
        private int _timesRequired;
        private int _bonusPoints;

        public ChecklistGoal(string name, string description, int points, int timesRequired, int bonusPoints)
            : base(name, description, points)
        {
            _timesRequired = timesRequired;
            _bonusPoints = bonusPoints;
            _timesCompleted = 0;
        }

        public override int RecordEvent()
        {
            if (_timesCompleted < _timesRequired)
            {
                _timesCompleted++;
                if (_timesCompleted == _timesRequired)
                {
                    return _points + _bonusPoints;
                }
                return _points;
            }
            return 0;
        }

        public override bool IsComplete() => _timesCompleted >= _timesRequired;

        public override string GetStatus() => IsComplete() ? "[X]" : "[ ]";

        public override string GetDetailsString()
        {
            return $"{_name} ({_description}) -- Completed {_timesCompleted}/{_timesRequired} times";
        }

        public void SetTimesCompleted(int val)
        {
            _timesCompleted = val;
        }

        public override string Serialize()
        {
            return $"{base.Serialize()}|{_timesCompleted}|{_timesRequired}|{_bonusPoints}";
        }
    }

    class Program
    {
        static List<Goal> goals = new List<Goal>();
        static int totalScore = 0;

        static void Main(string[] args)
        {
            LoadGoals();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Total Score: {totalScore}");
                Console.WriteLine("Menu Options:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Save Goals");
                Console.WriteLine("5. Load Goals");
                Console.WriteLine("6. Quit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        ListGoals();
                        Pause();
                        break;
                    case "3":
                        RecordEvent();
                        break;
                    case "4":
                        SaveGoals();
                        Pause();
                        break;
                    case "5":
                        LoadGoals();
                        Pause();
                        break;
                    case "6":
                        SaveGoals();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        Pause();
                        break;
                }
            }
        }

        static void CreateGoal()
        {
            Console.Clear();
            Console.WriteLine("Select Goal Type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choice: ");
            string typeChoice = Console.ReadLine();

            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter goal description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter points for this goal: ");
            int points = int.Parse(Console.ReadLine());

            switch (typeChoice)
            {
                case "1":
                    goals.Add(new SimpleGoal(name, desc, points));
                    break;
                case "2":
                    goals.Add(new EternalGoal(name, desc, points));
                    break;
                case "3":
                    Console.Write("Enter number of times to complete: ");
                    int timesRequired = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points on completion: ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    goals.Add(new ChecklistGoal(name, desc, points, timesRequired, bonusPoints));
                    break;
                default:
                    Console.WriteLine("Invalid goal type.");
                    Pause();
                    return;
            }
            Console.WriteLine("Goal created!");
            Pause();
        }

        static void ListGoals()
        {
            Console.Clear();
            Console.WriteLine("Your Goals:");
            for (int i = 0; i < goals.Count; i++)
            {
                var goal = goals[i];
                string status = goal.GetStatus();
                string details = goal.GetDetailsString();
                Console.WriteLine($"{i + 1}. {status} {details}");
            }
        }

        static void RecordEvent()
        {
            if (goals.Count == 0)
            {
                Console.WriteLine("No goals to record. Create one first.");
                Pause();
                return;
            }

            ListGoals();
            Console.Write("Enter the number of the goal you accomplished: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= goals.Count)
            {
                var goal = goals[index - 1];
                int earned = goal.RecordEvent();
                if (earned > 0)
                {
                    totalScore += earned;
                    Console.WriteLine($"Congratulations! You earned {earned} points.");
                }
                else
                {
                    Console.WriteLine("This goal is already complete or no points earned.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
            Pause();
        }

        static void SaveGoals()
        {
            using StreamWriter writer = new StreamWriter("goals.txt");
            writer.WriteLine(totalScore);
            foreach (var goal in goals)
            {
                writer.WriteLine(goal.Serialize());
            }
            Console.WriteLine("Goals saved!");
        }

        static void LoadGoals()
        {
            goals.Clear();
            totalScore = 0;

            if (!File.Exists("goals.txt"))
            {
                return;
            }

            string[] lines = File.ReadAllLines("goals.txt");
            if (lines.Length == 0) return;

            totalScore = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    goals.Add(Goal.Deserialize(lines[i]));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading goal: {e.Message}");
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
