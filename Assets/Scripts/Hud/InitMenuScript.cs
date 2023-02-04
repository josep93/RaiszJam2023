using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitMenuScript : MonoBehaviour
{

    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject btnExit;
    [SerializeField] private GameObject fakeCanvas;
    [SerializeField] private float speed;
    
    public void Play()
    {      
        try
        {
            StartCoroutine(ResizeCamera());
            StartCoroutine(HideFakeButtons());

        }
        catch (System.Exception)
        {
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// Cerrar el juego
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }


    IEnumerator ResizeCamera()
    {
        float i = 4;
        GameObject gCamera = Camera.main.gameObject;
        Camera cam = gCamera.GetComponent<Camera>();

        btnPlay.SetActive(false);
        btnExit.SetActive(false);
        fakeCanvas.SetActive(true);
        
        while (i <= 10.75f) {
            i += 0.03f;
            cam.orthographicSize = i;
            gCamera.transform.position = Vector3.MoveTowards(gCamera.transform.position, new Vector3(0, 0, gCamera.transform.position.z), speed);
            yield return new WaitForSeconds(0.009f);
            
        }

        // Al finalizar la transicción, eliminar el fakeCcanvas
        Destroy(fakeCanvas);
    }

    IEnumerator HideFakeButtons()
    {
        yield return new WaitForSeconds(0.25f);

        float i = 1;
        Image[] fkBtn = fakeCanvas.GetComponentsInChildren<Image>();
        TextMeshProUGUI[] fkLetter = fakeCanvas.GetComponentsInChildren<TextMeshProUGUI>();

        
        yield return null;
        while (i > 0)
        {
            i -= 0.05f;
            // Cambiar el alpha de los botones   
            Color cColor = fkBtn[0].color;
            cColor.a = i;
            fkBtn[0].color = cColor;
            fkBtn[1].color = cColor;

            // Cambiar el alpha de las letras
            cColor = fkLetter[0].color;
            cColor.a = i;
            fkLetter[0].color = cColor;
            fkLetter[1].color = cColor;

            yield return new WaitForSeconds(0.05f);
        }

    }

}
