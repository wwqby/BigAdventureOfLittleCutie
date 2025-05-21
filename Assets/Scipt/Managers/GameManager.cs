using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.MENU);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WaveCompleteCallback()
    {
        if (PlayerLevel.instance.HasLevelUpInWave())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> listeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();
        foreach (IGameStateListener listener in listeners)
        {
            listener.OnGameStateChanged(gameState);
        }
    }


    public void StateWarponSelection() => SetGameState(GameState.WEAPON_SELECTION);
    public void StateStartGame() => SetGameState(GameState.GAME);
    public void StateOpenShop() => SetGameState(GameState.SHOP);
    

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}


public interface IGameStateListener
{
    void OnGameStateChanged(GameState gameState);
}