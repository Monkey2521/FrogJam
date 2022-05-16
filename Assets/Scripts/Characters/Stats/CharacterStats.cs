using UnityEngine;

[System.Serializable]
public struct CharacterStats
{
    public float MaxHP;
    [HideInInspector] public float HP;

    public float Damage;

    public float Speed;
    public void Init ()
    {
        HP = MaxHP;
    }
}
