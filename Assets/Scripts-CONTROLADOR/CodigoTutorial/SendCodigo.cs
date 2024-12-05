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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantén esta línea si optas por la Opción 1
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "tutorial")
        {
            // Reasignar las referencias a los elementos de la interfaz
            inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>();
            instruccion = GameObject.Find("Intricciones2").GetComponent<TextMeshProUGUI>();

            // Opcional: Limpiar el campo de texto y actualizar la instrucción
            inputField.text = "";
            instruccion.text = "Ingresa el código proporcionado.";
        }
    }

    public void OnButtoEntered(SelectEnterEventArgs args)
    {
        if (inputField == null || instruccion == null)
        {
            Debug.LogError("Las referencias a la interfaz de usuario son nulas.");
            return;
        }

        if (args.interactable.gameObject.CompareTag("enviar") && inputField.text.Length == 5)
        {
            savedCodigo = inputField.text;
            Debug.Log("Código guardado: " + savedCodigo);
            instruccion.text = "Código ingresado";

            LoadSceneBasedOnCode(savedCodigo);  
        }
        else if (args.interactable.gameObject.CompareTag("enviar") && inputField.text.Length < 5)
        {
            instruccion.text = "El código debe ser el de 5 dígitos que te dio la página";
        }
    }

    private void LoadSceneBasedOnCode(string code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            char firstDigit = code[0];  

            switch (firstDigit)
            {
                case '1':
                    SceneManager.LoadScene("DesfriManual");  
                    break;
                case '2':
                    SceneManager.LoadScene("SignosVitales");  
                    break;
                case '3':
                    SceneManager.LoadScene("DEA");  
                    break;
                case '4':
                    SceneManager.LoadScene("MarcaPas");  
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
