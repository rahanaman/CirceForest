using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanelController : MonoBehaviour
{
    [SerializeField] private Button _종료Button;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _panelRectTransform;
    void Awake()
    {
        EventManager.SetCardList += SetCardList;
        _종료Button.onClick.AddListener(OnClick종료Button);
    }

    private void OnDestroy()
    {
        EventManager.SetCardList -= SetCardList;
    }


    // Update is called once per frame
    private void OnClick종료Button()
    {
        _panel.SetActive(false);
    }
    public void SetCardList(List<int> value)
    {
        int n = value.Count;
        int k = Mathf.CeilToInt(n / 5f);
        _panelRectTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 600 * k);
        Vector3 pos = new Vector3(-11.5f, -5 + (4 * k), 0);
        for (int i = 0; i < n; i++)
        {
            var card = Instantiate(DataLoader.CardPref[value[i]], _panelRectTransform.transform);
            card.transform.position = pos;
            card.SetActive(true);
            
            if (pos.x == 11.5f)
            {
                pos.x = -11.5f;
                pos.y -= 8;
            }
            else
            {
                pos.x += 5.75f;
            }
        }
    }

}
