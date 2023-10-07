using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform window;
    public Vector2 clickPoint, offset;
    bool isDown, moveActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown)
        {
            if(Vector3.Distance(clickPoint, Input.mousePosition) > 5 && !moveActive) moveActive = true;
            
            if(moveActive)
            {
                /*
                Vector2 vec = Input.mousePosition;
                vec = WorldPoint(Input.mousePosition) - offset;
                
                window.position = vec;
                */
                Vector2 vec = Input.mousePosition;
                vec = (Vector2)GameManager.instance.cursor.position - offset;

                window.position = vec;

            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        /*
        isDown = true;
        offset = WorldPoint(Input.mousePosition) - (Vector2)window.position;
        */
        isDown = true;
        offset = (Vector2)GameManager.instance.cursor.position - (Vector2)window.position;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }

    Vector2 WorldPoint(Vector3 vec)
    {
        return Camera.main.ScreenToWorldPoint(vec);
    }
}
