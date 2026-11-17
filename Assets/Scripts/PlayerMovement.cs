using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float velocity = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Keyboard.current == null || Camera.main == null)
            return;

        Vector3 movement = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.z += 1;

        if (Keyboard.current.sKey.isPressed)
            movement.z -= 1;

        if (Keyboard.current.aKey.isPressed)
            movement.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            movement.x += 1;

        //Obtener las direcciones de la camara
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        //Evitar que el personaje se mueva hacia arriba o abajo
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        //Mover al personaje segun la direccion de la camara
        Vector3 direction = forward * movement.z + right * movement.x;

        //Aplicar gravedad al personaje
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        //Normalized para que no se mueva mas rapido en diagonal
        Vector3 finalMovement = direction.normalized * velocity;
        finalMovement.y = verticalVelocity;

        //Con el Move para mover al robot respetando las colisiones
        controller.Move(finalMovement * Time.deltaTime);
    }
}