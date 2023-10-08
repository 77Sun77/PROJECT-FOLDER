using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconOption : MonoBehaviour
{
    public GameObject OpenOption;
    public bool isIcon, DontDestroy;
    public GameObject iconOption, NotIconOption, DontDestroyIconOption;
    void Start()
    {
        if (DontDestroy)
        {
            DontDestroyIconOption.SetActive(true);
            iconOption.SetActive(false);
            NotIconOption.SetActive(false);

        }
        else
        {
            iconOption.SetActive(isIcon);
            NotIconOption.SetActive(!isIcon);
            DontDestroyIconOption.SetActive(false);
        }
        
    }

    void Update()
    {
        
    }

    public void Open()
    {
        OpenOption.GetComponent<OpenFile>().Open();
        Destroy(gameObject);
    }

    public void Delete()
    {
        Destroy(OpenOption);
        Destroy(gameObject);
    }
    public void NameChange()
    {

    }
}
