using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    //Indica si el generador tiene energia
    public bool hasEnergy = true;

    private Renderer generatorRenderer;

    void Start()
    {
        generatorRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    //Absorber energia del generador
    public void Interact(PlayerBattery battery)
    {
        //Si el generador esta vacio no se puede absorber energia
        if (!hasEnergy)
            return;

        //Intentar guardar la energia en el robot
        if (battery.StoreEnergy())
        {
            hasEnergy = false;
            UpdateColor();
        }
    }

    //Cambiar el color segun su estado
    void UpdateColor()
    {
        if (generatorRenderer != null)
        {
            if (hasEnergy)
                generatorRenderer.material.color = Color.yellow;
            else
                generatorRenderer.material.color = Color.gray;
        }
    }
}