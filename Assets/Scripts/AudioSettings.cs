using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Объекты со звуком")] [SerializeField]
    private AudioSource backgroundMusic, handClickSound, buySound;

    [Header("Текст в настройках")] [SerializeField]
    private Text musicButton, soundButton;

    [Header("Ползунки громкости")] [SerializeField]
    private Slider music, sound;

    
    
    public void switchMusic(float value)
    {
        //функция выключает или включает музыку на бэкграунде
        switcher(backgroundMusic, value);
        musicButton.text =
            "Music: " + (int) (value * 100) + "%";
    }

    void switcher(AudioSource src, float value)
    {
        //переключает состояние аудио
        src.volume = value;
        Debug.Log("Switched");
    }

    public void switchSound( float value)
    {
        //функция включает или выключает звуки
        switcher(buySound, value);
        switcher(handClickSound, value);
        soundButton.text =
           "Sound: "+ (int) (value * 100) + "%";
    }

    private void Update()
    {
        switchMusic(music.value);
        switchSound(sound.value);
    }
}
