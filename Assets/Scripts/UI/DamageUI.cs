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

    public void SetDamage(float damage, Transform parent)
    {
        _damageText.text = ((int)damage).ToString();
        _damageText.transform.position = parent.position;
    }

    public void ReturnToPull()
    {
        damageManager.ReturnToPull(this);
    }
}
