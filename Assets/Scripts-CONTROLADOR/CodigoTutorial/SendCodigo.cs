using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.SceneManagement;  

public class SendCodigo : MonoBehaviour
{
    public static SendCodigo Instance; 
    public TMP_InputField inputField;
    public TextMeshProUGUI instruccion;
    private string savedCodigo;

    void Awake()
    {
        // Implementar Singleton para que solo haya una instancia del script
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evita que este objeto sea destruido al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruimos la nueva para mantener solo una
        }
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.gameObject.tag == "enviar" && inputField.text.Length == 5)
        {
            savedCodigo = inputField.text;
            Debug.Log("Código guardado: " + savedCodigo);
            instruccion.text = "Código ingresado";

            LoadSceneBasedOnCode(savedCodigo);  
        }
        else if (args.interactable.gameObject.tag == "enviar" && inputField.text.Length < 5)
        {
            instruccion.text = "El código debe ser el de 5 dígitos que te dio la página";
        }
    }

    private void LoadSceneBasedOnCode(string code)
    {
        if (code.Length > 0)
        {
            char firstDigit = code[0];  

            switch (firstDigit)
            {
                case '1':
                    SceneManager.LoadScene("DesfriManual");  
                    break;
                case '2':
                    SceneManager.LoadScene("SignosVitales");  // Carga la escena SignosVitales
                    break;
                case '3':
                    SceneManager.LoadScene("DEA");  // Carga la escena DEA
                    break;
                case '4':
                    SceneManager.LoadScene("MarcaPas");  // Carga la escena MarcaPas
                    break;
                default:
                    Debug.Log("Código no reconocido o primer dígito no válido");
                    break;
            }
        }
    }

    public string GetSavedCodigo()
    {
        return savedCodigo;
    }
}
