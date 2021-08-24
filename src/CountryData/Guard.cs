using System;

static class Guard
{
    public static void AgainstNegative(int value, string argumentName)
    {
        if (value < 0)
        {
            throw new ArgumentNullException(argumentName);
        }
    }
    public static void AgainstWhiteSpace(string argumentName, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(argumentName);
        }
    }
}