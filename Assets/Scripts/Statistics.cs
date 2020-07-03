using System;
using System.Numerics;
using Game_Classes;
using UnityEngine;
using UnityEngine.UI;


public class Statistics : MonoBehaviour
{
    [Header("Экран Статистики")]
    [SerializeField]private GameObject statObject;
    
    [Header("Поля Статистики")] [SerializeField]
    private  Text inGameTimeWholeText, inGameTimeAfetrResetText, totalGText, 
        totalGAfterResetText, totalSpendGText, totalSpendGAfterResetText;
    
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
    
    //функция абдейта статистики (Вызывать тольлко пока экран статистики активен на постоянной основе)
    //(В остальное время функция не активна)
    public void info_update()
    {
        TimeUpdate();
        inGameTimeWholeText.text = "Time played (in total): " + inGameTimeWhole;
        inGameTimeAfetrResetText.text = "Time played (this reset): " + inGameTimeAfetrReset;
        totalGText.text = "G received (in total): " + BigToShort.Convert(totalG.ToString());
        totalGAfterResetText.text = "G received (this reset): " + BigToShort.Convert(totalGAfterReset.ToString());
        totalSpendGText.text = "G spent (in total): " + BigToShort.Convert(totalSpendG.ToString());
        totalSpendGAfterResetText.text = "G spent (this reset):  " + BigToShort.Convert(totalSpendGAfterReset.ToString());
    }

    //функция для получение статистики из сейва (чтобы убрать ве эти лишнее строки из gameLogic)
    public static void statLoad(Save save)
    {
        totalG = save.totalG;
        totalGAfterReset = save.totalGAfterReset;
        totalSpendG = save.totalSpendG;
        totalSpendGAfterReset = save.totalSpendGAfterReset;
        inGameTimeWhole = save.inGameTimeWhole;
        inGameTimeAfetrReset = save.inGameTimeAfetrReset;
    }
}
