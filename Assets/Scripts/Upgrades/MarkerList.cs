using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MarkerList
{
    [SerializeField] private List<UpgradeMarker> _markers;
    [Tooltip("Min markers match count for getting upgrade")]
    [SerializeField] private int _minMatchesCount = 1;

    public List<UpgradeMarker> Markers => _markers;

    public bool IsStrike(List<UpgradeMarker> markers)
    {
        int matches = 0;

        foreach(UpgradeMarker marker in markers)
        {
            if (Contains(marker)) matches++;

            if (matches >= _minMatchesCount) return true;
        }

        return false;
    }

    public bool IsStrike(MarkerList markers) => IsStrike(markers.Markers);

    public bool IsStrike(UpgradeMarker marker)
    {
        if (_minMatchesCount == 1)
        {
            return Contains(marker);
        }

        else return false;
    }

    private bool Contains(UpgradeMarker marker) => _markers.Find(item => item.Equals(marker));
}
