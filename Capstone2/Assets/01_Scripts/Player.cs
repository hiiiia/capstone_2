using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player inst;
    public GameObject bullet;
    // �÷��̾� ���ǵ�
    private float m_speed = 5f;
    public float speed { get { return this.m_speed; } set { this.m_speed = value; } }

    // Ű���� ���� �Է� , �� Ű : 1��, �� Ű : -1��
    private float m_vertical;
    public float vertical { get { return this.m_vertical; } set { this.m_vertical = value; } }

    // Ű���� �¿� �Է�, �� Ű : +��, �� Ű : -��
    private float m_horizontal;
    public float horizontal { get { return this.m_horizontal; } set { this.m_horizontal = value; } }

    // Ű���� z �Է�, z �Է� : 1 , ���Է� 0;
    private float m_fire;
    public float fire { get { return this.m_fire; } set { this.m_fire = value; } }

    // �÷��̾� �����̴� ���� ����
    private Vector3 m_moveVec;
    public Vector3 moveVec { get { return this.m_moveVec; } set { this.m_moveVec = value; } }

    // --------
    Rigidbody2D rb;
    bool isGround = false;
    float _time = 0f;
    bool isLeft = false;
    private void Awake()
    {
        inst = this;
        rb = GetComponent<Rigidbody2D>();
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
        //���� �κ�
        if (isGround && vertical > 0f)
        {
            isGround = false;
            
            rb.AddForce(Vector2.up * vertical * 1.5f * speed, ForceMode2D.Impulse);
        }
        else if(!isGround && vertical < 0f)
        {
            isGround = true; 
            rb.AddForce(Vector2.up * vertical * 2.5f * speed, ForceMode2D.Impulse);
        }

        transform.position += moveVec * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
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
