using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoundEvent : MonoBehaviour 
{
    private RoundScript roundScript;
    private Camera cam;
    private AudioSource audio;
    public static RoundEvent current = null;

    [Header("Datos de efectos")]
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private SpriteRenderer backgroundEffect;
    [SerializeField] private SpriteRenderer frontEffect;
    [SerializeField] private AudioClip[] clips;

    [Header("Controlador de movimiento")]
    [SerializeField] private bool moving = false;

    [Header("Variables de posici�n")]
    [SerializeField] float speedPosticion = 0f;
    [SerializeField] float xPosticion = 0f;
    [SerializeField] float yPosticion = 0f;

    [Header("Variables de rotaci�n")]
    [SerializeField] float speedRotation = 0f;
    [SerializeField] float camRotation = 0f;

    [Header("Variables de efoque")]
    [SerializeField] float camSize = 10.75f;
    [SerializeField] float speedSize = 0.5f;


    private void Start()
    {
        if (current != null) { Destroy(this); }
        current = this;
        roundScript = this.GetComponentInParent<RoundScript>();
        audio = this.GetComponent<AudioSource>();
        audio.loop = false;
        audio.playOnAwake = false;
        cam = Camera.main;
    }

    void Update()
    {
        if (!moving) { return; }
        cam.transform.SetPositionAndRotation(
            Vector3.MoveTowards(cam.transform.position, // Current position
            new Vector3(xPosticion, yPosticion, cam.transform.position.z), speedPosticion), // New position, speed
            Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(0, 0, camRotation), speedRotation)  // Rotation
            );
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camSize, speedSize);  // Tama�o c�mara
    }

    public void Run(RoundScript.RoundEnum round)
    {
        short damageTaken = 0;
        int[] attackRound = new int[5];

        switch (round)
        {
            case RoundScript.RoundEnum.Blizzard:
                ActiveEffect(14, 15, 1, 14);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Blizzard];
                break;
            case RoundScript.RoundEnum.Catapult:
                ActiveEffect(0, -1, 0, 0);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Catapult];
                break;
            case RoundScript.RoundEnum.Cloudy:
                ActiveEffect(4, -1, 0, 8);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Cloudy];
                break;
            case RoundScript.RoundEnum.Drizzle:
                ActiveEffect(0, 2, 0, 10);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Drizzle];
                break;
            case RoundScript.RoundEnum.DryStorm:
                ActiveEffect(11, 13, 1, 1);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.DryStorm];
                break;
            case RoundScript.RoundEnum.Fire:
                ActiveEffect(5, -1, 2, 3);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Fire];
                break;
            case RoundScript.RoundEnum.Earthquake:
                ActiveEffect(9, 10, 2, 2);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Earthquake];
                break;
            case RoundScript.RoundEnum.Frost:
                ActiveEffect(5, -1, 0, 6);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Frost];
                break;
            case RoundScript.RoundEnum.Hail:
                ActiveEffect(4, 1, 2, 4);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Hail];
                break;
            case RoundScript.RoundEnum.HeatWave:
                ActiveEffect(5, -1, 0, 5);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.HeatWave];
                break;
            case RoundScript.RoundEnum.Monsoon:
                ActiveEffect(4, 3, 1, 7);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Monsoon];
                break;
            case RoundScript.RoundEnum.Plague:
                ActiveEffect(5, 6, 2, 9);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Plague];
                break;
            case RoundScript.RoundEnum.Solarium:
                ActiveEffect(7, -1, 0, 11);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Solarium];
                break;
            case RoundScript.RoundEnum.Storm:
                ActiveEffect(11, 12, 1, 13);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Storm];
                break;
            case RoundScript.RoundEnum.Sunny:
                ActiveEffect(8, -1, 0, 12);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Sunny];
                break;
            case RoundScript.RoundEnum.Wind:
                ActiveEffect(0, 16, 1, 15);
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Wind];
                break;
        }

        for (int i = 0; i < 5; i++)
        {
            short result = (short)(attackRound[i] - TreeScript.current.ResistanceBonuses[i]);
            if (result > 0)
            {
                damageTaken += result;
            }
        }

        TreeScript.current.Health -= damageTaken;
    }


    #region Controlador de efectos
    private void ActiveEffect(int indexBack, int indexFront, int effects, int indexAudio)
    {
        
        backgroundEffect.sprite = backgrounds[indexBack];
        audio.clip = clips[indexAudio];
        audio.Play();
        StartCoroutine(ShowBackEffects());

        if (indexFront >= 0)
        {
            frontEffect.sprite = backgrounds[indexFront];
            StartCoroutine(ShowFrontEffects());
        }
        
        switch (effects)
        {
            case 0:
                StartCoroutine(CalmEffect());
                break; 
            case 1:
                StartCoroutine(TempestEffect());
                break; 
            case 2:
                StartCoroutine(ImpactEffect());
                break;
        }
    }

    IEnumerator ShowBackEffects()
    {
        float i = 0;

        while (i < 1f)
        {
            i += 0.01f;
            Color aux = backgroundEffect.color;
            aux.a = i;
            backgroundEffect.color = aux;

            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator ShowFrontEffects()
    {
        float i = 0;

        while (i < 1f)
        {
            i += 0.01f;
            Color aux = frontEffect.color;
            aux.a = i;
            frontEffect.color = aux;

            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator HideBackEffects()
    {
        float i = 1;

        while (i > 0)
        {
            i -= 0.01f;
            Color aux = backgroundEffect.color;
            aux.a = i;
            backgroundEffect.color = aux;

            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator HideFrontEffects()
    {
        float i = 1;

        while (i > 0)
        {
            i -= 0.01f;
            Color aux = frontEffect.color;
            aux.a = i;
            frontEffect.color = aux;

            yield return new WaitForSeconds(0.005f);
        }
        frontEffect.sprite = null;
    }
    #endregion


    #region Control efectos camara
    /// <summary>
    /// Reinica las variables de movimiento y bloquea el c�lculo en el Update
    /// </summary>
    private void RestartCam(float speedPosticion = 0f, float speedRotation = 0f, float speedSize = 0f)
    {
        // Move
        this.speedPosticion = speedPosticion;
        xPosticion = 0f;
        yPosticion = 0f;

        // Rotation
        this.speedRotation = speedRotation;
        camRotation = 0;

        // Size
        camSize = 10.75f;
        this.speedSize = speedSize;

        //moving = false;
        roundScript.EndRound();
    }

    #region LabRound
    IEnumerator LabRound(float speedPosticion = 0.1f, float speedRotation = 0.2f, float speedSize = 0.0125f)
    {
        moving = true;
        xPosticion = 5;
        yPosticion = -5;
        this.speedPosticion = speedPosticion;

        this.speedRotation = speedRotation;
        camRotation = -10f;

        camSize = 8f;
        this.speedSize = speedSize;
        yield return new WaitForSeconds(2);
        RestartCam();


        /*float i = 10;
        while (i > 0)
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(0, 0, camRotation), speedRotation);
            i -= 0.25f;
            yield return new  WaitForSeconds(0.01f);
        }

        yield return null;
        roundScript.EndRound();*/
    }
    #endregion


    #region Efects
    IEnumerator CalmEffect()
    {
        // Camera effects
        moving = true;
        camRotation = -1f;
        speedRotation = 0.005f;

        camSize = 9f;
        speedSize = 0.0063f;


        // Otros efectos aparte de la c�mara

        yield return new WaitForSeconds(3);

        if (TreeScript.current.Health <= 0)
        {
            Mortis();
            yield break;
        }

        // Esperamos al c�lculo
        moving = false;
        yield return new WaitForSeconds(1);
        
        // Retomamos la c�mara
        moving = true;
        RestartCam(0, speedRotation, speedSize);
        StartCoroutine(HideBackEffects());
        StartCoroutine(HideFrontEffects());
    }

    IEnumerator TempestEffect()
    {
        // Camera effects
        moving = true;

        // Position
        xPosticion = 2;
        yPosticion = 1;
        speedPosticion = 0.03f;
        
        // Rotation
        camRotation = -1.5f;
        speedRotation = 0.01f;

        // Size
        camSize = 9f;
        speedSize = 0.0126f;


        // Otros efectos aparte de la c�mara

        yield return new WaitForSeconds(2);

        if (TreeScript.current.Health <= 0)
        {
            Mortis();
            yield break;
        }

        // Esperamos al c�lculo
        moving = false;
        yield return new WaitForSeconds(1);

        // Retomamos la c�mara
        moving = true;
        RestartCam(speedPosticion, speedRotation, speedSize);
        StartCoroutine(HideBackEffects());
        StartCoroutine(HideFrontEffects());
    }

    IEnumerator ImpactEffect()
    {
        // Camera effects
        moving = true;

        // Position
        xPosticion = -0.5f;
        yPosticion = 2;
        speedPosticion = 0.03f;

        // Rotation
        camRotation = 2;
        speedRotation = 0.02f;

        // Size
        camSize = 8f;
        speedSize = 0.03f;


        // Otros efectos aparte de la c�mara

        yield return new WaitForSeconds(2);

        if (TreeScript.current.Health <= 0)
        {
            Mortis();
            yield break;
        }

        // Esperamos al c�lculo
        moving = false;
        yield return new WaitForSeconds(1);

        // Retomamos la c�mara
        moving = true;
        RestartCam(speedPosticion, speedRotation, speedSize);
        StartCoroutine(HideBackEffects());
        StartCoroutine(HideFrontEffects());
    }

    private void Mortis()
    {
        Debug.Log("Mortis");
    }

    #endregion
    #endregion
}
