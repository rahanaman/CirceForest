using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneStartView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _backgroundDark;
    [SerializeField] private Sprite[] _backgrounds;
    private Color _backgroundDarkColor;

    // Start is called before the first frame update
    void Start()
    {
        _backgroundDarkColor = _backgroundDark.color;
        _backgroundDarkColor.a = 0.5f;
        _backgroundDark.color = _backgroundDarkColor;
        EventManager.SetSelectionCG += SetSelectionCGUI;
        EventManager.SetSelection += 캐릭터선택;
        _backgrounds = Resources.LoadAll<Sprite>("SelectionCG");
    }

    private void SetSelectionCGUI(int value)
    {
        if (_background != null)
        {
            _background.sprite = _backgrounds[value];
        }
    }

    private void 캐릭터선택(int value)
    {
        EventManager.CallOnID((DataBase.SoundID)value, 5);
        StartCoroutine(SceneFade(value));
    }
    private void 씬이동(int value)
    {
        EventManager.SetSelectionCG = null;
        EventManager.SetSelection = null;
        EventManager.SetCGID = null;
        EventManager.SetID = null;
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator SceneFade(int value)
    {
        Debug.Log(1);
        float deltaAlpha = 3f; ;
        
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        yield return frame;
        while (_backgroundDarkColor.a > 0f)
        {
            _backgroundDarkColor.a -= Time.deltaTime * deltaAlpha;
            _backgroundDark.color = _backgroundDarkColor;
            yield return frame;
        }
        yield return new WaitUntil(() => !SoundController.instance.IsEFX);
        씬이동(value);
    }



}
