using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    private bool collected = false;

    [Header("Pickup Sound")]
    public AudioClip pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (collected)
        return;

        if (!other.CompareTag("Player"))
        return;

        if (KeyProgress.Instance == null)
        {
            Debug.LogError("KeyManager not found dude");
            return;
        }

        collected = true;

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupSound,
                transform.position,
                0.8f
            );
        }
        KeyProgress.Instance.CollectKey();
        Destroy(gameObject);
    }
}