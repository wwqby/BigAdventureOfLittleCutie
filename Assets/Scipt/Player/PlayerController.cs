using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]private Rigidbody2D rigid;
    [SerializeField]private MobileJoystick joystick;
    [SerializeField]private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
    private void FixedUpdate()
    {
        rigid.velocity = joystick.GetMoveVector() * moveSpeed * Time.fixedDeltaTime;
    }
}
