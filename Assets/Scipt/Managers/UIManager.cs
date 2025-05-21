using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject weaponSelectionPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject stageComnpletePanel;

    [SerializeField] private List<GameObject> panelList;


    void Awake()
    {
        panelList = new List<GameObject>
        {
            menuPanel,
            weaponSelectionPanel,
            gamePanel,
            waveTransitionPanel,
            shopPanel,
            gameoverPanel,
            stageComnpletePanel,
        };
    }

    public void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;
            case GameState.WEAPON_SELECTION:
                ShowPanel(weaponSelectionPanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;
            case GameState.WAVETRANSITION:
                ShowPanel(waveTransitionPanel);
                break;
            case GameState.SHOP:
                ShowPanel(shopPanel);
                break;
            case GameState.GAMEOVER:
                ShowPanel(gameoverPanel);
                break;
            case GameState.STAGE_COMPLETE:
                ShowPanel(stageComnpletePanel);
                break;
        }
    }

    private void ShowPanel(GameObject gamePanel, bool hidePreviousPanel = true)
    {
        if (hidePreviousPanel)
        {
            foreach (GameObject panel in panelList)
            {
                panel.SetActive(gamePanel == panel);
            }
        }
        else
        {
            gamePanel.SetActive(true);
        }
    }



}
