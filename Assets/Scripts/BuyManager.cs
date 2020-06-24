using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField] private GameObject game_info;
    [SerializeField] private int multiplier = 25;
    public void buy_manager()
    {
        var _ = game_info.GetComponent<GameLogic>();
        var build = _.Buildings[_.CurrentBuilding];
        if (_.Money >= build.nextManagerCost && build.Lvl >= 10 
                                                                      && build.upgradeCount<99 && build.IsAvaliable)
        {
            build.upgradeCount++;
            _.Money -= build.nextManagerCost;
            build.nextManagerCost *= multiplier;
            build.Time_ = build.baseTime - (build.baseTime / 100) * build.upgradeCount;

            

            
            _.update_info();
            
        }
    }
}
