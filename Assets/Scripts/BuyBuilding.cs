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
        {
            _.Money -= _.Buildings[_.CurrentBuilding].NextCost;
            _.Buildings[_.CurrentBuilding].upgrade();
           

            _.Buildings[_.CurrentBuilding].IsAvaliable = true;
            
            _.update_info();
            
        }
    }

   
}


 // if (money >= _buildings[current_building].NextCost)
 //        {
 //            money -= _buildings[current_building].NextCost;
 //            _buildings[current_building].upgrade();
 //            gPerSecond = 0;
 //            for(int i = 0; i < number_of_buildings; ++i)
 //                gPerSecond +=(((BigFloat) (_buildings[i].Income))/ (BigInteger)(_buildings[i].baseTime));
 //            print(gPerSecond);
 //            _buildings[current_building].IsAvaliable = true;
 //            BuyButton.GetComponentInChildren<Text>().text = "Улучшить";
 //            update_info();
 //        }