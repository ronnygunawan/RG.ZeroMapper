namespace RG.ZeroMapper.Structural;

/// <summary>
/// Base class for constant values in structural types.
/// </summary>
public abstract class Constant
{
    /// <summary>
    /// Gets the constant value.
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Constant"/> class with the specified value.
    /// </summary>
    /// <param name="value">The constant value.</param>
    protected Constant(object value)
    {
        Value = value;
    }
}
