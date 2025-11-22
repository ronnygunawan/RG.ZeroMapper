namespace RG.ZeroMapper.Structural;

/// <summary>
/// Base class for constant values in structural types.
/// </summary>
public abstract class Constant
{
    public object Value { get; }
    
    protected Constant(object value)
    {
        Value = value;
    }
}
