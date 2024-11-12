using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class SendButton : MonoBehaviour
{
    public TMP_InputField inputField;
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
        
        if(args.interactable.gameObject.tag == "enviar")
        {
            savedNumber = inputField.text;
            Debug.Log("Código guardado: " + savedNumber);
            
        

        }
    }
}
