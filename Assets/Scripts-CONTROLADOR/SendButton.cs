using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class SendButton : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI instruccion;
    private string savedNumber; // Variable para guardar el número
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnHoverEntered(HoverEnterEventArgs args)
    {
        
        if(args.interactable.gameObject.tag == "enviar" && inputField.text.Length == 5)
        {
            savedNumber = inputField.text;
            //Debug.Log("Código guardado: " + savedNumber);
            instruccion.text ="Datos enviados correctamente";

            
        

        }else if(args.interactable.gameObject.tag == "enviar" && inputField.text.Length < 5)
        {
            instruccion.text= "El código debe ser el de 5 dígitos que te dio la página";

        }
    }

     
}
