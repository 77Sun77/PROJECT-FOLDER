using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taskbar_Icon : MonoBehaviour
{
    public  OpenFile f;
    public Window win;
    public void OnClick_Icon()
    {
        f.Active();

    }
    private void Start()
    {
        win = f.go.GetComponent<Window>();
        win.icon = this;
    }
    private void Update()
    {
        if(f.go == null)
        {
            Destroy(gameObject);
        }
        
    }
}
