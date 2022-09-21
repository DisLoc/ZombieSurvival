using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPControl : MonoBehaviour, IGameOverHandler
{
    [SerializeField] private Slider _hpBar;
    private int _hpCount = 500;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Zombie>())
        {
            GetDamage(collision.gameObject.GetComponent<Zombie>().ReturnDamage());
        }
    }

    public void GetDamage(int damage)
    {
        _hpCount -= damage;
        _hpBar.value = _hpCount;

        if(_hpCount < 0)
        {
            OnGameOver();
        }
    }

    public void OnGameOver()
    {
        //Поставил как заглушку
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
