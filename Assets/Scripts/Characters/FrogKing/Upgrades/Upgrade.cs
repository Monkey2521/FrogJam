using UnityEngine;

[CreateAssetMenu(fileName = "New upgrade", menuName = "Scriptable objects/Upgrade")]
public class Upgrade : Item
{
    [SerializeField] private ItemInventory _defaultCost;
    public ItemInventory DefaultCost => _defaultCost;

    [SerializeField] private int _additionalCostPerLevel;
    public int AdditionalCostPerLevel => _additionalCostPerLevel;

    [SerializeField] private int _maxLevel;
    public int MaxLevel => _maxLevel;

    [SerializeField] private CharacterStats _additionalStats;
    public CharacterStats AdditionalStats => _additionalStats;
}
