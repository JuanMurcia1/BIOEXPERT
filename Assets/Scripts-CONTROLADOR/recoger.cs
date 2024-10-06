using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class recoger : MonoBehaviour
{
    public Transform followTarget;  // El punto de referencia al que el objeto seguir�
    public string texto;  // El texto que se establecer� desde el inspector
    public GameObject objetoParaActivar;  // El objeto que se activar� despu�s de 10 segundos
    public GameObject objetonoParaActivar;
    public GameObject objetoParaActivarConColision; // El objeto que se activar� y desactivar� con la colisi�n
    public GameObject panParaActivar;
    public GameObject panaParaActivar;

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;

        // Verificar el valor del texto
        if (texto == "brazo")
        {
            StartCoroutine(EsperarYActivar());
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    void Update()
    {
        if (isGrabbed && followTarget != null)
        {
            // Hacer que el objeto siga la posici�n y rotaci�n del followTarget
            transform.position = followTarget.position;
            transform.rotation = followTarget.rotation;
        }
    }

    private IEnumerator EsperarYActivar()
    {
        yield return new WaitForSeconds(5f);  // Esperar 10 segundos
        if (objetoParaActivar != null)
        {
            objetoParaActivar.SetActive(true);  // Activar el objeto
            objetonoParaActivar.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(texto))
        {
            if (objetoParaActivarConColision != null)
            {
                objetoParaActivarConColision.SetActive(true); // Activar el objeto
                StartCoroutine(DesactivarDespuesDeUnSegundo(this.gameObject));
            }
        }
    }

    private IEnumerator DesactivarDespuesDeUnSegundo(GameObject objeto)
    {
        yield return new WaitForSeconds(0.5f);  // Esperar 1 segundo
        panParaActivar.SetActive(true);  // Activar el objeto
        panaParaActivar.SetActive(false);
        objeto.SetActive(false);  // Desactivar el objeto
    }
}
