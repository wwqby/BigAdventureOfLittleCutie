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
    public void ShowEffectText(string text, Color color)
    {
        damageText.text = text;
        damageText.color = color;
        damageAnimator.Play("EffectTextShow");
    }

    public void ShowEffectText(string text)
    {
        ShowEffectText(text, Color.white);
    }
}
