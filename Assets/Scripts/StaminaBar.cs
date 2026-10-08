using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    [Header("Player")]
    public PlayerMovement player;
    [Header("Stamina Bar")]
    public Slider staminaSlider;
    void Start()
    {
        if (player == null)
        {
            Debug.LogError(
                "StaminaBar: Player is not assigned!"
            );
            return;
        }
        if (staminaSlider == null)
        {
            Debug.LogError(
                "StaminaBar: Stamina Slider is not assigned!"
            );
            return;
        }
        staminaSlider.maxValue =
            player.maxStamina;
        staminaSlider.value =
            player.stamina;
    }
    void Update()
    {
        if (player == null ||
            staminaSlider == null)
            return;
        staminaSlider.maxValue =
            player.maxStamina;
        staminaSlider.value =
            player.stamina;
    }
}