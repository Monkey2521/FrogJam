using UnityEngine;

public class EnemyMosquitoController : ClickableObject, IDamageable, IAttackable, IMoveable
{
    [SerializeField] private Animator _animator;

    [SerializeField] private CharacterStats _stats;
    public float MaxHP => _stats.MaxHP;
    public float HP => _stats.HP;
    public float Damage => _stats.Damage;
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

        if (collision is IDamageable)
        {
            MakeDamage(collision as IDamageable);
        }
    }

    public void TakeDamage(float damage)
    {
        _stats.HP -= damage;

        if (_stats.HP <= 0)
        {
            Spawner.DestroyMosquito(this);
            _eventManager.OnEnemyDeath?.Invoke();
        }
    }

    public void MakeDamage(IDamageable target)
    {
        target.TakeDamage(Damage);
        _eventManager.OnCharacterTakeDamage?.Invoke(this, target, Damage);

        Spawner.DestroyMosquito(this);
    }

    public void Move(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 currentPos = transform.position;

        transform.position += new Vector3
                                (
                                    targetPos.x - currentPos.x,
                                    targetPos.y - currentPos.y, 
                                    currentPos.z
                                ).normalized * Speed * Time.deltaTime;
    }
}
