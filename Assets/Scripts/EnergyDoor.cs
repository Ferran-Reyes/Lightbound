using UnityEngine;

public class EnergyDoor : MonoBehaviour
{
    //Puerta que controla este panel
    public Transform door;

    //Altura que subira la puerta
    public float openHeight = 2.5f;
    //Velocidad de movimiento
    public float moveSpeed = 2f;

    //Indica si la puerta tiene energia
    public bool isPowered = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Renderer panelRenderer;

    void Start()
    {
        //Guardar las posiciones de la puerta
        if (door == null)
            return;

        closedPosition = door.position;
        openPosition = closedPosition + Vector3.up * openHeight;

        panelRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    void Update()
    {
        if (door == null)
            return;

        //Mover la puerta segun su estado
        Vector3 targetPosition = isPowered ? openPosition : closedPosition;

        door.position = Vector3.MoveTowards(
            door.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    //Transferir o recuperar energia de la puerta
    public void Interact(PlayerBattery battery)
    {
        if (battery == null || door == null)
            return;

        if (!isPowered)
        {
            //Dar energia para abrir la puerta
            if (battery.UseEnergy())
                isPowered = true;
        }
        else
        {
            //Recuperar energia y cerrar la puerta
            if (battery.StoreEnergy())
                isPowered = false;
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
