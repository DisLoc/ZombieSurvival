using UnityEngine;

[System.Serializable]
public class Breakpoint
{
    [SerializeField][Range(0, 100)] protected int _requiredProgress;

    public int RequiredProgress => _requiredProgress;
}
