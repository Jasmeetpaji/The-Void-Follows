using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    private bool collected = false;

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
        KeyProgress.Instance.CollectKey();
        Destroy(gameObject);
    }
}