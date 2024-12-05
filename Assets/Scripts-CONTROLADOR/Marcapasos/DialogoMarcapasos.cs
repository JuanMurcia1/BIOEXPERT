using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DialogoMarcapasos : MonoBehaviour
{
    [System.Serializable]
    public class Datos
    {
        public string presentationDateTime;
        public int score;
        public int completionTime;
    }

    // Variables de control de interacción
    private bool buttonBWasPressed = false;
    private bool isTimerRunning = false;
    public bool pasoNext = true;

    // Variables de temporización
    public float tolerance = 0.1f;
    public float startTime;
    public int completionTime;
    public int errroresRest = 2;

    // Variables de estado
    public int indicador = 1;
    public int score;
    public string presentationDateTime;
    private string codigo;

    // Referencias a GameObjects y componentes
    public Transform MonitoriOriginal;
    public Transform DEAoRIGINAL;
    public GameObject flechaDEA;
    public GameObject flechaMonitori;
    public GameObject DEA2;
    public GameObject aciertoVisual1;
    public GameObject aciertoVisual2;
    public GameObject perillaOff;
    public GameObject perillaMarcapasos;
    public GameObject interfaceFinalizacion;
    public GameObject interfaceCanvaFlotante;
    public GameObject interfazMarcapasos;
    public GameObject interfazMarcaAumentaE;
    public GameObject interfazMarcaEntrega;
    public GameObject conectorIndependienteMoni;
    public GameObject conectormoniGood;
    public GameObject interfazPrincipal;
    public GameObject panelCodigo;
    public GameObject obj;

    // Referencias a otros scripts y componentes
    public Desfibrilador desfibrilador;
    public AudioSource acierto;
    public SendCodigo sendCodigo;
    public Light lightRaton;
    public Light lightBotonConfirm;
    public TextMeshProUGUI instruccion;
    public TextMeshProUGUI tiempo;

    // Vectores de posición
    private Vector3 vector3Monitori;
    private Vector3 vector3DEA;

    void Start()
    {
        pasoNext = true;
        indicador = 1;
        ObtenerCodigo();
        PasosSiguientes();
        InicializarLuces();
        obj = GameObject.Find("SendCodigoController");
    }

    void Update()
    {
        ActualizarIndicador();
        ActualizarTemporizador();
        
    }

    // Método para obtener el código desde otro script
    private void ObtenerCodigo()
    {
        if (SendCodigo.Instance != null)
        {
            codigo = SendCodigo.Instance.GetSavedCodigo();
            codigo = codigo.Substring(codigo.Length - 4);
            Debug.Log("Código obtenido: " + codigo);
        }
        else
        {
            Debug.Log("Código no encontrado en Marcapasos");
        }
    }

    // Inicializa las luces al inicio
    private void InicializarLuces()
    {
        lightRaton.enabled = false;
        lightBotonConfirm.enabled = false;
    }

    // Actualiza el temporizador si está en ejecución
    private void ActualizarTemporizador()
    {
        if (isTimerRunning)
        {
            float tiempoTranscurrido = Time.time - startTime;
            UpdateTimeText(tiempoTranscurrido);
        }
    }

   

    // Método principal que controla los pasos del diálogo
    public void PasosSiguientes()
    {
        switch (indicador)
        {
            case 1:
                instruccion.text = "Bienvenido al módulo de Marcapasos, aquí aprenderás a configurar la interfaz y utilizar algunos elementos de bioinstrumentación." +
                                   "\n\n\n Presiona B para comenzar con la simulación.";
                break;

            case 2:
                instruccion.text = "A tu derecha podrás ver el elemento con el cual vas a interactuar. Cuando agarres el objeto, podrás ver la información de el mismo en esta pantalla." +
                                   "\n\n Cuando hayas terminado presiona B.";
                pasoNext = false;
                break;

            case 3:
                instruccion.text = "¡Perfecto! Ahora vamos a empezar a colocar los elementos en el equipo y maniquí." +
                                   "\n\n\n Presiona B para ver el orden correcto.";
                break;

            case 4:
                instruccion.text = "El orden es:\n\n 1. Interfaz de monitorización \n\n 2. Interfaz DEA.";
                MostrarFlechaMonitorizacion(true);
                pasoNext = false;
                break;

            case 5:
                instruccion.text = "El orden es:\n\n 1. Interfaz de monitorización \n\n 2. Interfaz DEA.";
                break;

            case 6:
                instruccion.text = "¡Perfecto! Has conectado bien todos los elementos necesarios, ahora vamos a interactuar con el desfibrilador." +
                                   "\n\n Presiona B para avanzar.";
                break;

            case 7:
                instruccion.text = "Encendido del desfibrilador.\n\n Apunta a la perilla para ubicarla en el modo marcapasos. " +
                                   "Esto permitirá encender el desfibrilador y mostrar la interfaz correspondiente al módulo elegido.";
                pasoNext = false;
                break;

            case 8:
                instruccion.text = "¡Bien hecho!\n\nAumento de energía.\n\n Interactúa con la perilla de ratón, resaltada en azul, para aumentar la energía. " +
                                   "Esto nos permite elegir la energía que posteriormente empleará el marcapasos.";
                EncenderLuzRaton(true);
                break;

            case 9:
                instruccion.text = "¡Bien hecho!, energía aumentada.\n\n Proceso entrega energía.\n\n Interactúa con el botón de confirmación resaltado en azul para entregar la energía.";
                EncenderLuzRaton(false);
                EncenderLuzBotonConfirm(true);
                break;

            case 10:
                instruccion.text = "La descarga se realizará de forma automática y constante para lograr mantener ritmos regulares en la monitorización." +
                                   "\n\n Presiona B para continuar.";
                break;

            case 11:
                instruccion.text = "Simulación completada, para finalizar presiona el botón B.";
                break;

            case 12:
                MostrarInterfaceFinalizacion(true);
                pasoNext = false;
                break;

            case 13:
                PrepararErrores();
                break;

            case 14:
                instruccion.text = "!Algo anda mal! El monitor parece no tener lecturas de la interfaz Marcapasos y necesitamos confirmar la carga de desfibrilación para el paciente, apresúrate y arréglalo." +
                                   "\n\n\n Errores restantes: " + errroresRest;
                break;

            case 15:
                FinalizarSimulacion();
                DestruirCodigo();
                break;
        }
    }

    // Métodos auxiliares para mejorar la legibilidad
    private void MostrarFlechaMonitorizacion(bool estado)
    {
        desfibrilador.lightMonitoriza.enabled = estado;
        flechaMonitori.SetActive(estado);
    }

    private void EncenderLuzRaton(bool estado)
    {
        lightRaton.enabled = estado;
    }

    private void EncenderLuzBotonConfirm(bool estado)
    {
        lightBotonConfirm.enabled = estado;
    }

    private void MostrarInterfaceFinalizacion(bool estado)
    {
        interfaceCanvaFlotante.SetActive(!estado);
        interfaceFinalizacion.SetActive(estado);
    }

    private void PrepararErrores()
    {
        interfaceCanvaFlotante.SetActive(true);
        interfaceFinalizacion.SetActive(false);
        interfazMarcaEntrega.SetActive(false);
        conectormoniGood.SetActive(false);
        conectorIndependienteMoni.SetActive(true);

        instruccion.text = "!Algo anda mal! El monitor parece no tener lecturas de la interfaz Marcapasos y necesitamos confirmar la carga de desfibrilación para el paciente, apresúrate y arréglalo." +
                           "\n\n\n Errores restantes: " + errroresRest;

        StartTimer();
        ObtenerFechaHoraActual();
    }

    private void FinalizarSimulacion()
    {
        instruccion.text = "Perfecto, has solucionado los errores, tenemos lecturas y está listo para una desfibrilación si se requiere." +
                           "\n\nTus resultados han sido compartidos en la página web y serás redirigido a tutorial para ingresar un nuevo código.";
        StopTimer();
        CalcularPuntuacion();
        StartCoroutine(EnviarDatosAlServidor());
        StartCoroutine(redirigirTutorial(9));
    }

    private void DestruirCodigo(){
        
        if (obj != null)
        {
            Destroy(obj);
            Debug.Log("Objeto SendCodigoController destruido.");
        }

    }

    // Calcula la puntuación basada en el tiempo
    private void CalcularPuntuacion()
    {
        if (completionTime <= 10)
        {
            score = 100;
        }
        else if (completionTime > 10 && completionTime <= 100)
        {
            score = 60;
        }
        else
        {
            score = 20;
        }
        Debug.Log("Puntuación: " + score);
    }

    // Obtiene la fecha y hora actual
    private void ObtenerFechaHoraActual()
    {
        presentationDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        Debug.Log("Fecha y Hora guardadas: " + presentationDateTime);
    }

    // Inicia el temporizador
    private void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    // Detiene el temporizador
    public void StopTimer()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            completionTime = (int)elapsedTime;
            isTimerRunning = false;
            Debug.Log("Tiempo completado: " + completionTime + " segundos.");
        }
    }

    // Actualiza el texto del tiempo
    private void UpdateTimeText(float completionTime)
    {
        tiempo.text = "Tiempo: " + completionTime.ToString("F2") + " Seg";
    }

    // Maneja la actualización del indicador basado en la entrada del usuario
    public void ActualizarIndicador()
    {
        if (Input.GetKeyDown(KeyCode.B) && pasoNext)
        {
            indicador++;
            PasosSiguientes();
        }

        UnityEngine.XR.InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool buttonBPressed))
        {
            if (buttonBPressed && !buttonBWasPressed && pasoNext)
            {
                indicador++;
                PasosSiguientes();
                Debug.Log("Indicador: " + indicador);
                Debug.Log("Botón B presionado.");
            }
            buttonBWasPressed = buttonBPressed;
        }
        else
        {
            buttonBWasPressed = false;
        }
    }

    // Enviar datos al servidor
    IEnumerator EnviarDatosAlServidor()
    {
        Datos datos = new Datos
        {
            presentationDateTime = presentationDateTime,
            score = score,
            completionTime = completionTime
        };

        string jsonData = JsonUtility.ToJson(datos);
        string url = $"https://bioexpert-backend-c3afbb8cfa06.herokuapp.com/api/performance/{codigo}/marcapasos";

        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Datos enviados correctamente: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error al enviar datos: " + request.error);
        }
    }


     IEnumerator redirigirTutorial(float t)
    {
        
        yield return new WaitForSeconds(t);

        SceneManager.LoadScene("tutorial"); 

    }

    // Métodos de interacción
    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.gameObject.CompareTag("PerillaOff") && indicador == 7)
        {
            CambiarEstadoPerilla(true);
            interfazMarcapasos.SetActive(true);
            pasoNext = false;
            indicador = 8;
            PasosSiguientes();
            EncenderLuzRaton(true);
        }
        else if (args.interactable.gameObject.CompareTag("Raton") && indicador == 8)
        {
            interfazMarcapasos.SetActive(false);
            interfazMarcaAumentaE.SetActive(true);
            indicador = 9;
            PasosSiguientes();
            EncenderLuzRaton(false);
            EncenderLuzBotonConfirm(true);
        }
        else if (args.interactable.gameObject.CompareTag("ConfirmEnergia") && indicador == 9)
        {
            interfazMarcaAumentaE.SetActive(false);
            interfazMarcaEntrega.SetActive(true);
            indicador = 10;
            PasosSiguientes();
            EncenderLuzBotonConfirm(false);
            pasoNext = true;
        }
        else if (args.interactable.gameObject.CompareTag("monitoriIndi") && indicador == 13)
        {
            conectorIndependienteMoni.SetActive(false);
            conectormoniGood.SetActive(true);
            interfazMarcaAumentaE.SetActive(true);
            errroresRest--;
            indicador = 14;
            PasosSiguientes();
            acierto.Play();
        }
        else if (args.interactable.gameObject.CompareTag("ConfirmEnergia") && indicador == 14)
        {
            interfazMarcaAumentaE.SetActive(false);
            interfazMarcaEntrega.SetActive(true);
            errroresRest--;
            indicador = 15;
            PasosSiguientes();
            acierto.Play();
        }
    }

    // Cambia el estado de la perilla
    private void CambiarEstadoPerilla(bool estadoMarcapasos)
    {
        perillaMarcapasos.SetActive(estadoMarcapasos);
        perillaOff.SetActive(!estadoMarcapasos);
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactable.gameObject.CompareTag("Monitoriza") && indicador <= 2)
        {
            instruccion.text = "Los electrodos son dispositivos conductores usados para registrar la actividad eléctrica de órganos como el corazón, el cerebro y los músculos. " +
                               "Se colocan sobre la piel y son fundamentales en procedimientos como el electrocardiograma (ECG), que detecta anomalías cardíacas.";
        }
        else if (args.interactable.gameObject.CompareTag("DEA") && indicador < 3)
        {
            instruccion.text = "Los parches de desfibrilación automática se utilizan con desfibriladores automáticos externos (DEA). Los DEA analizan automáticamente el ritmo cardíaco y, de ser necesario, " +
                               "administran una descarga eléctrica para tratar arritmias sin necesidad de intervención manual en la configuración de la carga.";
        }
        else if (args.interactable.gameObject.CompareTag("AllowGuiada") && indicador == 12)
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

        if (args.interactable.gameObject.CompareTag("Monitoriza"))
        {
            ResetearPosicionObjeto(MonitoriOriginal, new Vector3(-3.8f, 2.3f, -0.4f));
            pasoNext = true;
        }
        else if (args.interactable.gameObject.CompareTag("DEA"))
        {
            ResetearPosicionObjeto(DEAoRIGINAL, new Vector3(-4.3f, 2.3f, 3.3f));
            pasoNext = true;
        }
    }

    // Resetea la posición de un objeto a su posición original si es necesario
    private void ResetearPosicionObjeto(Transform objeto, Vector3 posicionOriginal)
    {
        if (Vector3.Distance(objeto.position, posicionOriginal) > tolerance)
        {
            objeto.position = posicionOriginal;
        }
    }

    // Método para abrir o cerrar la interfaz principal
    public void AperturaInterfaz()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            interfazPrincipal.SetActive(!interfazPrincipal.activeSelf);
        }
    }
}
