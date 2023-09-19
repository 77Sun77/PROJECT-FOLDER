using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Window lastOpenWin;
    void Start()
    {
        if(!instance) instance = this;
    }

    
    void Update()
    {
        
    }
}
