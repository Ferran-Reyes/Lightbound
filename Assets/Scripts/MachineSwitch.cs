using UnityEngine;

public class MachineSwitch : MonoBehaviour
{
    //Indica si la maquina esta encendida
    public bool isActive = false;

    private Renderer machineRenderer;

    void Start()
    {
        machineRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    //Encender o apagar la maquina
    public void Interact()
    {
        isActive = !isActive;
        UpdateColor();
    }

    //Cambiar el color segun su estado
    void UpdateColor()
    {
        if (machineRenderer != null)
        {
            if (isActive)
                machineRenderer.material.color = Color.green;
            else
                machineRenderer.material.color = Color.red;
        }
    }
}