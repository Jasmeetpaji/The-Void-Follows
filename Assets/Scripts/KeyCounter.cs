using UnityEngine;
using TMPro;
public class KeyCounter : MonoBehaviour
{
    public static KeyCounter Instance;
    [Header("UI")]
    public TMP_Text keyCounterText;
    public int keysCollected = 0;
    void Awake()
    {
        Instance = this;
        UpdateUI();
    }
    public void AddKey()
    {
        keysCollected++;
        UpdateUI();
    }
    void UpdateUI()
    {
        if (keyCounterText != null)
        {
            keyCounterText.text = "Keys: " + keysCollected;
        }
    }
}
