using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableTypeOrDiagnostic : IEquatable<EmittableTypeOrDiagnostic>
{
    public EmittableType? EmittableType { get; }
    public Diagnostic? Diagnostic { get; }

    public EmittableTypeOrDiagnostic(EmittableType emittableType)
    {
        EmittableType = emittableType;
        Diagnostic = null;
    }
    
    public EmittableTypeOrDiagnostic(Diagnostic diagnostic)
    {
        EmittableType = null;
        Diagnostic = diagnostic;
    }


    public bool Equals(EmittableTypeOrDiagnostic? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Equals(EmittableType, other.EmittableType) && Equals(Diagnostic, other.Diagnostic);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is EmittableTypeOrDiagnostic other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((EmittableType != null ? EmittableType.GetHashCode() : 0) * 397) ^ (Diagnostic != null ? Diagnostic.GetHashCode() : 0);
        }
    }
}