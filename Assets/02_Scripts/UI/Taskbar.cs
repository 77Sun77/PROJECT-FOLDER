using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taskbar : MonoBehaviour
{
    public static Taskbar instance;
    public Dictionary<GameObject, OpenFile> objects = new();

    public GameObject TaskbarIcon;
    public Transform Icon_Parent;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void Add_Obj(GameObject obj, OpenFile f)
    {
        objects.Add(obj, f);
        Instantiate(TaskbarIcon, Icon_Parent).GetComponent<Taskbar_Icon>().f = f;
    }
    public void Delete_Obj(GameObject obj, bool isPrefab)
    {
        objects.Remove(obj);
        if (isPrefab)
        {
            Destroy(obj);
        }
        else
        {
            
            obj.SetActive(false);
        }
    }
}
