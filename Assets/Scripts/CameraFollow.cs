using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    //Objeto al que debe seguir la camara
    public Transform player;
    //Distancia inicial de la camara al robot
    public Vector3 offset = new Vector3(0, 3, -6);
    //Velocidad con la que sigue al personaje
    public float velocity = 5f;
    //Velocidad de rotacion
    public float rotationSpeed = 0.2f;

    //Angulos de rotacion de la camara
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        //Calcular el angulo vertical inicial
        rotationX = Mathf.Atan2(offset.y, -offset.z) * Mathf.Rad2Deg;
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        //Rotar la camara manteniendo el click derecho
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouse = Mouse.current.delta.ReadValue();

            rotationY += mouse.x * rotationSpeed;
            rotationX -= mouse.y * rotationSpeed;

            //Limitar la rotacion vertical
            rotationX = Mathf.Clamp(rotationX, -20f, 80f);
        }
    }

    //Actualiza la camara despues de que el personaje se mueva
    void LateUpdate()
    {
        if (player == null)
            return;

        //Punto al que mira la camara
        Vector3 center = player.position + Vector3.up * 0.5f;

        //Calcular rotacion y direccion de la camara
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 direction = rotation * Vector3.back;

        float distance = offset.magnitude;
        float finalDistance = distance;

        //Detectar obstaculos entre el robot y la camara
        RaycastHit[] hits = Physics.SphereCastAll(
            center,
            0.25f,
            direction,
            distance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore
        );

        foreach (RaycastHit hit in hits)
        {
            //Ignorar las colisiones del propio robot
            if (hit.collider.transform == player ||
                hit.collider.transform.IsChildOf(player))
                continue;

            //Acercar la camara si encuentra un obstaculo
            float newDistance = Mathf.Max(0.3f, hit.distance - 0.1f);
            finalDistance = Mathf.Min(finalDistance, newDistance);
        }

        //Calcular posicion final de la camara
        Vector3 desiredPosition = center + direction * finalDistance;

        if (finalDistance < distance)
        {
            //Acercar la camara inmediatamente para evitar obstaculos
            transform.position = desiredPosition;
        }
        else
        {
            //Seguir suavemente al personaje
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPosition,
                velocity * Time.deltaTime
            );
        }

        //Que la camara siempre mire al personaje
        transform.LookAt(center);
    }
}