using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR;
using System;
using UnityEngine.Networking;



public class DialogosDesfriManual : MonoBehaviour
{
    [System.Serializable]
    public class Datos
    {
    public string presentationDateTime;
    public int score;
    public int completionTime;
    }
    public TextMeshProUGUI instruccion;
    public TextMeshProUGUI tiempo;
    public GameObject perillaOff;
    public GameObject perillaManual;
    public Desfibrilador desfibrilador;
    public Transform PalasMesaOriginal;
    public Transform DEAoRIGINAL;
    public float tolerance = 0.1f;
    private Vector3 vector3PalasMesas;
    private Vector3 vector3DEA;
    public int indicador = 0;
    public GameObject interfazPrincipal;
    public GameObject interfazDismiCarga;
    public GameObject interfazConfirmacion;
    public GameObject flechaPalas;
    public GameObject flechaDea;
    public AudioSource acierto;
    public GameObject aciertoVisual1;
    public GameObject aciertoVisual2;
    public  bool PasoNext;
    public GameObject interfazManual;
    public VitalesInstrumentosDesfri vitalesInstrumentosDesfri;
    public Light lightRaton;
    public Light lightBotonConfirm;
    private bool finGuiada;
    public GameObject interfaceFinalización;
    public GameObject interfaceCanvaFlotante;
    public int errroresRest = 2;
    public GameObject conectorIndependiente;
    public GameObject conectorPalasDea;
    public float startTime;
    private bool isTimerRunning = false;
    public GameObject panelCodigo;
    private bool codeOn = false;
    private bool buttonBWasPressed = false; // Bandera para evitar múltiples detecciones por frame

    public string presentationDateTime;
    public int score;        
    public int completionTime;
    
