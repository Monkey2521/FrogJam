using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeItem
{
    [SerializeField] private Upgrade _upgrade;
    public Upgrade upgrade => _upgrade;

    private UpgradeUI _upgradeUI;

    private int _cost;
    public int Cost => _cost;

    private int _level;
    public int Level => _level;

    public bool IsMaxLevel => Level == _upgrade.MaxLevel;

    #region Constructors
    protected UpgradeItem() { }

    public UpgradeItem (Upgrade upgrade, UpgradeUI upgradeUI) : this(upgrade, 0, upgradeUI) { }

    public UpgradeItem(Upgrade upgrade, int level, UpgradeUI upgradeUI)
    {
        _upgrade = upgrade;
        _level = level;
        _upgradeUI = upgradeUI;

        _cost = _upgrade.DefaultCost.Count + _upgrade.AdditionalCostPerLevel * Level;
        
        SetText();
    }
    #endregion

    public void AddLevel()
    {
        _level++;

        _cost = _upgrade.DefaultCost.Count + _upgrade.AdditionalCostPerLevel * Level;

        SetText();
    }

    protected void SetText()
    {
        string buyText = ":\nbuy (" + Cost + ")";
        string upgradeText = ":\nLevel = " + Level + "\nupgrade (" + Cost + ")";
        string maxLevelText = ":\nMAX LEVEL";

        _upgradeUI.SetText(_upgrade.Name + (Level > 0 ? (IsMaxLevel ? maxLevelText : upgradeText) : buyText));
    }

    public virtual void TakeEffect()
    {
        Debug.Log("Take " + _upgrade.Name + " upgrade");
    }
}
