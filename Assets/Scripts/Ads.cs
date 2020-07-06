using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Advertisements;
public class Ads : MonoBehaviour
{

    private string gameId = "3697725", type = "video";
    private bool testMode = true;
    [SerializeField] private GameLogic _;
    public void watchAd()
    {
        Advertisement.Initialize(gameId, testMode);

        while (!Advertisement.IsReady(type))
        {
            Thread.Sleep(1000);
        }
        Advertisement.Show(type);
        _.GPrime += 1;
    }
}
