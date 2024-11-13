using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class DialogoMarcapasos : MonoBehaviour
{
    public float tolerance = 0.1f;
    private Vector3 vector3Monitori;
    private Vector3 vector3DEA;
    public Transform MonitoriOriginal;
    public Transform DEAoRIGINAL;
    public AudioSource acierto;
    public GameObject flechaDEA;
    public GameObject flechaMonitori;
    public GameObject DEA2;
    public Desfibrilador desfibrilador;
    public GameObject aciertoVisual1;
    public GameObject aciertoVisual2;
    public GameObject perillaOff;
    public GameObject perillaMarcapasos;

    public GameObject interfazMarcapasos;
    
   public Light lightRaton;
   public Light lightBotonConfirm;

    public TextMeshProUGUI instruccion;
    public int indicador;
    public bool PasoNext;
    // Start is called before the first frame update
    void Start()
    {
        
        PasoNext = true;
        indicador = 1;
        PasosSiguientes();
        lightRaton.enabled= false;
        lightBotonConfirm.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        actualizacionIndicador();
        
        
       
    }
     public void PasosSiguientes()
    {
        if (indicador == 1){
            instruccion.text = "Bienvenido al módulo de Marcapasos, aquí aprenderás a configurar la interfaz y utilizar algunos elementos de bioinstrumentación." 
            + "\n\n\n Presiona H para comenzar con la simulación. ";
        }else if (indicador == 2)
        {
            instruccion.text = "A tu derecha podrás ver el elemento con el cual vas a interactuar." + " Cuando agarres el objeto, podrás ver la información de el mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona H ";
            PasoNext=false;

            
        }else if (indicador ==3)
        {
            instruccion.text = "¡Perfecto! Ahora vamos a empezar a colocar los elementos en el equipo y maniquí" +
            "\n\n\n Presiona H para ver el orden correcto.";

            
        }else if (indicador == 4)
        {
            PasoNext=false;
            instruccion.text = "El orden es:"+
            "\n\n 1.Interfaz de monitorización \n\n 2.Interfaz DEA." ;
            desfibrilador.lightMonitoriza.enabled= true;
            flechaMonitori.SetActive(true);

        }else if (indicador == 5)
        {
            
            instruccion.text = "El orden es:"+
            "\n\n 1.Interfaz de monitorización \n\n 2.Interfaz DEA." ;
            
            
        }
        else if (indicador ==6)
        {
            instruccion.text= "Perfecto!, has conectado bien todos los elementos necesarios, ahora vamos a interactuar con el desfibrilador" +
            "\n\n Presion H para avanzar";
            
            
        
            
        }else if (indicador ==7)
        {

            instruccion.text= "Encendido del desfibrilador" +
            "\n\n Apunta a la perilla para ubicarla en el modo marcapasos. " +
            "Esto permitirá encender el desfibrilador y mostrar la interfaz correspondiente al módulo elegido.";

            PasoNext = false;
        }
    }

    public void actualizacionIndicador()
    {
        if(Input.GetKeyDown(KeyCode.H) && PasoNext== true)
        {
            indicador ++;
            PasosSiguientes();
            
        }
        
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if(args.interactable.gameObject.tag == "PerillaOff" && indicador == 7)
        {
            perillaMarcapasos.SetActive(true);
            perillaOff.SetActive(false);
            interfazMarcapasos.SetActive(true);
            PasoNext= false;
            indicador =8;
            PasosSiguientes();
            lightRaton.enabled = true;
            
        

        }
        
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
      if (args.interactable.gameObject.tag == "Monitoriza" && indicador <= 2)
        {
            
            instruccion.text = "Los electrodos son dispositivos conductores usados para registrar la actividad eléctrica" + 
            " de órganos como el corazón, el cerebro y los músculos." +
            " Se colocan sobre la piel y son fundamentales en procedimientos como el electrocardiograma (ECG),"  
            + " que detecta anomalías cardíacas.";

            
        }else if (args.interactable.gameObject.tag == "DEA" && indicador <3)
        {
            instruccion.text = "Los parches de desfibrilación automática se utilizan con desfibriladores automáticos"
            + " externos (DEA). Los DEA analizan automáticamente el ritmo cardíaco y, de ser necesario, "+
            "administran una descarga eléctrica para tratar arritmias sin necesidad de intervención manual"
            + " en la configuración de la carga.";

            
        }
        
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        PasosSiguientes();
        
        if (args.interactable.gameObject.tag == "Monitoriza")
        {
            
            vector3Monitori = new Vector3(-3.8f, 2.3f,-0.4f);
            PasoNext= true;

            if (Vector3.Distance(MonitoriOriginal.position, vector3Monitori) > tolerance)
            {
                
                MonitoriOriginal.position = vector3Monitori;
                
            }   

            

        }else if (args.interactable.gameObject.tag == "DEA")
        {
            PasoNext= true;

            vector3DEA = new Vector3(-4.3f, 2.3f,3.3f);

        if (Vector3.Distance(DEAoRIGINAL.position, vector3DEA) > tolerance)
        {
           
            DEAoRIGINAL.position = vector3DEA;
        }   
        }
        
        
    }

}
