using UnityEngine;
using UnityEngine.InputSystem;
public class Flashlight : MonoBehaviour
{
    [Header("Flashlight")]
    public Light flashlight;
    private bool isOn = false;
    void Start()
    {
        if (flashlight != null)
        {
            flashlight.enabled = false;
        }
    }
    void Update()
    {
        if (Keyboard.current == null)
            return;
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            ToggleFlashlight();
        }
    }
    void ToggleFlashlight()
    {
        isOn = !isOn;
        if (flashlight != null)
        {
            flashlight.enabled = isOn;
        }
    }
}