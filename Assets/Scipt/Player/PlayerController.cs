using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerStatsListener
{

    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedPercent;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        rigid.velocity = joystick.GetMoveVector() * moveSpeed * Time.fixedDeltaTime;
    }

    public void OnPlayerStatsChanged(PlayerStatsManager playerStatsManager)
    {
        float baseSpeed = playerStatsManager.GetValue(Stats.MoveSpeed);
        float speedPercent = playerStatsManager.GetValue(Stats.MoveSpeedPercent) / 100f;
        moveSpeed = baseSpeed * (1 + speedPercent);
    }
}
