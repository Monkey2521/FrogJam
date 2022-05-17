using UnityEngine;

public interface IMoveable
{
   public float Speed { get; }

    public void Move(Vector3 targetPos);
}
