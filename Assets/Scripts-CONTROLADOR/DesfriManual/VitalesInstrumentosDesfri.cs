using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class VitalesInstrumentosDesfri : MonoBehaviour
{
    public DialogosDesfriManual dialogosDesfriManual;
    public GameObject deaManiqui;
    public GameObject DEA;
    public GameObject conectorPalasDea;
    public GameObject palasMesa;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter(Collision collision)
    {

        if (gameObject.CompareTag("DEA") && collision.gameObject.CompareTag("Desfibrilador") && dialogosDesfriManual.indicador ==3)
        {

           DEA.SetActive(false);
           deaManiqui.SetActive(true);
           conectorPalasDea.SetActive(true);
           dialogosDesfriManual.flechaDea.SetActive(false);
           dialogosDesfriManual.indicador= 4;
           dialogosDesfriManual.PasosSiguientes();
           dialogosDesfriManual.acierto.Play();
           dialogosDesfriManual.aciertoVisual1.SetActive(false);
           palasMesa.SetActive(false);
           dialogosDesfriManual.PasoNext= true;

           
           

        }   
        // }else if (gameObject.CompareTag("PalasMesa") && collision.gameObject.CompareTag("Desfibrilador") && dialogosDesfriManual.indicador ==4)
        // {

        //    palasMesa.SetActive(false);
        //    conectorPalasDea.SetActive(true);
        //    dialogosDesfriManual.flechaPalas.SetActive(false);
        //    dialogosDesfriManual.indicador= 5;
        //    dialogosDesfriManual.acierto.Play();
        //    dialogosDesfriManual.aciertoVisual1.SetActive(false);
        //    dialogosDesfriManual.aciertoVisual2.SetActive(true);

           
           
            
        // }
    }

}
