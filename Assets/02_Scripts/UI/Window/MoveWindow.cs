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
                Vector2 vec = Input.mousePosition;
                vec = WorldPoint(Input.mousePosition) - offset;
                
                window.position = vec;
                print(offset.x);
                
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        offset = WorldPoint(Input.mousePosition) - (Vector2)window.position;
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
