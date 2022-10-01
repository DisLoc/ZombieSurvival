using UnityEngine;
using Zenject;

public class PlayerMoveController : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Vector3 _cameraPos;

    [Inject] private Player _player;

    private bool _isMobile;

    private Vector2 _touchPosition;
    private bool _onTouch;

    private void OnEnable()
    {
        _isMobile = Application.isMobilePlatform;
    }

    private void FixedUpdate()
    {
        if (_isMobile)
        {
            if (Input.touchCount > 0)
            {
                if (_onTouch)
                {
                    Vector3 moveDirection = new Vector3
                        (
                            Input.GetTouch(0).position.x - _touchPosition.x,
                            0,
                            Input.GetTouch(0).position.y - _touchPosition.y
                        );

                    _player.Move(moveDirection);
                        
                }
                else
                {
                    _onTouch = true;
                    _touchPosition = Input.GetTouch(0).position;
                }
            }
            else
            {
                _onTouch = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                _player.Move(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _player.Move(Vector3.back);
            }
            if (Input.GetKey(KeyCode.A))
            {
                _player.Move(Vector3.left);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _player.Move(Vector3.right);
            }

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
            }
        }

        transform.position = _player.transform.position + _cameraPos;
    }
}
