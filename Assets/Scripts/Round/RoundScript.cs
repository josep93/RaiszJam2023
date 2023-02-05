using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using JetBrains.Annotations;
using System.Linq.Expressions;
using TMPro;

public class RoundScript : MonoBehaviour
{
    [SerializeField] private GameObject btnNextRound;
    [SerializeField] private GameObject[] btnPerks;
    [SerializeField] private float speed = 6;
    private List<Perk.PerkEnum> upgradablePerks;

    RoundEvent roundEvent;
    private GameObject hud;
    private float originalBtnX = 0;
    private bool isShow = false;
    public static RoundScript instance = null;

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

    public short roundNumber = 0;
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

    List<RoundEnum> roundList = new();

    // Start is called before the first frame update
    void Start()
    {
        // Singelton
        if (instance != null) { Destroy(this.gameObject); }
        instance = this;

        roundEvent = this.GetComponentInChildren<RoundEvent>();
        hud = GameObject.FindGameObjectWithTag("Hud");
        originalBtnX = btnPerks[0].transform.position.x;

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
        roundEvent.Run(roundList[roundNumber]);
        roundNumber++;
        Debug.Log("Ronda lanzada");

        //hud.SetActive(false);
    }

    public void EndRound()
    {
        //Activate Hud
        //hud.SetActive(true);

    }

    /// <summary>
    /// Alterna el estado de los botones de los perks
    /// </summary>
    public void ShowHideButtonsPerks()
    {
        // Muestra los perks
        if (isShow)
        {
            StartCoroutine(HidePerskAvalible());
            return;
        }

        // Oculta los perks
        StartCoroutine(ShowPerksAvalible());
        ShowHalfNextRound();
    }

    #region Control boton de Siguiente ronda

    #region Mostrar mitad
    
    /// <summary>
    /// Mostrar la mitad del bot�n de Siguiente ronda
    /// </summary>
    public void ShowHalfNextRound()
    {
        // Mostrar el bot�n pero estando desactivado
        btnNextRound.SetActive(true);
        btnNextRound.GetComponent<Button>().enabled = false;
        StartCoroutine(ShowHalfBtn());
    }


    IEnumerator ShowHalfBtn()
    {
        yield return null;
        float i = 0;
        Image btnImagen = btnNextRound.GetComponent<Image>();
        TextMeshProUGUI letter = btnNextRound.GetComponentInChildren<TextMeshProUGUI>();

        while (i < 0.5f)
        {
            i += 0.01f;
            Color aux = btnImagen.color;
            aux.a = i;
            btnImagen.color = aux;

            aux = letter.color;
            aux.a = i;
            letter.color = aux;

            yield return new WaitForSeconds(0.005f);
        }

    }
    #endregion

    #region Mostrar completo
    /// <summary>
    /// Mostrar y activar bot�n de siguiente ronda
    /// </summary>
    public void ShowFullNextRound()
    {
        // Mostrar el bot�n completo y activarlo
        StartCoroutine(ShowFullBtn());
    }


    IEnumerator ShowFullBtn()
    {
        yield return null;
        float i = 0.5f;
        Image btnImagen = btnNextRound.GetComponent<Image>();
        TextMeshProUGUI letter = btnNextRound.GetComponentInChildren<TextMeshProUGUI>();

        while (i < 1)
        {
            i += 0.01f;
            Color aux = btnImagen.color;
            aux.a = i;
            btnImagen.color = aux;

            aux = letter.color;
            aux.a = i;
            letter.color = aux;

            yield return new WaitForSeconds(0.005f);
        }
        btnNextRound.GetComponent<Button>().enabled = true;
    }

    #endregion

    #region Ocultar
    /// <summary>
    /// Ocular bot�n de Siguiente ronda
    /// </summary>
    public void HideNextRoundBtn()
    {
        StartCoroutine(HideBtn());
    }

    IEnumerator HideBtn()
    {
        yield return null;
    }
    #endregion

    #endregion


    /// <summary>
    /// Mostrar los botones con los perks disponibles
    /// </summary>
    IEnumerator ShowPerksAvalible()
    {
        isShow = true;
        yield return new WaitForSeconds(0.5f);

        upgradablePerks = TreeScript.current.UpgradablePerks();
        int i = 0;

        foreach (GameObject btn in btnPerks)
        {
            if (i >= upgradablePerks.Count) { break; }
            StartCoroutine(ShowButton(btn, (int)upgradablePerks[i]));
            i++;
            yield return new WaitForSeconds(0.25f);
        }

    }

    IEnumerator ShowButton(GameObject btn, int index)
    {
        int i = 30;

        btn.GetComponentInChildren<TextMeshProUGUI>().text = Perk.PerkStringList[index];

        while (i > 0)
        {
            btn.transform.position = Vector3.MoveTowards(
                btn.transform.position,
                new Vector3(Screen.width * 0.7f, btn.transform.position.y, 0),
                speed);
            btn.GetComponent<Button>().enabled = true;
            i--;
            yield return new WaitForSeconds(0.01f);
        }

    }


    /// <summary>
    /// Ocultar los botones de los perks disponibles
    /// </summary>
    IEnumerator HidePerskAvalible()
    {
        isShow = false;
        foreach (GameObject btn in btnPerks)
        {
            StartCoroutine(HideButton(btn));
            yield return new WaitForSeconds(0.25f);
        }

    }

    IEnumerator HideButton(GameObject btn)
    {
        int i = 30;
        while (i > 0)
        {
            btn.transform.position = Vector3.MoveTowards(
                btn.transform.position,
                new Vector3(-originalBtnX, btn.transform.position.y, 0),
                -speed);
            btn.GetComponent<Button>().enabled = false;
            i--;
            yield return new WaitForSeconds(0.01f);
        }
    }


    /// <summary>
    /// Activa el perk pulsado
    /// </summary>
    /// <param name="indexPerk"></param>
    public void ActivePerk(int indexPerk)
    {
        ShowFullNextRound();

        Perk.ActivePerks.Add(upgradablePerks[indexPerk]);
        TreeRenderScript.current.UpdateSprites();
        
        ShowHideButtonsPerks();
    }



    public void NextRound()
    {
        HideNextRoundBtn();
        /*roundEvent.Run(roundList[roundNumber]);
        roundNumber++;*/
        StartCoroutine(WaitToStart());

    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(1);
        InitiateRound();
    }


    private void RoundsGameGeneration()
    {
        // Fisher�Yates shuffle algorithm
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

