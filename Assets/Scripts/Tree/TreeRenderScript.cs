using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRenderScript : MonoBehaviour
{
    public static TreeRenderScript current;

    SpriteRenderer[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        current = this;
        //Perk.TestPerks();
    }

    // Update is called once per frame
    public void UpdateSprites()
    {
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }

        if (RoundScript.instance.roundNumber < 2) {
            sprites[0].enabled = true;
            return;
        }

        if (RoundScript.instance.roundNumber == 10)
        {
            sprites[20].enabled = true;
        }
        else if (RoundScript.instance.roundNumber > 7)
        {
            sprites[19].enabled = true;
        }

        sprites[6].enabled = true;
        sprites[13].enabled = true;
        if(Perk.ActivePerks.Contains(Perk.PerkEnum.BranchThick))sprites[14].enabled = true;
        if(Perk.ActivePerks.Contains(Perk.PerkEnum.BranchThin))sprites[15].enabled = true;

        foreach (Perk.PerkEnum perk in Perk.ActivePerks)
        {
            ActivateSrpitePart(perk);
        }
    }

    private void ActivateSrpitePart(Perk.PerkEnum perk)
    {
        switch (perk)
        {
            case (Perk.PerkEnum.BranchThin):
                sprites[5].enabled = true;
                return;
            case (Perk.PerkEnum.BranchThick):
                sprites[4].enabled = true;
                return;
            case (Perk.PerkEnum.Cork):
                sprites[7].enabled = true;
                return;
            case (Perk.PerkEnum.FlexibleStem):
                sprites[8].enabled = true;
                return;
            case (Perk.PerkEnum.LeavesEmpty):
                sprites[13].enabled = false;
                sprites[14].enabled = false;
                sprites[15].enabled = false;
                return;
            case (Perk.PerkEnum.LeavesFull):
                return;
            case (Perk.PerkEnum.LeavesWaxed):
                sprites[16].enabled = true;
                if (Perk.ActivePerks.Contains(Perk.PerkEnum.BranchThick)) sprites[17].enabled = true;
                if (Perk.ActivePerks.Contains(Perk.PerkEnum.BranchThin)) sprites[18].enabled = true;
                return;
            case (Perk.PerkEnum.RootAdventitious):
                sprites[1].enabled = true;
                return;
            case (Perk.PerkEnum.RootLateral):
                sprites[2].enabled = true;
                return;
            case (Perk.PerkEnum.RootMain):
                sprites[3].enabled = true;
                return;
            case (Perk.PerkEnum.Thorns):
                sprites[12].enabled = true;
                return;
            case (Perk.PerkEnum.WoodDense):
                sprites[10].enabled = true;
                return;
            case (Perk.PerkEnum.WoodHard):
                sprites[9].enabled = true;
                return;
            case (Perk.PerkEnum.WoodWarm):
                sprites[10].enabled = true;
                return;
        }
    }
}
