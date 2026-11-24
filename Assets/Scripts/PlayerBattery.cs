using UnityEngine;

public class PlayerBattery : MonoBehaviour
{
    //Indica si el robot tiene energia almacenada
    [SerializeField] private bool hasEnergy = false;

    //Guardar energia en la bateria
    public bool StoreEnergy()
    {
        //Si ya tiene energia no puede guardar mas
        if (hasEnergy)
            return false;

        hasEnergy = true;
        return true;
    }

    //Utilizar la energia almacenada
    public bool UseEnergy()
    {
        //Si no tiene energia no puede utilizarla
        if (!hasEnergy)
            return false;

        hasEnergy = false;
        return true;
    }
}