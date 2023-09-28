using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconOption : MonoBehaviour
{
    public GameObject OpenOption;
    public bool isIcon;
    public GameObject iconOption, NotIconOption;
    void Start()
    {
        
        iconOption.SetActive(isIcon);
        NotIconOption.SetActive(!isIcon);
    }

    void Update()
    {
        
    }
}
