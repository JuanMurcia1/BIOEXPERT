using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandoLight : MonoBehaviour
{
    
    public Material newMaterial; // Asigna este material en el Inspector
    
    public GameObject Jostik;
    public GameObject JostikButtonInterfaz;
    public tablero tableroScript;
    public Material originalMaterial;

    void Start()
    {
    
   
        
    
    }

    void Update() {
        ColorMando();
    
}

public void ColorMando() 
{
    if (tableroScript.indicador == 1) {
  
        Jostik.GetComponent<Renderer>().material = newMaterial;
        

    }else if (tableroScript.indicador ==2)
    {
        JostikButtonInterfaz.GetComponent<Renderer>().material = newMaterial;
        Jostik.GetComponent<Renderer>().material = originalMaterial;
        

    }else if (tableroScript.indicador ==3)
    {
        JostikButtonInterfaz.GetComponent<Renderer>().material = originalMaterial;
    }

}


    
}
