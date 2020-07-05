using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Game_Classes;
using UnityEngine;

public class CallMessageBox : MonoBehaviour
{
    [SerializeField] private GameObject msgBox, titleBox;
    
    [SerializeField] private Metamechanics metamech;
    
    public void showMessage(string text, string qtMessage,  string yesMessage = null)
    {
        var tmpMsgBox = Instantiate(msgBox, this.transform);
        tmpMsgBox.transform.SetParent(this.transform);
        tmpMsgBox.GetComponent<MassageBox>().metamech = this.metamech;
        tmpMsgBox.GetComponent<MassageBox>().showMessage(text, qtMessage, yesMessage);
    }

    public void showMessageWithTitle(string title, string text, string btnText)
    {
        var tmp = Instantiate(titleBox, this.transform);
        tmp.transform.SetParent(this.transform);
        tmp.GetComponent<GetTypeMsgBox>().showMessage(title, text, btnText);
    }
    
    private void showMessage(GameObject tmpMsgBox, string text, string qtMessage,  string yesMessage = null)
    {
        tmpMsgBox.GetComponent<MassageBox>().showMessage(text, qtMessage, yesMessage);
    }

    public void showIncome(BigInteger income, TimeSpan time)
    {
        showMessage("You’ve been gone for " + time + " and got " + BigToShort.Convert(income) + " G.","ok");
    }

    public void showEvent(int index)
    {
        
        
        //todo: events texts
        showMessage("Show text for event: " + index, "ok");
    }

    public void showPrestige(BigInteger money)
    {
        var tmp =  Instantiate(msgBox, this.transform);
        tmp.transform.SetParent(this.transform);
        tmp.GetComponent<MassageBox>().metamech = this.metamech;
        tmp.GetComponent<MassageBox>().showPrestige(money);
        
    }
}
