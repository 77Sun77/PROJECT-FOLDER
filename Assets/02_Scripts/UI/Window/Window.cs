using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public RectTransform fullSize, smallSize; // 이름 변경 예정
    RectTransform tr;
    public bool isDisable,isPrefab, firstSetting;
    float disableNum, smallNum, fullNum;
    Vector3 pos, posTemp, size;
    public OpenFile f;

    public bool NotEditMode;

    public GameObject[] btns;
    public Text nameTxt;

    Window winTemp;
    public Taskbar_Icon icon;

    bool isMax, isSmall, isFull, isOpen;
    void Awake()
    {
        tr = GetComponent<RectTransform>();

        fullSize = GameObject.Find("BG").GetComponent<RectTransform>();
        smallSize = GameObject.Find("BG_2").GetComponent<RectTransform>();


        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        if (NotEditMode)
        {
            Restore();
            for (int i = 0; i < 2; i++) btns[i].SetActive(false);
        }
        else
        {
            Maximize();
        }

        //nameTxt = transform.Find("Name").GetComponent<Text>();
        if (nameTxt)
        {
            string txt = nameTxt.text.Replace(" ", "");
            int count = String.GetCount(nameTxt.text, ' ');
            int length = 0;
            
            for(int i = 0; i < txt.Length; i++)
            {
                
                if (((int)txt[i]) >= 65 && ((int)txt[i]) <= 122)
                {
                    length = 20;
                    
                    break;
                }
                else if(((int)txt[i]) > 122)
                {
                    length = 15;
                    
                    break;
                }
            }
            if (txt.Length >= length)
            {
                txt = nameTxt.text.Substring(0, length+ count); // 공백 포함 함
                //txt = txt.Substring(0, 4); // 공백 포함 안함
                
                nameTxt.text = txt;

            }
        }
    }
    
    void Update()
    {
        Disable();
        Open();
        Restore();
        Maximize();

    }
    public void Minimization() // 최소화
    {
        if (!isDisable)
        {
            pos = tr.anchoredPosition;
            isDisable = true;
            posTemp = tr.anchoredPosition;
            size = new Vector3(tr.rect.width, tr.rect.height);
        }
        
    }
    public void Maximize()
    {

        // GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = resolution;

        /*
        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullSize.rect.width);

        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fullSize.rect.height);
        
        tr.anchoredPosition = fullSize.anchoredPosition;
        */
        if (isFull)
        {
            Vector3 fullVec = new Vector3(fullSize.rect.width, fullSize.rect.height);
            Vector3 smallVec = new Vector3(smallSize.rect.width, smallSize.rect.height);
            fullNum += Time.deltaTime * 15;
            Vector3 vec = Vector3.Lerp(smallVec, fullVec, fullNum);
            tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vec.x);

            tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, vec.y);


            Vector3 pos = Vector3.Lerp(this.pos, fullSize.anchoredPosition, fullNum);
            tr.anchoredPosition = pos;


            if (fullNum > 1)
            {
                isFull = false;
                fullNum = 0;
                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullSize.rect.width);

                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fullSize.rect.height);
            }


        }
        
    }
    public void Restore()
    {
        /*
        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, smallSize.rect.width);

        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, smallSize.rect.height);

        tr.anchoredPosition = pos;
        */
        if (isSmall)
        {
            Vector3 fullVec = new Vector3(fullSize.rect.width, fullSize.rect.height);
            Vector3 smallVec = new Vector3(smallSize.rect.width, smallSize.rect.height);
            smallNum += Time.deltaTime * 15;
            Vector3 vec = Vector3.Lerp(fullVec, smallVec, smallNum);
            tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vec.x);

            tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, vec.y);


            Vector3 pos = Vector3.Lerp(Vector3.zero, this.pos, smallNum);
            tr.anchoredPosition = pos;
            

            if (smallNum > 1)
            {
                isSmall = false;
                smallNum = 0;
            }


        }
    }

    public void MaxMiniSIze()
    {
        if (isMax)
        {
            //Restore();
            isSmall = true;
        }
        else
        {
            //Maximize();
            isFull = true;
        }
        isMax = !isMax;
    }
    public void Quit()
    {
        //Destroy(gameObject);
        Taskbar.instance.Delete_Obj(gameObject, isPrefab);
        //f.go = null;
    }


    // Animation Trigger
    public void Disable()
    {
        
        if (isDisable)
        {
            
            disableNum += Time.deltaTime*15;
            Vector3 vec = Vector3.Lerp(size, Vector3.zero, disableNum);
            tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vec.x);
            tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, vec.y);

            RectTransform icon = this.icon.GetComponent<RectTransform>();
            Vector3 ancherPos = new Vector3(icon.localPosition.x, tr.rect.height);
            ancherPos.y = ancherPos.y / 2  -490;
            print(tr.rect.height);
            tr.localPosition = Vector3.Lerp(posTemp, ancherPos, disableNum);


            if (disableNum > 1)
            {
                disableNum = 0;
                posTemp = tr.anchoredPosition;
                gameObject.SetActive(false);
                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
                isDisable = false;
            }


        }
    }
    public void Active()
    {
        gameObject.SetActive(true);
        //isDisable = false;
        SetAsLastSibling();
    }
    
    private void OnEnable()
    {
        
        //tr.anchoredPosition = pos;
        //if(firstSetting) tr.anchoredPosition = pos;
        //else firstSetting = true;
        SetAsLastSibling();
        isOpen = true;
    }

    private void OnDisable()
    {
        GameManager.instance.lastOpenWin = winTemp;
    }

    void SetAsLastSibling()
    {
        tr.SetAsLastSibling();
        if (GameManager.instance.lastOpenWin != this) winTemp = GameManager.instance.lastOpenWin;
        GameManager.instance.lastOpenWin = this;
    }

    void Open()
    {
        if (isOpen)
        {
            if (isMax)
            {
                Vector3 fullVec = new Vector3(fullSize.rect.width, fullSize.rect.height);
                Vector3 vecZero = Vector3.zero;
                fullNum += Time.deltaTime * 15;
                Vector3 vec = Vector3.Lerp(vecZero, fullVec, fullNum);
                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vec.x);

                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, vec.y);


                Vector3 pos = Vector3.Lerp(posTemp, fullSize.anchoredPosition, fullNum);
                tr.anchoredPosition = pos;


                if (fullNum > 1)
                {
                    isOpen = false;
                    fullNum = 0;
                    tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullSize.rect.width);

                    tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fullSize.rect.height);
                }


            }
            else
            {
                Vector3 smallVec = new Vector3(smallSize.rect.width, smallSize.rect.height);
                Vector3 vecZero = Vector3.zero;
                smallNum += Time.deltaTime * 15;
                Vector3 vec = Vector3.Lerp(vecZero, smallVec, smallNum);
                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vec.x);

                tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, vec.y);


                Vector3 pos = Vector3.Lerp(posTemp, this.pos, smallNum);
                tr.anchoredPosition = pos;


                if (smallNum > 1)
                {
                    isOpen = false;
                    smallNum = 0;
                    tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, smallSize.rect.width);

                    tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, smallSize.rect.height);
                }


            }


        }
    }
}
