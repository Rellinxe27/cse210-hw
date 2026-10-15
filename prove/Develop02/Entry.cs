using System;

public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;

    public Entry(string date, string prompt, string response)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_prompt}");
        Console.WriteLine(_response);
        Console.WriteLine();
    }

    public string ToFileLine()
    {
        // Encoding each field keeps special characters such as | safe in saved entries.
        string date = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_date));
        string prompt = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_prompt));
        string response = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_response));
        return $"{date}|{prompt}|{response}";
    }

    public static Entry FromFileLine(string line)
    {
        string[] parts = line.Split('|');
        if (parts.Length != 3)
        {
            throw new FormatException("The saved journal has an invalid entry.");
        }

        string date = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(parts[0]));
        string prompt = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(parts[1]));
        string response = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(parts[2]));
        return new Entry(date, prompt, response);
    }

    public bool ContainsText(string keyword)
    {
        return _prompt.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            || _response.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }
}
