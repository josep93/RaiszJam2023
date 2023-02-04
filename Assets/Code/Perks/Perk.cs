using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk
{
    protected enum ResistancesEnum : short
    {
        Cold,
        Heat,
        Torsion,
        Impact,
        Drought
    }

    protected enum Perk : short
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

    protected short[] resistanceBonuses = new short[5];

    public short[] ResistanceBonuses { get => resistanceBonuses; }

    public virtual void Run()
    {
    }
}
