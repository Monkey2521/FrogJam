using UnityEngine;

[CreateAssetMenu(fileName = "New upgrade", menuName = "Scriptable objects/Upgrade")]
public class Upgrade : Item
{
    [SerializeField] protected UpgradeTypes _type;

    [SerializeField] private ItemInventory _defaultCost;
    public ItemInventory DefaultCost => _defaultCost;

    [SerializeField] private int _additionalCostPerLevel;
    public int AdditionalCostPerLevel => _additionalCostPerLevel;

    [SerializeField] private int _maxLevel;
    public int MaxLevel => _maxLevel;

    [SerializeField] private CharacterStats _additionalStats;
    public CharacterStats AdditionalStats => _additionalStats;

    [HideInInspector] public UpgradeItem BehaviorScript;

    public void Init(UpgradeUI upgradeUI)
    {
        switch (_type)
        {
            case UpgradeTypes.Attack:
                BehaviorScript = new MegaAttackUpgrade(this, upgradeUI);
                break;
            case UpgradeTypes.LevelUp:
                BehaviorScript = new LevelUpUpgrade(this, upgradeUI);
                break;
            case UpgradeTypes.Lifesteal:
                BehaviorScript = new LifestealUpgrade(this, upgradeUI);
                break;
            default:
                Debug.Log("???");
                break;
        }
    }
}
