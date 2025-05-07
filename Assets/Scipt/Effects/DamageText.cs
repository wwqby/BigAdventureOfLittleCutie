using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    [Header("components")]
    [SerializeField] private Animator damageAnimator;
    [SerializeField] private TextMeshPro damageText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    public void ShowDamageText()
    {
        damageText.text = Random.Range(100, 1000).ToString();
        damageAnimator.Play("DamageShow");
    }
}
