using UnityEngine;

[System.Serializable]
public class ItemInventory
{
    [SerializeField] private Item _item;
    public Item Item => _item;

    [SerializeField] private int _count;
    public int Count => _count;

    public ItemInventory(Item item, int count)
    {
        _item = item;
        _count = count;
    }

    public ItemInventory(ItemInventory itemInventory) {
        _item = itemInventory.Item;
        _count = itemInventory.Count;
    }

    public bool AddCount (int count)
    {
        _count += count;
        return true;
    }

    public bool RemoveCount (int count)
    {
        if (_count < count)
        {
            return false;
        }

        _count -= count;
        return true;
    }
}
