using System.Collections.Generic;
using UnityEngine;

public sealed class Inventory : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header ("Settings")]
    [SerializeField][Range (1, 100)] 
    private int _inventoryCapacity = 1; 
    
    [SerializeField] private List<ItemInventory> _items = new List<ItemInventory>();
    
    private static Inventory _instance;
    private EventManager _eventManager;

    #region Getting inventory instance
    private void Awake ()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_isDebug) { Debug.Log("Inventory already created!"); }
    }

    public static Inventory GetInventory() => _instance;
    #endregion

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
    }

    #region Operations
    public bool Add (ItemInventory item)
    {
        if (InInventory(item.Item, out int index))
        {
            _items[index].AddCount(item.Count);

            if (_isDebug) { Debug.Log("Add count to '" + _items[index].Item.Name + "'"); }

            _eventManager.OnInventoryUpdate?.Invoke();
            return true;
        }

        else if (_items.Count < _inventoryCapacity)
        {
            ItemInventory newItem = new ItemInventory(item); 
            _items.Add(newItem);

            if (_isDebug) { Debug.Log("Add '" + _items[index].Item.Name + "' to inventory."); }

            _eventManager.OnInventoryUpdate?.Invoke();
            return true;
        }

        else
        {
            if (_isDebug) { Debug.Log("Inventory is full!"); }

            _eventManager.OnInventoryUpdate?.Invoke();
            return false;
        }

    }

    public bool Remove(ItemInventory item, int count = 1)
    {
        if (InInventory (item.Item, out int index))
        {
            if (_items[index].RemoveCount(count))
            {
                if (_items[index].Count == 0) { _items.RemoveAt(index); }

                return true;
            }
            else
            {
                if (_isDebug) { Debug.Log("Not enough '" + _items[index].Item.Name + "' for remove!"); }

                return false;
            }
        }
        else
        {
            if (_isDebug) { Debug.Log("'" + _items[index].Item.Name + "' not found!"); }

            return false;
        }

    }
    #endregion

    private bool InInventory (Item item, out int index)
    {
        index = 0;

        foreach (ItemInventory itemInventory in _items)
        {
            if (itemInventory.Item.Equals (item))
            {
                if (_isDebug) { Debug.Log("Find index of '" + itemInventory.Item.Name + "': " + index); }

                return true;
            }

            index++;
        }

        return false;
    }

    public bool EnoughResources(List<ItemInventory> requireItems)
    {
        foreach (ItemInventory requireItem in requireItems)
        {
            if (InInventory(requireItem.Item, out int index))
            {
                if (_items[index].Count < requireItem.Count)
                {
                    if (_isDebug) Debug.Log("Not enough resources! "); 
                    return false;
                }
            }
        }

        return true;
    }
}
