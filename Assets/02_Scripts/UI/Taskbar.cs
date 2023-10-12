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

    public void Add_Obj(GameObject obj, OpenFile f, Sprite sprite)
    {
        objects.Add(obj, f);
        Instantiate(TaskbarIcon, Icon_Parent).GetComponent<Taskbar_Icon>().Setting(f, sprite);
    }
    public void Delete_Obj(GameObject obj, bool isPrefab)
    {
        objects.Remove(obj);
        Destroy(obj.GetComponent<Window>().icon.gameObject);
        if (isPrefab)
        {
            //Destroy(obj);
            obj.SetActive(false);// Test coding
        }
        else
        {
            
            obj.SetActive(false);
        }
    }
}
