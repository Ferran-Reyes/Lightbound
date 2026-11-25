using UnityEngine;
//Para utilizar las Coroutines y hacer que la plataforma espere 2 segundos antes de moverse.
using System.Collections;

public class EnergyPlatform : MonoBehaviour
{
    //Plataforma que controla este panel
    public Transform platform;

    //Puntos entre los que se movera
    public Transform pointA;
    public Transform pointB;

    //Velocidad y tiempo de espera
    public float moveSpeed = 2f;
    public float waitTime = 2f;

    //Indica si la plataforma tiene energia
    public bool isPowered = false;

    private Renderer panelRenderer;
    private Coroutine movementCoroutine;
    private bool moveToB = true;

    void Start()
    {
        panelRenderer = GetComponent<Renderer>();
        UpdateColor();

        //Colocar la plataforma en el punto inicial
        if (platform != null && pointA != null)
            platform.position = pointA.position;

        //Iniciar movimiento si empieza encendida
        if (isPowered && platform != null &&
            pointA != null && pointB != null)
        {
            movementCoroutine = StartCoroutine(MovePlatform());
        }
    }

    //Mover la plataforma continuamente entre los dos puntos
    IEnumerator MovePlatform()
    {
        while (isPowered)
        {
            //Esperar antes de moverse
            yield return new WaitForSeconds(waitTime);

            //Elegir el punto de destino
            Transform target = moveToB ? pointB : pointA;

            //Moverse hacia el punto elegido
            while (isPowered &&
                   Vector3.Distance(platform.position, target.position) > 0.01f)
            {
                platform.position = Vector3.MoveTowards(
                    platform.position,
                    target.position,
                    moveSpeed * Time.deltaTime
                );

                yield return null;
            }

            if (!isPowered)
                yield break;

            //Asegurar que llega exactamente al punto
            platform.position = target.position;

            //Cambiar el siguiente destino
            moveToB = !moveToB;
        }
    }

    //Transferir o recuperar energia
    public void Interact(PlayerBattery battery)
    {
        if (battery == null || platform == null ||
            pointA == null || pointB == null)
            return;

        if (!isPowered)
        {
            //Dar energia y comenzar el movimiento
            if (battery.UseEnergy())
            {
                isPowered = true;
                movementCoroutine = StartCoroutine(MovePlatform());
            }
        }
        else
        {
            //Recuperar energia y detener la plataforma
            if (battery.StoreEnergy())
            {
                isPowered = false;

                if (movementCoroutine != null)
                {
                    StopCoroutine(movementCoroutine);
                    movementCoroutine = null;
                }
            }
        }

        UpdateColor();
    }

    //Cambiar el color del panel
    void UpdateColor()
    {
        if (panelRenderer != null)
        {
            if (isPowered)
                panelRenderer.material.color = Color.green;
            else
                panelRenderer.material.color = Color.red;
        }
    }
}