using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundEvent : MonoBehaviour 
{
    private RoundScript roundScript;
    private Camera cam;

    [SerializeField] float zPos = -10f;
    [SerializeField] float tVal = 0.2f;

    private void Start()
    {
        roundScript = this.GetComponentInParent<RoundScript>();
        cam = Camera.main;
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

    #region LabRound
    IEnumerator LabRound()
    {
        float i = 10;
        while (i > 0)
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(0, 0, zPos), tVal);
            i -= 0.25f;
            yield return new  WaitForSeconds(0.01f);
        }

        yield return null;
        roundScript.EndRound();
    }
    #endregion

}
