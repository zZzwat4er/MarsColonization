using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class MassageBox : MonoBehaviour
{

    [SerializeField] private GameObject massageBox;// massage box it self
    [SerializeField] private Text massage;
    [SerializeField] private Button yes;
    [SerializeField] private Button quit;

    private int currentState = 0;

    private void Start()
    {
        yes.gameObject.SetActive(false);
    }

    public void quitMB()
    {
        yes.gameObject.SetActive(false);
        massageBox.SetActive(false);
    }

    public void yesButton()
    {
        switch (currentState)
        {
            case 1:
                //todo: prestige function
                break;
            default:
                Debug.LogError("Unexpected massage box state");
                break;
        }
    }

    public void showIncome(BigInteger income, TimeSpan time)
    {
        quit.GetComponentInChildren<Text>().text = "ok";
        massageBox.SetActive(true);
        massage.text = "За прошедшие " + time + " вы получили " + income + " G.";
    }

    public void showEvent(int index)
    {
        quit.GetComponentInChildren<Text>().text = "ok";
        massageBox.SetActive(true);
        //todo: events texts
        massage.text = "Show text for event: " + index;
    }

    public void showPrestige()
    {
        quit.GetComponentInChildren<Text>().text = "no";
        yes.GetComponentInChildren<Text>().text = "yes";
        currentState = 1;
        yes.gameObject.SetActive(true);
        massageBox.SetActive(true);
        //todo prestige text
        massage.text = "Show text for prestige";
    }

}
