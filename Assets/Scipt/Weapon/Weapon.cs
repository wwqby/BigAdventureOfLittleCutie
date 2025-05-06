using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField]private Transform hitDetection;
    [SerializeField]private float hitDetectionRadius;
    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Collider2D[] enemies;
    [SerializeField] private float aniLerp;

    // Update is called once per frame
    void Update()
    {
        AutoAim();
        Attack();
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitDetection.position, hitDetectionRadius, targetMask);
        foreach (Collider2D hitEnem in hitEnemies){
            Destroy(hitEnem.gameObject);
        }
    }

    private void AutoAim()
    {
        Vector3 targetVector = Vector3.up;
        Enemy enemy = GetClosetEnemy();
        if (enemy != null)
        {
            targetVector = (enemy.transform.position - transform.position).normalized;

        }
        transform.up = Vector3.Lerp(transform.up, targetVector, aniLerp);
    }


    private Enemy GetClosetEnemy()
    {
        Enemy closetEnemy = null;
        enemies = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
        if (enemies.Length == 0)
        {
            return null;
        }
        float minDistance = radius;
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                closetEnemy = enemy;
            }
        }
        return closetEnemy;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetection.position, hitDetectionRadius);
    }
}
