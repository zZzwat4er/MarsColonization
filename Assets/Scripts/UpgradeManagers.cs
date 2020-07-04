using UnityEngine;
using UnityEngine.UI;

public class UpgradeManagers : MonoBehaviour
{
    [SerializeField] private GameLogic _;
    private int[] lvls = {0, 0};

    public int TimeLVL
    {
        get => lvls[0];
        set => lvls[0] = value;
    }
    
    public int multiLVL
    {
        get => lvls[1];
        set => lvls[1] = value;
    }
    
    [SerializeField] private Button[] upgrades;

    public void onShow()
    {
        Debug.Log("onShow UpgradeBuild In");

        upgrades[0].GetComponentsInChildren<Text>()[1].text = "Цена: " + _currentBuilding.NextCommonUpgradeCost[i] 
                                                                       + "G\nДоход: ";
        upgrades[1].GetComponentsInChildren<Text>()[1].text = "Цена: " + _currentBuilding.NextCommonUpgradeCost[i] 
                                                                       + "G\nДоход: ";
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
                    if(_currentBuilding.Lvl >= 10)
                    {
                        _currentBuilding.coef += 0.2f;
                        _currentBuilding.NextCommonUpgradeCost[type] *= 2;
                    }
                    break;
                case 1:
                    if(_currentBuilding.Lvl >= 25)
                    {
                        _currentBuilding.coef += 0.5f;
                        _currentBuilding.NextCommonUpgradeCost[type] *= 5;
                    }
                    break;
                case 2:
                    if(_currentBuilding.Lvl >= 50)
                    {
                        _currentBuilding.coef += 1f;
                        _currentBuilding.NextCommonUpgradeCost[type] *= 10;
                    }
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
            if (_currentBuilding.NextCommonUpgradeCost[i] > _.Money ||
                levelsRcuaers[i] > _currentBuilding.Lvl) upgrades[i].interactable = false;//если денег не хватает, то выключаем кнопку
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