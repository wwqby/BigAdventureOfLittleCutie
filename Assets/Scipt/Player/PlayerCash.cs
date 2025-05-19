using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCash : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI cashText;

    [Header("Settings")]
    [SerializeField] private int cashCount;

    void OnEnable()
    {
        Cash.OnCollected += CashGain;
        UpdateCash();
    }

    void OnDisable()
    {
        Cash.OnCollected -= CashGain;
    }


    private void CashGain(Cash cash)
    {
        cashCount++;
        UpdateCash();
    }
    private void UpdateCash()
    {
        cashText.text = cashCount.ToString();
    }
}
