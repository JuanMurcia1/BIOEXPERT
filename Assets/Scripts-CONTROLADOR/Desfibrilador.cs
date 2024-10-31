using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Desfibrilador : MonoBehaviour
{
    public Light areaLight;  
    public Light areaLightmani;
    public Light lightMonitoriza;
    public Light lightSPO2;
    
    public Light lightPresion;
    public Light lightDEA;


    

    private void Start()
    {
        
        // Asegúrate de que la luz esté apagada inicialmente si es necesario
        areaLight.enabled = false;
        areaLightmani.enabled = false;
        lightMonitoriza.enabled = false;
        lightDEA.enabled = false;
        lightPresion.enabled = false;
        lightSPO2.enabled = false;
    }

   

     public void OnHoverEntered(HoverEnterEventArgs args)
    {
         if (args.interactable.gameObject.tag == "Maniqui")
        {
            Debug.Log("maniqui");
            
            areaLightmani.enabled = true;   
        }else if ( args.interactable.gameObject.tag == "Desfibrilador")
        {
            Debug.Log("desfibrilador");
            areaLight.enabled = true;

        }else if ( args.interactable.gameObject.tag == "Monitoriza")
        {
            
            lightMonitoriza.enabled = true;

        }else if ( args.interactable.gameObject.tag == "Presion")
        {
            
            lightPresion.enabled = true;

        }else if ( args.interactable.gameObject.tag == "SPO2")
        {
            Debug.Log ("SP");
            lightSPO2.enabled = true;

        }else if ( args.interactable.gameObject.tag == "DEA")
        {
            
            lightDEA.enabled = true;

        }

       
       
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        if (args.interactable.gameObject.tag == "Maniqui")
        {
            
            areaLightmani.enabled = false;   
        }else if ( args.interactable.gameObject.tag == "Desfibrilador")
        {
            areaLight.enabled = false;
        }else if ( args.interactable.gameObject.tag == "Monitoriza")
        {
            
            lightMonitoriza.enabled = false;

        }else if ( args.interactable.gameObject.tag == "Presion")
        {
            
            lightPresion.enabled = false;

        }else if ( args.interactable.gameObject.tag == "SPO2")
        {
            
            lightSPO2.enabled = false;

        }else if ( args.interactable.gameObject.tag == "DEA")
        {
            
            lightDEA.enabled = false;

        }
    }
   


}
