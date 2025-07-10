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
        // Placeholder code for saving to a file
        Console.WriteLine($"Entries saved to {fileName} (stub).");
    }

    // Loads entries from a file
    public void LoadFromFile(string fileName)
    {
        // Placeholder code for loading from a file
        Console.WriteLine($"Entries loaded from {fileName} (stub).");
    }
}