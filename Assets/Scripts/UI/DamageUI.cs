using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text _damageText;
    [SerializeField] private Animator _animator;

    [HideInInspector] public DamageManager damageManager;

    public void SetDamage(float damage)
    {
        _damageText.text = ((int)damage).ToString();
    }

    public void ReturnToPool()
    {
        damageManager.ReturnToPool(this);
    }
}
