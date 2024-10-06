using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Desfibrilador : MonoBehaviour
{
    public Light areaLight;  // Referencia a tu Area Light

    private void Start()
    {
        // Asegúrate de que la luz esté apagada inicialmente si es necesario
        areaLight.enabled = false;
    }

   

     public void OnHoverEntered(HoverEnterEventArgs args)
    {
       areaLight.enabled = true;
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        areaLight.enabled = false;
    }
}
