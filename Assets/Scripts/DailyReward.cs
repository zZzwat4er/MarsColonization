using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    
    [SerializeField] private GameLogic _;
    
    // Rewards =======================================================================================================//
    /*
    0) G+ - премиум валюта
    1) xb UP - бесплатное улучшение здания типа x, покупка при его отсутствии
    2) +x% от банка - добавление к имеющимся средствам на счете x% от суммы

    1 G+ ->             +10% от банка -> 1b UP ->          4G+ ->
    +15% от банка ->    +20% от банка -> 2b UP ->          7 G+ -> 
    +25% от банка ->    3b UP ->         10 G+ ->          13 G+ ->
    +30% от банка ->    15 G+ ->         4b UP ->          +35% от банка ->
    +40% от банка ->    20 G+ ->         5b UP ->          6b UP ->
    25 G+ ->            7b UP ->         +45% от банка ->  8b UP ->
    30 G+ ->            9b UP ->         +50% от банка ->  10b UP -> 
    40 G+ ->            11b UP ->        50G+ ->           +60% от банка -> 
    55 G+ ->            12b UP ->        +70% от банка ->  70 G+ -> 
    13b UP ->           +90% от банка -> 14b UP ->         100 G+
    */
    private Reward[] rewards =
    {
        new Reward(0, 1), new Reward(2, 10), new Reward(1, 1), new Reward(0, 4), 
        new Reward(2, 15), new Reward(2, 20), new Reward(1, 2), new Reward(0, 7), 
        new Reward(2, 25), new Reward(1, 3), new Reward(0, 10), new Reward(0, 13), 
        new Reward(2, 30), new Reward(0, 15), new Reward(1, 4), new Reward(2, 35), 
        new Reward(2, 40), new Reward(0, 20), new Reward(1, 5), new Reward(1, 6), 
        new Reward(0, 25), new Reward(1, 7), new Reward(2, 45), new Reward(1, 8), 
        new Reward(0, 30), new Reward(1, 9), new Reward(2, 50), new Reward(1, 10), 
        new Reward(0, 40), new Reward(1, 11), new Reward(0, 50), new Reward(2, 60), 
        new Reward(0, 55), new Reward(1, 12), new Reward(2, 70), new Reward(0, 70), 
        new Reward(1, 13), new Reward(2, 90), new Reward(1, 14), new Reward(0, 100) 
    };

    private string[] messages =
    {
        "You are playing for the {0} day in a row.\nYou get {1} G+",
        "You are playing for the {0} day in a row.\nYou can improve {1} for free",
        "You are playing for the {0} day in a row.\nYou get + {1}%G from your balance"
    };
    //================================================================================================================//
    
    private DateTime now = DateTime.Now;
    private int currentState;
    
    public DateTime lastDay;

    private void Start()
    {
        Save save = SaveSystem.load();
        if (save != null)
        {
            lastDay = save.lastDailyReward;
            TimeSpan deltaTime = now.Subtract(lastDay);
            Debug.Log(deltaTime);
            if (deltaTime > new TimeSpan(1, 0, 0, 0) &&
                deltaTime < new TimeSpan(2, 0, 0, 0))
            {
                currentState++;
                if (currentState >= rewards.Length) currentState = 0;
                Debug.Log("enter the reward 1");
                showDailyReward();
                getReward();
            }
            else if (deltaTime > new TimeSpan(2, 0, 0, 0))
            {
                currentState = 0;
                Debug.Log("enter the reward 2");
                showDailyReward();
                getReward();
            }
            else
            {
                
                return;
            }
        }
        else
        {
            currentState = 0;
            Debug.Log("enter the reward 3");
            showDailyReward();
            getReward();
        }
        
    }


    public void exitBT()
    {
        
    }

    public void showDailyReward()
    {
        
       _.msgShower.GetComponent<CallMessageBox>().showMessageWithTitle("Daily Reward", String.Format(messages[rewards[currentState].RewardType], currentState, rewards[currentState].Rew), "GET IT");
    }

    private void getReward()
    {
        Debug.Log("enter the reward");
        switch (rewards[currentState].RewardType)
        {
            case 0:
                _.GPrime += rewards[currentState].Rew;
                break;
            case 1:
                _.Buildings[_.CurrentBuilding].upgrade();
                break;
            case 2:
                _.Money += _.Money * rewards[currentState].Rew / 100;
                break;
            default:
                Debug.LogError("Incorrect Reward Type: " +
                               "\n get type -> " + rewards[currentState].RewardType +
                               "\n reward index -> " + currentState);
                break;
        }
    }
}
