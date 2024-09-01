using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tablero : MonoBehaviour
{
    public GameObject[] pantallas;
    public GameObject desfibrilador;
    private int indicador = 0;

    private void Start()
    {
        pantallas[indicador].SetActive(true);
        StartCoroutine(CambiarPantallaConRetraso(15));
    }

    public void Siguiente()
    {
        indicador++;
        pantallas[indicador].SetActive(true);
        pantallas[indicador - 1].SetActive(false);


        if (indicador == 1 )
        {
            Debug.Log("ETAPA 1");
            

            
            
        }
        if (indicador == 2)
        {
            Debug.Log("ETAPA 2");
            desfibrilador.SetActive(true);

            
        }
    }

    public void SiguienteEtapa(){
         if(Input.GetKeyDown(KeyCode.H)){
            StartCoroutine(CambiarPantallaConRetraso(1));
            Debug.Log("Press");
        }
    }

    private void Update()
    {        
        Debug.Log(indicador);
       SiguienteEtapa();

        
    }

    private IEnumerator CambiarPantallaConRetraso(float t)
    {
        yield return new WaitForSeconds(t); // Esperar 5 segundos
        Siguiente();
    }

}
