using UnityEngine;

public abstract class UpgradeItem
{
    protected Upgrade _upgrade;
    public Upgrade upgrade => _upgrade;

    protected UpgradeUI _upgradeUI;

    protected int _cost;
    public int Cost => _cost;

    protected int _level;
    public int Level => _level;

    public bool IsMaxLevel => Level == _upgrade.MaxLevel;

    protected EventManager _eventManager;
    protected FrogKing _player;

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

    public virtual void TakeEffect(FrogKing player)
    {
        Debug.Log("Take " + _upgrade.Name + " upgrade");

        if (_player == null) _player = player;
    }

    public virtual void RevealEffect()
    {
        Debug.Log("Reveal " + _upgrade.Name + " upgrade");
    }
}

public enum UpgradeTypes
{
    LevelUp,
    Lifesteal,
    Attack
};
