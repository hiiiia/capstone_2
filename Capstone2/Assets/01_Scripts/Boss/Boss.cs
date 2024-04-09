using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStates
{
    idle,
    Attack_1,
    Attack_2,
    Attack_3,
    Attack_4,
    Attack_5
}

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

    public StateBase[] AttackStates = new StateBase[6];

    StateBase currentState;

    private void Awake()
    {
        inst = this;
        LoadAttackStates();
    }

    private void FixedUpdate()
    {
        if(currentState != null)
        {
            currentState.Excute(this);
        }
    }

    public void NextState(BossStates state)
    {
        if(currentState != null)
        {
            currentState.Exit(this);
        }

        currentState = AttackStates[(int)state];
        currentState.Enter(this);
    }

    //공격 상태 매핑
    void LoadAttackStates()
    {
        for(int i = 0; i< transform.childCount; i++)
        {
            StateBase np = transform.GetChild(i).GetComponent<StateBase>();
            if( np != null)
            {
                AttackStates[i] = np;
            }
        }

        currentState = AttackStates[(int)BossStates.idle];
        currentState.Enter(this);

    }

    //데미지 받는 함수
    public void GetDamage(int damage)
    {
        if( hp - damage <= 0) 
        {
            OnDie();
            return;
        }
        hp -= damage;
    }

    // 충돌시 발생하는 함수
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
