using UnityEngine;

public class FrogKing : ClickableObject, IDamageable, IAttackable
{
    [SerializeField] private Animator _animator;

    [SerializeField] private CharacterStats _stats;
    public float MaxHP => _stats.MaxHP;
    public float HP => _stats.HP;
    public float Damage => _stats.Damage;
    public float AttackTime => _stats.AttackTime;
    private float _attackTimer = 0;

    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
    }

    public void AttackControl ()
    {
        if (_attackTimer > 0) _attackTimer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        _stats.HP -= damage;

        if (_stats.HP <= 0)
        {
            _eventManager.OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void MakeDamage (IDamageable target)
    {
        target.TakeDamage(Damage);
        _eventManager.OnCharacterTakeDamage?.Invoke(this, target, Damage);

        _attackTimer = AttackTime;
    }
}