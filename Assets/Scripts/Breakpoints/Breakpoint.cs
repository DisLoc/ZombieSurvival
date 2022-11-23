using UnityEngine;

[System.Serializable]
public class Breakpoint
{
    [SerializeField] protected string _name;

    [Header("Breakpoint settings")]
    [SerializeField] protected Sprite _breakpointIcon;
    [SerializeField] protected Sprite _breakpointLine;
    [SerializeField] protected Color _breakpointLineColor = Color.white;
    
    [SerializeField][Range(0, 100)] protected int _requiredProgress;

    private bool _isReached;

    public Sprite Icon => _breakpointIcon;
    public Sprite Line => _breakpointLine;
    public Color LineColor => _breakpointLineColor;

    public string Name => _name;
    public int RequiredProgress => _requiredProgress;
    public bool IsReached => _isReached;

    public void SetReached(bool value)
    {
        _isReached = value;
    }

    protected Breakpoint(Breakpoint breakpoint)
    {
        _name = breakpoint._name;
        _breakpointIcon = breakpoint._breakpointIcon;
        _requiredProgress = breakpoint._requiredProgress;
        _isReached = false;
    }

    public virtual Breakpoint Clone() => new Breakpoint(this);
}
