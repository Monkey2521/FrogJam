using System.Collections.Generic;
using UnityEngine;

public class FrogKing : ClickableObject, IDamageable, IAttackable, IUpgradable
{
    [SerializeField] private Animator _animator;

    [SerializeField] private CharacterStats _stats;
    public float MaxHP => _stats.MaxHP;
    public float HP => _stats.HP;
    public float Damage => _stats.Damage;
    public float AttackTime => _stats.AttackTime;
    private float _attackTimer = 0;

    private List<UpgradeItem> _upgrades = new List<UpgradeItem>();
    public List<UpgradeItem> Upgrades => _upgrades;

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
        _eventManager.OnEnemyClicked.AddListener(MakeDamage);
    }

    private void Restart()
    {
        if (_isCool)
        {
            _stats.MaxHP /= _glassesMultiplier;
            _stats.Damage /= _glassesMultiplier;
        }

        _stats.Init();
        _eventManager.OnPlayerChangeHP?.Invoke();

        _glasses.gameObject.transform.position = _glassesDefaultPosition;
        _glasses.gameObject.SetActive(false);

        _eventManager.OnEnemyDeath.AddListener(TakeGlasses);
        _isCool = false;

        _upgrades.Clear();
    }

    public void AttackControl ()
    {
        if (_attackTimer > 0) _attackTimer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        _stats.HP -= damage;

        _soundManager.PlaySound(SoundTypes.OnFrogTakeDamage);
        _eventManager.OnPlayerChangeHP?.Invoke();

        if (_stats.HP <= 0)
        {
            _eventManager.OnPlayerDeath?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void MakeDamage(IDamageable target)
    {
        if (_attackTimer <= 0)
        {
            target.TakeDamage(Damage);
            _eventManager.OnCharacterTakeDamage?.Invoke(this, target, Damage);
            _attackTimer = AttackTime;
        }
        else if (_isDebug) Debug.Log("Not time yet");
    }

    private void TakeGlasses()
    {
        if (ScoreCounter.Score >= _scoreGoal)
        {
            _glasses.gameObject.SetActive(true);
            
            _stats.Damage *= _glassesMultiplier;
            _stats.MaxHP *= _glassesMultiplier;

            _stats.Init();
            _eventManager.OnPlayerChangeHP?.Invoke();

            _eventManager.OnEnemyDeath.RemoveListener(TakeGlasses);

            if (_isDebug) Debug.Log("COOL!");

            _isCool = true;

            _soundManager.PlaySound(SoundTypes.OnFrogGetGlasses);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool GetUpgrade(Upgrade upgrade)
    {
        if (HaveUpgrade(upgrade, out int index))
            _upgrades[index].AddLevel();
        else
            _upgrades.Add(new UpgradeItem(upgrade));

        if (_isDebug) Debug.Log("Get " + upgrade.Name + " upgrade");

        return true;
    }

    private bool HaveUpgrade(Upgrade upgrade, out int index)
    {
        index = 0;
        
        foreach (UpgradeItem item in _upgrades)
        {
            if (item.upgrade == upgrade)
                return true;
            
            index++;
        }

        return false;
    }
}