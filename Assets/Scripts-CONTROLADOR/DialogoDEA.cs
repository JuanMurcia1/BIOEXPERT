using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class DialogoDEA : MonoBehaviour
{
    public Desfibrilador dea;
    public TextMeshProUGUI instruccion;
    private int indicador;
    private bool PasoNext;
    // Start is called before the first frame update
    void Start()
    {
        PasoNext = true;
        indicador = 1;
    }

    // Update is called once per frame
    void Update()
    {
        instruccion.fontSize = 70;
        instruccion.alignment = TextAlignmentOptions.Center;
        actualizacionIndicador();

        PasosSiguientes();
    }
     public void PasosSiguientes()
    {
        if (indicador == 1){
            instruccion.text = "Bienvenido al módulo de Desfibrilación Externo Automático, aquí aprenderás a configurar la interfaz y utilizar algunos elementos de bioinstrumentación \n\n\n Presiona H para comenzar con la simulación. ";
        }else if (indicador == 2)
        {
            instruccion.text = "A tu derecha podrás ver el elemento con el cual vas a interactuar." + " Cuando agarres el objeto, podrás ver la información del mismo en esta pantalla." + 
            " \n\n Cuando hayas terminado presiona H ";
        }else if (indicador ==3)
        {
            instruccion.text = "perfecto!, ahora vamos a empezar a colocar el DEA en el desfibrilador y maniquí \n\n\n Presiona H para ver el orden correcto en el que tienes que colocar";

        }else if (indicador ==4)
        {
            instruccion.text = "el orden es: \n\n  1.Electrodos \n\n 2.Interfaz de pulsioximetría \n\n 3.La interfaz de tensión arterial";
            dea.lightDEA.enabled = true;

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
}
