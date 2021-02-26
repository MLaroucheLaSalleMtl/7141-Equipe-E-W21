using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private static AttackState instance = null;

    private AttackState() { }

    public static AttackState GetState()
    {
        if (instance == null) //v�rfie l'�tat actuel du pattern
        {
            instance = new AttackState(); //fait une instance du pattern
        }
        return instance;
    }

    public bool CanAttackEnemy()
    {
        return true;
    }

    public bool GoToBase()
    {
        return false;
    }

    public bool DefendBase()
    {
        return false;
    }
}
