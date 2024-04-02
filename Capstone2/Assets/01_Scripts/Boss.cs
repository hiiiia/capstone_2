using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss inst; 

    private int m_hp = 100;
    public int hp {
        get { return m_hp; } 
        private set 
        { 

            m_hp = value; 
        } 
    }

    private void Awake()
    {
        inst = this;
    }

    public void GetDamage(int damage)
    {
        if( hp - damage <= 0) 
        {
            OnDie();
            return;
        }
        hp -= damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            
            GetDamage(1);
            collision.gameObject.GetComponent<bullet>().DeleteObj();
        }
    }

    void OnDie()
    {
        Debug.Log("GameOver");
    }
}
