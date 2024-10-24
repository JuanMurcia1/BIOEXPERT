using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class DialogoSignos : MonoBehaviour
{
    public TextMeshProUGUI instruccion;
    public TextMeshProUGUI instruccionAgarre;

    public int indicador =0;
    // Start is called before the first frame update
    void Start()
    {
        indicador ++;
        PasosSiguientes();
    }

    // Update is called once per frame
    void Update()
    {
        actualizacionIndicador();
    }

    public void PasosSiguientes()
    {
       

        if (indicador == 1)
        {
            instruccion.text = "Bienvenido al módulo de signos vitales, aquí aprenderás a configurar la interfaz" + 
            " y utilizar algunos elementos de bioinstrumentación \n\n\n Presiona H para comenzar con la simulación. ";
        }else if (indicador == 2)
        {
            instruccion.text = "A tu derecha podrás ver los elementos con los cuales vas a interactuar," + 
            " cuando agarres un objeto podrás ver información del mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona H ";
        }



    }

    public void actualizacionIndicador()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            indicador ++;
            PasosSiguientes();
        }
        
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
         if (args.interactable.gameObject.tag == "SPO2")
        {
        
            instruccion.text = "La interfaz de pulsioximetría mide indirectamente la saturación de oxígeno en la sangre (SpO2). "+
            "Utiliza un sensor en partes delgadas del cuerpo, como un dedo, y luz de distintas longitudes de onda para" + 
            "calcular el oxígeno en la sangre, siendo esencial en cuidados críticos y durante procedimientos quirúrgicos.";
            
        }else if (args.interactable.gameObject.tag == "Monitoriza")
        {
            instruccion.text = "Los electrodos son dispositivos conductores usados para registrar la actividad eléctrica" + 
            " de órganos como el corazón, el cerebro y los músculos." +
            " Se colocan sobre la piel y son fundamentales en procedimientos como el electrocardiograma (ECG),"  
            + " que detecta anomalías cardíacas.";
        }else if (args.interactable.gameObject.tag == "Presion")
        {
            instruccion.text = "La interfaz de tensión arterial facilita la medición de la presión arterial utilizando"
             + " métodos invasivos y no invasivos. Los métodos no invasivos, como el esfigmomanómetro digital,"
             +  " emplean un brazalete inflable para medir la presión" ;
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        
        PasosSiguientes();
    
    }
}

