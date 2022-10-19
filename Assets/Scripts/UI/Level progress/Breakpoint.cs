using UnityEngine;

[System.Serializable]
public class Breakpoint
{
    [SerializeField] protected int _requiredProgress;

    public int RequiredProgress => _requiredProgress;
}
