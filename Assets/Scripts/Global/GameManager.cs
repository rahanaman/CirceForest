using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool IsCardClick;
    [SerializeField] private Texture2D _pointerIdle;
    [SerializeField] private Texture2D _pointerCard;
    public bool IsAnim;
    public bool IsCardPanel;

    private void Start()
    {
        SetCursorIdle();
        IsAnim = false;
        IsCardClick = false;
        IsCardPanel = false;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetCursorIdle()
    {
        Cursor.SetCursor(_pointerIdle, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void SetCursorCard()
    {
        Cursor.SetCursor(_pointerCard, Vector2.zero, CursorMode.ForceSoftware);
    }
}
