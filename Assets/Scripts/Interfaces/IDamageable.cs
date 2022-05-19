using UnityEngine;

public interface IDamageable
{
    public float HP { get; }
    public void TakeDamage (float damage);

    public Transform GetTransform();
}
