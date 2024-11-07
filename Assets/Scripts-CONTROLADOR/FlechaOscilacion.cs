using UnityEngine;

public class Oscillate : MonoBehaviour
{
    public float amplitude = 0.1f; // Altura máxima del movimiento
    public float frequency = 3f;   // Velocidad del movimiento

    private Vector3 startPosition;

    void Start()
    {
        // Guarda la posición inicial para que el objeto oscile en torno a ella
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcula el desplazamiento vertical usando una función seno
        float yOffset = amplitude * Mathf.Sin(Time.time * frequency);
        
        // Actualiza la posición del objeto
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}
