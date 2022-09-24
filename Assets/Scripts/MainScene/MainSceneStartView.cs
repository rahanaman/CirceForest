using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneStartView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.SetSelectionCG += SetSelectionCGUI;
        EventManager.SetSelection += æ¿¿Ãµø;
        _backgrounds = Resources.LoadAll<Sprite>("SelectionCG");
    }

    private void SetSelectionCGUI(int value)
    {
        if (_background != null)
        {
            _background.sprite = _backgrounds[value];
        }
    }

    private void æ¿¿Ãµø(int value)
    {
        SceneManager.LoadScene("GameScene");
    }


}
