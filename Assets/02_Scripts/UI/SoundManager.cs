using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGM, Effect;
    SettingManager Setting;

    float BGM_Volume, Effect_Volume;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Setting = SettingManager.instance;
    }

    void Update()
    {
        BGM.volume = Setting.BGM_Slider.value;
        Effect.volume = Setting.Effect_Slider.value;
    }

    public void Set_AudioClip(AudioClip clip)
    {

    }
}
