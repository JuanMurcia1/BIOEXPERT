using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class VitalesInstrumentos : MonoBehaviour
{
   
    public GameObject SPO2Mani;
    public GameObject monitoriMani;
    public GameObject presionMani;
    public GameObject SPO2Desfri;
    public GameObject monitoriDesfri;
    public GameObject presionDesfri;
    public GameObject SPO2R;
    public GameObject monitoriR;
    public GameObject presionR;

    public DialogoSignos dialogoSignos;
    public Desfibrilador desfibrilador;
    
    
   

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
       
        if (gameObject.CompareTag("Monitoriza") && collision.gameObject.CompareTag("Desfibrilador") && dialogoSignos.indicador ==4)
        {
         
         monitoriDesfri.SetActive(true);
         monitoriMani.SetActive(true);
         monitoriR.SetActive(false);
         dialogoSignos.indicador= 5;
         dialogoSignos.flechaMonitori.SetActive(false);
       
         desfibrilador.lightSPO2.enabled = true;
         dialogoSignos.aciertoVisual1.SetActive(true);
         dialogoSignos.acierto.Play();
       
         
          
        
           
            
        }else if (gameObject.CompareTag("SPO2") && collision.gameObject.CompareTag("Desfibrilador") && dialogoSignos.indicador ==5)
        {

            
           SPO2Desfri.SetActive(true);
           SPO2Mani.SetActive(true);
           SPO2R.SetActive(false);
           dialogoSignos.indicador= 6;
           dialogoSignos.flechaSPO2.SetActive(false);
           dialogoSignos.flechaPresion.SetActive(true);
           desfibrilador.lightPresion.enabled = true;
           dialogoSignos.acierto.Play();
           dialogoSignos.aciertoVisual2.SetActive(true);
           
            
        }else if (gameObject.CompareTag("Presion") && collision.gameObject.CompareTag("Desfibrilador") && dialogoSignos.indicador ==6)
        {

            
           presionDesfri.SetActive(true);
           presionMani.SetActive(true);
           presionR.SetActive(false);
           dialogoSignos.flechaPresion.SetActive(false);

           dialogoSignos.indicador= 7;
           dialogoSignos.acierto.Play();
           dialogoSignos.aciertoVisual3.SetActive(true);
           dialogoSignos.PasosSiguientes();

           
           
            
        }
    }
}
