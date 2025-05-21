using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private PlayerHealth PlayerHealth;
    [SerializeField] private Transform centerPoint;

    [Header("settings")]
    public static Player instance;

    void Awake()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        PlayerHealth.TakeDamage(damage);
    }

    public Vector2 GetCenterPoint()
    {
        return centerPoint.position;
    }
}
