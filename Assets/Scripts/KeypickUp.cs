using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class KeyPickup : MonoBehaviour
{
    [Header("UI")]
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
        if (pickupPrompt != null)
        {
            pickupPrompt.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}