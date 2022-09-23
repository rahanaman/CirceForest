using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartController : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private Button _종료Button;
    [SerializeField] private List<Button> _선택Button = new List<Button>(3);
    [SerializeField] private Image _background;
    private Image[] _backgroundImages;
    void Start()
    {
        _backgroundImages = Resources.LoadAll<Image>("Animation/SelectionCG");
        _종료Button.onClick.AddListener(OnClick종료Button);
    }

    private void OnClick종료Button()
    {
        Panel.SetActive(false);
    }

}
