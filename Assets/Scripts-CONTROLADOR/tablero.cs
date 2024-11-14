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
    public GameObject Intrucciones2;
    public GameObject Intrucciones1;

    public TextMeshProUGUI Instrucciones3;

    public int indicador = 0;
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
        Intrucciones2.SetActive(true);
        StartCoroutine(CambiarPantallaConRetraso(15));
        gatilloLight.enabled = false;
    }

    public void Siguiente()
    {
        indicador++;
        Intrucciones2.SetActive(false);

        if (isOk){
            Intrucciones1.SetActive(true);
        }
        

        if (indicador == 1 )
        {
            SiguienteTexto();
        
            
        }
        if (indicador == 2)
        {
            //Debug.Log("ETAPA 2");
            //Intrucciones.text = "ETAPA 2   Aquí aprenderás a desplegar la interfaz y observar los módulos disponibles, Observa el botón resaltado en azúl y oprímelo en tu mando. abre la interfaz con (J)";
            //gatilloLight.enabled = false;
            Siguiente();
            
        }

        if (indicador == 3)
        {
            Intrucciones.text= "Visualiza el desfibrilador, ahora, acércate y estira tu mano, cuando estés cerca del objeto, presiona el gatillo trasero reflejado en el holograma del mando, PRESIONA H cuando hayas terminado.";
            Debug.Log("ETAPA 3");
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

    // FUNCIONES (ASPECTO CONTROLADOR)

    public void ActivarInterfaz()
    {
        if(Input.GetKeyDown(KeyCode.J)){
        bool estadoActual = interfazPrincipal.activeSelf;
        interfazPrincipal.SetActive(!estadoActual);
        mando.SetActive(false);
    // ASPECTO MODELO
        if(!estadoActual & indicador==2)
            { // MODELO VISTA actualizado.
                Intrucciones.text = "Perfecto! Interactuaste con la interfaz, ahora vamos a interactuar con objetos, puedes cerrarla con J. Presiona H cuando estés listo.";
            }
        }
    }
    public void SiguienteEtapa(){
         if(Input.GetKeyDown(KeyCode.H)){
            StartCoroutine(CambiarPantallaConRetraso(1));
            Debug.Log("Press");
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

     public void SiguienteTexto(){
         if(indicador==1){
            StartCoroutine(CambiarTexto(6));
            
        }
    }



    private void Update()
    {        
       SiguienteEtapa();
       ActivarInterfaz();

        
    }


    /// CURUTINAS

    private IEnumerator CambiarPantallaConRetraso(float t)
    {
        yield return new WaitForSeconds(t); // Esperar 5 segundos
        Siguiente();
        buttonSkip.SetActive(false);
    }

    private IEnumerator CambiarTexto(float t)
    {
        yield return new WaitForSeconds(t);
        Intrucciones.text = "Observa cómo un holograma de tu mando aparece en el entorno y te resalta el botón para moverte. presiona H cuando termines de explorar.";

        if (mando != null){
            mando.SetActive(true);
        }
        
        
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.gameObject.tag == "skip")
        {
            Debug.Log("Hoverrr");      
            isOk = false;
            buttonSkip.SetActive(false);

            Intrucciones2.SetActive(false);
            Intrucciones1.SetActive(false);

            interfazCodigos.SetActive(true);
            Destroy(mando);

            Instrucciones3.text= "Tutorial saltado.\n\n\n Ingresa tu código a tu derecha para ingresar a un módulo";
          
    
            
        }

    }

}
