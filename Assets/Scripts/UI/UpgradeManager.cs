using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text _levelUpText;
    [SerializeField] private Upgrade _levelUpUpgrade;
    [SerializeField] private UpgradeItem _levelUpItem;

    [SerializeField] private Text _lifestealText;
    [SerializeField] private Upgrade _lifestealUpgrade;
    [SerializeField] private UpgradeItem _lifestealItem;

    [SerializeField] SoundManager _soundManager;

    [SerializeField] FrogKing _player;
    private EventManager _eventManager;
    private Inventory _inventory;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnStartGame.AddListener(Restart);

        _inventory = Inventory.GetInventory();
    }

    private void Restart()
    {
        _levelUpText.text = "Level = 0\nLevelUp:\nbuy(" + _levelUpUpgrade.DefaultCost.Count + ")";
        _levelUpItem = new UpgradeItem(_levelUpUpgrade, 0);

        _lifestealText.text = "Level = 0\nLifesteal:\nbuy(" + _lifestealUpgrade.DefaultCost.Count + ")";
        _lifestealItem = new UpgradeItem(_lifestealUpgrade, 0);
    }

    public void OnLevelUpClick()
    {
        _soundManager.PlaySound(SoundTypes.OnClickUI);

        ItemInventory requiredResources = new ItemInventory(_levelUpUpgrade.DefaultCost.Item, _levelUpItem.Cost);
        Debug.Log(requiredResources.Count);

        if (_levelUpItem.Level < _levelUpItem.upgrade.MaxLevel && _inventory.EnoughResources(requiredResources))
        {
            if (_player.GetUpgrade(_levelUpUpgrade))
            {
                if (_inventory.Remove(requiredResources.Item, requiredResources.Count))
                {
                    _levelUpItem.AddLevel();

                    _levelUpText.text = "Level = " + _levelUpItem.Level + 
                        "\nLevelUp:\nupgrade(" + _levelUpItem.Cost + ")";
                }
            }
        }
    }

    public void OnLifestealClick()
    {
        _soundManager.PlaySound(SoundTypes.OnClickUI);

        ItemInventory requiredResources = new ItemInventory(_lifestealUpgrade.DefaultCost.Item, _lifestealItem.Cost);

        if (_lifestealItem.Level < _lifestealItem.upgrade.MaxLevel && _inventory.EnoughResources(requiredResources))
        {
            if (_player.GetUpgrade(_lifestealUpgrade))
            {
                _inventory.Remove(requiredResources.Item, requiredResources.Count);

                _lifestealItem.AddLevel();

                _lifestealText.text = "Level = " + _lifestealItem.Level +
                    "\nLevelUp:\nupgrade(" + _lifestealItem.Cost + ")";
            }
        }
    }
}
