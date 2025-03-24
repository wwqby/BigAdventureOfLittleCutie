using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MoblileJoyStick : MonoBehaviour
{


    [Header("Elements")]
    [SerializeField] private RectTransform joyStickOutline;
    [SerializeField] private RectTransform joyStickKnob;
    [Header("Settings")]
    private bool canControl;
    private Vector3 clickedPos;
    [SerializeField]private float moveFactor;
    [SerializeField]private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        HideJoyStick();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl){
            ControlJoystic();
        }
    }

    public void ClickedOnJoyStickZoneCallback(){
        joyStickOutline.position = Input.mousePosition;
        clickedPos = Input.mousePosition;
        ShowJoyStick();
    }


    private void HideJoyStick(){
        joyStickOutline.gameObject.SetActive(false);
        canControl = false;
    }

    private void ShowJoyStick(){
        joyStickOutline.gameObject.SetActive(true);
        canControl = true;
    }


    private void ControlJoystic(){
        Vector3 currentPos = Input.mousePosition;
        Vector3 direction = currentPos - clickedPos;
        float moveMagitude = direction.magnitude * moveFactor / Screen.width;
        moveMagitude = math.min(moveMagitude, joyStickOutline.rect.width/2);
        move = direction.normalized * moveMagitude;
        joyStickKnob.position = clickedPos + move;
        if ( Input.GetMouseButtonUp(0) ) {//0 左键 1 右键 2 中建
            HideJoyStick();
        }
    }

    public Vector3 GetMove(){
        return move;
    }
}
