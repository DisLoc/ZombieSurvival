using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SphereCollider))]
public class ExpCrystal : PickableObject, IPoolable
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField][Range(0.01f, 10f)] private float _pickUpSpeed;
    [Tooltip("Speed increases every frame while moving")]
    [SerializeField][Range(0f, 0.01f)] private float _speedMultiplier;

    private float _speed;
    private int _expValue;

    private Player _player;
    private MonoPool<ExpCrystal> _pool;

    /// <summary>
    /// Value that character get when take this crystal
    /// </summary>
    public int ExpValue => _expValue;

    /// <summary>
    /// Initializing exp value and color of this crystal & pool to release to
    /// </summary>
    /// <param name="param">Params need to set</param>
    /// <param name="pool">Pool to release to</param>
    public void Initialize(CrystalParam param, MonoPool<ExpCrystal> pool, Player player)
    {
        _expValue = param.ExpValue;
        _renderer.material.color = param.Color;

        _pool = pool;
        _player = player;
    }

    public void ResetObject()
    {
        _expValue = 0;
    }

    public void AddExp()
    {
        StopAllCoroutines();

        if (_player != null)
        {
            if (_isDebug) Debug.Log("Add exp to player");
            
            (_player.Stats as PlayerStats).AddExpirience(_expValue);
        }    
        else if (_isDebug) Debug.Log("Missing player!");

        _pool.Release(this);
    }

    public override void PickUp()
    {
        base.PickUp();

        _speed = _pickUpSpeed;

        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        if (_player == null)
        {
            if (_isDebug) Debug.Log("Missing player!");

            yield return null;
        }

        if (_isDebug) Debug.Log(name + " moves");

        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.fixedUnscaledDeltaTime);

        _speed += _speed * _speedMultiplier;

        yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);

        StartCoroutine(MoveToPlayer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player.ToString()))
        {
            AddExp();
        }
    }
}
