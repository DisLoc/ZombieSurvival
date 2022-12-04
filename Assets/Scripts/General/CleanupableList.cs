using System.Collections.Generic;

[System.Serializable]
public class CleanupableList<T> where T : class
{
    protected List<T> _list;
    protected bool _needCleanup;

    protected int _currentIndex = 0;

    public List<T> List => _list;
    public int Count => _list.Count;

    public CleanupableList()
    {
        _list = new List<T>();
        _needCleanup = false;
    }

    public CleanupableList(int capacity)
    {
        _list = new List<T>(capacity);
        _needCleanup = false;
    }

    public T this[int index] => _list[index];

    public virtual void Add(T item)
    {
        _list.Add(item);
    }

    /// <summary>
    /// Remove item from list
    /// </summary>
    /// <param name="item">Item need to remove</param>
    /// <param name="canNotModify">If canModify equals true, item will be set null value</param>
    /// <returns>Return true if remove item or item setted null</returns>
    public bool Remove(T item, bool canNotModify = false)
    {
        if (_list.Contains(item))
        {
            if (canNotModify)
            {
                _needCleanup = true;
                _list[_list.IndexOf(item)] = null;
                return true;
            }
            else
            {
                return _list.Remove(item);
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Remove all null items
    /// </summary>
    public void Cleanup()
    {
        if (_needCleanup)
        {
            _list.RemoveAll(item => item == null);
            _needCleanup = false;
        }

        else return;
    }
}
