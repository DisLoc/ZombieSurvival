using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : CharacterBase, IEnemyKilledHandler
{
    [SerializeField] private int damageToPlayer;

    [SerializeField] private int speed = 3;
    private Transform _player;

    public EnemiesList _enemiesList;

 //   private GameObject test;

    [SerializeField] private GameObject crystal;

    [SerializeField] private Slider _hpBar;
    [SerializeField] private int _hpCount = 100;

    public override CharacterStats Stats => throw new System.NotImplementedException();

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;


        _enemiesList.enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        Move(new Vector3(0, 0, 0));
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
        //_enemiesList.enemies.Remove(this);
    }

    public override void Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position,  _player.transform.position, speed * Time.deltaTime);
        transform.LookAt(_player);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

}
