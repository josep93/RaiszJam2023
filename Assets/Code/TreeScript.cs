using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    //singleton
    public static TreeScript current;

    protected short[] resistanceBonuses = new short[5];

    List<Perk> perks;

    void Start()
    {
        current = this;
        perks = new List<Perk>();
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
