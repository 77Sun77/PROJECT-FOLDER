using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRaycast : MonoBehaviour
{
    public GraphicRaycaster gr;
    public PointerEventData pe;
    public EventSystem es;

    public List<RaycastResult> UI_Raycast(Vector2 pos)
    {
        pe = new PointerEventData(es);
        pe.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pe, results);
        return results;

    }
}
