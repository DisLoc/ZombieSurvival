using UnityEngine;

public interface IObjectDisableHandler : ISubscriber
{
    public void OnObjectDisable(GameObject obj);
}
