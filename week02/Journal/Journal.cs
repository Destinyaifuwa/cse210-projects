using System;
using System.Collections.Generic;
using System.IO;
public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    // Adds an entry to the journal
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    // Displays all entries
    public void DisplayAll()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    // Saves entries to a file
    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine($"Entries saved to {fileName}.");
    }

    // Loads entries from a file
    public void LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File not found.");
            return;
        }
        _entries.Clear();
        string[] lines = File.ReadAllLines(fileName);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                Entry entry = new Entry
                {
                    Date = parts[0],
                    Prompt = parts[1],
                    Response = parts[2]
                };
                _entries.Add(entry);
            }
        }
        Console.WriteLine($"Entries loaded from {fileName}.");
    }
}