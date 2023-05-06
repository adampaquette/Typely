namespace Typely.Generators.Typely.Parsing;

public class TypeProperties : Dictionary<string, string>
{
    public const string MaxLength = nameof(MaxLength);
    
    public void SetMaxLength(int value)
    {
        if (!ContainsKey(MaxLength))
        {
            Add(MaxLength, value.ToString());
        }
        else
        {
            if (int.Parse(this[MaxLength]) > value)
            {
                this[MaxLength] = value.ToString();
            }
        }
    }

    public bool ContainsMaxLength() => ContainsKey(MaxLength);

    public object GetMaxLength() => this[MaxLength];
}