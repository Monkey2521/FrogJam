using UnityEngine;

[System.Serializable]
public class LevelUpUpgrade : UpgradeItem
{
    public LevelUpUpgrade (Upgrade upgrade, UpgradeUI upgradeUI)
    {
        _upgrade = upgrade;
        _level = default;
        _upgradeUI = upgradeUI;

        _cost = _upgrade.DefaultCost.Count;

        SetText();

        _eventManager = EventManager.GetEventManager();
    }
    public override void TakeEffect(FrogKing player)
    {
        base.TakeEffect(player);

        _player.GetStatsUpgrade(upgrade.AdditionalStats);
    }
}
