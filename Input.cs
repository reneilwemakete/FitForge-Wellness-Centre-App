namespace Project_Fit_Forge;

public static class Input
{
    public static int ReadInt(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);
            string? raw = Console.ReadLine();
            if (raw is null) throw new EndOfStreamException("Input ended unexpectedly.");
            if (int.TryParse(raw, out int value) && value >= minimum && value <= maximum)
                return value;
            Console.WriteLine($"Enter a whole number between {minimum} and {maximum}.");
        }
    }

    public static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? value = Console.ReadLine();
            if (value is null) throw new EndOfStreamException("Input ended unexpectedly.");
            if (!string.IsNullOrWhiteSpace(value)) return value.Trim();
            Console.WriteLine("This field cannot be empty.");
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            string answer = ReadRequired(prompt);
            if (answer.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                answer.Equals("y", StringComparison.OrdinalIgnoreCase)) return true;
            if (answer.Equals("no", StringComparison.OrdinalIgnoreCase) ||
                answer.Equals("n", StringComparison.OrdinalIgnoreCase)) return false;
            Console.WriteLine("Please enter yes or no.");
        }
    }
}
