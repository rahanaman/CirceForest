using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartController : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private Button _����Button;
    [SerializeField] private List<Button> _����Button = new List<Button>(3);
    [SerializeField] private Image _background;
    private Image[] _backgroundImages;
    void Start()
    {
        _backgroundImages = Resources.LoadAll<Image>("Animation/SelectionCG");
        _����Button.onClick.AddListener(OnClick����Button);
    }

    private void OnClick����Button()
    {
        Panel.SetActive(false);
    }

}
