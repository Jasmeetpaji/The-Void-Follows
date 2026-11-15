using System;
using UnityEngine;
using TMPro;

public class KeyProgress : MonoBehaviour
{
    public static KeyProgress Instance { get; private set; }

    [Header("Key Settings")]
    [SerializeField] private int requiredKeys = 8;
    [SerializeField] private TMP_Text keyCounterText;
    public int KeysCollected { get; private set; }
    public bool HasAllKeys => KeysCollected >= requiredKeys;
    public event Action AllKeysCollected;

    private bool completionSent = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        UpdateCounter();
    }
    
    public void CollectKey()
    {
        if (HasAllKeys)
        return;

        KeysCollected++;
        UpdateCounter();

        Debug.Log("Keys: " + KeysCollected + "/" + requiredKeys);

        if (HasAllKeys && !completionSent)
        {
            completionSent = true;

            Debug.Log("ALL KEYS COLLECTED! EXIT UNLOCKED!");

            AllKeysCollected?.Invoke();
        }
    }

    void UpdateCounter()
    {
        if (keyCounterText != null)
        {
            keyCounterText.text = "KEYS: " + KeysCollected + "/" + requiredKeys;
        }
    }
    void OnDestroy()
    {
        if (Instance == this) 
        Instance = null;
    }
}