using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float speed = 50f; // rotation speed in degrees per second

    private void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime); // rotate around the Z-axis
    }
}
