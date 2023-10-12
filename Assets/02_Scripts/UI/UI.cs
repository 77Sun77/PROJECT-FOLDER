using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void AutoSetActive()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    //public void SetActive()
}
