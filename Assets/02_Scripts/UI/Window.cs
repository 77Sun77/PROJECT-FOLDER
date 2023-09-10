using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public RectTransform bg; // 이름 변경 예정
    RectTransform tr;
    public bool isDisable;
    float num;
    Vector3 pos;
    Animator anim;
    void Awake()
    {
        tr = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisable)
        {
            num += Time.deltaTime*9;
            tr.anchoredPosition = Vector3.Lerp(pos,new Vector3(0, -400), num);
            if(num > 1)
            {
                isDisable = false;
                num = 0;
            }
        }   
    }
    public void Minimization()
    {
        pos = tr.anchoredPosition;
        isDisable = true;
        anim.SetTrigger("Disable");
    }
    public void Maximize()
    {

        // GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = resolution;


        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bg.rect.width);

        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, bg.rect.height);
        
        tr.anchoredPosition = Vector2.zero;
    }
    public void Quit()
    {
        Destroy(gameObject);
    }


    // Animation Trigger
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Active()
    {

    }

    private void OnEnable()
    {
        tr.anchoredPosition = pos;
    }
}
