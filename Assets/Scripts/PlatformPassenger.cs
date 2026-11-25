using UnityEngine;

public class PlatformPassenger : MonoBehaviour
{
    private CharacterController controller;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        if (controller == null)
            return;

        //Mover al robot junto con la plataforma
        if (currentPlatform != null)
        {
            Vector3 platformMovement =
                currentPlatform.position - lastPlatformPosition;

            controller.Move(platformMovement);
        }

        //Actualizar las posiciones de los colliders
        Physics.SyncTransforms();

        //Detectar si el robot esta encima de una plataforma
        RaycastHit hit;

        if (Physics.Raycast(
            transform.position + Vector3.up * 0.1f,
            Vector3.down,
            out hit,
            1.4f,
            ~0,
            QueryTriggerInteraction.Ignore)
            && hit.collider.CompareTag("MovingPlatform")
            && hit.normal.y > 0.5f)
        {
            currentPlatform = hit.collider.transform;
            lastPlatformPosition = currentPlatform.position;
        }
        else
        {
            currentPlatform = null;
        }
    }
}