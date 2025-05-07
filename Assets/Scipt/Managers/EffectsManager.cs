using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private DamageText DamageTextPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    private void InitializeEffects()
    {
        Vector3 position = Random.insideUnitCircle * 3f;
        DamageText instance = Instantiate<DamageText>(DamageTextPrefab, position,Quaternion.identity,transform);
        instance.ShowDamageText();
    }
}
