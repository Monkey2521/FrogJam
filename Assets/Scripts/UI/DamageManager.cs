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
    private List<DamageUI> _damagePull = new List<DamageUI>();
    
    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnCharacterTakeDamage.AddListener(ShowDamage);

        for (int i = 0; i < _maxDamageCount; i++)
        {
            _damagePull.Add((DamageUI)Instantiate(_damagePrefab, _damageParent));
            _damagePull[i].damageManager = this;
            _damagePull[i].gameObject.SetActive(false);
        }
    }

    private void ShowDamage(IAttackable dealer, IDamageable target, float damage)
    {
        DamageUI currentDamage = _damagePull?[0];
        _damagePull.Remove(currentDamage);
        currentDamage.gameObject.SetActive(true);
        currentDamage.SetDamage(damage, target.GetTransform());
    }

    public void ReturnToPull(DamageUI damageUI)
    {
        damageUI.gameObject.SetActive(false);
        _damagePull.Add(damageUI);
    }
}
