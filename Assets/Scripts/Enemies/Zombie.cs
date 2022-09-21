using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int damageToPlayer;
    [SerializeField] private int speed = 3;
    private Transform _player;
    [SerializeField] private Slider _hpBar;
    private int _hpCount = 100;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
        transform.LookAt(_player);
    }

    private void GetDamage(int damage)
    {
        _hpCount -= damage;
        _hpBar.value = _hpCount;
    }

    public int ReturnDamage()
    {
        return damageToPlayer;
    }
}
