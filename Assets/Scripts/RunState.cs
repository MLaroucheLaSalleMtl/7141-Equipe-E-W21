using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private static RunState instance;

    private RunState() { }

    public static RunState GetState()
    {
        if (instance == null)
        {
            instance = new RunState();
        }
        return instance;
    }

    public bool CanAttackEnemy()
    {
        return false;
    }

    public bool GoToBase()
    {
        return true;
    }

    public bool DefendBase()
    {
        return false;
    }
}
