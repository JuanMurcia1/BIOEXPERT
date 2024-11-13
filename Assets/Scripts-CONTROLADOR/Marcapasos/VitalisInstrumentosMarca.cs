using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class VitalisInstrumentosMarca : MonoBehaviour
{
    public GameObject monitoriR;
    public GameObject monitoriMani;
    public GameObject monitoriDesfri;
    public DialogoMarcapasos dialogoMarcapasos;
    public Desfibrilador desfibrilador;
    public GameObject deaManiqui;
    public GameObject conectorPalasDea;
    public GameObject DEA;
    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
       
        if (gameObject.CompareTag("Monitoriza") && collision.gameObject.CompareTag("Desfibrilador") && dialogoMarcapasos.indicador ==4)
        {
         
         monitoriDesfri.SetActive(true);
         monitoriMani.SetActive(true);
         monitoriR.SetActive(false);
         dialogoMarcapasos.indicador= 5;
         dialogoMarcapasos.flechaMonitori.SetActive(false);
         dialogoMarcapasos.flechaDEA.SetActive(true);
         desfibrilador.lightDEA.enabled = true;
         dialogoMarcapasos.aciertoVisual1.SetActive(true);
         dialogoMarcapasos.acierto.Play();
       
         
            
        }else if (gameObject.CompareTag("DEA") && collision.gameObject.CompareTag("Desfibrilador") && dialogoMarcapasos.indicador ==5)
        {

           DEA.SetActive(false);
           deaManiqui.SetActive(true);
           conectorPalasDea.SetActive(true);
           dialogoMarcapasos.flechaDEA.SetActive(false);
           dialogoMarcapasos.indicador= 6;
           dialogoMarcapasos.PasosSiguientes();
           dialogoMarcapasos.acierto.Play();
           dialogoMarcapasos.aciertoVisual1.SetActive(false);
           dialogoMarcapasos.PasoNext= true;

           
           

        } 
    }

}
