using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //Distancia maxima para interactuar
    public float interactionDistance = 1f;

    void Update()
    {
        //Comprobar si se pulsa la tecla E
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            //Buscar objetos cercanos al jugador
            Collider[] objects = Physics.OverlapSphere(transform.position, interactionDistance);

            Collider closestObject = null;
            float closestDistance = interactionDistance;

            foreach (Collider obj in objects)
            {
                //Comprobar si el objeto es interactuable
                if (obj.CompareTag("Interactable"))
                {
                    float distance = Vector3.Distance(transform.position, obj.ClosestPoint(transform.position));

                    //Guardar el objeto mas cercano
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj;
                    }
                }
            }

            //Interactuar con el objeto encontrado
            if (closestObject != null)
            {
                MachineSwitch machine = closestObject.GetComponent<MachineSwitch>();

                if (machine != null)
                    machine.Interact();
            }
        }
    }
}