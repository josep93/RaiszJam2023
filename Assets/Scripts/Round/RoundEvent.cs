using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundEvent : MonoBehaviour 
{
    private RoundScript roundScript;
    private Camera cam;

    [Header("Controlador de movimiento")]
    [SerializeField] private bool moving = false;

    [Header("Variables de posición")]
    [SerializeField] float speedPosticion = 0f;
    [SerializeField] float xPosticion = 0f;
    [SerializeField] float yPosticion = 0f;

    [Header("Variables de rotación")]
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
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camSize, speedSize);  // Tamaño cámara
    }

    public enum Eventos : byte
    {
        LabRound
    }

    public void Run(RoundScript.RoundEnum round)
    {
        short damageTaken = 0;

        int[] attackRound = new int[5];

        switch (round)
        {
            case RoundScript.RoundEnum.Blizzard:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Blizzard][i];
                }
                return;
            case RoundScript.RoundEnum.Catapult:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Catapult][i];
                }
                return;
            case RoundScript.RoundEnum.Cloudy:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Cloudy][i];
                }
                return;
            case RoundScript.RoundEnum.Drizzle:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Drizzle][i];
                }
                return;
            case RoundScript.RoundEnum.DryStorm:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.DryStorm][i];
                }
                return;
            case RoundScript.RoundEnum.Fire:
                return;
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Fire][i];
                }
            case RoundScript.RoundEnum.Earthquake:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Earthquake][i];
                }
                return;
            case RoundScript.RoundEnum.Frost:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Frost][i];
                }
                return;
            case RoundScript.RoundEnum.Hail:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Hail][i];
                }
                return;
            case RoundScript.RoundEnum.HeatWave:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.HeatWave][i];
                }
                return;
            case RoundScript.RoundEnum.Monsoon:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Monsoon][i];
                }
                return;
            case RoundScript.RoundEnum.Plague:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Plague][i];
                }
                return;
            case RoundScript.RoundEnum.Solarium:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Solarium][i];
                }
                return;
            case RoundScript.RoundEnum.Storm:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Storm][i];
                }
                return;
            case RoundScript.RoundEnum.Sunny:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Sunny][i];
                }
                return;
            case RoundScript.RoundEnum.Wind:
                for (int i = 0; i < 5; i++)
                {
                    attackRound[i] = roundScript.RoundDict[RoundScript.RoundEnum.Wind][i];
                }
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
    /// Reinica las variables de movimiento y bloquea el cálculo en el Update
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


        // Otros efectos aparte de la cámara

        yield return new WaitForSeconds(3);

        // Esperamos al cálculo
        moving = false;
        yield return new WaitForSeconds(1);
        
        // Retomamos la cámara
        moving = true;
        RestartVar(0, speedRotation, speedSize);
    }

    IEnumerator TempestEffect()
    {
        // Camera effects
        moving = true;
        camRotation = -1f;
        speedRotation = 0.005f;

        camSize = 9f;
        speedSize = 0.0063f;


        // Otros efectos aparte de la cámara

        yield return new WaitForSeconds(3);

        // Esperamos al cálculo
        moving = false;
        yield return new WaitForSeconds(1);

        // Retomamos la cámara
        moving = true;
        RestartVar(0, speedRotation, speedSize);
    }

    #endregion

}
