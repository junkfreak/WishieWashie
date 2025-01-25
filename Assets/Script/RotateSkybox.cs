using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    // The speed of the skybox rotation
    public float rotationSpeed = 1f;

    void Update()
    {
        // Rotate the skybox around the Y-axis
        float rotationAmount = rotationSpeed * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") + rotationAmount);
    }
}
