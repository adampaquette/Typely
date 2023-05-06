namespace Typely.Generators.Typely.Parsing;

public class TypeProperties : Dictionary<string, object>
{
    public const string MaxLength = nameof(MaxLength);
    
    public void SetMaxLength(int value)
    {
        if (!ContainsKey(MaxLength))
        {
            Add(MaxLength, value);
        }
        else
        {
            if ((int)this[MaxLength] > value)
            {
                this[MaxLength] = value;
            }
        }
    }

    public bool ContainsMaxLength() => ContainsKey(MaxLength);

    public object GetMaxLength() => this[MaxLength];
}