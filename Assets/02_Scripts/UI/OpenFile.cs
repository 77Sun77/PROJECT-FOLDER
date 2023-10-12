using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public static class String
{
    public static int GetCount(string s, char c)
    {
        int count = 0;
        foreach (char ch in s)
        {
            if (ch == c) count++;
        }
        return count;
    }
}
public class OpenFile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject window;
    public bool isPrefab;
    public GameObject go;

    public TextMeshProUGUI nameTxt;
    Image img, icon;

    //Double Click 처리용 변수
    public static GameObject SelectFile;
    public float m_DoubleClickSecond = 1.5f;
    private int clickCount;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;
    private bool PointerUp, OnDrag;
    public bool DontDestroy, isFolder, isRecycleBin, inFolder;
    Vector2 clickVec, offset;

    bool OnClick;

    public TableData thisTable;
    public Transform ContentsParent;
    public List<GameObject> Contents = new List<GameObject>();

    private void Start()
    {
        MovePoint();
        nameTxt = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        icon = transform.Find("Icon").GetComponent<Image>();
        /*
        if (nameTxt)
        {
            string txt = nameTxt.text.Replace(" ", "");
            int count = String.GetCount(nameTxt.text,' ');

            if (txt.Length >= 5)
            {
                //txt = nameTxt.text.Substring(0, 4+ count); // 공백 포함 함
                txt = txt.Substring(0, 4); // 공백 포함 안함
                txt += "...";
                nameTxt.text = txt;

            }
        }*/
        img = GetComponent<Image>();

        if (isPrefab)
        {
            go = Instantiate(window, GameManager.instance.windowParent);
            go.name = gameObject.name + " Window";
            go.GetComponent<Window>().f = this;
            go.SetActive(false);
        }
        else
        {
            go = window;
        }
        ContentsParent = go.transform.Find("Contents");



    }
    void Update()
    {
        if (PointerUp)
        {
            Color c = img.color;
            c.a = 15f / 255f;
            img.color = c;

        }
        else if(SelectFile == gameObject)
        {
            Color c = img.color;
            c.a = 35f / 255f;
            img.color = c;
            
        }
        else
        {
            Color c = img.color;
            c.a = 0;
            img.color = c;
        }

        if (Input.GetButtonDown("Submit") && SelectFile == gameObject)
        {
            Open();
        }

        if (m_IsOneClick)
        {
            m_Timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnClick = true;
        }


        if (Input.GetMouseButtonUp(0))
        {
            clickVec = Vector2.zero;
            if (OnDrag)
            {
                MovePoint();
                OnDrag = false;

            }
            
        }

        if(clickVec != Vector2.zero && Vector2.Distance(clickVec, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f && !OnDrag)
        {
            OnDrag = true;
            clickCount = 0;
            m_Timer = 0;
            m_IsOneClick = false;
            GameManager.instance.isGrab = true;
        }

        if (OnDrag)
        {
            transform.position = (Vector2)GameManager.instance.cursor.position- offset;
        }
    }
    public void OnPointerDown(PointerEventData eData) 
    {
        if(eData.button == PointerEventData.InputButton.Left && !OnDrag)
        {
            clickVec = GameManager.instance.cursor.position;
            clickCount++;
            if (clickCount == 2)
            {


                if (m_Timer < m_DoubleClickSecond)
                {
                    Open();
                    return;

                }
                else
                {
                    clickCount = 1;
                    m_Timer = 0;
                    //m_IsOneClick = false;
                }
            }
            else
            {
                m_IsOneClick = true;
            }

            SelectFile = gameObject;
            offset = (Vector2)GameManager.instance.cursor.position - (Vector2)transform.position;
        }

    }
    public void OnPointerEnter(PointerEventData eData)
    {
        PointerUp = true;
    }
    public void OnPointerExit(PointerEventData eData)
    {
        PointerUp = false;
    }
    public void Open()
    {
        clickCount = 0;
        m_Timer = 0;
        m_IsOneClick = false;
        SelectFile = null;
        clickVec = Vector2.zero;
        if (!go)
        {
            if (isPrefab) go = Instantiate(window, GameManager.instance.screen);
            else
            {
                window.SetActive(true);
                go = window;

            }
            window.SetActive(true);
            go = window;
            Taskbar.instance.Add_Obj(go, this, icon.sprite);
            go.GetComponent<Window>().f = this;
        }
        else
        {
            Active();
        }
        window.SetActive(true);
        Taskbar.instance.Add_Obj(go, this, icon.sprite);
        
        go.GetComponent<Window>().isPrefab = isPrefab;


    }

    public void Active()
    {
        Window win = go.GetComponent<Window>();
        if (go.GetComponent<Window>().isDisable || GameManager.instance.lastOpenWin != win)
        {
            win.Active();
        }
        else
        {
            win.Minimization();
        }

    }

    public void AddFile(GameObject file)
    {
        //Contents.Add(file);
        file.transform.parent = ContentsParent;
        inFolder = true;
    }

    void MovePoint()
    {
        var results = GameManager.instance.UR.UI_Raycast(Camera.main.WorldToScreenPoint(transform.position));
        if (results.Count != 0)
        {
            foreach (RaycastResult result in results)
            {

                if (result.gameObject.CompareTag("TableData"))
                {
                    result.gameObject.GetComponent<TableData>().ChangePos(this);

                    break;
                }
                else if (thisTable)
                {
                    transform.position = thisTable.transform.position;
                }
            }
        }
    }
}
