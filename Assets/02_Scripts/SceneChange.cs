using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public string transferMapName; //�ӽ� �̵��� ���� �̸�

    public void _StartBtn()
    {
        SceneManager.LoadScene("Select Episode");
    }
    public void _ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

}
