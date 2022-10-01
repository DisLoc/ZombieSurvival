using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPBar : FillBar, IGameOverHandler
{  
    protected override void OnEnable()
    {
        base.OnEnable();

        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void UpdateHealth(int value)
    {
        _value = value;
        UpdateBar();
    }

    public void OnGameOver()
    {
        //Поставил как заглушку
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
