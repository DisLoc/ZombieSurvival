using UnityEngine;

public class PlayerMoveController : MonoBehaviour, IFixedUpdatable
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [Tooltip("Camera always follow player")]
    [SerializeField] private Vector3 _cameraPos;
    [SerializeField] private Player _player;

    private bool _isMobile;

    /// <summary>
    /// Position of the first touch on screen
    /// </summary>
    private Vector2 _touchPosition;
    /// <summary>
    /// Equals true while TouchCount > 0
    /// </summary>
    private bool _onTouch;

    private void OnEnable()
    {
        _isMobile = Application.isMobilePlatform;
    }

    public void OnFixedUpdate()
    {
        if (_isMobile)
        {
            if (Input.touchCount > 0) 
            {
                // Move if already touch screen
                if (_onTouch) 
                {
                    Vector3 moveDirection = new Vector3
                        (
                            Input.GetTouch(0).position.x - _touchPosition.x,
                            0,
                            Input.GetTouch(0).position.y - _touchPosition.y
                        );

                    _player.Move(moveDirection);

                    _player.isMoving = true;
                }
                // Set new touch position
                else
                {
                    _onTouch = true;
                    _touchPosition = Input.GetTouch(0).position;
                }
            }
            else // Reset flags
            {
                _onTouch = false;

                _player.isMoving = false;
            }
        }
        else
        {
            #region WASD control
            if (Input.GetKey(KeyCode.W))
            {
                _player.Move(Vector3.forward);
                _player.isMoving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _player.Move(Vector3.back);
                _player.isMoving = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _player.Move(Vector3.left);
                _player.isMoving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _player.Move(Vector3.right);
                _player.isMoving = true;
            }
            #endregion

            #region Mouse control (Equals mobile control)
            if (Input.GetMouseButton(0)) 
            {
                if (_onTouch)
                {
                    Vector3 moveDirection = new Vector3
                        (
                            Input.mousePosition.x - _touchPosition.x,
                            0,
                            Input.mousePosition.y - _touchPosition.y
                        ).normalized;

                    if (_isDebug) Debug.Log(moveDirection);

                    _player.Move(moveDirection);

                    _player.isMoving = true;
                }
                else
                {
                    _onTouch = true;
                    _touchPosition = Input.mousePosition;

                    if (_isDebug) Debug.Log(_touchPosition);
                }
            }
            else
            {
                _onTouch = false;

                _player.isMoving = false;
            }
            #endregion
        }

        transform.position = _player.transform.position + _cameraPos; // Follow player
        transform.LookAt(_player.transform);
    }
}
