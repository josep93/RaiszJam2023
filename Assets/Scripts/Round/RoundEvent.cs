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

    public void Run()
    {
        CustomEvent(Eventos.LabRound);
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

    private void RestartVar()
    {
        // Move
        speedPosticion = 0f;
        xPosticion = 0f;
        yPosticion = 0f;

        // Rotation
        speedRotation = 0;
        camRotation = 0;

        // Size
        camSize = 10.75f;
        speedSize = 0;

        moving = false;
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

}
