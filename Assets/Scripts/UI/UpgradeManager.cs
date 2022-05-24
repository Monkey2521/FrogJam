using System.Collections.Generic;
using UnityEngine;

public sealed class UpgradeManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private List<Upgrade> _upgrades = new List<Upgrade>();


    [SerializeField] private UpgradeUI _upgradePrefab;
    [SerializeField] private Transform _upgradeParent;
    private List<UpgradeUI> _upgradesUI = new List<UpgradeUI>();

    [SerializeField] SoundManager _soundManager;

    [SerializeField] FrogKing _player;
    private EventManager _eventManager;
    private Inventory _inventory;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnStartGame.AddListener(Restart);
        _eventManager.OnUpgradeUIClicked.AddListener(OnUpgradeClick);

        _inventory = Inventory.GetInventory();

        for (int i = 0; i < _upgrades.Count; i++)
        {
            UpgradeUI newUpgrade = Instantiate(_upgradePrefab, _upgradeParent);
            newUpgrade.Init(i);

            _upgradesUI.Add(newUpgrade);
        }
    }

    private void Restart()
    {
        for (int i = 0; i < _upgrades.Count; i++) {
            _upgrades[i].BehaviorScript?.RevealEffect();
            _upgrades[i].Init(_upgradesUI[i]);
        }
    }

    public void OnUpgradeClick(int index)
    {
        if (_isDebug)
            Debug.Log("Clicked on upgrade with index: " + index + ". Name: " + _upgrades[index].Name);

        ItemInventory requiredItems = new ItemInventory(_upgrades[index].DefaultCost.Item, _upgrades[index].BehaviorScript.Cost);
        
        if (!_upgrades[index].BehaviorScript.IsMaxLevel && _inventory.EnoughResources(requiredItems) && _inventory.Remove(requiredItems))
        {
            _upgrades[index].BehaviorScript.TakeEffect(_player);
            _upgrades[index].BehaviorScript.AddLevel();
        }
    }
}
