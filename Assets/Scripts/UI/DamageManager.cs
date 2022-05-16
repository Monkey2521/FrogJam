using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]

    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnCharacterTakeDamage.AddListener(ShowDamage);
    }

    private void ShowDamage(IAttackable dealer, IDamageable target, float damage)
    {

    }
}
