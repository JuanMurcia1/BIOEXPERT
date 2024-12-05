using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Mono.Reflection;

public class tablero : MonoBehaviour
{
    public GameObject maniqui;
    public GameObject SPO2;
    public GameObject presion;
    public GameObject monitorizacion;
    public GameObject DEA;
    public GameObject desfibrilador;
    public TextMeshProUGUI Intrucciones;

    public TextMeshProUGUI Instrucciones3;

    public int indicador;
    public GameObject mando;

    public GameObject interfazPrincipal;

    public Light gatilloLight;
    private bool buttonBWasPressed = false; // Bandera para evitar múltiples detecciones por frame
    private bool isOk;

    public GameObject interfazCodigos;
    public GameObject buttonSkip;


    private void Start()
    {
        interfazCodigos.SetActive(false);
        isOk = true;
        
        gatilloLight.enabled = false;
        indicador =1;
        Siguiente();
    }

    public void Siguiente()
    {
        

        if (isOk){
            
        }
        

        if (indicador == 1 )
        {
           Intrucciones.text = "Bienvenido al tutorial interactivo para familiarizarte con el entorno simulado. "+ 
           "En esta pantalla se mostrará paso a paso la información que necesitas para completar cada acción en" +
           "el proceso \n\n Presiona B para continuar" ;
        
            
        }
        if (indicador == 2)
        {
            
            Intrucciones.text = "ETAPA 1 \n\n Ahora te enseñaremos a usar el mando para moverte. Usa el joystick para desplazarte en el entorno virtual."
            + "Observa el mando a tu derecha para ver resaltado el jostick. \n\n Presiona B para continuar." ;
            mando.SetActive(true);
           
            
            
        }

        if (indicador == 3)
        {
            Intrucciones.text= "Visualiza el desfibrilador, ahora, acércate y estira tu mano, cuando estés cerca del objeto, presiona el gatillo trasero reflejado en el holograma del mando. \n\n Presiona B cuando hayas terminado.";
            desfibrilador.SetActive(true);
            mando.SetActive(true);
            gatilloLight.enabled = true;
           
        }
         if (indicador == 4)
        {
            Intrucciones.text= "¡Excelente trabajo! Has completado el tutorial básico. Antes de terminar, reconozcamos el entorno.Aquí podrás encontrar todos los elementos necesarios para las prácticas.\n\n Ingresa tu código a tu derecha para ingresar a un módulo" ;
            Debug.Log("ETAPA LIBRE");
            desfibrilador.SetActive(true);
            mando.SetActive(false);
            gatilloLight.enabled = true;
            DEA.SetActive(true);
            presion.SetActive(true);
            SPO2.SetActive(true);
            monitorizacion.SetActive(true);
            maniqui.SetActive(true);
            interfazCodigos.SetActive(true);
           
            

        }


    }


    public void SiguienteEtapa(){
         if(Input.GetKeyDown(KeyCode.B)){
            indicador++;
            Siguiente();
            
            
        }

        UnityEngine.XR.InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        bool buttonBPressed;
        if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out buttonBPressed))
        {
            if (buttonBPressed && !buttonBWasPressed)
            {
                // Solo incrementar cuando se detecta el inicio del botón presionado
                StartCoroutine(CambiarPantallaConRetraso(1));
            }
            // Actualizar la bandera para evitar múltiples detecciones
            buttonBWasPressed = buttonBPressed;
    }
    else
    {
        // Reiniciar la bandera cuando el botón no está presionado
        buttonBWasPressed = false;
    }
    }

    



    private void Update()
    {        
       SiguienteEtapa();

        
    }


    /// CURUTINAS

    private IEnumerator CambiarPantallaConRetraso(float t)
    {
        yield return new WaitForSeconds(t); // Esperar 5 segundos
        Siguiente();
        buttonSkip.SetActive(false);
    }

  

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.gameObject.tag == "skip")
        {   
            isOk = false;
            buttonSkip.SetActive(false);
            Intrucciones.text="";

            interfazCodigos.SetActive(true);
            Destroy(mando);

            Instrucciones3.text= "Tutorial saltado.\n\n\n Ingresa tu código a tu derecha para ingresar a un módulo";
          
    
            
        }

    }

}
