using UnityEngine;

public class ColliderAction : MonoBehaviour
{
    public GameObject colliderObj; // Objeto con el que DEA colisionará
    public GameObject objectToActivate; // Objeto que se activará al colisionar
    private AudioSource audioSource; // Variable para almacenar el AudioSource del objeto a activar

    void Start()
    {
        // Intenta obtener el componente AudioSource solo del objeto que se activará
        if (objectToActivate != null)
        {
            audioSource = objectToActivate.GetComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica que el objeto que colisiona es el esperado
        if (other.gameObject == colliderObj)
        {
            // Desactiva el objeto DEA (este script está asignado a DEA)
            gameObject.SetActive(false);

            // Activa el objeto especificado y reproduce el sonido si el AudioSource está presente
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);

                // Si el AudioSource existe en el objeto a activar, reproduce el audio
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
