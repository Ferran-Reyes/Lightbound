using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //Distancia maxima para interactuar
    public float interactionDistance = 1f;

    private PlayerBattery battery;

    void Start()
    {
        battery = GetComponent<PlayerBattery>();
    }

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
                    float distance = Vector3.Distance(
                        transform.position,
                        obj.ClosestPoint(transform.position)
                    );

                    //Guardar el objeto mas cercano
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj;
                    }
                }
            }

            if (closestObject != null && battery != null)
            {
                //Generador
                EnergyGenerator generator = closestObject.GetComponent<EnergyGenerator>();

                if (generator != null)
                {
                    generator.Interact(battery);
                    return;
                }

                //Maquina
                MachineSwitch machine = closestObject.GetComponent<MachineSwitch>();

                if (machine != null)
                {
                    machine.Interact(battery);
                    return;
                }

                //Puerta
                EnergyDoor door = closestObject.GetComponent<EnergyDoor>();

                if (door != null)
                {
                    door.Interact(battery);
                    return;
                }

                //Plataforma movil
                EnergyPlatform platform = closestObject.GetComponent<EnergyPlatform>();

                if (platform != null)
                {
                    platform.Interact(battery);
                }
            }
        }
    }
}