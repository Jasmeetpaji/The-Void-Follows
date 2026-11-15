using UnityEngine;

public class ExitEnd : MonoBehaviour
{
    [Header("Exit Settings")]
    public GameObject lockedBarrier;
    public GameObject monster;

    private bool escaped = false;

    void Start()
    {
        if (KeyProgress.Instance == null)
        {
            Debug.LogError("Key not found! ");
            return;
        }

        KeyProgress.Instance.AllKeysCollected += UnlockExit;

        if (KeyProgress.Instance.HasAllKeys)
        {
            UnlockExit();
        }
    }

    void OnDestroy()
    {
        if (KeyProgress.Instance != null)
        {
            KeyProgress.Instance.AllKeysCollected -= UnlockExit;
        }
    }

    void UnlockExit()
    {
        if (lockedBarrier != null)
        {
            lockedBarrier.SetActive(false);
        }

        Debug.Log("THE EXIT HAS OPENED");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (escaped)
        return;

        if (!other.CompareTag("Player"))
        return;

        if (KeyProgress.Instance == null || !KeyProgress.Instance.HasAllKeys)
        return;

        escaped = true;

        if (monster != null)
        {
            monster.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void OnGUI()
    {
        if (!escaped)
        return;

        GUI.color = Color.black;

        GUI.DrawTexture(
            new Rect(0, 0, Screen.width, Screen.height),
            Texture2D.whiteTexture
        );

        GUI.color = Color.white;

        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);

        titleStyle.fontSize = 65;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;

        GUI.Label(
            new Rect(0, Screen.height / 2 - 100,
            Screen.width, 100),
            "YOU ESCAPED!",
            titleStyle
        );    
    }
}