using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private XRNode xRNode = XRNode.LeftHand;

    private List<InputDevice> inputDevices = new List<InputDevice>();

    private InputDevice inputDevice;

    public XRRayInteractor interactor;
    public LineRenderer lineRenderer;
    public XRInteractorLineVisual interactorLineVisual;
    public Transform bpmModifierTransform;

    public AudioSource audioSource;
    public bool hasShooted = false;

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xRNode, inputDevices);
        inputDevice = inputDevices.FirstOrDefault();
    }

    private void Start()
    {
        interactor= gameObject.GetComponent<XRRayInteractor>();
        lineRenderer= gameObject.GetComponent<LineRenderer>();
        interactorLineVisual = gameObject.GetComponent<XRInteractorLineVisual>();
        audioSource= gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (!inputDevice.isValid)
        {
            GetDevice();
        }
    }

    private void Update()
    {
        if (!inputDevice.isValid)
        {
            GetDevice();
        }
        bool triggered = false;
        if(inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggered))
        {
            if(triggered)
            {
                if (!hasShooted)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                    hasShooted=true;
                }
                interactor.enabled = true;
                lineRenderer.enabled = true;
                interactorLineVisual.enabled = true;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.CompareTag("BPMModifier"))
                    {
                        bpmModifierTransform.SetParent(transform);
                    }
                }
            }
            else
            {   
                hasShooted=false;
                interactor.enabled = false;
                lineRenderer.enabled = false;
                interactorLineVisual.enabled = false;
                bpmModifierTransform.SetParent(null);
            }
        }
        if (bpmModifierTransform.transform.position.y >= 1 && bpmModifierTransform.transform.position.y <= 3)
            bpmModifierTransform.position = new Vector3(10, bpmModifierTransform.position.y, 10);
        else
            if(bpmModifierTransform.transform.position.y < 1)
                bpmModifierTransform.position = new Vector3(10, 1, 10);
            if (bpmModifierTransform.transform.position.y > 3)
                bpmModifierTransform.position = new Vector3(10, 3, 10);


    }
}
