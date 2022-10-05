public interface IStat
{
    public float BaseValue { get; }
    public float Value { get; }
    public float MinValue { get; }
    public float MaxValue { get; }
    public bool MaxValueIsInfinite { get; }

    public void SetValue(float value);
}
