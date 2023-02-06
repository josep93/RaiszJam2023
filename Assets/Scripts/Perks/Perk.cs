using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk
{
    protected enum ResistancesEnum : short
    {
        Cold,
        Drought,
        Heat,
        Impact,
        Torsion
    }

    public enum PerkEnum : short
    {
        BranchFine,
        BranchThick,
        Cork,
        FlexibleStem,
        GenericCold,
        GenericDrought,
        GenericHeat,
        GenericImpact,
        GenericTorsion,
        LeavesEmpty,
        LeavesFull,
        LeavesWaxed,
        RootAdventitious,
        RootLateral,
        RootMain,
        SapDense,
        Thorns,
        Wood,
        WoodDense,
        WoodHard,
        WoodWarm
    }

    public static List<String> PerkStringList = new List<String>
    {
        "Branch fine",
        "Branch thick",
        "Cork",
        "Flexible stem",
        "More cold resistance",
        "More drought resistance",
        "More heat resistance",
        "More impact resistance",
        "More torsion resistance",
        "Leaves empty",
        "Leaves full",
        "Leaves waxed",
        "Root adventitious",
        "Root lateral",
        "Root main",
        "Sap dense",
        "Thorns",
        "Wood",
        "Wood dense",
        "Wood hard",
        "Wood warm"
    };

    public static void TestPerks()
    {
        activePerks.Add(PerkEnum.RootAdventitious);
        activePerks.Add(PerkEnum.RootLateral);
        activePerks.Add(PerkEnum.RootMain);


        activePerks.Add(PerkEnum.Thorns);
        activePerks.Add(PerkEnum.BranchFine);
        activePerks.Add(PerkEnum.BranchThick);
    }

    private static HashSet<PerkEnum> activePerks = new HashSet<PerkEnum>();
    public static HashSet<PerkEnum> ActivePerks { get => activePerks; }

    public void Run()
    {
    }
    public static List<PerkEnum> AvailablePerks()
    {
        List<PerkEnum> available = new List<PerkEnum>();
        foreach (PerkEnum perk in (PerkEnum[]) Enum.GetValues(typeof(PerkEnum)))
        {
            if (CheckViability(perk)) available.Add(perk);
        }
        return available;
    }
    private static bool CheckViability(PerkEnum perkChosen)
    {
        if (perkChosen == PerkEnum.GenericCold
            || perkChosen == PerkEnum.GenericDrought
            || perkChosen == PerkEnum.GenericHeat
            || perkChosen == PerkEnum.GenericImpact
            || perkChosen == PerkEnum.GenericTorsion) { }
        else if (activePerks.Contains(perkChosen)) return false;
        switch (perkChosen)
        {
            case PerkEnum.BranchFine:
                {
                    if (!activePerks.Contains(PerkEnum.BranchThick)) return false;
                    return true;
                }
            case PerkEnum.BranchThick:
                {
                    return true;
                }
            case PerkEnum.Cork:
                {
                    if (!activePerks.Contains(PerkEnum.Wood)) return false;
                    if (activePerks.Contains(PerkEnum.WoodDense)) return false;
                    if (activePerks.Contains(PerkEnum.WoodHard)) return false;
                    if (activePerks.Contains(PerkEnum.WoodWarm)) return false;
                    return true;
                }
            case PerkEnum.FlexibleStem:
                {
                    if (activePerks.Contains(PerkEnum.Wood)) return false;
                    return true;
                }
            case PerkEnum.GenericCold:
                {
                    return true;
                }
            case PerkEnum.GenericDrought:
                {
                    return true;
                }
            case PerkEnum.GenericHeat:
                {
                    return true;
                }
            case PerkEnum.GenericImpact:
                {
                    return true;
                }
            case PerkEnum.GenericTorsion:
                {
                    return true;
                }
            case PerkEnum.LeavesEmpty:
                {
                    if (activePerks.Contains(PerkEnum.LeavesFull)) return false;
                    if (activePerks.Contains(PerkEnum.LeavesWaxed)) return false;
                    if (!activePerks.Contains(PerkEnum.Thorns)) return false;
                    return true;
                }
            case PerkEnum.LeavesFull:
                {
                    if (activePerks.Contains(PerkEnum.LeavesEmpty)) return false;
                    return true;
                }
            case PerkEnum.LeavesWaxed:
                {
                    if (activePerks.Contains(PerkEnum.LeavesEmpty)) return false;
                    return true;
                }
            case PerkEnum.RootAdventitious:
                {
                    if (!activePerks.Contains(PerkEnum.RootMain)) return false;
                    return true;
                }
            case PerkEnum.RootLateral:
                {
                    if (!activePerks.Contains(PerkEnum.RootMain)) return false;
                    return true;
                }
            case PerkEnum.RootMain:
                {
                    return true;
                }
            case PerkEnum.SapDense:
                {
                    return true;
                }
            case PerkEnum.Thorns:
                {
                    return true;
                }
            case PerkEnum.Wood:
                {
                    if (activePerks.Contains(PerkEnum.FlexibleStem)) return false;
                    return true;
                }
            case PerkEnum.WoodDense:
                {
                    if (!activePerks.Contains(PerkEnum.Wood)) return false;
                    if (activePerks.Contains(PerkEnum.WoodHard)) return false;
                    if (activePerks.Contains(PerkEnum.WoodWarm)) return false;
                    if (activePerks.Contains(PerkEnum.Cork)) return false;
                    return true;
                }
            case PerkEnum.WoodHard:
                {
                    if (!activePerks.Contains(PerkEnum.Wood)) return false;
                    if (activePerks.Contains(PerkEnum.WoodDense)) return false;
                    if (activePerks.Contains(PerkEnum.WoodWarm)) return false;
                    if (activePerks.Contains(PerkEnum.Cork)) return false;
                    return true;
                }
            case PerkEnum.WoodWarm:
                {
                    if (!activePerks.Contains(PerkEnum.Wood)) return false;
                    if (activePerks.Contains(PerkEnum.WoodDense)) return false;
                    if (activePerks.Contains(PerkEnum.WoodHard)) return false;
                    if (activePerks.Contains(PerkEnum.Cork)) return false;
                    return true;
                }
        }
        return false;
    
    }
    /// <summary>
    /// Instantiates the perk as the selected one
    /// </summary>
    /// <param name="perkChosen">Choosen perk Id</param>
    public static short[] PerkChosen(PerkEnum perkChosen)
    {
        short[] resistanceBonuses = new short[5];
        activePerks.Add(perkChosen);
        switch (perkChosen)
        {

            case PerkEnum.BranchFine:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Drought] -= 2;
                    return resistanceBonuses;
                }
            case PerkEnum.BranchThick:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 2;
                    return resistanceBonuses;
                }
            case PerkEnum.Cork:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 3;

                    resistanceBonuses[(short)ResistancesEnum.Impact] -= 2;
                    return resistanceBonuses;
                }
            case PerkEnum.FlexibleStem:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 3;

                    resistanceBonuses[(short)ResistancesEnum.Drought] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.GenericCold:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 1;
                    return resistanceBonuses;
                }
            case PerkEnum.GenericDrought:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 1;
                    return resistanceBonuses;
                }
            case PerkEnum.GenericHeat:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 1;
                    return resistanceBonuses;
                }
            case PerkEnum.GenericImpact:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 1;

                    return resistanceBonuses;
                }
            case PerkEnum.GenericTorsion:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 1;

                    return resistanceBonuses;
                }
            case PerkEnum.LeavesEmpty:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Drought] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.LeavesFull:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 1;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.LeavesWaxed:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 2;
                    return resistanceBonuses;
                }
            case PerkEnum.RootAdventitious:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 4;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.RootLateral:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 2;
                    return resistanceBonuses;
                }
            case PerkEnum.RootMain:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 4;
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 4;
                    return resistanceBonuses;
                }
            case PerkEnum.SapDense:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 5;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 2;
                    return resistanceBonuses;
                }
            case PerkEnum.Thorns:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 2;
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 4;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.Wood:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.WoodDense:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.WoodHard:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 3;
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 3;

                    resistanceBonuses[(short)ResistancesEnum.Impact] -= 1;
                    return resistanceBonuses;
                }
            case PerkEnum.WoodWarm:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 4;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 1;
                    return resistanceBonuses;
                }

        }
        return resistanceBonuses;
    }
}
