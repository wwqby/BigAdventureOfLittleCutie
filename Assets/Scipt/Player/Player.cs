using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private PlayerHealth PlayerHealth;

    void Awake()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage){
        PlayerHealth.TakeDamage(damage);
    }
}
