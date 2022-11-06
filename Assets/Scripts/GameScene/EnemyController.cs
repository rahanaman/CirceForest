using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyManager _enemyManager = new EnemyManager();
    [SerializeField] GameObject _efx;

    public void SetEnemy(EnemyData data)
    {
        _enemyManager.SetEnemyData(data);
    }

    private void OnMouseEnter()
    {
        Debug.Log("sss");
        if (!GameManager.instance.IsCardPanel)
        {
            _efx.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.instance.IsCardPanel)
        {
            _efx.SetActive(false);
        }
    }
}
