using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    //singleton
    public static TreeScript current;

    protected short[] resistanceBonuses = new short[5];
    private List<Perk> perks;
    private List<Perk.PerkEnum> upgradablePerks; 

    void Start()
    {
        current = this;
        perks = new List<Perk>();
    }

   bool UpgradablePerks()
    {
        upgradablePerks = new List<Perk.PerkEnum>();
        List<Perk.PerkEnum> availablePerks = Perk.AvailablePerks();
        System.Random random = new System.Random();

        int options = 3;
        options = availablePerks.Count < options ? availablePerks.Count : options;

        if (options <= 0) return false;

        for(int i = 0; i<options; i++)
        {
            int integer = random.Next(0,availablePerks.Count-1);
            upgradablePerks.Add(availablePerks[integer]);
            availablePerks.Remove(availablePerks[integer]);
        }

        return true;
    }

    /// <summary>
    /// Updates the Bonuses array. Should be used in round startup.
    /// </summary>
    public void UpdateBonuses()
    {
        Array.Clear(resistanceBonuses,0,resistanceBonuses.Length);
        foreach (Perk perk in perks)
        {
            for (int i = 0; i<5; i++)
            {
                resistanceBonuses[i] += perk.ResistanceBonuses[i];
            }
        }
    }

    /// <summary>
    /// Run additional effects of perks
    /// </summary>
    public void RunPerks()
    {
        foreach (Perk perk in perks)
        {
            perk.Run();
        }
    }
}
