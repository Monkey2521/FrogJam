using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text _damageText;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        
    }

    public void SetDamage(float damage, Transform parent)
    {
        _damageText.text = ((int)damage).ToString();
    }
}
