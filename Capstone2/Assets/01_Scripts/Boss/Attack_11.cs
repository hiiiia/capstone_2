using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_11 : StateBase
{
    public float wait_time = 1f;

    public GameObject[] AttackAreas = new GameObject[2];
    public GameObject[] Attack = new GameObject[2];

    bool isAttackEnd = false;
    float _time = 0f;
    public override void Enter(Boss entity)
    {
        isAttackEnd = false;
        _time = 0f;
        StartCoroutine(attack());
    }

    public override void Excute(Boss entity)
    {
        if (!isAttackEnd) return;

        _time += Time.deltaTime;
        if (_time >= wait_time)
        {
            entity.NextState(BossStates.idle);
        }
    }

    public override void Exit(Boss entity)
    {
        isAttackEnd = false;
        _time = 0f;
    }

    IEnumerator attack()
    {
        Debug.Log("11자 공격 표시--");
        
        foreach(GameObject obj in AttackAreas)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        foreach (GameObject obj in AttackAreas)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in Attack)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        foreach (GameObject obj in Attack)
        {
            obj.SetActive(false);
        }
        Debug.Log("11자 공격 완료");
        isAttackEnd = true;
    }
}
