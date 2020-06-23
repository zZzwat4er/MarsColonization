using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BuyBuilding : MonoBehaviour
{
    [SerializeField] private GameObject game_info;

    public void buy_building()
    {
        
        var _ = game_info.GetComponent<GameLogic>();
        if (_.Money >= _.Buildings[_.CurrentBuilding].NextCost)
        {//если достаточно денег, то проводим оперцию покупки здания
            _.Money -= _.Buildings[_.CurrentBuilding].NextCost;
            _.Buildings[_.CurrentBuilding].upgrade();
            if (!_.Buildings[_.CurrentBuilding].IsAvaliable)
            {
                /*если здание до этого не было купленым, то выставляем ему начальный тик
                 если мы не будем делать эту проверку, то каждый раз когда будет апгрейд здания, будет обнуляться таймер
                 */
                _.Buildings[_.CurrentBuilding].startWorkAt = new TimeSpan(DateTime.Now.Ticks);
            }
            _.Buildings[_.CurrentBuilding].IsAvaliable = true;
            _.update_info();
            
        }
    }

   
}
