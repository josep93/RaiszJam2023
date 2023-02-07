using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeScript : MonoBehaviour
{
    //singleton
    public static TreeScript current;

    private short[] resistanceBonuses = new short[5];
    private List<Perk> perks;
    private List<Perk.PerkEnum> upgradablePerks;

    public short[] ResistanceBonuses { get => resistanceBonuses; }

    public short Health = 40;

    void Start()
    {
        current = this;
        perks = new List<Perk>();
        TreeRenderScript.current.UpdateSprites();
    }

    public List<Perk.PerkEnum> UpgradablePerks()
    {
        upgradablePerks = new List<Perk.PerkEnum>();
        List<Perk.PerkEnum> availablePerks = Perk.AvailablePerks();

        

        int options = 3;
        options = availablePerks.Count < options ? availablePerks.Count : options;

        if (options <= 0) return availablePerks;

        for (int i = 0; i < options; i++)
        {
            int integer = Random.Range(0, availablePerks.Count); ;
            upgradablePerks.Add(availablePerks[integer]);
            availablePerks.Remove(availablePerks[integer]);
        }

        return upgradablePerks;
    }
        

    /// <summary>
    /// Updates the Bonuses array. Should be used in round startup.
    /// </summary>
    public void UpdateBonuses()
    {
        resistanceBonuses = new short[] { 0,0,0,0,0};
        foreach (Perk.PerkEnum perk in Perk.ActivePerks)
        {
            for (int i = 0; i<5; i++)
            {
                resistanceBonuses[i] += Perk.PerkChosen(perk)[i];
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
