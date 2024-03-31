using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player inst;
    public GameObject bullet;
    // 플레이어 스피드
    private float m_speed = 5f;
    public float speed { get { return this.m_speed; } private set { this.m_speed = value; } }

    // 키보드 상하 입력 , 상 키 : 1값, 하 키 : -1값
    private float m_vertical;
    public float vertical { get { return this.m_vertical; } private set { this.m_vertical = value; } }

    // 키보드 좌우 입력, 우 키 : +값, 좌 키 : -값
    private float m_horizontal;
    public float horizontal { get { return this.m_horizontal; } private set { this.m_horizontal = value; } }

    // 키보드 z 입력, z 입력 : 1 , 미입력 0;
    private float m_fire;
    public float fire { get { return this.m_fire; } private set { this.m_fire = value; } }

    // 플레이어 움직이는 방향 벡터
    private Vector3 m_moveVec;
    public Vector3 moveVec { get { return this.m_moveVec; } private set { this.m_moveVec = value; } }

    // --------
    Rigidbody rb;
    bool isGround = false;
    float _time = 0f;
    bool isLeft = false;
    private void Awake()
    {
        inst = this;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerInput();
        PlayerMove();
        Shoot();
    }

    void PlayerInput()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        fire = Input.GetAxisRaw("Fire1");

        if(horizontal > 0f)
        {
            isLeft = false;
            this.transform.localScale = new Vector3(5, 5, 1);
        }
        else if(horizontal < 0f)
        {
            isLeft = true;
            this.transform.localScale = new Vector3(-5, 5, 1);
        }

        moveVec = new Vector3(horizontal, 0f, 0f);
        if (moveVec.magnitude > 1f) moveVec.Normalize();
    }

    void PlayerMove()
    {
        //점프 부분
        if (isGround && vertical > 0f)
        {
            isGround = false;
            rb.AddForce(Vector3.up * vertical* 1.5f *speed , ForceMode.Impulse);
        }
        else if(!isGround && vertical < 0f)
        {
            isGround = true;
            rb.AddForce(Vector3.up * vertical * 2f * speed, ForceMode.Impulse);
        }

        transform.position += moveVec * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    void Shoot()
    {
        if(fire > 0)
        {
            _time += Time.deltaTime;
            if(_time > 0.125f)
            {
                _time = 0f;
                var bl = Instantiate(bullet);
                bl.transform.position = transform.position;
                bl.GetComponent<bullet>().Shoot(isLeft);
            }
            
        }
    }

}
