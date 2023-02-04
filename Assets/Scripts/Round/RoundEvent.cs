using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundEvent : MonoBehaviour 
{
    private RoundScript roundScript;
    private Camera cam;

    private bool customValue = true;

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
        roundScript = this.GetComponentInParent<RoundScript>();
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

    public enum Eventos : byte
    {
        LabRound
    }

    public void Run(RoundScript.RoundEnum round)
    {
        if (customValue)
        {
            StartCoroutine(ImpactEffect());
            return;
        }

        short damageTaken = 0;
        int[] attackRound = new int[5];

        switch (round)
        {
            case RoundScript.RoundEnum.Blizzard:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Blizzard];
                return;
            case RoundScript.RoundEnum.Catapult:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Catapult];
                return;
            case RoundScript.RoundEnum.Cloudy:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Cloudy];
                return;
            case RoundScript.RoundEnum.Drizzle:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Drizzle];
                return;
            case RoundScript.RoundEnum.DryStorm:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.DryStorm];
                return;
            case RoundScript.RoundEnum.Fire:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Fire];
                return;
            case RoundScript.RoundEnum.Earthquake:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Earthquake];
                return;
            case RoundScript.RoundEnum.Frost:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Frost];
                return;
            case RoundScript.RoundEnum.Hail:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Hail];
                return;
            case RoundScript.RoundEnum.HeatWave:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.HeatWave];
                return;
            case RoundScript.RoundEnum.Monsoon:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Monsoon];
                return;
            case RoundScript.RoundEnum.Plague:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Plague];
                return;
            case RoundScript.RoundEnum.Solarium:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Solarium];
                return;
            case RoundScript.RoundEnum.Storm:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Storm];
                return;
            case RoundScript.RoundEnum.Sunny:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Sunny];
                return;
            case RoundScript.RoundEnum.Wind:
                attackRound = roundScript.RoundDict[RoundScript.RoundEnum.Wind];
                return;
        }

        for (int i = 0; i < 5; i++)
        {
            short result = (short)(attackRound[i] - TreeScript.current.ResistanceBonuses[i]);
            if (result > 0)
            {
                damageTaken += result;
            }
        }

        //CustomEvent(Eventos.LabRound);
    }


    public void CustomEvent(Eventos cEvent)
    {
        
        switch (cEvent)
        {
            case Eventos.LabRound:
                StartCoroutine(LabRound());
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Reinica las variables de movimiento y bloquea el c�lculo en el Update
    /// </summary>
    private void RestartVar(float speedPosticion = 0f, float speedRotation = 0f, float speedSize = 0f)
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
        RestartVar();


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

        // Esperamos al c�lculo
        moving = false;
        yield return new WaitForSeconds(1);
        
        // Retomamos la c�mara
        moving = true;
        RestartVar(0, speedRotation, speedSize);
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

        // Esperamos al c�lculo
        moving = false;
        yield return new WaitForSeconds(1);

        // Retomamos la c�mara
        moving = true;
        RestartVar(speedPosticion, speedRotation, speedSize);
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

        // Esperamos al c�lculo
        moving = false;
        yield return new WaitForSeconds(1);

        // Retomamos la c�mara
        moving = true;
        RestartVar(speedPosticion, speedRotation, speedSize);
    }

    #endregion

}
