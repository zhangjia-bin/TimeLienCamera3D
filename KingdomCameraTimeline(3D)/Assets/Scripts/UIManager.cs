using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }


    public GameObject DialogUI;
    public Text CharacterName;
    public Text Content;
    public GameObject PauserTip;


    public RectTransform Rectangle;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DialogUI.SetActive(false);
    }


    public void SetDialong(string name,string content)
    {
        DialogUI.SetActive(true);
        CharacterName.text = name;
        Content.text = content;
    }

    public void HideDialog()
    {
        DialogUI.SetActive(false);
    }
    public void ShowPauserTips(bool pauseAfterPlay)
    {
        PauserTip.SetActive(pauseAfterPlay);
    }

    public void SetRectangle(Vector2 pos,Vector2 size)
    {
        Rectangle.gameObject.SetActive(true);
        Rectangle.position = pos;
        Rectangle.sizeDelta = size;
    }

    public void HideRectangle()
    {
        Rectangle.gameObject.SetActive(false);
    }
}
