using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;

public class RoundScript : MonoBehaviour
{
    RoundEvent roundEvent;
    private GameObject hud;

    public enum RoundEnum : short
    {
        Blizzard,
        Catapult,
        Cloudy,
        Drizzle,
        DryStorm,
        Earthquake,
        Fire,
        Frost,
        Hail,
        HeatWave,
        Monsoon,
        Plague,
        Solarium,
        Storm,
        Sunny,
        Wind
    }
    private enum AttackEnum : short
    {
        Cold,
        Drought,
        Heat,
        Impact,
        Torsion
    }

    private List<RoundEnum> RoundsSoft = new List<RoundEnum> { RoundEnum.Cloudy, RoundEnum.Sunny, RoundEnum.Drizzle, RoundEnum.Solarium, RoundEnum.Wind };
    private List<RoundEnum> RoundsMedi = new List<RoundEnum> { RoundEnum.DryStorm, RoundEnum.Earthquake, RoundEnum.Hail, RoundEnum.Blizzard, RoundEnum.Storm, RoundEnum.Plague };
    private List<RoundEnum> RoundsHard = new List<RoundEnum> { RoundEnum.Catapult, RoundEnum.Fire, RoundEnum.Frost, RoundEnum.HeatWave, RoundEnum.Monsoon };

    public Dictionary<RoundEnum, int[]> RoundDict = new Dictionary<RoundEnum, int[]>
    {
        {RoundEnum.Blizzard, new int[]   {3, 0, 0, 0, 6 }}, // Medium         new int[] {Cold, Drought, Heat, Impact, Torsion}
        {RoundEnum.Catapult, new int[]   {0, 0, 0, 10, 0}}, // Hard
        {RoundEnum.Cloudy, new int[]     {0, 0, 0, 0, 0 }}, // Soft
        {RoundEnum.Drizzle, new int[]    {0, 0, 0, 0, 0 }}, // Soft
        {RoundEnum.DryStorm, new int[]   {0, 2, 4, 0, 0 }}, // Medium
        {RoundEnum.Earthquake, new int[] {0, 0, 0, 4, 6 }}, // Medium
        {RoundEnum.Fire, new int[]       {0, 0, 10, 0, 0}}, // Hard
        {RoundEnum.Frost, new int[]      {10, 0, 0, 0, 0}}, // Hard
        {RoundEnum.Hail, new int[]       {2, 0, 0, 5, 0 }}, // Medium
        {RoundEnum.HeatWave, new int[]   {0, 10, 0, 0, 0}}, // Hard
        {RoundEnum.Monsoon, new int[]    {0, 0, 0, 0, 10}}, // Hard
        {RoundEnum.Plague, new int[]     {0, 5, 0, 3, 0 }}, // Medium
        {RoundEnum.Solarium, new int[]   {0, 0, 2, 0, 0 }}, // Soft
        {RoundEnum.Storm, new int[]      {0, 0, 0, 5, 5 }}, // Medium
        {RoundEnum.Sunny, new int[]      {0, 0, 0, 0, 0 }}, // Soft
        {RoundEnum.Wind, new int[]       {0, 0, 0, 0, 3 }}  // Soft
    };

    List<RoundEnum> roundList = new List<RoundEnum>();

    // Start is called before the first frame update
    void Start()
    {
        roundEvent = this.GetComponentInChildren<RoundEvent>();
        hud = GameObject.FindGameObjectWithTag("Hud");

        RoundsGameGeneration();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitiateRound()
    {
        //Deactivate Hud

        TreeScript.current.UpdateBonuses();
        roundEvent.Run();
        hud.SetActive(false);
    }

    public void EndRound()
    {
        //Activate Hud
        hud.SetActive(true);

    }

    private void RoundsGameGeneration()
    {
        // Fisher–Yates shuffle algorithm
        List<int> mediRounds = new List<int> { 0, 0, 1, 1, 1 };
        List<int> hardRounds = new List<int> { 1, 2, 2 };

        // Fisher-Yates shuffle implementation for medium rounds
        int nMedi = mediRounds.Count;
        for (int i = 0; i < nMedi; i++)
        {
            int r = i + (int)(UnityEngine.Random.value * (nMedi - i));
            int t = mediRounds[r];
            mediRounds[r] = mediRounds[i];
            mediRounds[i] = t;
        }

        // Fisher-Yates shuffle implementation for hard rounds
        int nHard = hardRounds.Count;
        for (int i = 0; i < nHard; i++)
        {
            int r = i + (int)(UnityEngine.Random.value * (nHard - i));
            int t = hardRounds[r];
            hardRounds[r] = hardRounds[i];
            hardRounds[i] = t;
        }

        // Rounds creation
        for (int i = 0; i < 2; i++)
        {
            int rnd = UnityEngine.Random.Range(0, 5);
            roundList.Add(RoundsSoft[rnd]);
        }

        for (int i = 0; i < 5; i++)
        {            
            if (mediRounds[i] == 0)
            {
                int rnd = UnityEngine.Random.Range(0, 5);
                roundList.Add(RoundsSoft[rnd]);
            }

            if (mediRounds[i] == 1)
            {
                int rnd = UnityEngine.Random.Range(0, 6);
                roundList.Add(RoundsMedi[rnd]);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (hardRounds[i] == 1)
            {
                int rnd = UnityEngine.Random.Range(0, 6);
                roundList.Add(RoundsMedi[rnd]);
            }

            if (hardRounds[i] == 2)
            {
                int rnd = UnityEngine.Random.Range(0, 5);
                roundList.Add(RoundsHard[rnd]);
            }
        }
    }

}

