using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EffectsManager : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private DamageText DamageTextPrefab;
    [Header("Pooling")]
    [SerializeField] private ObjectPool<DamageText> DamageTextPool;

    void OnEnable()
    {
        Enemy.OnTakeDamage += ShowDamageTextOnPosition;
    }

    void OnDisable()
    {
        Enemy.OnTakeDamage -= ShowDamageTextOnPosition;
    }

    void Start()
    {
        DamageTextPool = new ObjectPool<DamageText>(CreateFucc,ActionOnGet,ActionOnRelease,ActionOnDestory);
    }

    #region Pooling
    private void ActionOnDestory(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }

    private void ActionOnRelease(DamageText damageText)
    {
         damageText.gameObject.SetActive(false);
    }

    private void ActionOnGet(DamageText damageText)
    {
        damageText.gameObject.SetActive(true);
    }

    private DamageText CreateFucc()
    {
        return Instantiate<DamageText>(DamageTextPrefab,transform);
    }

    #endregion

    private void ShowDamageTextOnPosition(int damage, Vector2 position)
    {
        DamageText instance = DamageTextPool.Get();
        instance.transform.position = position + UnityEngine.Random.insideUnitCircle * 2f;
        instance.ShowDamageText(damage);
        LeanTween.delayedCall(1.2f,()=>{
            DamageTextPool.Release(instance);
        });
    }
}
