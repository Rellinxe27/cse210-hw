using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is one thing I learned today that I did not know yesterday?",
        "What small thing made me smile today?",
        "Who am I grateful for today, and why?",
        "What was the hardest thing I faced today, and how did I handle it?",
        "What is one goal I want to work on tomorrow?"
    };

    private Random _random = new Random();
    private int _lastIndex = -1;

    public string GetRandomPrompt()
    {
        // Pick a different prompt than the last one so the user never sees the same question twice in a row.
        int index = _random.Next(_prompts.Count);
        while (index == _lastIndex)
        {
            index = _random.Next(_prompts.Count);
        }

        _lastIndex = index;
        return _prompts[index];
    }
}
