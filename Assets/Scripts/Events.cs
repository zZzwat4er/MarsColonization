﻿using System;
using Game_Classes;
using UnityEngine;
using Random = System.Random;

public class Events
{ 
    private int index;//индех эвента (вроде пока не нужен но пусть будет)
    private float time;//время до конца эвент
    private float modification;//модификаторы 
    private int[] buildsIndexes;//индексы зданий на которые распростроняются модификаторы(индей хенд клика = кол-во зданий)
    private Building[] businesses;//массив зданий

    public Events(int index, Building[] buildings)
    {
        this.index = index;
        businesses = buildings;
        eventSetup();
    }

    #region geter - seter
    public float Time
    {
        get => time;
        set => time = value;
    }
    public int Index => index;
    //инфа для вычесления модификаторов
    public Tuple<int[], float> getInfo() { return new Tuple<int[], float>(buildsIndexes, modification); }
    #endregion

    
/*
 * eventType = true/false = хорошое/плохое событие соответственно.
 * eventMod:
 *     0/1) удвоение эфективности или полная остановка на 10 мин (Вероятно изменение именно приносимых денег)
 *     2/3) Увеличение/Снижение производительности двух построек на 30% на 20 минут
 *     4/5) +/- 5% от текущей сумы денег
 *     6)   усиление тапа в 5 раз на 30 сек
 *     8/9) Увеличение ручного клика в 5 раз на 10 минут и в 3 раза на 20 минут.
 *     10/11) Ускорение автокликов в 10 раз на 5 минут и в 100 раз на 30 секунд.
 *     12)  Скип - получить прибыль за 300 секунд в один момент
*/
    private void eventSetup()
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        switch (index)
        {
            case 0:
                buildsIndexes = new int[1];
                buildsIndexes[0] = rnd.Next(0, businesses.Length);
                while(!businesses[buildsIndexes[0]].isAvaliable) buildsIndexes[0] = rnd.Next(0, businesses.Length);
                modification = 2;
                time = 600;
                break;
            case 1:
                buildsIndexes = new int[1];
                buildsIndexes[0] = rnd.Next(0, businesses.Length);
                while(!businesses[buildsIndexes[0]].isAvaliable) buildsIndexes[0] = rnd.Next(0, businesses.Length);
                modification = 0.0001f;
                time = 600;
                break;
            case 2:
                buildsIndexes = new int[2];
                buildsIndexes[0] = rnd.Next(0, businesses.Length);
                buildsIndexes[1] = rnd.Next(0, businesses.Length);
                while(!businesses[buildsIndexes[0]].isAvaliable) buildsIndexes[0] = rnd.Next(0, businesses.Length);
                while(!businesses[buildsIndexes[1]].isAvaliable) buildsIndexes[1] = rnd.Next(0, businesses.Length);
                modification = 1.3f;
                time = 1200;
                break;
            case 3:
                buildsIndexes = new int[2];
                buildsIndexes[0] = rnd.Next(0, businesses.Length);
                buildsIndexes[1] = rnd.Next(0, businesses.Length);
                while(!businesses[buildsIndexes[0]].isAvaliable) buildsIndexes[0] = rnd.Next(0, businesses.Length);
                while(!businesses[buildsIndexes[1]].isAvaliable) buildsIndexes[1] = rnd.Next(0, businesses.Length);
                modification = 0.7f;
                time = 1200;
                break;
            case 4:
                buildsIndexes = null;
                modification = 0.05f;
                time = 1;
                break;
            case 5:
                buildsIndexes = null;
                modification = -0.05f;
                time = 1;
                break;
            case 6:
                buildsIndexes = new[] { businesses.Length };
                modification = 5;
                time = 30;
                break;
            case 8:
                buildsIndexes = new[] { businesses.Length };
                modification = 5;
                time = 600;
                break;
            case 9:
                buildsIndexes = new[] { businesses.Length };
                modification = 3;
                time = 1200;
                break;
            case 10:
                buildsIndexes = new int[businesses.Length];
                for (int i = 0; i < businesses.Length; i++)
                {
                    buildsIndexes[i] = i;
                }
                modification = 10f;
                time = 300;
                break;
            case 11:
                buildsIndexes = new int[businesses.Length];
                for (int i = 0; i < businesses.Length; i++)
                {
                    buildsIndexes[i] = i;
                }
                modification = 10f;
                time = 30;
                break;
            case 12:
                buildsIndexes = new [] {businesses.Length + 1};
                
                modification = 300f;
                time = 1;
                break;
            default:
                time = -1f;
                break;
        }
    }

}