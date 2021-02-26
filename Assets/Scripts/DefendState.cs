using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : IState
{
    private static DefendState instance;

    private DefendState() { }

    public static DefendState GetState()
    {
        if (instance == null)
        {
            instance = new DefendState();
        }
        return instance;
    }

    public bool CanAttackEnemy()
    {
        return false;
    }

    public bool GoToBase()
    {
        return false;
    }

    public bool DefendBase()
    {
        return true;
    }
}
