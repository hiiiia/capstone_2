using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    public abstract void Enter(Boss entity);
    public abstract void Excute(Boss entity);
    public abstract void Exit(Boss entity);
}
