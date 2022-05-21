using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New upgrade", menuName = "Scriptable objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private string _description;
    public string Description => _description;

    [SerializeField] private ItemInventory _defaultCost;
    public ItemInventory DefaultCost => _defaultCost;

    [SerializeField] private int _additionalCostPerLevel;
    public int AdditionalCostPerLevel => _additionalCostPerLevel;

    [SerializeField] private int _maxLevel;
    public int MaxLevel => _maxLevel;

    [SerializeField] private Image _icon;
    public Image Icon => _icon;
}
