using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartController : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private Button _����Button;
    [SerializeField] private List<GameObject> _����Button;

    void Start()
    {
        EventManager.SetSelection += DataManager.SetSelctionData;
        EventManager.SetCGID += SetCGID;
        EventManager.SetID += SetID;
        _����Button.onClick.AddListener(OnClick����Button);
    }

    private void OnClick����Button()
    {
        Panel.SetActive(false);
    }

    private void SetCGID(GameObject value1)
    {
        int index = _����Button.IndexOf(value1) + 1;
        EventManager.CallOnSelectionCG(index);
    }
    private void SetID(GameObject value1)
    {
        int index = _����Button.IndexOf(value1) + 1;
        EventManager.CallOnSelection(index);
    }



}
