using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR;

public class DialogoSignos : MonoBehaviour
{

    public TextMeshProUGUI instruccion;
    public TextMeshProUGUI tiempo;
    public GameObject flechaMonitori;
    public GameObject flechaSPO2;
    public GameObject flechaPresion;
    private Vector3 vector3Monitori;
    private Vector3 vector3Presion;
    public Transform MonitoriOriginal;
    public Transform presionOriginal;
    public Transform SPO2Original;
    public float tolerance = 0.1f;
    public Desfibrilador desfibrilador;
    public GameObject perillaSignos;
    public GameObject perillaOff;
    public GameObject InterfazSignos;
    public GameObject InterfazSignosCompleta;
    public Light BotonInvas;
    public GameObject interfazPrincipal;
    public GameObject interfaceFinalización;
    public GameObject interfaceCanvaFlotante;

    public AudioSource acierto;
    public GameObject aciertoVisual1;
    public GameObject aciertoVisual2;
    public GameObject aciertoVisual3;
    public GameObject cableSignosIndependiente;
    public int errroresRest = 2;
    public GameObject perillaRight;
    public GameObject conectorRight;

    public VitalesInstrumentos vitalesInstrumentos;

    public bool error1 = false;
    public bool error2 = false;
    public bool PasoNext;
     public float startTime;
    private bool isTimerRunning = false;
    private bool boolcito= false;
    public GameObject panelCodigo;
    
   
    private bool buttonBWasPressed = false; // Bandera para evitar múltiples detecciones por frame


    public int indicador;
    // Start is called before the first frame update
    void Start()
    {
        indicador=0;
        indicador ++;
        PasosSiguientes();
        BotonInvas.enabled = false;
        acierto = GetComponent<AudioSource>();
        PasoNext = true;
    }

    // Update is called once per frame
    void Update()
    {
        actualizacionIndicador();
        aperturaInterfaz();
       if(indicador>=4)
       {
        
       } 

        if (isTimerRunning)
        {
            float currentTime = Time.time - startTime;
            UpdateTimeText(currentTime);
        }
    }

