using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel, handClickPanel,BuildUp, Boosts, Managers;
    public void showShop()
    {
        
        shopPanel.SetActive(!shopPanel.active);
        handClickPanel.SetActive(false);
        BuildUp.SetActive(false);
        Boosts.SetActive(false);
        Managers.SetActive(false);
        
    }

    public void Hide()
    {
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
