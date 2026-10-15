using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class KeyPickup : MonoBehaviour
{
    [Header("Pickup UI")]
    public TMP_Text pickupPrompt;
    private bool playerNearby = false;
    void Start()
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (playerNearby &&
            Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            PickUpKey();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (pickupPrompt != null)
            {
                pickupPrompt.text = "Press R to Pick Up Key";
                pickupPrompt.gameObject.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (pickupPrompt != null)
            {
                pickupPrompt.gameObject.SetActive(false);
            }
        }
    }
    void PickUpKey()
    {
        Debug.Log("Key picked up!");
        if (KeyCounter.Instance != null)
        {
            KeyCounter.Instance.AddKey();
        }
        else
        {
            Debug.LogWarning(
                "KeyCounter not found! Make sure KeyCounterManager is in the scene."
            );
        }
        if (pickupPrompt != null)
        {
            pickupPrompt.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}