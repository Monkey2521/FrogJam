using UnityEngine;

[System.Serializable]
public class UpgradeItem
{
    [SerializeField] private Upgrade _upgrade;
    public Upgrade upgrade => _upgrade;

    private int _cost;
    public int Cost => _cost;

    [SerializeField] private int _level = 0;
    public int Level => _level;

    public UpgradeItem (Upgrade upgrade)
    {
        _upgrade = upgrade;
        _level = 1;

        _cost = _upgrade.DefaultCost.Count + _upgrade.AdditionalCostPerLevel * Level;
    }

    public UpgradeItem(Upgrade upgrade, int level)
    {
        _upgrade = upgrade;
        _level = level;

        _cost = _upgrade.DefaultCost.Count + _upgrade.AdditionalCostPerLevel * Level;
    }

    public void AddLevel()
    {
        _level++;

        _cost = _upgrade.DefaultCost.Count + _upgrade.AdditionalCostPerLevel * Level;
    }
}
