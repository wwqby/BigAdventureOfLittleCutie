using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;
    [Header("Data")]
    [SerializeField] private CharactorDataSO charactorData;
    [Header("Elements")]
    [SerializeField] private Dictionary<Stats, float> playerStats;
    [SerializeField] private Dictionary<Stats, float> appends = new Dictionary<Stats, float>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerStats = charactorData.BaseStats;
        foreach (KeyValuePair<Stats, float> item in playerStats)
        {
            appends[item.Key] = 0;
        }
    }

    void Start()
    {
        UpdatePlayerStats();
    }
    public void UpgradeStats(Stats stats, float value)
    {
        appends[stats] += value;
        UpdatePlayerStats();
    }

    public void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsListener> listeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPlayerStatsListener>();
        foreach (IPlayerStatsListener listener in listeners)
        {
            listener.OnPlayerStatsChanged(this);
        }
    }

    public float GetValue(Stats stats)
    {
        return playerStats[stats] + appends[stats];
    }
}

public interface IPlayerStatsListener
{
    void OnPlayerStatsChanged(PlayerStatsManager playerStatsManager);
}
