using UnityEngine;

[System.Serializable]
public class Breakpoint
{
    [SerializeField] protected string _name;

    [Header("Breakpoint settings")]
    [Tooltip("Field can be null (if null this breakpoint will not be displayed on LevelProgress bar)")]
    [SerializeField] protected Sprite _breakpointIcon;
    [SerializeField] protected Sprite _breakpointLine;
    [SerializeField] protected Color _breakpointLineColor = Color.white;

    [SerializeField][Range(1f, 10f)] protected float _breakpointDisplayTime = 5f;
    [Tooltip("If BreakpointIcon not null this description will be displayed BreakpointDisplayTime seconds before reaching breakpoint")]
    [SerializeField] protected string _breakpointDescription;
    [SerializeField] protected SoundType _breakpointSound;
    
    [Space(5)]
    [SerializeField][Range(0, 100)] protected int _requiredProgress;

    private bool _isReached;

    public Sprite Icon => _breakpointIcon;
    public Sprite Line => _breakpointLine;
    public Color LineColor => _breakpointLineColor;
    public float DisplayTime => _breakpointDisplayTime;
    public string Description => _breakpointDescription;
    public SoundType Sound => _breakpointSound;

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
