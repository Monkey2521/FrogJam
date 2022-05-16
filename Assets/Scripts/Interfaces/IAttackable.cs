public interface IAttackable
{
    public float Damage { get; }

    public void MakeDamage(IDamageable target);
}
