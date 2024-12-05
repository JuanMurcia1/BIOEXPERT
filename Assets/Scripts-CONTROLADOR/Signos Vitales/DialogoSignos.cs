using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class DialogoSignos : MonoBehaviour
{
    [System.Serializable]
    public class Datos
    {
        public string presentationDateTime;
        public int score;
        public int completionTime;
    }

    // Variables de UI
    public TextMeshProUGUI instruccion;
    public TextMeshProUGUI tiempo;

    // GameObjects y referencias
    public GameObject flechaMonitori;
    public GameObject flechaSPO2;
    public GameObject flechaPresion;
    public GameObject perillaSignos;
    public GameObject perillaOff;
    public GameObject InterfazSignos;
    public GameObject InterfazSignosCompleta;
    public GameObject interfazPrincipal;
    public GameObject interfaceFinalizacion;
    public GameObject interfaceCanvaFlotante;
    public GameObject aciertoVisual1;
    public GameObject aciertoVisual2;
    public GameObject aciertoVisual3;
    public GameObject cableSignosIndependiente;
    public GameObject perillaRight;
    public GameObject conectorRight;
    public GameObject obj;


    // Audio y luces
    public AudioSource acierto;
    public Light BotonInvas;

    // Transforms y posiciones
    public Transform MonitoriOriginal;
    public Transform presionOriginal;
    public Transform SPO2Original;
    private Vector3 vector3Monitori;
    private Vector3 vector3Presion;
    private Vector3 vector3SPO2;
    public float tolerance = 0.1f;

    // Componentes y scripts
    public Desfibrilador desfibrilador;
    public VitalesInstrumentos vitalesInstrumentos;

    // Variables de control
    public int indicador = 1;
    public bool pasoNext = true;
    private bool error1 = false;
    private bool error2 = false;
    private bool boolcito = false;
    private bool buttonBWasPressed = false;
    private bool isTimerRunning = false;

    // Variables de tiempo y puntuación
    private float startTime;
    private int completionTime;
    private int erroresRestantes = 2;
    private int score;
    private string presentationDateTime;

    // Variables de código
    private string codigo;

    void Start()
    {
        indicador = 1;
        ObtenerCodigo();
        PasosSiguientes();
        InicializarComponentes();
        obj = GameObject.Find("SendCodigoController");
    }

    void Update()
    {
        ActualizarIndicador();
        AperturaInterfaz();
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
            Debug.Log("Código no encontrado en SignosVitales");
        }
    }

    // Inicializa componentes al inicio
    private void InicializarComponentes()
    {
        BotonInvas.enabled = false;
        acierto = GetComponent<AudioSource>();
        pasoNext = true;
    }

    // Actualiza el temporizador si está en ejecución
    private void ActualizarTemporizador()
    {
        if (isTimerRunning)
        {
            float tiempoActual = Time.time - startTime;
            UpdateTimeText(tiempoActual);
        }
    }

    // Método principal que controla los pasos del diálogo
    public void PasosSiguientes()
    {
        switch (indicador)
        {
            case 1:
                instruccion.text = "Bienvenido al módulo de signos vitales, aquí aprenderás a configurar la interfaz y utilizar algunos elementos de bioinstrumentación.\n\n\n Presiona B para comenzar con la simulación.";
                break;

            case 2:
                instruccion.text = "A tu derecha podrás ver los elementos con los cuales vas a interactuar. Cuando agarres un objeto, podrás ver información del mismo en esta pantalla.\n\n Cuando hayas terminado presiona B.";
                break;

            case 3:
                instruccion.text = "Perfecto, ahora vamos a empezar a colocar los elementos de bioinstrumentación en el desfibrilador y maniquí.\n\n Presiona B para ver el orden correcto en el que tienes que colocar cada elemento.";
                break;

            case 4:
                instruccion.text = "El orden es:\n\n 1. Electrodos \n\n 2. Interfaz de pulsioximetría \n\n 3. Interfaz de tensión arterial.";
                MostrarFlechaMonitorizacion(true);
                pasoNext = false;
                break;

            case 5:
            case 6:
                instruccion.text = "El orden es:\n\n 1. Electrodos \n\n 2. Interfaz de pulsioximetría \n\n 3. Interfaz de tensión arterial.";
                if (indicador == 6)
                {
                    pasoNext = true;
                }
                break;

            case 7:
                instruccion.text = "¡Perfecto! Has conectado bien todos los elementos necesarios, ahora vamos a interactuar con el desfibrilador.\n\n Presiona B para avanzar.";
                ReiniciarAciertosVisuales();
                break;

            case 8:
                instruccion.text = "Encendido del desfibrilador.\n\n Apunta a la perilla para ubicarla en el modo monitor.";
                pasoNext = false;
                break;

            case 9:
                instruccion.text = "Desfibrilador encendido y con lecturas estables, ¡sigamos adelante!\n\n Despliegue de lecturas de presión:\n Presiona el botón de acceso directo para ver la lectura de la presión.\n Lo puedes ver resaltado en azul en el desfibrilador.";
                BotonInvas.enabled = true;
                break;

            case 10:
                instruccion.text = "Las lecturas que puedes ver son correspondientes a la monitorización del corazón por medio de los electrodos, en segundo nivel, la pulsioximetría y por último la toma de la presión.\n\n Simulación completada, para finalizar presiona el botón B.";
                BotonInvas.enabled = false;
                pasoNext = true;
                break;

            case 11:
                instruccion.text = "¡Felicidades! Ahora puedes tomar la simulación independiente de signos vitales.\n\n Presiona B para abrir la interfaz de finalización y acceder al módulo independiente.";
                break;

            case 12:
                MostrarInterfaceFinalizacion(true);
                pasoNext = false;
                boolcito = true;
                break;

            case 13:
                PrepararErrores();
                break;

            case 14:
                instruccion.text = "!Algo anda mal! No tenemos lecturas en el monitor y la monitorización debe ser constante, apresúrate y arréglalo.\n\n Errores restantes: " + erroresRestantes;
                break;

            case 15:
                FinalizarSimulacion();
                DestruirCodigo();
                break;
        }
    }

    // Métodos auxiliares
    private void MostrarFlechaMonitorizacion(bool estado)
    {
        flechaMonitori.SetActive(estado);
        desfibrilador.lightMonitoriza.enabled = estado;
    }

    private void ReiniciarAciertosVisuales()
    {
        aciertoVisual1.SetActive(false);
        aciertoVisual2.SetActive(false);
        aciertoVisual3.SetActive(false);
    }

    private void MostrarInterfaceFinalizacion(bool estado)
    {
        interfaceCanvaFlotante.SetActive(!estado);
        interfaceFinalizacion.SetActive(estado);
    }

    private void PrepararErrores()
    {
        instruccion.text = "!Algo anda mal! No tenemos lecturas en el monitor y la monitorización debe ser constante, apresúrate y arréglalo.\n\n Errores restantes: " + erroresRestantes;

        if (boolcito)
        {
            interfaceCanvaFlotante.SetActive(true);
            interfaceFinalizacion.SetActive(false);
            perillaSignos.SetActive(false);
            perillaRight.SetActive(true);
            conectorRight.SetActive(true);
            InterfazSignosCompleta.SetActive(false);
            vitalesInstrumentos.presionDesfri.SetActive(false);
            cableSignosIndependiente.SetActive(true);
            StartCoroutine(CambiarIndicador(1));
            StartTimer();
            ActualizarTemporizador();
        }
        boolcito = false;

        StartTimer();
        ActualizarTemporizador();
        GetCurrentDateTime();
    }

    private void DestruirCodigo(){
        
        if (obj != null)
        {
            Destroy(obj);
            Debug.Log("Objeto SendCodigoController destruido.");
        }

    }

    private void FinalizarSimulacion()
    {
        instruccion.text = "¡Perfecto! Has solucionado los errores, tenemos lecturas de nuevo.\n\n" +
        "Tus resultados han sido compartidos en la página web y serás redirigido a tutorial para ingresar un nuevo código.";
        StopTimer();
        CalcularPuntuacion();
        StartCoroutine(EnviarDatosAlServidor());
        StartCoroutine(redirigirTutorial(9));
    }

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

    private void GetCurrentDateTime()
    {
        presentationDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        Debug.Log("Fecha y Hora guardadas: " + presentationDateTime);
    }

    private void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

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

    private void UpdateTimeText(float tiempo)
    {
        this.tiempo.text = "Tiempo: " + tiempo.ToString("F2") + " Seg";
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

    // Método para abrir o cerrar la interfaz principal
    public void AperturaInterfaz()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            interfazPrincipal.SetActive(!interfazPrincipal.activeSelf);
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
        string url = $"https://bioexpert-backend-c3afbb8cfa06.herokuapp.com/api/performance/{codigo}/monitorizacion";

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

    // Interacciones con objetos
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactable.gameObject.CompareTag("SPO2") && indicador <= 4)
        {
            instruccion.text = "La interfaz de pulsioximetría mide indirectamente la saturación de oxígeno en la sangre (SpO2). Utiliza un sensor en partes delgadas del cuerpo, como un dedo, y luz de distintas longitudes de onda para calcular el oxígeno en la sangre, siendo esencial en cuidados críticos y durante procedimientos quirúrgicos.";
        }
        else if (args.interactable.gameObject.CompareTag("Monitoriza") && indicador < 4)
        {
            instruccion.text = "Los electrodos son dispositivos conductores usados para registrar la actividad eléctrica de órganos como el corazón, el cerebro y los músculos. Se colocan sobre la piel y son fundamentales en procedimientos como el electrocardiograma (ECG), que detecta anomalías cardíacas.";
        }
        else if (args.interactable.gameObject.CompareTag("Presion") && indicador < 4)
        {
            instruccion.text = "La interfaz de tensión arterial facilita la medición de la presión arterial utilizando métodos invasivos y no invasivos. Los métodos no invasivos, como el esfigmomanómetro digital, emplean un brazalete inflable para medir la presión.";
            pasoNext = true;
        }
        else if (args.interactable.gameObject.CompareTag("Monitoriza") && indicador == 4)
        {
            instruccion.text = "Acerca los electrodos donde te indica la flecha.";
        }
        else if (args.interactable.gameObject.CompareTag("SPO2") && indicador == 5)
        {
            instruccion.text = "Acerca la interfaz de pulsioximetría donde te indica la flecha.";
        }
        else if (args.interactable.gameObject.CompareTag("Presion") && indicador == 6)
        {
            instruccion.text = "Acerca la interfaz arterial donde te indica la flecha.";
        }
        else if (args.interactable.gameObject.CompareTag("AllowGuiada") && indicador == 12)
        {
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
        }
        else if (args.interactable.gameObject.CompareTag("Presion"))
        {
            ResetearPosicionObjeto(presionOriginal, new Vector3(-4.0f, 2.3f, 0.7f));
        }
        else if (args.interactable.gameObject.CompareTag("SPO2"))
        {
            ResetearPosicionObjeto(SPO2Original, new Vector3(-4.3f, 2.3f, 3.3f));
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

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.gameObject.CompareTag("PerillaOff") && indicador == 8)
        {
            perillaSignos.SetActive(true);
            perillaOff.SetActive(false);
            InterfazSignos.SetActive(true);
            indicador = 9;
            PasosSiguientes();
        }
        else if (args.interactable.gameObject.CompareTag("Perilla"))
        {
            perillaOff.SetActive(false);
        }
        else if (args.interactable.gameObject.CompareTag("BotonInvasivo") && indicador == 9)
        {
            InterfazSignosCompleta.SetActive(true);
            InterfazSignos.SetActive(false);
            indicador = 10;
            PasosSiguientes();
        }
        else if (args.interactable.gameObject.CompareTag("signosIndependiente") && indicador == 13)
        {
            erroresRestantes--;
            error1 = true;
            cableSignosIndependiente.SetActive(false);
            vitalesInstrumentos.presionDesfri.SetActive(true);
            acierto.Play();
            PasosSiguientes();

            if (error1 && error2)
            {
                indicador = 15;
                InterfazSignosCompleta.SetActive(true);
                InterfazSignos.SetActive(false);
                PasosSiguientes();
            }
        }
        else if (args.interactable.gameObject.CompareTag("PerillaRight") && indicador == 13)
        {
            erroresRestantes--;
            boolcito = false;
            error2 = true;
            perillaRight.SetActive(false);
            perillaSignos.SetActive(true);
            InterfazSignos.SetActive(true);
            acierto.Play();
            PasosSiguientes();

            if (error1 && error2)
            {
                indicador = 15;
                InterfazSignosCompleta.SetActive(true);
                InterfazSignos.SetActive(false);
                acierto.Play();
                PasosSiguientes();
            }
        }
    }

    private IEnumerator CambiarIndicador(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        indicador = 13;
    }
}
