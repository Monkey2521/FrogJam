using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Events")]
    public UnityEvent OnStartGame;
    public UnityEvent OnInventoryUpdate;
    public UnityEvent<IAttackable, IDamageable, float> OnCharacterTakeDamage;
    public UnityEvent OnEnemyDeath;
    public UnityEvent OnPlayerDeath;

    private static EventManager _instance;

    private void Awake ()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else if (_isDebug)
        {
            Debug.Log("EventManager already created!");
        }
    }

    public static EventManager GetEventManager() => _instance;
}
