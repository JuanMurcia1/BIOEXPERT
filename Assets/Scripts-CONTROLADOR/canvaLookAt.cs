using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Asigna el transform del jugador en el Inspector

    void Update()
    {
        // Si el jugador está asignado, ajustar la rotación del panel hacia el jugador
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(-direction);
            transform.rotation = rotation;
        }
    }
}
