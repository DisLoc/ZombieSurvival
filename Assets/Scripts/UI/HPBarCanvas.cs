using UnityEngine;

public class HPBarCanvas : MonoBehaviour, IFixedUpdatable
{
    public void OnFixedUpdate()
    {
        Vector3 pos = transform.position;
        transform.LookAt(new Vector3(pos.x, pos.y + 1f, pos.z));
    }
}
