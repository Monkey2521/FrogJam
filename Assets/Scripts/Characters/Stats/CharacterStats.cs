using UnityEngine;

[System.Serializable]
public struct CharacterStats
{
    public float MaxHP;
    public float HP;

    public float Damage;
    public float AttackTime;

    public float Speed;
    public void Init ()
    {
        HP = MaxHP;
    }


}
