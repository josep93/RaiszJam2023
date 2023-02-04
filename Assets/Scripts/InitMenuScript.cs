using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitMenuScript : MonoBehaviour
{

    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject btnExit;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject fakeCanvas;
    [SerializeField] private float speed;
    
    public void Play()
    {
        StartCoroutine(ResizeCamera());
        StartCoroutine(HideFakeButtons());
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
        // Tarda unos 3 - 2 segundos en colocarse en su pposición original
        // 0.44
        float i = 1;
        GameObject gCamera = Camera.main.gameObject;
        Camera cam = gCamera.GetComponent<Camera>();

        canvas.SetActive(false);
        fakeCanvas.SetActive(true);
        
        while (i <= 4.5f) {
            cam.orthographicSize = i;
            i += 0.01f;
            gCamera.transform.position = Vector3.MoveTowards(gCamera.transform.position, new Vector3(0, 0, gCamera.transform.position.z), speed);
            yield return new WaitForSeconds(0.01f);
            
        }
    }

    IEnumerator HideFakeButtons()
    {
        yield return new WaitForSeconds(0.25f);

        float i = 1;
        Image[] fkBtn = fakeCanvas.GetComponentsInChildren<Image>();

        yield return null;
        while (i > 0)
        {
            i -= 0.1f;
            // Cambiar el alpha de los botones   
            var cColor = fkBtn[0].color;
            cColor.a = i;
            fkBtn[0].color = cColor;
            fkBtn[1].color = cColor;

            yield return new WaitForSeconds(0.11f);
        }

    }

}
