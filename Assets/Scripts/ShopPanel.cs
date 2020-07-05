using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel, handClickPanel,BuildUp, Boosts, Managers;
    private int currentIndex = -1;
    public void showShop(int index)
    {
        if(currentIndex == index || currentIndex == -1)
        {
            shopPanel.SetActive(!shopPanel.active);
            handClickPanel.SetActive(false);
            BuildUp.SetActive(false);
            Boosts.SetActive(false);
            Managers.SetActive(false);
        }
        else
        {
            shopPanel.SetActive(true);
            handClickPanel.SetActive(false);
            BuildUp.SetActive(false);
            Boosts.SetActive(false);
            Managers.SetActive(false);
        }

        currentIndex = index;
    }

    public void Hide()
    {
        currentIndex = -1;
        shopPanel.SetActive(false);
        handClickPanel.SetActive(false);
        BuildUp.SetActive(false);
        Boosts.SetActive(false);
        Managers.SetActive(false);
    }
    public void showClick()
    {
        handClickPanel.SetActive(!handClickPanel.activeInHierarchy);
    }

    public void showBuildUp()
    {
        BuildUp.SetActive(!handClickPanel.activeInHierarchy);
    }

    public void showBoost()
    {
        Boosts.SetActive(!Boosts.activeInHierarchy);
    }

    public void showManagers()
    {
        Managers.SetActive(!Managers.activeInHierarchy);
    }
}
