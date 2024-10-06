using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escenas : MonoBehaviour
{
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    private IEnumerator cambiar()
    {
        // Esperar 2 segundos
        yield return new WaitForSeconds(60);
        SceneManager.LoadScene("simulacion");
    }
}
