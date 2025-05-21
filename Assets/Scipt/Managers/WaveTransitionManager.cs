using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Button[] btnList;


    public void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.WAVETRANSITION)
        {
            ConfigUpgradeButtons();
        }
    }

    [NaughtyAttributes.Button]
    public void ConfigUpgradeButtons()
    {
        foreach (Button btn in btnList)
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(PlayerAttr)).Length);
            PlayerAttr attr = (PlayerAttr)Enum.GetValues(typeof(PlayerAttr)).GetValue(randomIndex);
            string attrString = attr.FormatEnumName();
            btn.GetComponentInChildren<TextMeshProUGUI>().text = attrString;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => Debug.Log("" + attrString + ""));
        }
    }
}
