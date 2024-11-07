using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class DialogoDEA : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject[] flecha;
    public GameObject DEA2;
    public GameObject correctPlace;
    public GameObject perillaOff;
    public GameObject perillaDEA;
    public Desfibrilador dea;
    public TextMeshProUGUI instruccion;
    private int indicador;
    private bool PasoNext;
    // Start is called before the first frame update
    void Start()
    {
        instruccion.fontSize = 70;
        instruccion.alignment = TextAlignmentOptions.Center;
        PasoNext = true;
        indicador = 1;
        audioSource = perillaDEA.GetComponent<AudioSource>();
        PasosSiguientes();
    }

    // Update is called once per frame
    void Update()
    {
        
        actualizacionIndicador();

        
        PasosSiguientes();
        
        
       
    }
     public void PasosSiguientes()
    {
        if (indicador == 1){
            instruccion.text = "Bienvenido al módulo de Desfibrilación Externo Automático, aquí aprenderás a configurar la interfaz y utilizar algunos elementos de bioinstrumentación. \n\n\n Presiona H para comenzar con la simulación. ";
        }else if (indicador == 2)
        {
            instruccion.text = "A tu derecha podrás ver el elemento con el cual vas a interactuar." + " Cuando agarres el objeto, podrás ver la información de el mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona H ";
        }else if (indicador ==3)
        {
            instruccion.text = "¡Perfecto! Ahora vamos a empezar a colocar el DEA en el desfibrilador y maniquí \n\n\n Presiona H para ver el orden correcto en el que tienes que colocar";

            correctPlace.SetActive(true);

        }else if (indicador ==4)
        {
            instruccion.text = "Primero conecta el cable de los parches al desfibrilador, busca en la mesa a tu derecha el elemento y llevalo a donde indica la flecha roja";
            flecha[0].SetActive(true);
            dea.lightDEA.enabled = true;
            PasoNext=false;

            if (DEA2.activeSelf){
                indicador++;
                Debug.Log(indicador);
                flecha[0].SetActive(false);
            }

        }else if (indicador ==5)
        {
            instruccion.text = "Ahora gira la perilla que te indica la flecha roja para colocarlo en modo Desfinbrilador Externo Automático";
            flecha[1].SetActive(true);
            if (perillaDEA.activeSelf){
                indicador++;
                audioSource.Play();
                Debug.Log(indicador);
                flecha[1].SetActive(false);
            }
        }else if(indicador ==6)
        {
            instruccion.text = "Muy bien, ahora vamos a esperar a que el monitor analice el estado del paciente para saber si es necesaria una descarga electrica.";
        }else if(indicador == 100){

            instruccion.text = "Un DEA es un tipo de desfibrilador computarizado que analiza automáticamente el ritmo cardiaco en personas que están sufriendo un paro. Cuando sea necesario, envía una descarga eléctrica al corazón para normalizar su ritmo.  La conversión de una arritmia ventricular a su ritmo normal mediante una descarga eléctrica se denomina desfibrilación.";
        }
    }

    public void actualizacionIndicador()
    {
        if(Input.GetKeyDown(KeyCode.H) && PasoNext== true)
        {
            indicador ++;
            PasosSiguientes();
            Debug.Log(indicador);
        }
        
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {

         if(args.interactable.gameObject.tag == "PerillaOff" && indicador == 5){

            Debug.Log("OnHover detectado");
            perillaDEA.SetActive(true);
            perillaOff.SetActive(false);
         }
        
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactable.gameObject.tag == "DEA" && indicador <= 2)
        {
            indicador=100;
            instruccion.text = "Un DEA es un tipo de desfibrilador computarizado que analiza automáticamente el ritmo cardiaco en personas que están sufriendo un paro. Cuando sea necesario, envía una descarga eléctrica al corazón para normalizar su ritmo.  La conversión de una arritmia ventricular a su ritmo normal mediante una descarga eléctrica se denomina desfibrilación.";
        }
        
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactable.gameObject.tag == "DEA")
        {
            
            if (indicador == 100)
            {
            indicador = 2;
            }
        }
        
        
        
        PasosSiguientes();
    }

}
