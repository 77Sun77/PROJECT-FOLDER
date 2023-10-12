using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Window lastOpenWin;

    [Header("CustomComponent")]
    public UIRaycast UR;

    [Header("IconOption")]
    public GameObject IconOptionPrefab;
    public Transform Canvas;
    GameObject createPrefab;

    public Transform screen, windowParent;

    public Transform cursor;

    public Boundary Boundary_X, Boundary_Y;

    public bool isGrab;

    [Serializable]
    public class Boundary
    {
        public float min, max;
        public enum Direction { Horizontal, Vertical };
        public Direction Dir;
    }
    void Awake()
    {
        if(!instance) instance = this;
        Canvas = GameObject.Find("Canvas").transform;
        
        Cursor.visible = false;
    }


    void Update()
    {
        cursor.GetComponent<RectTransform>().SetAsLastSibling();
        if (Input.GetMouseButtonUp(0)) isGrab = false;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pos.x = Mathf.Clamp(pos.x, -6.2f, 5.95f);
        //pos.y = Mathf.Clamp(pos.y, -2.22f, 3.45f);
        Vector2 posTemp = Vector2.zero;
        bool isOver_X = false, isOver_Y = false;
        (isOver_X, posTemp.x) = IsOver(Boundary_X, pos);
        (isOver_Y, posTemp.y) = IsOver(Boundary_Y, pos);
        if((isOver_X || isOver_Y) && !isGrab)
        {
            if(Cursor.visible == false)
            {
                if (isOver_X) pos.x = posTemp.x;
                if (isOver_Y) pos.y = posTemp.y;
                cursor.position = pos;
            }
            
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            if (isOver_X)
            {
                pos.x = posTemp.x;

                UnityEngine.InputSystem.Mouse.current.WarpCursorPosition(PositionConverter(pos));
            }
            if (isOver_Y)
            {
                pos.y = posTemp.y;

                UnityEngine.InputSystem.Mouse.current.WarpCursorPosition(PositionConverter(pos));
            }

            cursor.position = pos;
        }
        
        
        //pos.x = Mathf.Clamp(pos.x, FloatConverter(Boundary_X).Item1, FloatConverter(Boundary_X).Item2);
        //pos.y = Mathf.Clamp(pos.y, FloatConverter(Boundary_Y).Item1, FloatConverter(Boundary_Y).Item2);

        //cursor.position = pos;
        //UnityEngine.InputSystem.Mouse.current.WarpCursorPosition(PositionConverter(pos));
        // pos = Camera.main.WorldToScreenPoint(pos);
        //SetCursorPos((int)pos.x, (int)pos.y);
        //if (Vector2.Distance(pos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 1) CursorControl.SetPosition(Camera.main.WorldToScreenPoint(pos));

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {

            bool isReturn = false;
            var results = UR.UI_Raycast(Input.mousePosition);

            if (results.Count != 0)
            {
                GameObject go = null;
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.CompareTag("IconOption"))
                    {
                        return;
                    }
                    else if (result.gameObject.CompareTag("Icon"))
                    {

                        go = result.gameObject;
                        break;
                    }
                    else if(result.gameObject.CompareTag("Calendar") || result.gameObject.CompareTag("Taskbar"))
                    {
                        isReturn = true;
                        break;
                    }
                }

                if (createPrefab)
                {
                    createPrefab.GetComponent<IconOption>().OpenOption = null;
                    Destroy(createPrefab);
                }

                if (Input.GetMouseButtonDown(1) && !isReturn)
                {
                    createPrefab = Instantiate(IconOptionPrefab, Canvas);
                    Vector2 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    createPrefab.transform.position = vec;
                    if (go)
                    {
                        
                        IconOption io = createPrefab.GetComponent<IconOption>();
                        io.OpenOption = go;
                        io.DontDestroy = go.GetComponent<OpenFile>().DontDestroy;
                        if (OpenFile.SelectFile != go) OpenFile.SelectFile = null;
                        
                    }
                    else
                    {
                        createPrefab.GetComponent<IconOption>().isIcon = false;
                    }
                    return;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    if (OpenFile.SelectFile != go) OpenFile.SelectFile = null;
                }

            }



        }



    }

    Vector2 PositionConverter(Vector2 pos)
    {
        pos = Camera.main.WorldToScreenPoint(pos);
        return pos;
    }
    /*
    (float, float) FloatConverter(Vector2 vec)
    {
        return (vec.x, vec.y);
    }*/

    (bool, float) IsOver(Boundary b, Vector2 pos)
    {
        if (b.Dir == Boundary.Direction.Horizontal)
        {
            if(pos.x < b.min) return (true, b.min);
            else if(pos.x > b.max) return (true, b.max);
        }
        else
        {
            if (pos.y < b.min) return (true, b.min);
            else if (pos.y > b.max) return (true, b.max);
        }
        return (false, 0);
    }
}
