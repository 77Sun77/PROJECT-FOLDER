using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFile : MonoBehaviour
{
    public GameObject window;
    public bool isPrefab;
    public GameObject go;
    public void Open()
    {
        if (!go)
        {
            if (isPrefab) go = Instantiate(window, GameObject.Find("Canvas").transform);
            else
            {
                window.SetActive(true);
                go = window;
                Taskbar.instance.Add_Obj(go, this);
            }
            go.GetComponent<Window>().f = this;
        }
        else
        {
            go.SetActive(true);
        }
        go.GetComponent<Window>().isPrefab = isPrefab;
        

    }

    public void Active()
    {
        Window win = go.GetComponent<Window>();
        if (go.GetComponent<Window>().isDisable)
        {
            win.Active();
        }
        else
        {
            win.Minimization();
        }

    }
}
