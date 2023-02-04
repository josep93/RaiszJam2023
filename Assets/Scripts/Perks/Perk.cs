using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk
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

    private static HashSet<short> activePerks = new HashSet<short>();

    protected short[] resistanceBonuses = new short[5];

    public short[] ResistanceBonuses { get => resistanceBonuses; }
    public static HashSet<short> ActivePerks { get => activePerks; }

    public virtual void Run()
    {
    }


        
    /// <summary>
    /// Instantiates the perk as the selected one
    /// </summary>
    /// <param name="perkChosen">Choosen perk Id</param>
    public void PerkChosen(PerkEnum perkChosen)
    {
        activePerks.Add((short)perkChosen);
        switch (perkChosen)
        {
            case PerkEnum.BranchFine:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Drought] -= 2;
                    return;
                }
            case PerkEnum.BranchThick:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 2;
                    return;
                }
            case PerkEnum.Cork:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 3;

                    resistanceBonuses[(short)ResistancesEnum.Impact] -= 2;
                    return;
                }
            case PerkEnum.FlexibleStem:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += -2;

                    resistanceBonuses[(short)ResistancesEnum.Drought] -= 2;
                    return;
                }
            case PerkEnum.GenericCold:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 1;
                    return;
                }
            case PerkEnum.GenericDrought:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 1;
                    return;
                }
            case PerkEnum.GenericHeat:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 1;
                    return;
                }
            case PerkEnum.GenericImpact:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 1;

                    return;
                }
            case PerkEnum.GenericTorsion:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 1;

                    return;
                }
            case PerkEnum.LeavesEmpty:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Drought] -= 1;
                    return;
                }
            case PerkEnum.LeavesFull:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 1;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return;
                }
            case PerkEnum.LeavesWaxed:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 2;
                    return;
                }
            case PerkEnum.RootAdventitious:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 4;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return;
                }
            case PerkEnum.RootLateral:
                {
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 2;
                    return;
                }
            case PerkEnum.RootMain:
                {
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 4;
                    resistanceBonuses[(short)ResistancesEnum.Torsion] += 4;
                    return;
                }
            case PerkEnum.SapDense:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 5;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 2;
                    return;
                }
            case PerkEnum.Thorns:
                {
                    resistanceBonuses[(short)ResistancesEnum.Heat] += 2;
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 4;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return;
                }
            case PerkEnum.Wood:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 1;
                    return;
                }
            case PerkEnum.WoodDense:
                {
                    resistanceBonuses[(short)ResistancesEnum.Impact] += 2;
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 2;

                    resistanceBonuses[(short)ResistancesEnum.Cold] -= 1;
                    return;
                }
            case PerkEnum.WoodHard:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 3;
                    resistanceBonuses[(short)ResistancesEnum.Drought] += 3;

                    resistanceBonuses[(short)ResistancesEnum.Impact] -= 1;
                    return;
                }
            case PerkEnum.WoodWarm:
                {
                    resistanceBonuses[(short)ResistancesEnum.Cold] += 4;

                    resistanceBonuses[(short)ResistancesEnum.Heat] -= 1;
                    return;
                }
        }
    }
}
