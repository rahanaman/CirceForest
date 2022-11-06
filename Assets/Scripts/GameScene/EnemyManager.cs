using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager 
{ 
    private int _maxHP;
    public int MaxHP
    {
        get { return _maxHP; }
    }
    private int _currentHP;
    public int CurrentHP
    {
        set { _currentHP = value; }
    }
    private int _enemyId;

    public int EnemyId
    {
        get { return _enemyId; }
    }
    public void SetEnemyData(EnemyData data)
    {
        _currentHP = data.EnemyHP;
        _maxHP = data.EnemyHP;
        _enemyId = data.EnemyId;
    }
}
