using UnityEngine;

[System.Serializable]
public struct CharacterStats
{
    public float MaxHP;
    public float HP;

    public float Damage;
    public float AttackTime;

    public float Speed;
    public void Init()
    {
        HP = MaxHP;
    }

    public void ReInit()
    {
        MaxHP = default;
        HP = default;
        Damage = default;
        AttackTime = default;
        Speed = default;
    }

    public static CharacterStats operator + (CharacterStats first, CharacterStats second)
    {
        CharacterStats result = new CharacterStats();

        result.MaxHP = first.MaxHP + second.MaxHP;
        result.HP = first.HP + second.HP;
        result.Damage = first.Damage + second.Damage;
        result.AttackTime = first.AttackTime + second.AttackTime;
        result.Speed = first.Speed + second.Speed;

        return result;
    }

    public static CharacterStats operator - (CharacterStats first, CharacterStats second)
    {
        CharacterStats result = new CharacterStats();

        result.MaxHP = first.MaxHP - second.MaxHP;
        result.HP = first.HP - second.HP;
        result.Damage = first.Damage - second.Damage;
        result.AttackTime = first.AttackTime - second.AttackTime;
        result.Speed = first.Speed - second.Speed;

        return result;
    }
}
