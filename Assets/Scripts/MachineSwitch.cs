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

    //Transferir o recuperar energia de la maquina
    public void Interact(PlayerBattery battery)
    {
        //Si la maquina esta apagada, necesita energia
        if (!isActive)
        {
            if (battery.UseEnergy())
            {
                isActive = true;
                UpdateColor();
            }
        }
        else
        {
            //Si esta encendida, el robot puede recuperar la energia
            if (battery.StoreEnergy())
            {
                isActive = false;
                UpdateColor();
            }
        }
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