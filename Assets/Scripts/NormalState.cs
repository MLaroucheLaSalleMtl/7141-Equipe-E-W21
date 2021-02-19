using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : IState
{
    private static NormalState instance = null;

    private NormalState() { }

    public static NormalState GetState()
    {
        if (instance == null) //v�rifie l'�tat actuel du pattern
        { 
            instance = new NormalState(); //fait une instance du pattern
        }
        return instance;
    }

    public bool CanAttackEnemy()
    {
        return false;
    }
}
