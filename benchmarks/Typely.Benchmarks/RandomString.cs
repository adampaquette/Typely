namespace Typely.Benchmarks;

public class RandomString
{
    private readonly Random _random = new();
    
    public string Next(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var stringChars = new char[length];

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[_random.Next(chars.Length)];
        }

        return new String(stringChars);
    }
}