    public void PasosSiguientes()
    {
       

        if (indicador == 1)
        {
            instruccion.text = "Bienvenido al módulo de signos vitales, aquí aprenderás a configurar la interfaz" + 
            " y utilizar algunos elementos de bioinstrumentación \n\n\n Presiona H para comenzar con la simulación. ";
        }else if (indicador == 2)
        {
            instruccion.text = "A tu derecha podrás ver los elementos con los cuales vas a interactuar." + 
            " Cuando agarres un objeto, podrás ver información del mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona H ";
        }else if (indicador ==3)
        {
            instruccion.text = "Perfecto!, ahora vamos a empezar a colocar los elementos" + 
            " de bioinstrumentación en el desfibrilador y maniquí, presiona H para ver el orden" +
            " correcto en el que tienes que colocar cada elemento.";

        }else if (indicador ==4)
        {

            instruccion.text= "El orden es: \n\n  1.Electrodos \n\n 2.Interfaz de pulsioximetría \n\n 3.La interfaz de tensión arterial";
            flechaMonitori.SetActive(true);
            desfibrilador.lightMonitoriza.enabled = true;
            PasoNext= false;
            
            


        }else if (indicador ==5)
        {
            instruccion.text= "El orden es: \n\n  1.Electrodos \n\n 2.Interfaz de pulsioximetría \n\n 3.La interfaz de tensión arterial";
            
            
            
            

        }else if (indicador ==6)
        {
            instruccion.text= "El orden es: \n\n  1.Electrodos \n\n 2.Interfaz de pulsioximetría \n\n 3.La interfaz de tensión arterial";
            
            
            PasoNext= true;
            
            
        }else if (indicador ==7)
        {
            instruccion.text= "Perfecto!, has conectado bien todos los elementos necesarios, ahora vamos a interactuar con el desfibrilador" +
            "\n\n Presion H para avanzar";
            
            aciertoVisual3.SetActive(true);
             aciertoVisual1.SetActive(false);
            aciertoVisual2.SetActive(false);
            aciertoVisual3.SetActive(false);
            
            
        }else if (indicador ==8)
        {

            instruccion.text= "Encendido del desfibrilador" +
            "\n\n Apunta a la perilla para ubicarla en el modo monitor. ";
            
       
            PasoNext = false;

           
            
            
        }else if (indicador ==9)
        {
            instruccion.text= "Desfibrilador encendido y con lecturas estables, !sigamos adelante!"+
            "\n\n Despliegue de lecturas presión:" +
            "\n Presiona el botón de acceso directo para ver la lectura de la presión. " +
            "\n Lo puedes ver resaltado en azul en el desfibrilador.";
    
            

            BotonInvas.enabled = true;
            
            
            
        } else if (indicador ==10)
        {
            instruccion.text =" Las lecturas que puedes ver son correspondientes a la monitorización del corazón por medio de los electrodos," + 
            " en segundo nivel, la pulxiometría y por último la toma de la presión"+ 
            "\n\n Simulación completada, para finalizar presiona el botón H ";
            BotonInvas.enabled = false;
            PasoNext = true;


        }else if (indicador ==11)
        {
            instruccion.text="Felicidades, ahora puedes tomar la simulación independiente de signosVitales" +
            "\n\n Presiona H para abrir la interfaz de finalización y acceder al módulo independiente de signosvitales.";
            
        }else if (indicador ==12)
        {
            interfaceCanvaFlotante.SetActive(false);
            interfaceFinalización.SetActive(true);
            PasoNext= false;
            boolcito= true;
             
        }
        else if (indicador==13)
        {
        
            instruccion.text="!Algo anda mal¡, no tenemos lecturas en el monitor y" +
            "\n\n la monitorización debe ser constante, apresúrate y arréglalo." +
            " \n\n\n Errores restantes: " +errroresRest;

            if (boolcito == true)
            {
            interfaceCanvaFlotante.SetActive(true);
            interfaceFinalización.SetActive(false);
            perillaSignos.SetActive(false);
            perillaRight.SetActive(true);
            conectorRight.SetActive(true);
            InterfazSignosCompleta.SetActive(false);
            vitalesInstrumentos.presionDesfri.SetActive(false);
            cableSignosIndependiente.SetActive(true);
            StartCoroutine(CambiarTexto(1));
            StartTimer();
                if (isTimerRunning)
            {
                float currentTime = Time.time - startTime;
                 UpdateTimeText(currentTime);
            }

            }
            boolcito= false;


            

    

        }else if (indicador==14)
        {
            instruccion.text="!Algo anda mal¡, no tenemos lecturas en el monitor y" +
            "\n\n la monitorización debe ser constante, apresúrate y arréglalo." +
            " \n\n\n Errores restantes: " +errroresRest;       
            
            
          

        }else if (indicador==15)
        {
                  
            instruccion.text="Perfecto, has solucionado los errores, tenemos lecturas de nuevo.!"
            + "\n\nPresiona H para compartir los resultados";
            StopTimer(); 
            panelCodigo.SetActive(true);
            interfaceCanvaFlotante.SetActive(false);
            
          

        }
        
            

        
        


    }

    private void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    private void StopTimer()
    {
        if (isTimerRunning)
        {
            float endTime = Time.time - startTime;
            UpdateTimeText(endTime);
            isTimerRunning = false;
        }
    }

    private void UpdateTimeText(float currentTime)
    {
        tiempo.text = "Tiempo: " + currentTime.ToString("F2") + "Seg";
    }


