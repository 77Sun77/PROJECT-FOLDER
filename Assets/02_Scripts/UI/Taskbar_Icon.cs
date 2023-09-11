using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taskbar_Icon : MonoBehaviour
{
    public  OpenFile f;
    public void OnClick_Icon()
    {
        f.Active();

    }

    private void Update()
    {
        if(f.go == null)
        {
            Destroy(gameObject);
        }
        
    }
}
