using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    public Text positionText;

    public void CenterCursor()
    {
        UnityEngine.InputSystem.Mouse.current.WarpCursorPosition(PositionConverter(new Vector2(3, 4))) ;
    }

    private void Update()
    {
        Vector2 cursorPos = (CursorControl.GetPosition()*2) /*- new Vector2(1920, 1080)*/;
        positionText.text = Camera.main.ScreenToWorldPoint(cursorPos).ToString() + " "+ Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString();
        //print(UnityEngine.InputSystem.Mouse.current.position.x. +" "+ UnityEngine.InputSystem.Mouse.current.position.y);
      //  UnityEngine.InputSystem.Mouse.current.position.

    }

    Vector2 PositionConverter(Vector2 pos)
    {
        pos = Camera.main.WorldToScreenPoint(pos);
        return pos;
    }
}
