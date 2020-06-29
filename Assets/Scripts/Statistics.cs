using System;
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
        inGameTimeWholeText.text = "Время в игре: " + inGameTimeWhole;
        inGameTimeAfetrResetText.text = "Время в игре послу сброса: " + inGameTimeAfetrReset;
        totalGText.text = "Всего G заработанно: " + totalG;
        totalGAfterResetText.text = "G заработанное после сброса: " + totalGAfterReset;
        totalSpendGText.text = "Всего G потрачено: " + totalSpendG;
        totalSpendGAfterResetText.text = "G потрачено после сброса: " + totalSpendGAfterReset;
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
