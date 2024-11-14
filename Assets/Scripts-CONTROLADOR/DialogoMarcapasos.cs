using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR;
using System;
using UnityEngine.Networking;

public class DialogoMarcapasos : MonoBehaviour
{
    [System.Serializable]
    public class Datos
    {
    public string presentationDateTime;
    public int score;
    public int completionTime;
    }
    private bool buttonBWasPressed = false; // Bandera para evitar múltiples detecciones por frame
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
    public GameObject interfaceFinalización;
    public GameObject interfaceCanvaFlotante;

    public GameObject interfazMarcapasos;
    public GameObject interfazmarcaAumentaE;
    public GameObject interfazMarcaEntrega;
    public GameObject conectorIndependienteMoni;
    public GameObject conectormoniGood;
    public GameObject interfazPrincipal;
    
   public Light lightRaton;
   public Light lightBotonConfirm;
   public int errroresRest = 2;

    public TextMeshProUGUI instruccion;
    public TextMeshProUGUI tiempo;
    public int indicador;
    public bool PasoNext;
    public string presentationDateTime;
    public int score;        
    public int completionTime;
    public float startTime;
    private bool isTimerRunning = false;
    private bool codeOn = false;
    public GameObject panelCodigo;
    public SendCodigo sendCodigo;
    
    // Start is called before the first frame update
    void Start()
    {
        
        PasoNext = true;
        indicador = 1;
        PasosSiguientes();
        lightRaton.enabled= false;
        lightBotonConfirm.enabled = false;
        //sendCodigo.savedCodigo=
    }

