using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Statistics : MonoBehaviour
{
    [SerializeField] private GameLogic _;
    public static float inGameTimeWhole, inGameTimeAfetrReset;
    public static BigFloat totalG, totalGAfterReset, totalSpendG, totalSpendGAfterReset;

    private void Update()
    {
        inGameTimeWhole += Time.deltaTime;
        inGameTimeAfetrReset += Time.deltaTime;
        totalG += (_.GPerSecond * ((int) (Time.deltaTime * 1000000))) / 1000000;
        totalGAfterReset += (_.GPerSecond * ((int) (Time.deltaTime * 1000000))) / 1000000;
    }
}
