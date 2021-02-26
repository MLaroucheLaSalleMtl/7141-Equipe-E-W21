using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    bool CanAttackEnemy();
    bool GoToBase();
    bool DefendBase();
}

