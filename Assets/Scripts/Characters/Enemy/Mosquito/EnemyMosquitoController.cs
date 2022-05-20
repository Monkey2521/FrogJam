using UnityEngine;
using UnityEngine.EventSystems;

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

    public MosquitoSpawner Spawner;

    private Inventory _inventory;
    [SerializeField] private ItemInventory _reward;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnPlayerDeath.AddListener(ReturnToPool);

        _inventory = Inventory.GetInventory();
    }

    public void Init()
    {
        _stats.Init();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDebug) Debug.Log("Collision with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {

            MakeDamage(collision.gameObject.GetComponent<FrogKing>());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0f && collision.gameObject.tag == "Player") 
            MakeDamage(collision.gameObject.GetComponent<FrogKing>());
    }

    public void TakeDamage(float damage)
    {
        _stats.HP -= damage;

        _soundManager.PlaySound(SoundTypes.OnMosquitoTakeDamage);

        if (_stats.HP <= 0)
        {
            _isSucking = false;

            ReturnToPool();       
            _eventManager.OnEnemyDeath?.Invoke();

            ItemInventory reward = new ItemInventory(_reward);
            reward.AddCount((int)(reward.Count * GameManager.DifficultyMultiplier) - reward.Count);

            _inventory.Add(reward);
        }
    }

    public void MakeDamage(IDamageable target)
    {
        float damage = (_isSucking ? Damage * AttackTime : Damage) * GameManager.DifficultyMultiplier;

        target.TakeDamage(damage);
        _eventManager.OnCharacterTakeDamage?.Invoke(this, target, damage);

        if (_isSucking) _soundManager.PlaySound(SoundTypes.OnMosquitoSuck);

        if (_isDebug)
        {
            Debug.Log("Make " + damage + " damage. Time: " + Time.frameCount);
        }

        _attackTimer = AttackTime;
        _isSucking = true;

    }

    public void Move(Vector3 targetPos)
    {
        if (_isSucking || !gameObject.activeSelf) return;

        Vector3 currentPos = transform.position;

        transform.position += new Vector3
                                (
                                    targetPos.x - currentPos.x,
                                    targetPos.y - currentPos.y, 
                                    currentPos.z
                                ).normalized * Speed * GameManager.DifficultyMultiplier * Time.deltaTime;

        _soundManager.PlaySound(SoundTypes.OnMosquitoMove);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);

        _eventManager.OnEnemyClicked?.Invoke(this);
    }

    private void ReturnToPool()
    {
        Spawner.AddToPool(this);
    }
}
