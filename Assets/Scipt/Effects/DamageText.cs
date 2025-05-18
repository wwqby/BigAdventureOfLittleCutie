using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    [Header("components")]
    [SerializeField] private Animator damageAnimator;
    [SerializeField] private TextMeshPro damageText;
    

    [NaughtyAttributes.Button]
    public void ShowDamageText(int damage,bool isCritical)
    {
        damageText.text = damage.ToString();
        damageText.color = isCritical ? Color.yellow : Color.white;
        damageAnimator.Play("DamageShow");
    }
}
