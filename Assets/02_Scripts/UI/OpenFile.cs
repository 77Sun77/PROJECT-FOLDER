using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
public class OpenFile : MonoBehaviour
{
    public GameObject window;
    public bool isPrefab;
    public GameObject go;

    public Text nameTxt;

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
        
    }
    
    public void Open()
    {
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
