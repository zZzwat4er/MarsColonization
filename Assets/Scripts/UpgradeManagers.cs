using System.Numerics;
using Game_Classes;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManagers : MonoBehaviour
{
    [SerializeField] private GameLogic _;
    //уровни менеджеров
    //1) не активное время
    //2) множетель на не активное время
    private static int[] lvls = {0, 0};

    public static int TimeLVL
    {
        get => lvls[0];
        set => lvls[0] = value;
    }
    
    public static int multiLVL
    {
        get => lvls[1];
        set => lvls[1] = value;
    }
    
    [SerializeField] private Button[] upgrades;

    public void onShow()
    {
        Debug.Log("onShow UpgradeBuild In");

        upgrades[0].GetComponentsInChildren<Text>()[1].text = "Price: " + BigToShort.Convert(1000000 * BigInteger.Pow(10, TimeLVL)) 
                                                                       + "G\nAdds: + 1 hour In not active time";
        upgrades[1].GetComponentsInChildren<Text>()[1].text = "Price: " + BigToShort.Convert(1000000 * BigInteger.Pow(10, multiLVL)) 
                                                                       + "G\nAdds: ";
        _.update_info();
        Debug.Log("onShow UpgradeBuild out");
    }

    public void upgrade(int type)
    {
        if (1000000 * BigInteger.Pow(10, lvls[type]) <= _.Money)
        {
            _.Money -= 1000000 * BigInteger.Pow(10, lvls[type]);
            Statistics.totalSpendG += 1000000 * BigInteger.Pow(10, lvls[type]);
            Statistics.totalSpendGAfterReset += 1000000 * BigInteger.Pow(10, lvls[type]);
            lvls[type]++;
            update_info();
        }
    }

    public void update_info()
    {
        
        for (int i = 0; i < upgrades.Length; ++i)
        {
            if (1000000 * BigInteger.Pow(10, lvls[i]) > _.Money) upgrades[i].interactable = false;//если денег не хватает, то выключаем кнопку
            else upgrades[i].interactable = true;//а если хватает, то включаем
            
            upgrades[0].GetComponentsInChildren<Text>()[1].text = "Price: " + BigToShort.Convert(1000000 * BigInteger.Pow(10, TimeLVL)) 
                                                                            + "G\nAdds: + 1 hour In not active time";
            upgrades[1].GetComponentsInChildren<Text>()[1].text = "Price: " + BigToShort.Convert(1000000 * BigInteger.Pow(10, multiLVL)) 
                                                                            + "G\nAdds: ";
        }
    }
}