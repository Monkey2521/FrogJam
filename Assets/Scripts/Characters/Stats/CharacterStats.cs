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

    public static CharacterStats operator + (CharacterStats first, CharacterStats second)
    {
        CharacterStats result = new CharacterStats();

        result.MaxHP = first.MaxHP + second.MaxHP;
        result.Damage = first.Damage + second.Damage;
        result.AttackTime = first.AttackTime + second.AttackTime;
        result.Speed = first.Speed + second.Speed;

        return result;
    }

    public static CharacterStats operator - (CharacterStats first, CharacterStats second)
    {
        CharacterStats result = new CharacterStats();

        result.MaxHP = first.MaxHP - second.MaxHP;
        result.Damage = first.Damage - second.Damage;
        result.AttackTime = first.AttackTime - second.AttackTime;
        result.Speed = first.Speed - second.Speed;

        return result;
    }
}
