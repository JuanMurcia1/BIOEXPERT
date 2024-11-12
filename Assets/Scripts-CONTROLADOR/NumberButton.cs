using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NumberButton : MonoBehaviour
{
    public TMP_InputField inputField; // Referencia al InputField
    public string number; // El número que representa este botón



   

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.gameObject.tag == "1"&& inputField.text.Length < 4)
        {
        
           inputField.text += 1;
    
            
            
        }else if(args.interactable.gameObject.tag == "2" && inputField.text.Length < 4)
        {
            inputField.text += 2;

        }else if(args.interactable.gameObject.tag == "3" && inputField.text.Length < 4)
        {
            inputField.text += 3;

        }else if(args.interactable.gameObject.tag == "4" && inputField.text.Length < 4)
        {
            inputField.text += 4;

        }else if(args.interactable.gameObject.tag == "5" && inputField.text.Length < 4)
        {
            inputField.text += 5;

        }else if(args.interactable.gameObject.tag == "6" && inputField.text.Length < 4)
        {
            inputField.text += 6;

        }else if(args.interactable.gameObject.tag == "7" && inputField.text.Length < 4)
        {
            inputField.text += 7;

        }else if(args.interactable.gameObject.tag == "8" && inputField.text.Length < 4)
        {
            inputField.text += 8;

        }else if(args.interactable.gameObject.tag == "9" && inputField.text.Length < 4)
        {
            inputField.text += 9;

        }else if(args.interactable.gameObject.tag == "0" && inputField.text.Length < 4)
        {
            inputField.text += 0;
            

        }else if(args.interactable.gameObject.tag == "borrar" && inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);

        }
        
    }

 

    
}
