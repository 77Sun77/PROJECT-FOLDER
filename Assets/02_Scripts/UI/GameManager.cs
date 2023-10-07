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

    [Header("IconOption")]
    public GameObject IconOptionPrefab;
    public Transform Canvas;
    public GraphicRaycaster gr;
    public PointerEventData pe;
    public EventSystem es;
    GameObject createPrefab;

    public Transform screen;

    public Transform cursor;

    public Boundary Boundary_X, Boundary_Y;

    [Serializable]
    public class Boundary
    {
        public float min, max;
        public enum Direction { Horizontal, Vertical };
        public Direction Dir;
    }
    void Start()
    {
        if(!instance) instance = this;
        Canvas = GameObject.Find("Canvas").transform;
        
        Cursor.visible = false;
    }


    void Update()
    {

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pos.x = Mathf.Clamp(pos.x, -6.2f, 5.95f);
        //pos.y = Mathf.Clamp(pos.y, -2.22f, 3.45f);
        Vector2 posTemp = Vector2.zero;
        bool isOver = false;
        (isOver, posTemp.x) = IsOver(Boundary_X, pos);
        if (isOver)
        {
            pos.x = posTemp.x;
            
            UnityEngine.InputSystem.Mouse.current.WarpCursorPosition(PositionConverter(pos));
        }
        (isOver, posTemp.y) = IsOver(Boundary_Y, pos);
        if (isOver)
        {
            pos.y = posTemp.y;
            
            UnityEngine.InputSystem.Mouse.current.WarpCursorPosition(PositionConverter(pos));
        }
        cursor.position = pos;
        //pos.x = Mathf.Clamp(pos.x, FloatConverter(Boundary_X).Item1, FloatConverter(Boundary_X).Item2);
        //pos.y = Mathf.Clamp(pos.y, FloatConverter(Boundary_Y).Item1, FloatConverter(Boundary_Y).Item2);

        //cursor.position = pos;
        //UnityEngine.InputSystem.Mouse.current.WarpCursorPosition(PositionConverter(pos));
        // pos = Camera.main.WorldToScreenPoint(pos);
        //SetCursorPos((int)pos.x, (int)pos.y);
        //if (Vector2.Distance(pos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 1) CursorControl.SetPosition(Camera.main.WorldToScreenPoint(pos));

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {

            pe = new PointerEventData(es);
            pe.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pe, results);


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
                }

                if (createPrefab)
                {
                    createPrefab.GetComponent<IconOption>().OpenOption = null;
                    Destroy(createPrefab);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    createPrefab = Instantiate(IconOptionPrefab, Canvas);
                    Vector2 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    createPrefab.transform.position = vec;
                    if (go)
                    {
                        createPrefab.GetComponent<IconOption>().OpenOption = go;
                        if (OpenFile.SelectFile != go) OpenFile.SelectFile = null;
                        print(OpenFile.SelectFile);
                        print(go);
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
