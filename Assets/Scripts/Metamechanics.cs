using System;
using Game_Classes;
using UnityEngine;
using Random = System.Random;

public class Metamechanics : MonoBehaviour
{

    [SerializeField] private GameLogic _;
    
    //ресет игры
    public void gameReset(int resetBoost)
    {
        _.Money = 0;
        Statistics.totalGAfterReset = 0;
        Statistics.totalSpendGAfterReset = 0;
        Statistics.inGameTimeAfetrReset = new TimeSpan(0, 0, 0);
        _.buildingsInit(resetBoost);
        //todo: improve handclick "base_income"
        _.GetComponent<UpgradeHandClick>().HandClicker = new HandClicker(_.Buildings);
    }
    //сиграть эвент
    public void playEvent()
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        if (rnd.Next(0, 2) == 0)
        {
            _.events.Add(new Events(2 * rnd.Next(0, 4), _.Buildings));
        }
        else
        {
            _.events.Add(new Events(2 * rnd.Next(0, 3) + 1, _.Buildings));
        }
    }

}