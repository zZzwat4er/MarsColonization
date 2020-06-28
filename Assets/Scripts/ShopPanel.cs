using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject handClickPanel;
    [SerializeField] private GameObject BuildUp;
    public void showShop()
    {
        
        shopPanel.SetActive(!shopPanel.active);
        handClickPanel.SetActive(false);
        BuildUp.SetActive(false);
        
    }

    public void showClick()
    {
        handClickPanel.SetActive(!handClickPanel.active);
    }

    public void showBuildUp()
    {
        BuildUp.SetActive(!handClickPanel.active);
    }
}
