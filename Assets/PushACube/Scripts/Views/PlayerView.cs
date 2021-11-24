using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public void ChangeHealth(float health)
    {

    }

    public void Death()
    {

    }

    public void ChangeColor(Color color)
    {
        var playerLight = transform.GetComponent<Light>();
        var playerMaterial = transform.GetComponent<Renderer>().material;

        playerLight.color = color;
        playerLight.intensity += 1;
        playerMaterial.color = color;
    }
}