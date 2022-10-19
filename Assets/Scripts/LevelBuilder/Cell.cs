using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Quaternion _rotationQuaternion;

    private int _xIndex;
    private int _zIndex;
    private GridXZ _grid;

    public int X => _xIndex;
    public int Z => _zIndex;
    public Quaternion RotationQuaternion => _rotationQuaternion; 

    public void Initialize(int xIndex, int zIndex, GridXZ grid)
    {
        _xIndex = xIndex;
        _zIndex = zIndex;
        _grid = grid;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Player.ToString())
        {
            if (_grid != null)
            {
                _grid.OnPlayerEnter(this);
            }
            else Debug.Log("Missing grid!");
        }
    }
}
