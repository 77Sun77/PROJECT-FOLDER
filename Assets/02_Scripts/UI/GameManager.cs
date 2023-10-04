using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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
    void Start()
    {
        if(!instance) instance = this;
        Canvas = GameObject.Find("Canvas").transform;
    }

    
    void Update()
    {
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
}
