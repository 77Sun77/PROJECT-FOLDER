using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public RectTransform fullSize, smallSize; // 이름 변경 예정
    RectTransform tr;
    public bool isDisable, isMax, isPrefab;
    float num;
    Vector3 pos;
    Animator anim;
    public OpenFile f;

    void Awake()
    {
        tr = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();

        fullSize = GameObject.Find("BG").GetComponent<RectTransform>();
        smallSize = GameObject.Find("BG_2").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisable)
        {
            num += Time.deltaTime*9;
            tr.anchoredPosition = Vector3.Lerp(pos,new Vector3(0, -400), num);
            
        }   
    }
    public void Minimization() // 최소화
    {
        pos = tr.anchoredPosition;
        isDisable = true;
        anim.SetTrigger("Disable");
    }
    public void Maximize()
    {

        // GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = resolution;


        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullSize.rect.width);

        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fullSize.rect.height);
        
        tr.anchoredPosition = Vector2.zero;
    }
    public void Restore()
    {
        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, smallSize.rect.width);

        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, smallSize.rect.height);

        tr.anchoredPosition = Vector2.zero;
    }

    public void MaxMiniSIze()
    {
        if (isMax)
        {
            Restore();
        }
        else
        {
            Maximize();
        }
        isMax = !isMax;
    }
    public void Quit()
    {
        //Destroy(gameObject);
        Taskbar.instance.Delete_Obj(gameObject, isPrefab);
        f.go = null;
    }


    // Animation Trigger
    public void Disable()
    {
        gameObject.SetActive(false);
        num = 0;
    }
    public void Active()
    {
        gameObject.SetActive(true);
        isDisable = false;
        tr.SetAsLastSibling();
    }

    private void OnEnable()
    {

        //tr.anchoredPosition = pos;
        tr.anchoredPosition = Vector2.zero;
    }
}