    // Start is called before the first frame update
    void Start()
    {
        PasosSiguientes();
        acierto = GetComponent<AudioSource>();
        PasoNext= true;
        lightRaton.enabled= false;
        lightBotonConfirm.enabled = false;
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
       

        if (indicador == 0)
        {
            instruccion.text = "Bienvenido al módulo de desfibrilación manual, aquí aprenderás a configurar la interfaz" + 
            " y utilizar algunos elementos de bioinstrumentación \n\n\n Presiona B para comenzar con la simulación. ";
        }else if (indicador == 1)
        {
            instruccion.text = "A tu derecha podrás ver los elementos con los cuales vas a interactuar." + 
            " Cuando agarres un objeto, podrás ver información del mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona B ";
            PasoNext= false;
        }else if (indicador ==2)
        {
            instruccion.text = "perfecto!, ahora vamos a empezar a colocar los elementos" + 
            " de bioinstrumentación en el desfibrilador y maniquí. \n\n Presiona B para ver el orden" +
            " correcto en el que tienes que colocar cada elemento.";

        }else if (indicador ==3)
        {

            instruccion.text= "El orden es: \n\n  1.Parches Electrodos";
            flechaDea.SetActive(true);
            desfibrilador.lightDEA.enabled = true;
            PasoNext= false;

        
        }else if (indicador ==4)
        {
            instruccion.text= "Perfecto!, has conectado bien todos los elementos necesarios, ahora vamos a interactuar con el desfibrilador" +
            " y la interfaz de desfibrilación manual. \n\n Presion B para avanzar";
            aciertoVisual1.SetActive(false);
            

        }else if (indicador ==5)
        {

            instruccion.text= "Encendido del desfibrilador" +
            "\n\n Apunta a la perilla para ubicarla en el modo desfibrilación automática (DEA). " +
            "Esto permitirá encender el desfibrilador y mostrar la interfaz correspondiente al módulo elegido.";
    

            
       
            PasoNext = false;
        }else if (indicador ==6)
        { 
            instruccion.text= "!Bien hecho!." + "\n\nSelección de energía" +
            "\n\n Interactua Con la perilla de ratón, resaltada en azúl, para disminuir la energía " +
            "Esto nos permite elegir la energía que posteriormente se descargará sobre el paciente.";

       
            
        }else if (indicador ==7)
        {

            instruccion.text= "!Bien hecho!, energía reducida. "+ "\n\n Proceso de descarga automática" +
            "\n\n Interactua con el botón de confirmación resaltado en azúl para establecer la energía. ";
            lightRaton.enabled= false;
            lightBotonConfirm.enabled= true;
       
            
        }else if (indicador ==8)
        {

            instruccion.text= "La descarga se realizará de forma automática cuando el paciente presente" +
            " ritmos irregulares en la monitorización de su corazón. " +
            "\n\n Presiona B para continuar";
       
            
        }else if (indicador ==9)
        {

            instruccion.text= "Simulación completada, para finalizar presiona el botón B.";
            
            
        }else if (indicador ==10)
        {
            interfaceCanvaFlotante.SetActive(false);
            interfaceFinalización.SetActive(true);
            PasoNext=false;
            
       
            
        }else if (indicador ==11)
        {
           
            conectorPalasDea.SetActive(false);
            conectorIndependiente.SetActive(true);
            
            interfaceCanvaFlotante.SetActive(true);
            interfaceFinalización.SetActive(false);
            instruccion.text= "!Algo anda mal¡, el monitor parece no tener lecturas de la interfaz DEA" +
            " y necesitamos confirmar la carga de desfibrilación para el paciente, apresúrate y arréglalo" +
            " \n\n\n Errores restantes: " +errroresRest;
            StartTimer();
             if (isTimerRunning)
              {
                completionTime = Mathf.RoundToInt(Time.time - startTime);
                UpdateTimeText(completionTime);
                }
            GetCurrentDateTime();                
            
            
       
            
        }else if (indicador ==12)
        {
            
            
            instruccion.text= "!Algo anda mal¡, el monitor parece no tener lecturas de la interfaz DEA" +
            " y necesitamos confirmar la carga de desfibrilación para el paciente, apresúrate y arréglalo" +
            " \n\n\n Errores restantes: " +errroresRest;
            
        }
        else if (indicador==13)
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

    private void UpdateTimeText(float completionTime)
    {
        tiempo.text = "Tiempo: " + completionTime.ToString("F2") + "Seg";
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
        string url = "https://bioexpert-backend-c3afbb8cfa06.herokuapp.com/api/performance/2705/desfibrilacionManual"; // Reemplaza con tu URL

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
    


    public void panelCode()
    {
        if(Input.GetKeyDown(KeyCode.H) && codeOn== true)
        {
            panelCodigo.SetActive(true);
            interfaceCanvaFlotante.SetActive(false);

        }
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
         if (args.interactable.gameObject.tag == "PalasMesa" && indicador <= 3)
        {
        
            instruccion.text = "Las palas de desfibrilación son electrodos metálicos usados en desfibriladores manuales,"+
            "Se aplican sobre el pecho del paciente con el fin de corregir arritmias peligrosas como la fibrilación" +
            " ventricular.\n\n En este módulo no se usarán, pero es importante que lo sepas.";
            
            
            
        }else if (args.interactable.gameObject.tag == "DEA" && indicador <3)
        {
            instruccion.text = "Los parches de desfibrilación automática se utilizan con desfibriladores automáticos"
            + " externos (DEA). Los DEA analizan automáticamente el ritmo cardíaco y, de ser necesario, "+
            "administran una descarga eléctrica para tratar arritmias sin necesidad de intervención manual"
            + " en la configuración de la carga.";

            
        }
    }



    public void OnSelectExit(SelectExitEventArgs args)
    {
        
        PasosSiguientes();

        if (args.interactable.gameObject.tag == "PalasMesa")
        {
            PasoNext= true;
            
            vector3PalasMesas = new Vector3(-4.0f, 2.3f,0.7f);
            

        if (Vector3.Distance(PalasMesaOriginal.position, vector3PalasMesas) > tolerance)
        {
            
            PalasMesaOriginal.position = vector3PalasMesas;
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

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        
        if(args.interactable.gameObject.tag == "PerillaOff" && indicador == 5)
        {
            perillaManual.SetActive(true);
            perillaOff.SetActive(false);
            interfazManual.SetActive(true);
            PasoNext= false;
            indicador =6;
            PasosSiguientes();
            lightRaton.enabled = true;
        

        }else if (args.interactable.gameObject.tag == "Raton" && indicador == 6)
        {
            PasoNext=false;
            interfazManual.SetActive(false);
            interfazDismiCarga.SetActive(true);
            indicador = 7;
            PasosSiguientes();


        }else if (args.interactable.gameObject.tag == "ConfirmEnergia" && indicador == 7)
        {
            interfazDismiCarga.SetActive(false);
            interfazConfirmacion.SetActive(true);    
            indicador = 8;
            PasosSiguientes();
            lightBotonConfirm.enabled=false;
            PasoNext= true;
            

        }else if (args.interactable.gameObject.tag == "AllowGuiada" && indicador == 10)
        {
            interfazDismiCarga.SetActive(false);
            interfazConfirmacion.SetActive(false);    
            indicador = 11;
            PasosSiguientes();
            StartTimer();
            

        }else if (args.interactable.gameObject.tag == "ConectorIndependiente" && indicador == 11)
        {
            conectorIndependiente.SetActive(false);
            conectorPalasDea.SetActive(true);
            interfazDismiCarga.SetActive(true);
            errroresRest --;
            indicador=12;
            PasosSiguientes();
            acierto.Play();


        }else if (args.interactable.gameObject.tag == "ConfirmEnergia" && indicador == 12)
        {
            interfazDismiCarga.SetActive(false);
            interfazConfirmacion.SetActive(true);
            indicador=13;
            errroresRest --;
            PasosSiguientes();
            acierto.Play();

        }   
        
    }

}



