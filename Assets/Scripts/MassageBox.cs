﻿using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class MassageBox : MonoBehaviour
{
    [SerializeField] private Metamechanics metamech;

    [SerializeField] private GameObject messageBox;// massage box it self
    [SerializeField] private Text message;
    [SerializeField] private Button yes;
    [SerializeField] private Button quit;

    private int currentState = 0;
    private int zerosCount = 0;

    private void Start()
    {
        
    }


    void showMessage(string text, string qtMessage,  string yesMessage = null)
    {
        message.text = text;
        Debug.Log("msg: " + yesMessage);
        if(yesMessage == null) yes.gameObject.SetActive(false);
        else
        {
            Debug.Log("not null");
            yes.gameObject.SetActive(true);
            yes.GetComponentInChildren<Text>().text = yesMessage;
        }
        quit.GetComponentInChildren<Text>().text = qtMessage;
        
        messageBox.SetActive(true);
        
    }
    

    public void quitButton()
    {
        
        messageBox.SetActive(false);
    }

    public void yesButton()
    {
        switch (currentState)
        {
            case 1:
                metamech.gameReset(zerosCount);
                break;
            default:
                Debug.LogError("Unexpected massage box state");
                break;
        }
        messageBox.SetActive(false);
    }

    public void showIncome(BigInteger income, TimeSpan time)
    {
        showMessage("За прошедшие " + time + " вы получили " + income + " G.","ok");
    }

    public void showEvent(int index)
    {
        
        
        //todo: events texts
        showMessage("Show text for event: " + index, "ok");
    }

    public void showPrestige(BigInteger money)
    {
        zerosCount = 0;
        while (money > 0)
        {
            money /= 10;
            zerosCount++;
        }
        if(zerosCount < 4) return;
        
        currentState = 1;
        //todo prestige text
        showMessage("Show text for prestige", "no", "yes");
    }

}
