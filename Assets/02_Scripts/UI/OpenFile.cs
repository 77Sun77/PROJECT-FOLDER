using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class String
{
    public static int GetCount(string s, char c)
    {
        int count = 0;
        foreach (char ch in s)
        {
            if (ch == c) count++;
        }
        return count;
    }
}
public class OpenFile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject window;
    public bool isPrefab;
    public GameObject go;

    public Text nameTxt;
    Image img;

    //Double Click 처리용 변수
    public static GameObject SelectFile;
    public float m_DoubleClickSecond = 1.5f;
    private int clickCount;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;
    private bool PointerUp;
    private void Start()
    {

        if (nameTxt)
        {
            string txt = nameTxt.text.Replace(" ", "");
            int count = String.GetCount(nameTxt.text,' ');

            if (txt.Length >= 5)
            {
                //txt = nameTxt.text.Substring(0, 4+ count); // 공백 포함 함
                txt = txt.Substring(0, 4); // 공백 포함 안함
                txt += "...";
                nameTxt.text = txt;

            }
        }
        img = GetComponent<Image>();
    }
    void Update()
    {
        if (PointerUp)
        {
            Color c = img.color;
            c.a = 15f / 255f;
            img.color = c;

        }
        else if(SelectFile == gameObject)
        {
            Color c = img.color;
            c.a = 35f / 255f;
            img.color = c;
            
        }
        else
        {
            Color c = img.color;
            c.a = 0;
            img.color = c;
        }

        if (Input.GetButtonDown("Submit") && SelectFile == gameObject)
        {
            Open();
        }

        if (m_IsOneClick)
        {
            m_Timer += Time.deltaTime;
        }

    }
    public void OnPointerClick(PointerEventData eData) 
    {
        clickCount++;
        if(clickCount == 2)
        {
            
           
            if (m_Timer < m_DoubleClickSecond)
            {
                Open();
                return;

            }
            else
            {
                clickCount = 1;
                m_Timer = 0;
                m_IsOneClick = false;
            }
        }
        else
        {
            m_IsOneClick = true;
        }

        SelectFile = gameObject;
        
    }
    public void OnPointerEnter(PointerEventData eData)
    {
        PointerUp = true;
    }
    public void OnPointerExit(PointerEventData eData)
    {
        PointerUp = false;
    }
    public void Open()
    {
        clickCount = 0;
        m_Timer = 0;
        m_IsOneClick = false;
        SelectFile = null;
        if (!go)
        {
            if (isPrefab) go = Instantiate(window, GameObject.Find("Canvas").transform);
            else
            {
                window.SetActive(true);
                go = window;

            }
            Taskbar.instance.Add_Obj(go, this);
            go.GetComponent<Window>().f = this;
        }
        else
        {
            Active();
        }
        go.GetComponent<Window>().isPrefab = isPrefab;


    }

    public void Active()
    {
        Window win = go.GetComponent<Window>();
        if (go.GetComponent<Window>().isDisable || GameManager.instance.lastOpenWin != win)
        {
            win.Active();
        }
        else
        {
            win.Minimization();
        }

    }
}
