using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class DialogoMarcapasos : MonoBehaviour
{
    public float tolerance = 0.1f;
    private Vector3 vector3Monitori;
    public Transform MonitoriOriginal;
    private AudioSource audioSource;
    public GameObject[] flecha;
    public GameObject DEA2;
    public GameObject correctPlace;
    //public GameObject perillaOff;
    //public GameObject perillaDEA;
    public Desfibrilador monitoriza;
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
            instruccion.text = "Bienvenido al módulo de Marcapasos, aquí aprenderás a configurar la interfaz y utilizar algunos elementos de bioinstrumentación. \n\n\n Presiona H para comenzar con la simulación. ";
        }else if (indicador == 2)
        {
            instruccion.text = "A tu derecha podrás ver el elemento con el cual vas a interactuar." + " Cuando agarres el objeto, podrás ver la información de el mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona H ";
        }else if (indicador ==3)
        {
            instruccion.text = "¡Perfecto! Ahora vamos a empezar a colocar el Marcapasos en el equipo y maniquí \n\n\n Presiona H para ver el orden correcto en el que tienes que colocar";

            correctPlace.SetActive(true);
        }else if (indicador == 100)
        {
            instruccion.text = "Los electrodos son dispositivos conductores usados para registrar la actividad eléctrica" + 
            " de órganos como el corazón, el cerebro y los músculos." +
            " Se colocan sobre la piel y son fundamentales en procedimientos como el electrocardiograma (ECG),"  
            + " que detecta anomalías cardíacas.";
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
        
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
      if (args.interactable.gameObject.tag == "Monitoriza" && indicador <= 2)
        {
            indicador = 100;
            instruccion.text = "Los electrodos son dispositivos conductores usados para registrar la actividad eléctrica" + 
            " de órganos como el corazón, el cerebro y los músculos." +
            " Se colocan sobre la piel y son fundamentales en procedimientos como el electrocardiograma (ECG),"  
            + " que detecta anomalías cardíacas.";
        }
        
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactable.gameObject.tag == "Monitoriza")
        {
            
            if (indicador == 100)
            {
            indicador = 2;
            }

            
        }

        if (args.interactable.gameObject.tag == "Monitoriza")
        {
            vector3Monitori = new Vector3(-5.77f, 2.17f,4.0f);

            if (Vector3.Distance(MonitoriOriginal.position, vector3Monitori) > tolerance)
            {
                
                MonitoriOriginal.position = vector3Monitori;
            }   

        }
        
        
        
        PasosSiguientes();
    }

}
