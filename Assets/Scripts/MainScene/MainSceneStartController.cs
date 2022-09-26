using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartController : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private Button _종료Button;
    [SerializeField] private List<GameObject> _선택Button;

    void Start()
    {
        EventManager.SetSelection += DataManager.SetSelctionData;
        EventManager.SetCGID += SetCGID;
        EventManager.SetID += SetID;
        _종료Button.onClick.AddListener(OnClick종료Button);
    }

    private void OnClick종료Button()
    {
        Panel.SetActive(false);
    }

    private void SetCGID(GameObject value1)
    {
        int index = _선택Button.IndexOf(value1) + 1;
        EventManager.CallOnSelectionCG(index);
    }
    private void SetID(GameObject value1)
    {
        int index = _선택Button.IndexOf(value1) + 1;
        EventManager.CallOnSelection(index);
    }



}
