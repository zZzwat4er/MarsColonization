using System;
using UnityEngine;

public class Reward
{
    

    private int rewardType;
    private int rew;

    public Reward(int type, int rew)
    {
        rewardType = type;
        this.rew = rew;
    }

    public int Rew => rew;

    public int RewardType => rewardType;
}