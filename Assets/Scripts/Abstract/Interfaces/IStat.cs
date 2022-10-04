public interface IStat
{
    public float BaseValue { get; }
    public float Value { get; }
    public float MaxValue { get; }

    public void SetValue(float value);

    public void SetMaxValue(float value);
}
