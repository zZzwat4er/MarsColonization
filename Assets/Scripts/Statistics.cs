using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class Statistics : MonoBehaviour
{
    private static TimeSpan timeFromLastUpdate = new TimeSpan(0, 0, 0);
    public static TimeSpan inGameTimeWhole, inGameTimeAfetrReset;
    public static BigFloat totalG, totalGAfterReset, totalSpendG, totalSpendGAfterReset;
    //добавление времени к статистике
    public static void TimeUpdate(TimeSpan update)
    {
        inGameTimeWhole.Add(update);
        inGameTimeAfetrReset.Add(update);
    }
    // обновление времени в течении работы игры
    public static void TimeUpdate()
    {
        inGameTimeWhole += new TimeSpan(0, 0, (int)Time.fixedTime) - timeFromLastUpdate;
        inGameTimeAfetrReset += new TimeSpan(0, 0, (int)Time.fixedTime) - timeFromLastUpdate;
        timeFromLastUpdate = new TimeSpan(0, 0, (int)Time.fixedTime);
    }
}
