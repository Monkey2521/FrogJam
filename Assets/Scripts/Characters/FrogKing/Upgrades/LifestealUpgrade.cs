using UnityEngine;

[System.Serializable]
public class LifestealUpgrade : UpgradeItem
{
    [SerializeField][Range(0.01f, 0.1f)] private float _lifestealPercent = 0.05f;
    private float _lifestealMultiplier => _lifestealPercent * Level;

    public LifestealUpgrade (Upgrade upgrade, UpgradeUI upgradeUI)
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

        if (Level == 0) _eventManager.OnCharacterTakeDamage.AddListener(TakeLifesteal);
    }

    public override void RevealEffect()
    {
        base.RevealEffect();

        _eventManager.OnCharacterTakeDamage.RemoveListener(TakeLifesteal);
    }

    private void TakeLifesteal(IAttackable dealer, IDamageable target, float damage)
    {
        if (dealer is FrogKing)
        {
            _player.TakeDamage(-damage * _lifestealMultiplier);
        }
    }
}
