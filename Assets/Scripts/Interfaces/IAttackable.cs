public interface IAttackable
{
    public float Damage { get; }

    public float AttackTime { get; }

    public void MakeDamage(IDamageable target);
}
