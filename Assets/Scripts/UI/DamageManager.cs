using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private int _maxDamageCount;
    [SerializeField] private DamageUI _damagePrefab;
    [SerializeField] private Transform _damageParent;
    private List<DamageUI> _damagePool = new List<DamageUI>();
    private List<DamageUI> _activeDamage = new List<DamageUI>();
    
    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnCharacterTakeDamage.AddListener(ShowDamage);
        _eventManager.OnPlayerDeath.AddListener(ReturnAllToPool);

        for (int i = 0; i < _maxDamageCount; i++)
        {
            _damagePool.Add((DamageUI)Instantiate(_damagePrefab, _damageParent));
            _damagePool[i].damageManager = this;
            _damagePool[i].gameObject.SetActive(false);
        }
    }

    private void ShowDamage(IAttackable dealer, IDamageable target, float damage)
    {
        DamageUI currentDamage = _damagePool?[0];
        _damagePool.Remove(currentDamage);

        currentDamage.gameObject.SetActive(true);
        currentDamage.SetDamage(damage, target.GetTransform());
        _activeDamage.Add(currentDamage);
    }

    private void ReturnAllToPool()
    {
        foreach(DamageUI damageUI in _activeDamage)
        {
            _damagePool.Add(damageUI);
        }

        _activeDamage.Clear();
    }

    public void ReturnToPool(DamageUI damageUI)
    {
        damageUI.gameObject.SetActive(false);
        _activeDamage.Remove(damageUI);
        _damagePool.Add(damageUI);
    }
}
