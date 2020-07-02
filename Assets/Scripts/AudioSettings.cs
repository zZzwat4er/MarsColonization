using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Объекты со звуком")] [SerializeField]
    private AudioSource backgroundMusic, handClickSound, buySound;

    [Header("Кнопки в настройках")] [SerializeField]
    private Button musicButton, soundButton;

    public void switchMusic()
    {
        //функция выключает или включает музыку на бэкграунде
        switcher(backgroundMusic);
        musicButton.GetComponentInChildren<Text>().text =
            (backgroundMusic.enabled == true ? "Music: on" : "Music: off");
    }

    void switcher(AudioSource src)
    {
        //переключает состояние аудио
        src.enabled = !src.enabled;
        Debug.Log("Switched");
    }

    public void switchSound()
    {
        //функция включает или выключает звуки
        switcher(buySound);
        switcher(handClickSound);
        soundButton.GetComponentInChildren<Text>().text =
            (buySound.enabled == true ? "Sound: on" : "Sound: off");
    }
    
}
