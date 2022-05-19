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

    [SerializeField] private GameObject _crown;
    [SerializeField] private Animator _glasses;
    [SerializeField] private Vector3 _glassesDefaultPosition;
    [SerializeField][Range(1f, 5f)] private float _glassesMultiplier;
    [SerializeField] private int _scoreGoal;
    private bool _isCool = false;

    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnStartGame.AddListener(Restart);
    }

    private void Restart()
    {
        if (_isCool)
        {
            _stats.MaxHP /= _glassesMultiplier;
            _stats.Damage /= _glassesMultiplier;
        }

        _stats.Init();

        _glasses.gameObject.transform.position = _glassesDefaultPosition;
        _glasses.gameObject.SetActive(false);

        _eventManager.OnEnemyDeath.AddListener(TakeGlasses);
        _isCool = false;
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

    private void TakeGlasses()
    {
        if (ScoreCounter.Score >= _scoreGoal)
        {
            _glasses.gameObject.SetActive(true);
            
            _stats.Damage *= _glassesMultiplier;
            _stats.MaxHP *= _glassesMultiplier;

            float heal = HP - MaxHP;

            _stats.Init();

            _eventManager.OnCharacterTakeDamage?.Invoke(this, this, heal);
            _eventManager.OnEnemyDeath.RemoveListener(TakeGlasses);

            if (_isDebug) Debug.Log("COOL!");

            _isCool = true;
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}