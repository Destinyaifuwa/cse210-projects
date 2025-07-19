using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    private Random _random = new Random();

    public Scripture(string referenceText, string scriptureText)
    {
        _reference = new Reference(referenceText);
        _words = new List<Word>();

        // Split scripture text into words
        string[] splitWords = scriptureText.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string w in splitWords)
        {
            _words.Add(new Word(w));
        }
    }

    public void Display()
    {
        Console.WriteLine(_reference.GetDisplayText());
        Console.WriteLine();

        foreach (Word word in _words)
        {
            Console.Write(word.GetDisplayText() + " ");
        }
        Console.WriteLine();
    }

    // Hide a number of random words (could be already hidden, as per core requirement)
    public void HideRandomWords(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = _random.Next(_words.Count);
            _words[index].Hide();
        }
    }

    // Stretch challenge: hide only words not hidden yet
    // (Not used in core, but you can implement if you want)

    public bool AllWordsHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}
