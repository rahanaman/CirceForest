using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _enemySprite;
    [SerializeField] private TextMeshProUGUI _enemyHpText;
    [SerializeField] private Image _enemyHpBar;
    [SerializeField] private GameObject _enemyDefence;
    [SerializeField] private Animator _enemyAnim;


    public void SetEnemyUI(EnemyData data, Sprite sprite)
    {
        _enemySprite.sprite = sprite;
        SetEnemyHp(data.EnemyHP, data.EnemyHP);
    }

    private void SetEnemyHp(int currentHP, int maxHP)
    {
        _enemyHpBar.fillAmount = (float)currentHP / maxHP;
        _enemyHpText.text = currentHP.ToString() + "/" + maxHP.ToString();
    }
}
