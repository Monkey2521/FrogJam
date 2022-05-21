using UnityEngine;
using UnityEngine.UI;

public class GoldCounter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text _goldText;
    [SerializeField] private Item _goldItem;

    private EventManager _eventManager;
    private Inventory _inventory;


    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnInventoryUpdate.AddListener(UpdateUI);
        _eventManager.OnStartGame.AddListener(Restart);

        _inventory = Inventory.GetInventory();
    }

    private void Restart()
    {
        _goldText.text = "Gold:\n0";
    }

    private void UpdateUI()
    {
        _goldText.text = "Gold:\n" + _inventory.Items[0].Count;
    }
}
