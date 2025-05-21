using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BoxCollider2D mapBound;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Wave")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private float timer;
    [SerializeField] private int currentWave;
    [SerializeField] private int currentSegment;
    [SerializeField] private List<float> segmentTimer;
    [SerializeField] private bool isTimerOn;


    void OnEnable()
    {
        waveText.text = "Wave " + (currentWave + 1);
        timerText.text = (int)(waves[currentWave].waveDuration - timer) + "";
    }

    void OnDisable()
    {
        waveText.text = "";
        timerText.text = "";
    }

    void Start()
    {
        segmentTimer = new List<float>();
        for (int i = 0; i <= currentSegment; i++)
        {
            segmentTimer.Add(0f);
        }
        isTimerOn = true;
    }


    void Update()
    {

        if (!isTimerOn)
        {
            waveText.text = "Spawen Pause";
            return;
        }
        if (currentWave >= waves.Length)
        {
            // Debug.Log("Spawen completed");
            waveText.text = "Spawen completed";
            timerText.text = "";
            return;
        }

        timer += Time.deltaTime;
        timerText.text = (int)(waves[currentWave].waveDuration - timer) + "";
        //检查wave
        Wave wave = waves[currentWave];
        if (IsWaveCompleted(wave))
        {
            StartNextWave();
            return;
        }

        //检查segment
        WaveSegment waveSegment = wave.waveSegments[currentSegment];
        float duration = wave.waveDuration;
        float from = waveSegment.timeFromTo.x * duration;
        float to = waveSegment.timeFromTo.y * duration;
        if (IsSegmentsCompleted(timer, to))
        {
            StartNextSegment();
            return;
        }

        //生成对象
        SapwanRandomGameObject(waveSegment, from);
    }

    private void SapwanRandomGameObject(WaveSegment waveSegment, float from)
    {
        float spawnDelay = 1f / waveSegment.spawnRate;
        if (timer >= segmentTimer[currentSegment] + from)
        {
            SpawnInTileMap(waveSegment.spawnObject);
            segmentTimer[currentSegment] += spawnDelay;
            // Debug.Log("Spawning " + waveSegment.spawnObject.name + " at wave " + currentWave + " at segment " + currentSegment);
        }
    }

    private void StartNextSegment()
    {
        currentSegment++;
        segmentTimer.Add(0f);
        // Debug.Log("Timer" + timer + ">=" + " to:" + to);
        // Debug.Log("Segment completed");
    }

    private bool IsSegmentsCompleted(float timer, float to)
    {
        return timer >= to;
    }

    private void StartNextWave()
    {
        currentWave++;
        currentSegment = 0;
        segmentTimer.Clear();
        segmentTimer.Add(0f);
        timer = 0;
        // Debug.Log("Wave completed");
        waveText.text = "Wave " + (currentWave + 1);
    }

    private bool IsWaveCompleted(Wave wave)
    {
        return currentSegment >= wave.waveSegments.Count && timer >= wave.waveDuration;
    }

    private void SpawnInTileMap(GameObject spawnObject)
    {
        Vector2 position = player.GetCenterPoint() + UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(5, 10);
        position.x = Mathf.Clamp(position.x, mapBound.bounds.min.x, mapBound.bounds.max.x);
        position.y = Mathf.Clamp(position.y, mapBound.bounds.min.y, mapBound.bounds.max.y);
        GameObject instance = Instantiate(spawnObject, transform.position, Quaternion.identity);//TODO pooling
        instance.transform.position = position;

    }
}

[System.Serializable]
public struct Wave
{
    public string waveName;
    public float waveDuration;
    public List<WaveSegment> waveSegments;
}


[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 1)] public Vector2 timeFromTo;
    public float spawnRate;
    public GameObject spawnObject;
}
