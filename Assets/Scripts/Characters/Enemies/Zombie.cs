using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour, IEnemyKilledHandler
{
    [SerializeField] private int damageToPlayer;

    [SerializeField] private int speed = 3;
    private Transform _player;

    public EnemiesList _enemiesList;

 //   private GameObject test;

    [SerializeField] private GameObject crystal;

    [SerializeField] private Slider _hpBar;
    [SerializeField] private int _hpCount = 100;


    void Start()
    {
        _enemiesList = GameObject.FindGameObjectWithTag("EnemiesList").GetComponent<EnemiesList>();
        _enemiesList.enemies.Add(this);
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

        if(_hpCount < 0)
        {
            OnEnemyKilled();
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "SomeWeapon")
        {
            GetDamage(1);
        }
    }

    public void PlusHP(int factor)
    {
        _hpCount += (_hpCount + _hpCount) * factor;
        _hpBar.value = _hpCount;
    }

    public int ReturnDamage()
    {
        return damageToPlayer;
    }

    public void OnEnemyKilled()
    {
        Instantiate(crystal, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _enemiesList.enemies.Remove(this);
    }
}
