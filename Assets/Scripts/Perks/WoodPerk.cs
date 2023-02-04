using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPerk : Perk
{
    public WoodPerk()
    {
        resistanceBonuses[(short)ResistancesEnum.Impact] = +4;
        resistanceBonuses[(short)ResistancesEnum.Torsion] = -2;
    }
}
