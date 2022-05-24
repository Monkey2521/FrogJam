using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaAttackUpgrade : UpgradeItem
{ 
    public MegaAttackUpgrade(Upgrade upgrade, UpgradeUI upgradeUI)
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
