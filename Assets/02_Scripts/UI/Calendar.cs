using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Calendar : UI
{
    public Transform Contents;
    public List<Transform> Month = new List<Transform>();

    public Color c;

    public int count, num, firstEmpty;
    public GameObject month, empty, date;

    public Color DisableColor;
    public TextMeshProUGUI dayText;
    void Start()
    {
        //Set_Calendar();
    }

    // Update is called once per frame
    void Update()
    {
        SetDisable();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            var results = GameManager.instance.UR.UI_Raycast(Input.mousePosition);
            foreach(RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Calendar")) return;
            }
            gameObject.SetActive(false);
        }
        
        
    }

    [ContextMenu("Set_Calendar")]
    public void Set_Calendar()
    {
        int count =0;
        Sprite s=null;

        if (Month.Count != 0) Month.Clear();
        foreach (Transform content in Contents)
        {
            Month.Add(content);
        }
        foreach (Transform month in Month)
        {
            foreach (Transform line in month)
            {
                
                foreach(Transform date in line)
                {
                    
                    if (date.childCount != 0)
                    {
                        date.GetChild(0).GetComponent<TextMeshProUGUI>().text = date.name.Replace("_", "");
                        date.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
                        date.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 20;

                        if (count == 0)
                        {
                            s = date.GetComponent<Image>().sprite;
                            c = date.GetComponent<Image>().color;
                            c.a = date.GetComponent<Image>().color.a;
                            count++;
                        }
                        else
                        {
                            date.GetComponent<Image>().sprite = s;
                            date.GetComponent<Image>().color = c;
                            date.GetComponent<Image>().type = Image.Type.Sliced;
                        }
                    }
                }
                
            }
        }
    }

    [ContextMenu("Auto_Create")]
    public void Auto_Create()
    {
        GameObject month = Instantiate(this.month, Contents);
        month.name = "Month_" + num;
        bool isFirst = true;
        int count = 0;
        foreach (Transform line in month.transform)
        {
            if(isFirst)
            {
                for (int i = 0; i < firstEmpty; i++)
                {
                    Instantiate(empty, line);
                }
                for(int i=firstEmpty; i< 7; i++)
                {
                    Instantiate(date, line).name = "_" + (count + 1);
                    count++;
                }
                isFirst = false;
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    Instantiate(date, line).name = "_" + (count + 1);
                    count++;
                    if (count == this.count) break;
                }
            }
            
        }
    }

    void SetDisable()
    {
        float min = Vector2.Distance(Month[0].position, transform.position);
        int index = 0;
        for(int i=0; i<Month.Count; i++)
        {
            if(Vector2.Distance(Month[i].position, transform.position) < min)
            {
                min = Vector2.Distance(Month[i].position, transform.position);
                index = i;
            }
        }
        foreach (Transform line in Month[index])
        {
            foreach (Transform date in line)
            {

                if (date.childCount != 0)
                {
                    date.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;

                }
            }
        }
        for (int i=0; i<Month.Count; i++)
        {
            if (i == index) continue;
            foreach (Transform line in Month[i])
            {
                foreach (Transform date in line)
                {

                    if (date.childCount != 0)
                    {
                        date.GetChild(0).GetComponent<TextMeshProUGUI>().color = DisableColor;

                    }
                }
            }
        }
        dayText.text = "2010³â " + (index + 1) + "¿ù";
    }
}
