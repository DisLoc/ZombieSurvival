using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TagList
{
    [SerializeField] private List<Tags> _tags;


    public bool Contains(Tags tag) => _tags.Contains(tag);


    public bool Contains(string tag)
    {
        foreach (Tags t in _tags)
        {
            if (t.ToString().Equals(tag))
            {
                return true;
            }
        }

        return false;
    }
}
