using System;
using Game_Classes;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuildings : MonoBehaviour
{
    [SerializeField] private GameLogic _;
    private Building _currentBuilding;

    [SerializeField] private Button[] upgrades;
    [SerializeField] private Text NameBuild;
    public void onShow()
    {
        Debug.Log("onShow UpgradeBuild In");
        _currentBuilding = _.Buildings[_.CurrentBuilding];
        
        for(int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].GetComponentsInChildren<Text>()[1].text = "Цена: " + _currentBuilding.NextCommonUpgradeCost[i] 
                                                                           + "G\nДоход: ";
            if (i == 0)
            {
                upgrades[i].GetComponentsInChildren<Text>()[1].text += "20%";
            }
            else
            {
                upgrades[i].GetComponentsInChildren<Text>()[1].text += 50 * i + "%";
            }
        }
        _.update_info();
        Debug.Log("onShow UpgradeBuild out");
    }

    public void upgrade(int type)
    {
        if (_currentBuilding.NextCommonUpgradeCost[type] <= _.Money)
        {
            _.Money -= _currentBuilding.NextCommonUpgradeCost[type];
            Statistics.totalSpendG += _currentBuilding.NextCommonUpgradeCost[type];
            Statistics.totalSpendGAfterReset += _currentBuilding.NextCommonUpgradeCost[type];
            switch (type)
            {
                case 0:
                    _currentBuilding.coef += 0.2f;
                    _currentBuilding.NextCommonUpgradeCost[type] *= 10;
                    break;
                case 1:
                    _currentBuilding.coef += 0.5f;
                    _currentBuilding.NextCommonUpgradeCost[type] *= 50;
                    break;
                case 2:
                    _currentBuilding.coef += 1f;
                    _currentBuilding.NextCommonUpgradeCost[type] *= 100;
                    break;
                default:
                    break;
            }
            _currentBuilding.recalculateIncome();
            update_info();
        }
    }

    public void update_info()
    {
        _currentBuilding = _.Buildings[_.CurrentBuilding];
        NameBuild.text = "Улучшение на " + _currentBuilding.name;
        for (int i = 0; i < upgrades.Length; ++i)
        {
            if (_currentBuilding.NextCommonUpgradeCost[i] > _.Money) upgrades[i].interactable = false;//если денег не хватает, то выключаем кнопку
            else upgrades[i].interactable = true;//а если хватает, то включаем
            upgrades[i].GetComponentsInChildren<Text>()[1].text = "Цена: " + _currentBuilding.NextCommonUpgradeCost[i] 
                                                                           + "G\nДоход: ";
            if (i == 0)
            {
                upgrades[i].GetComponentsInChildren<Text>()[1].text += "20%";
            }
            else
            {
                upgrades[i].GetComponentsInChildren<Text>()[1].text += 50 * i + "%";
            }
        }
    }
}