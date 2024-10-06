using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.HID.HID;

public class menu : MonoBehaviour
{
    public Animator animator;  // Asigna el Animator en el Inspector
    public GameObject objectToActivate;  // El GameObject que se activar� despu�s de la animaci�n
    public GameObject objectToChao;  // El GameObject que se desactivar� despu�s de la animaci�n

    private bool isAnimationFinished = false;

    void Update()
    {
        if (!isAnimationFinished && IsAnimationFinished(animator))
        {
            isAnimationFinished = true;
            
            // Iniciar corrutina para activar objetos despu�s de 2 segundos
            StartCoroutine(ActivateObjectsAfterDelay());
        }
    }

    private bool IsAnimationFinished(Animator animator)
    {
        // Obtener la informaci�n del estado actual de la animaci�n
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Verificar si la animaci�n se est� reproduciendo y si ha terminado
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
    }

    private IEnumerator ActivateObjectsAfterDelay()
    {
        // Esperar 2 segundos
        yield return new WaitForSeconds(2);
        // Activar los GameObjects
        objectToChao.SetActive(false);
        objectToActivate.SetActive(true);
    }

}
