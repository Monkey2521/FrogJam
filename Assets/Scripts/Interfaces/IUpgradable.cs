using System.Collections.Generic;
public interface IUpgradable 
{
    public List<UpgradeItem> Upgrades { get; }

    public bool GetUpgrade(Upgrade upgrade);
}
