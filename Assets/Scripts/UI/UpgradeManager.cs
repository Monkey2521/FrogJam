using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UpgradeManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text _levelUpText;
    public LevelUpUpgrade up1;
    
    [SerializeField] private Text _lifestealText;
    public LifestealUpgrade up2;

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
        _levelUpText.text = "Level = 0\nLevelUp:\nbuy(";

        _lifestealText.text = "Level = 0\nLifesteal:\nbuy(";
    }

   
}
