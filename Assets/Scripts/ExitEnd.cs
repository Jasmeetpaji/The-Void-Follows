using UnityEngine;

public class ExitEnd : MonoBehaviour
{
    [Header("Exit Settings")]
    public GameObject lockedBarrier;
    public GameObject monster;

    private float endingTimer = 0f;

    public float titleDelay = 1f;
    public float storyDelay = 4f;
    public float finalMessageDelay = 10f;
    public float warningDelay = 14f;

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

    void Update()
    {
        if (escaped)
        {
            endingTimer += Time.unscaledDeltaTime;
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

        float titleAlpha = Mathf.Clamp01((endingTimer - titleDelay) / 2f);
        float storyAlpha = Mathf.Clamp01((endingTimer - storyDelay) / 3f);
        float finalAlpha = Mathf.Clamp01((endingTimer - finalMessageDelay) / 3f);
        float warningAlpha = Mathf.Clamp01((endingTimer - warningDelay) / 3f);

        if (titleAlpha > 0)
        {
            GUIStyle titleStyle = new GUIStyle(GUI.skin.label);

            titleStyle.fontSize = 70;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.normal.textColor = Color.white;

            GUI.Label(
                new Rect(0, 80,
                Screen.width, 100),
                "YOU ESCAPED!",
                titleStyle
            );
        }

        if (storyAlpha > 0)
        {
            GUIStyle storyStyle = new GUIStyle(GUI.skin.label);
            storyStyle.fontSize = 23;
            storyStyle.alignment = TextAnchor.MiddleCenter;
            storyStyle.normal.textColor = Color.white;
            storyStyle.wordWrap = true;

            GUI.Label(
                new Rect(
                    Screen.width / 2 - 450,
                    Screen.height / 2 - 50,
                    900,
                    120
                ),
                "After searching through the abandoned hospital, you finally managed to find all the keys.\n\nYou managed to open the Exit and finally excape the nightmare.",
                storyStyle
            );
        }

        if (finalAlpha > 0)
        {
            GUIStyle finalStyle = new GUIStyle(GUI.skin.label);
            finalStyle.fontSize = 22;
            finalStyle.alignment = TextAnchor.MiddleCenter;
            finalStyle.normal.textColor = Color.gray;

            GUI.Label(
                new Rect(0, Screen.height / 2 + 100,
                Screen.width, 70),
                "But as you walked away...\nyou heard somthing following you.",
                finalStyle
            );
        }

        if (warningAlpha > 0)
        {
            GUIStyle warningStyle = new GUIStyle(GUI.skin.label);
            warningStyle.fontSize = 30;
            warningStyle.fontStyle = FontStyle.Bold;
            warningStyle.alignment = TextAnchor.MiddleCenter;
            warningStyle.normal.textColor = Color.red;

            GUI.Label(
                new Rect(0, Screen.height / 2 + 190,
                Screen.width, 60),
                "RUN,Patient-27.",
                warningStyle
            );
        }
    }
}