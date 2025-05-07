using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    [Header("components")]
    [SerializeField] private Animator damageAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    private void ShowDamageText()
    {
        damageAnimator.Play("DamageShow");
    }
}
