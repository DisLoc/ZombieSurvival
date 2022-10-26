using UnityEngine;

public class HPBarCanvas : MonoBehaviour, IFixedUpdatable
{
    private Camera _camera;

    private void OnEnable()
    {
        _camera = Camera.main;
    }

    public void OnFixedUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, transform.position.z));
    }
}