    // Update is called once per frame
    void Update()
    {
        
        actualizacionIndicador();
        if (isTimerRunning)
        {
            float completionTime = Time.time - startTime;
            UpdateTimeText(completionTime);
        }
        panelCode();
        
        
       
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
        }else if (indicador ==8)
        {

           instruccion.text= "!Bien hecho!." + "\n\nAumento de energía" +
            "\n\n Interactua Con la perilla de ratón, resaltada en azúl, para aumentar la energía " +
            "Esto nos permite elegir la energía que posteriormente empleara el marcapasos.";

            
        }else if (indicador ==9)
        {

           instruccion.text= "!Bien hecho!, energía aumentada. "+ "\n\n Proceso entrega energía" +
            "\n\n Interactua con el botón de confirmación resaltado en azúl para entregar la energía. ";

            
        }else if (indicador ==10)
        {

           instruccion.text= "La descarga se realizará de forma automática y constante para lograr mantener" +
            " ritmos regulares en la monitorización. " +
            "\n\n Presiona H para continuar";

            
        }else if (indicador ==11)
        {

            instruccion.text= "Simulación completada, para finalizar presiona el botón B.";
            
            
        }else if (indicador ==12)
        {
            interfaceCanvaFlotante.SetActive(false);
            interfaceFinalización.SetActive(true);
            PasoNext=false;
            
       
            
        }else if (indicador ==13)
        {
           
            
            interfaceCanvaFlotante.SetActive(true);
            interfaceFinalización.SetActive(false);
            interfazMarcaEntrega.SetActive(false);
            conectormoniGood.SetActive(false);
            conectorIndependienteMoni.SetActive(true);
            instruccion.text= "!Algo anda mal¡, el monitor parece no tener lecturas de la interfaz Marcapasos" +
            " y necesitamos confirmar la carga de desfibrilación para el paciente, apresúrate y arréglalo" +
            " \n\n\n Errores restantes: " +errroresRest;
            StartTimer();
             if (isTimerRunning)
              {
                completionTime = Mathf.RoundToInt(Time.time - startTime);
                UpdateTimeText(completionTime);
                }
            GetCurrentDateTime();                
            
            
       
            
        }else if (indicador ==14)
        {
           
            
        
            instruccion.text= "!Algo anda mal¡, el monitor parece no tener lecturas de la interfaz Marcapasos" +
            " y necesitamos confirmar la carga de desfibrilación para el paciente, apresúrate y arréglalo" +
            " \n\n\n Errores restantes: " +errroresRest;
                        
            
            
       
            
        }else if (indicador ==15)
        {
           
            
        
            instruccion.text="Perfecto, has solucionado los errores, tenemos lecturas y está listo para una desfibrilación si se requiere.!"+
            "\n\nPresiona B para compartir los resultados";
             StopTimer();

             codeOn=true;
            if(completionTime <= 10 )
            {
                score= 100;
                Debug.Log("Su puntuación es de: " + score);
            }else if (completionTime > 10 )
            {
                score= 60;

            }else if(completionTime > 100)
            {
                score=20;
            }

            Debug.Log(completionTime);

            StartCoroutine(EnviarDatosAlServidor());
            
             
                        
            
            
       
            
        }
    }

    void GetCurrentDateTime()
    {
        presentationDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // Guarda la fecha y hora actual
        Debug.Log("Fecha y Hora guardadas: " + presentationDateTime.ToString());
    }

     private void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        if (isTimerRunning) // Verifica si el temporizador está activo
        {
            float elapsedTime = Time.time - startTime; // Calcula el tiempo transcurrido
            completionTime = (int)elapsedTime; // Convierte a segundos enteros y guarda en completionTime
            isTimerRunning = false; // Desactiva el temporizador
            Debug.Log("Tiempo completado: " + completionTime + " segundos.");
        }
    }
    public void panelCode()
    {
        if(Input.GetKeyDown(KeyCode.H) && codeOn== true)
        {
            panelCodigo.SetActive(true);
            interfaceCanvaFlotante.SetActive(false);

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

    IEnumerator EnviarDatosAlServidor()
    {
        // Crea una instancia de la clase Datos y asigna tus variables
        Datos datos = new Datos();
        datos.presentationDateTime = presentationDateTime;
        datos.score = score;
        datos.completionTime = Mathf.RoundToInt(completionTime );

        string jsonData = JsonUtility.ToJson(datos);

    // URL del endpoint al que enviarás los datos
        string url = "https://bioexpert-backend-c3afbb8cfa06.herokuapp.com/api/performance/3771/marcapasos"; // Reemplaza con tu URL

    // Crear la petición PUT
        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Enviar la petición y esperar la respuesta
        yield return request.SendWebRequest();

        // Verificar si hubo errores
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Datos enviados correctamente: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error al enviar datos: " + request.error);
        }
    }

    private void UpdateTimeText(float completionTime)
    {
        tiempo.text = "Tiempo: " + completionTime.ToString("F2") + "Seg";
    }

    public void actualizacionIndicador()
    {
        if(Input.GetKeyDown(KeyCode.H) && PasoNext== true)
        {
            indicador ++;
            PasosSiguientes();
            
        }

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
            
        

        }else if (args.interactable.gameObject.tag == "Raton" && indicador == 8)
        {
            
            interfazMarcapasos.SetActive(false);
            interfazmarcaAumentaE.SetActive(true);
            indicador = 9;
            PasosSiguientes();
            lightRaton.enabled = false;
            lightBotonConfirm.enabled = true;


        }else if (args.interactable.gameObject.tag == "ConfirmEnergia" && indicador == 9)
        {
        
            interfazmarcaAumentaE.SetActive(false);
            interfazMarcaEntrega.SetActive(true);
            indicador = 10;
            PasosSiguientes();
            lightBotonConfirm.enabled = false;
            PasoNext=true;


        }else if (args.interactable.gameObject.tag == "monitoriIndi" && indicador == 13)
        {
            conectorIndependienteMoni.SetActive(false);
            conectormoniGood.SetActive(true);
            
            interfazmarcaAumentaE.SetActive(true);
            errroresRest --;
            indicador=14;
            PasosSiguientes();
            acierto.Play();


        }else if (args.interactable.gameObject.tag == "ConfirmEnergia" && indicador == 14)
        {
        
            interfazmarcaAumentaE.SetActive(false);
            interfazMarcaEntrega.SetActive(true);
            errroresRest --;
            indicador=15;
            PasosSiguientes();
            acierto.Play();


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

            
        }else if (args.interactable.gameObject.tag == "AllowGuiada" && indicador == 12)
        {
            interfazMarcaEntrega.SetActive(false);
               
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
