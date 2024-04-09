using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_IDLE : StateBase
{
    public override void Enter(Boss entity)
    {
    }

    public override void Excute(Boss entity)
    {
        float randNum = Random.Range(0f, 1f);
        //Debug.Log(randNum);
        if(randNum > 0.5f)
        {
            entity.NextState(BossStates.Attack_1);
        }
        else
        {
            entity.NextState(BossStates.Attack_2);
        }
    }

    public override void Exit(Boss entity)
    {
    }
}
