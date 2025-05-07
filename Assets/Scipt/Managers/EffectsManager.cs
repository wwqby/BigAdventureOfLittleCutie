using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private DamageText DamageTextPrefab;
    // Start is called before the first frame update

    void OnEnable()
    {
        Enemy.OnTakeDamage += InitializeEffects;
    }

    void OnDisable()
    {
        Enemy.OnTakeDamage -= InitializeEffects;
    }

    [NaughtyAttributes.Button]
    private void InitializeEffects(int damage, Vector2 position)
    {
        DamageText instance = Instantiate<DamageText>(DamageTextPrefab, position + Random.insideUnitCircle * 2f, Quaternion.identity, transform);
        instance.ShowDamageText(damage);
    }
}
