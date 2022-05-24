using System.Collections.Generic;
using UnityEngine;

public sealed class UpgradeManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private List<UpgradeItem> _upgrades = new List<UpgradeItem>();

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
            _upgrades[i] = new UpgradeItem(_upgrades[i].upgrade, _upgradesUI[i]);
        }
    }

    public void OnUpgradeClick(int index)
    {
        if (_isDebug)
            Debug.Log("Clicked on upgrade with index: " + index + ". Name: " + _upgrades[index].upgrade.Name);

        ItemInventory requiredItems = new ItemInventory(_upgrades[index].upgrade.DefaultCost.Item, _upgrades[index].Cost);
        
        if (!_upgrades[index].IsMaxLevel && _inventory.EnoughResources(requiredItems) && _inventory.Remove(requiredItems))
        {
            _upgrades[index].AddLevel();

        }
    }
}
