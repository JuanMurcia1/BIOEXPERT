using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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
    public int indicador = 0;
    public GameObject mando;

    public GameObject interfazPrincipal;

    public Light gatilloLight;

    private void Start()
    {
        Intrucciones2.SetActive(true);
        StartCoroutine(CambiarPantallaConRetraso(15));
        gatilloLight.enabled = false;
    }

    public void Siguiente()
    {
        indicador++;
        Intrucciones2.SetActive(false);
        Intrucciones1.SetActive(true);
        
        


        if (indicador == 1 )
        {
            SiguienteTexto();
        
            
        }
        if (indicador == 2)
        {
            Debug.Log("ETAPA 2");
            Intrucciones.text = "ETAPA 2   Aquí aprenderás a desplegar la interfaz y observar los módulos disponibles, Observa el botón resaltado en azúl y oprímelo en tu mando. abre la interfaz con (J)";
            gatilloLight.enabled = false;
            
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
            Intrucciones.text= "¡Excelente trabajo! Has completado el tutorial básico. Antes de terminar, reconozcamos el entorno.Aquí podrás encontrar todos los elementos necesarios para las prácticas.Cuando estés listo presiona el botón del menú J para seleccionar qué tipo de experiencia quieres realizar." ;
            Debug.Log("ETAPA LIBRE");
            desfibrilador.SetActive(true);
            mando.SetActive(false);
            gatilloLight.enabled = true;
            DEA.SetActive(true);
            presion.SetActive(true);
            SPO2.SetActive(true);
            monitorizacion.SetActive(true);
            maniqui.SetActive(true);
           
            

        }


    }

    // FUNCIONES

    public void ActivarInterfaz()
    {
        if(Input.GetKeyDown(KeyCode.J)){
        bool estadoActual = interfazPrincipal.activeSelf;
        interfazPrincipal.SetActive(!estadoActual);
        mando.SetActive(false);

        if(!estadoActual & indicador==2)
            {
                Intrucciones.text = "Perfecto! Interactuaste con la interfaz, ahora vamos a interactuar con objetos, puedes cerrarla con J. Presiona H cuando estés listo.";
            }
            
            
        }
    }

    public void SiguienteEtapa(){
         if(Input.GetKeyDown(KeyCode.H)){
            StartCoroutine(CambiarPantallaConRetraso(1));
            Debug.Log("Press");
        }
    }

     public void SiguienteTexto(){
         if(indicador==1){
            StartCoroutine(CambiarTexto(6));
            
        }
    }



    private void Update()
    {        
        Debug.Log(indicador);
       SiguienteEtapa();
       ActivarInterfaz();

        
    }


    /// CURUTINAS

    private IEnumerator CambiarPantallaConRetraso(float t)
    {
        yield return new WaitForSeconds(t); // Esperar 5 segundos
        Siguiente();
    }

    private IEnumerator CambiarTexto(float t)
    {
        yield return new WaitForSeconds(t);
        Intrucciones.text = "Observa cómo un holograma de tu mando aparece en el entorno y te resalta el botón para moverte. presiona H cuando termines de explorar.";
        mando.SetActive(true);
        
    }

}
