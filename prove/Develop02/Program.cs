using System;

// Showing creativity / exceeding requirements:
// 1. Safe file format: each field of an entry (date, prompt, response) is Base64-encoded
//    before it is saved, so responses containing commas, quotes, "|" or even line breaks
//    can never break the saved file (see Entry.ToFileLine and Entry.FromFileLine).
// 2. Search: menu option 5 shows every entry whose prompt or response contains a keyword
//    (case-insensitive), so a long journal is easy to look back through.
// 3. Safe loading: Journal.LoadFromFile builds a new list first and only replaces the
//    journal when the whole file was read, so a missing or damaged file never erases
//    the entries already in memory.
// 4. The prompt list has 10 prompts (5 of my own), and PromptGenerator never repeats the
//    same prompt twice in a row.
// 5. Input checks: empty responses and empty file names are rejected, and file errors
//    show a friendly message instead of crashing the program.

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        Console.WriteLine("Welcome to the Journal Program!");

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Search");
            Console.WriteLine("6. Quit");
            Console.Write("What would you like to do? ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteEntry(journal, promptGenerator);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    LoadJournal(journal);
                    break;
                case "4":
                    SaveJournal(journal);
                    break;
                case "5":
                    SearchJournal(journal);
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Goodbye! Keep writing every day.");
                    break;
                default:
                    Console.WriteLine("Please enter a number from 1 to 6.");
                    break;
            }
        }
    }

    static void WriteEntry(Journal journal, PromptGenerator promptGenerator)
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine(prompt);
        Console.Write("> ");
        string response = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(response))
        {
            Console.WriteLine("Nothing was written, so no entry was added.");
            return;
        }

        string date = DateTime.Now.ToShortDateString();
        journal.AddEntry(new Entry(date, prompt, response));
        Console.WriteLine("Entry added.");
    }

    static void LoadJournal(Journal journal)
    {
        string filename = AskForFilename("load from");
        if (filename == "")
        {
            return;
        }

        try
        {
            journal.LoadFromFile(filename);
            Console.WriteLine("Journal loaded.");
        }
        catch (Exception ex) when (ex is System.IO.IOException || ex is FormatException || ex is UnauthorizedAccessException)
        {
            Console.WriteLine($"Could not load that file: {ex.Message}");
        }
    }

    static void SaveJournal(Journal journal)
    {
        string filename = AskForFilename("save to");
        if (filename == "")
        {
            return;
        }

        try
        {
            journal.SaveToFile(filename);
            Console.WriteLine("Journal saved.");
        }
        catch (Exception ex) when (ex is System.IO.IOException || ex is UnauthorizedAccessException)
        {
            Console.WriteLine($"Could not save that file: {ex.Message}");
        }
    }

    static void SearchJournal(Journal journal)
    {
        Console.Write("What word or phrase do you want to search for? ");
        string keyword = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            Console.WriteLine("No search text entered.");
            return;
        }

        journal.Search(keyword.Trim());
    }

    static string AskForFilename(string action)
    {
        Console.Write($"What is the filename to {action}? ");
        string filename = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("No filename entered.");
            return "";
        }

        return filename.Trim();
    }
}
