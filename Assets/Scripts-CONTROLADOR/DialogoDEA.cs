using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class DialogoDEA : MonoBehaviour
{
    private bool buttonBWasPressed = false; // Bandera para evitar múltiples detecciones por frame
    private  XRControls controls;
    public GameObject colliderDEA3;
    public int errores;
    public float tolerance = 0.1f;
    private Vector3 vector3Monitori;
    public Transform deaOriginal;
    private AudioSource audioSource;
    public GameObject[] flecha;
    public GameObject DEA2;
    public GameObject DEA3;
    public GameObject correctPlace;
    public GameObject perillaOff;
    public GameObject perillaDEA;
    public Desfibrilador dea;
    public TextMeshProUGUI instruccion;
    public int indicador;
    private bool PasoNext;

    public GameObject videoDEA;
    private bool isRunning; // Estado del temporizador
    public float elapsedTime = 0f; // Tiempo acumulado
    
    // Start is called before the first frame update
     void Awake()
    {
        controls = new XRControls();
    }

    void OnEnable()
    {
        controls.Controllers.AdvanceStep.performed += OnAdvanceStepPerformed;
        controls.Enable();
        Debug.Log($"Binding Path: {controls.Controllers.AdvanceStep.bindings[0].path}");
    }


    void OnDisable()
    {
        controls.Controllers.AdvanceStep.performed -= OnAdvanceStepPerformed;
        controls.Disable();
    }
    void Start()
    {   
        isRunning = false;
        errores = 2;
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

        
        if (indicador <9){
            PasosSiguientes();
         }



        if (indicador == 9 && !isRunning && errores == 2)
        {
            StartTimer();
        }
        // Verifica si el indicador es igual a 0 para detener el temporizador
        else if (errores == 0 && isRunning)
        {
            StopTimer();
            isRunning= false;
            indicador = 10;
            PasosSiguientes();
        }

        // Si el temporizador está corriendo, acumula el tiempo y actualiza el texto
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            PasosSiguientes();
        }
    


         
        
        
       
    }

    void StartTimer()
    {
        isRunning = true; // Inicia el temporizador
        elapsedTime = 0f; // Reinicia el contador
        PasosSiguientes(); // Actualiza el texto
        Debug.Log("Temporizador iniciado.");
    }

    void StopTimer()
    {
        isRunning = false; // Detiene el temporizador
        PasosSiguientes(); // Actualiza el texto
        Debug.Log($"Temporizador detenido. Tiempo total: {elapsedTime} segundos.");
    }
    private void OnAdvanceStepPerformed(InputAction.CallbackContext context)
    {
        if (PasoNext)
        {
            
            indicador++;
            PasosSiguientes();
        }
    }
     public void PasosSiguientes()
    {
        if (indicador == 1){
            instruccion.text = "Bienvenido al módulo de Desfibrilación Externo Automático, aquí aprenderás a configurar la interfaz y utilizar algunos elementos de bioinstrumentación. \n\n\n Presiona B para comenzar con la simulación. ";
        }else if (indicador == 2)
        {
            instruccion.text = "A tu derecha podrás ver el elemento con el cual vas a interactuar." + " Cuando agarres el objeto, podrás ver la información de el mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona B ";
        }else if (indicador ==3)
        {
            instruccion.text = "¡Perfecto! Ahora vamos a empezar a colocar el DEA en el desfibrilador y maniquí \n\n\n Presiona B para ver el orden correcto en el que tienes que colocar";

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
            instruccion.text = "Muy bien, ahora vamos a esperar a que el monitor analice el estado del paciente para saber si es necesaria una descarga electrica.\n\n presiona B para continuar.";
            videoDEA.SetActive(true);

            PasoNext=true;

        }else if(indicador == 7){

            instruccion.text = "El análisis automático determinó que es necesaria una descarga eléctrica, recuerda evitar cualquier contacto físico con el paciente una vez se vaya a inciar la descarga eléctrica. \n\n presiona B para continuar.";
            videoDEA.SetActive(false);

        }else if(indicador == 8){

            instruccion.text = "Felicidades ahora puedes tomar la simulación independiente de el DEA. \n\n presiona B para continuar";
            
        }else if(indicador == 9){
            Debug.Log(indicador);
            PasoNext = false;
            instruccion.text = "El DEA no está funcionando correctamente señala qué cosas están mal para arreglarlo, ¡Apresúrate!. \n\n errores: "+errores+$"\n\nTiempo: {elapsedTime:F1} segundos";

            if (perillaDEA.activeSelf && errores ==2){
                perillaOff.SetActive(true);
                perillaDEA.SetActive(false);
            }
            
            
            if (DEA2.activeSelf && errores ==2){
                DEA2.SetActive(false);
                DEA3.SetActive(true);
            }
            


            

        }else if(indicador == 10){

            instruccion.text = $"Felicidades haz terminado la simulación independiente de el DEA. \n\n con un tiempo de: {elapsedTime:F1} segundos";
            
        }else if(indicador == 100){

            instruccion.text = "Un DEA es un tipo de desfibrilador computarizado que analiza automáticamente el ritmo cardiaco en personas que están sufriendo un paro. Cuando sea necesario, envía una descarga eléctrica al corazón para normalizar su ritmo.  La conversión de una arritmia ventricular a su ritmo normal mediante una descarga eléctrica se denomina desfibrilación.";
        }
    }

    

public void actualizacionIndicador()
{
    // Detectar la tecla H como antes
    if (Input.GetKeyDown(KeyCode.H) && PasoNext)
    {
        indicador++;
        PasosSiguientes();
        Debug.Log(indicador);
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


    public void OnHoverEntered(HoverEnterEventArgs args)
    {

         if(args.interactable.gameObject.tag == "PerillaOff" && indicador == 5){

            perillaDEA.SetActive(true);
            perillaOff.SetActive(false);
         }

         if(args.interactable.gameObject.tag == "PerillaOff" && indicador == 9){

            perillaDEA.SetActive(true);
            perillaOff.SetActive(false);
            audioSource.Play();

            if (DEA2.activeSelf){
                errores = 0;
            } else{
                errores = 1;
            }
            PasosSiguientes();


         }else if(args.interactable.gameObject.tag == "DEA" && indicador == 9){

            DEA2.SetActive(true);
            DEA3.SetActive(false);
            colliderDEA3.SetActive(false);
            audioSource.Play();

            if (perillaDEA.activeSelf){
                errores = 0;
            } else{
                errores = 1;
            }
            PasosSiguientes();
            
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
        if (args.interactable.gameObject.tag == "DEA")
        {
            vector3Monitori = new Vector3(-4.0f, 2.17f,3.5f);

            if (Vector3.Distance(deaOriginal.position, vector3Monitori) > tolerance)
            {
                
                deaOriginal.position = vector3Monitori;
            }   

        }
        
        
        
        PasosSiguientes();
    }

}
