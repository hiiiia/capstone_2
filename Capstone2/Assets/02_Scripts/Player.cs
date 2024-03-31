using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 5;
    Vector3 moveVec = Vector3.zero;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerInput();
        PlayerMove();
    }

    void PlayerInput()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        moveVec = new Vector3(h, v, 0);
    }

    void PlayerMove()
    {
        if (moveVec == Vector3.zero) return;

        if (moveVec.magnitude > 1f) moveVec.Normalize();
        
        transform.position += moveVec * speed * Time.deltaTime;

    }

}
