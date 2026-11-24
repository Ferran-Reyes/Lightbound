using UnityEngine;

public class BatteryUI : MonoBehaviour
{
    public PlayerBattery playerBattery;
    public RectTransform energyFill;
    public float fillSpeed = 2f;

    void Start()
    {
        if (playerBattery == null || energyFill == null)
            return;

        //Establecer la barra segun la energia inicial
        float maxWidth = ((RectTransform)energyFill.parent).rect.width - 6f;
        float width = playerBattery.HasEnergy() ? maxWidth : 0f;

        energyFill.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, width
        );
    }

    void Update()
    {
        if (playerBattery == null || energyFill == null)
            return;

        //Calcular la anchura maxima de la barra
        float maxWidth = ((RectTransform)energyFill.parent).rect.width - 6f;

        //Comprobar si el robot tiene energia
        float targetWidth = playerBattery.HasEnergy() ? maxWidth : 0f;

        //Rellenar o vaciar la barra suavemente
        float newWidth = Mathf.MoveTowards(
            energyFill.rect.width,
            targetWidth,
            fillSpeed * maxWidth * Time.deltaTime
        );

        energyFill.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, newWidth
        );
    }
}