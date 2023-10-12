using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taskbar_Icon : MonoBehaviour
{
    public  OpenFile f;
    public Window win;

    public Image icon;
    
    
    public void OnClick_Icon()
    {
        f.Active();

    }

    public void Setting(OpenFile f ,Sprite sprite)
    {
        this.f = f;
        
        win = f.go.GetComponent<Window>();
        win.icon = this;
        icon.sprite = sprite;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }


}
