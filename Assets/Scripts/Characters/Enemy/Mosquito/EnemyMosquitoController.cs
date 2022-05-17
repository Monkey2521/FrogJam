using UnityEngine;

public class EnemyMosquitoController : ClickableObject, IDamageable, IAttackable, IMoveable
{
    [SerializeField] private Animator _animator;

    [SerializeField] private CharacterStats _stats;
    public float MaxHP => _stats.MaxHP;
    public float HP => _stats.HP;
    public float Damage => _stats.Damage;
    public float AttackTime => _stats.AttackTime;
    private float _attackTimer = 0f;
    private bool _isSucking = false;
    public float Speed => _stats.Speed;

    private EventManager _eventManager;

    [HideInInspector] public MosquitoSpawner Spawner;

    private void Awake()
    {
        _stats.Init();
    }

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDebug) Debug.Log("Collision with " + collision.name);

        if (collision.tag == "Player")
        {
            MakeDamage(collision as IDamageable);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0f && collision.tag == "Player") MakeDamage(collision as IDamageable);
    }

    public void TakeDamage(float damage)
    {
        _stats.HP -= damage;

        if (_stats.HP <= 0)
        {
            _isSucking = false;

            Spawner.AddToPull(this);
            _eventManager.OnEnemyDeath?.Invoke();
        }
    }

    public void MakeDamage(IDamageable target)
    {
        target.TakeDamage((_isSucking ? Damage * AttackTime : Damage) * GameManager.DifficultyMultiplier);
        _eventManager.OnCharacterTakeDamage?.Invoke(this, target, Damage);

        _attackTimer = AttackTime;
        _isSucking = true;
    }

    public void Move(Vector3 targetPos)
    {
        Vector3 currentPos = transform.position;

        transform.position += new Vector3
                                (
                                    targetPos.x - currentPos.x,
                                    targetPos.y - currentPos.y, 
                                    currentPos.z
                                ).normalized * Speed * GameManager.DifficultyMultiplier * Time.deltaTime;
    }
}