    public void actualizacionIndicador()
    {
        if(Input.GetKeyDown(KeyCode.H) && PasoNext== true)
        {
            indicador ++;
            PasosSiguientes();
        }

        // Detectar el botón B del mando derecho
    UnityEngine.XR.InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

    bool buttonBPressed;
    if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out buttonBPressed))
    {
        if (buttonBPressed && !buttonBWasPressed && PasoNext)
        {
            // Solo incrementar cuando se detecta el inicio del botón presionado
            indicador++;
            PasosSiguientes();
            Debug.Log(indicador);
            Debug.Log("Botón B presionado.");
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

    public void aperturaInterfaz()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
        bool estadoActual = interfazPrincipal.activeSelf;
        interfazPrincipal.SetActive(!estadoActual);
        }
        
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
         if (args.interactable.gameObject.tag == "SPO2" && indicador <= 4)
        {
        
            instruccion.text = "La interfaz de pulsioximetría mide indirectamente la saturación de oxígeno en la sangre (SpO2). "+
            "Utiliza un sensor en partes delgadas del cuerpo, como un dedo, y luz de distintas longitudes de onda para" + 
            "calcular el oxígeno en la sangre, siendo esencial en cuidados críticos y durante procedimientos quirúrgicos.";
            
        }else if (args.interactable.gameObject.tag == "Monitoriza" && indicador <4)
        {
            instruccion.text = "Los electrodos son dispositivos conductores usados para registrar la actividad eléctrica" + 
            " de órganos como el corazón, el cerebro y los músculos." +
            " Se colocan sobre la piel y son fundamentales en procedimientos como el electrocardiograma (ECG),"  
            + " que detecta anomalías cardíacas.";
        }else if (args.interactable.gameObject.tag == "Presion" && indicador <4)
        {
            instruccion.text = "La interfaz de tensión arterial facilita la medición de la presión arterial utilizando"
             + " métodos invasivos y no invasivos. Los métodos no invasivos, como el esfigmomanómetro digital,"
             +  " emplean un brazalete inflable para medir la presión" ;

             PasoNext= true;

        }else if (args.interactable.gameObject.tag == "Monitoriza" && indicador ==4)
        {
            instruccion.text = "Acerca los electrodos en donde te indica la flecha";

        }else if (args.interactable.gameObject.tag == "SPO2" && indicador ==5)
        {
            instruccion.text = "Acerca la interfaz de pulxiometría en donde te indica la flecha";

        }else if (args.interactable.gameObject.tag == "Presion" && indicador ==6)
        {
            instruccion.text = "Acerca la interfaz arterial en donde te indica la flecha";
            
        }else if (args.interactable.gameObject.tag == "AllowGuiada" && indicador == 12)
        {
              
            indicador = 13;
            PasosSiguientes();
            StartTimer();
            

        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        
        PasosSiguientes();

        if (args.interactable.gameObject.tag == "Monitoriza")
        {
            vector3Monitori = new Vector3(-3.8f, 2.3f,-0.4f);

        if (Vector3.Distance(MonitoriOriginal.position, vector3Monitori) > tolerance)
        {
            
            MonitoriOriginal.position = vector3Monitori;
        }   

        }else if (args.interactable.gameObject.tag == "Presion")
        {

            vector3Presion = new Vector3(-4.0f, 2.3f,0.7f);

        if (Vector3.Distance(presionOriginal.position, vector3Presion) > tolerance)
        {
           
            presionOriginal.position = vector3Presion;
        }   
        }

        else if (args.interactable.gameObject.tag == "SPO2")
        {

            Vector3 Vector3SPO2 = new Vector3(-4.3f, 2.3f,3.3f);

        if (Vector3.Distance(SPO2Original.position, Vector3SPO2) > tolerance)
        {
           
            SPO2Original.position = Vector3SPO2;
        }   
        }

        
        
    
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        
        if(args.interactable.gameObject.tag == "PerillaOff" && indicador == 8)
        {
            perillaSignos.SetActive(true);
            perillaOff.SetActive(false);
            InterfazSignos.SetActive(true);
            indicador =9;
            PasosSiguientes();
            
            

            
            

        }else if(args.interactable.gameObject.tag == "Perilla")
        {
           
            perillaOff.SetActive(false);
            
        }else if(args.interactable.gameObject.tag == "BotonInvasivo" && indicador==9)
        {
           
            InterfazSignosCompleta.SetActive(true);
            InterfazSignos.SetActive(false);
            
            indicador=10;
            PasosSiguientes();
        } else if (args.interactable.gameObject.tag == "signosIndependiente" && indicador ==13)
        {
            errroresRest --;
            
            
            error1= true;
            cableSignosIndependiente.SetActive(false);
            vitalesInstrumentos.presionDesfri.SetActive(true);
            acierto.Play();
            PasosSiguientes();
            
            

            if(error1== true & error2==true)
            {
                indicador=15;
                InterfazSignosCompleta.SetActive(true);
                InterfazSignos.SetActive(false);
                PasosSiguientes();
            }
           
           
            

        }else if (args.interactable.gameObject.tag == "PerillaRight" && indicador ==13 )
        {
            errroresRest --;
            boolcito= false;
            
            error2 = true;
            perillaRight.SetActive(false);
            perillaSignos.SetActive(true);
            InterfazSignos.SetActive(true);
            acierto.Play();
            PasosSiguientes();
            
             if(error1 == true & error2 ==true)
            {
                
                indicador =15;
                InterfazSignosCompleta.SetActive(true);
                InterfazSignos.SetActive(false);
                acierto.Play();
                PasosSiguientes();

            }

        }
        



    }

    private IEnumerator CambiarTexto(float t)
    {
        yield return new WaitForSeconds(t);
        indicador =13;
        
        
    }

    

 
}

