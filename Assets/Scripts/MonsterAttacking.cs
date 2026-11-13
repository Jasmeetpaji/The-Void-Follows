using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCatch : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Catch Settings")]
    public float catchDistance = 1.5f;

    private bool playerCaught = false;

    void Update()
    {
        if (player == null || playerCaught)
        return;

        Vector3 monsterPosition = transform.position;
        Vector3 playerPosition = player.position;

        monsterPosition.y = 0;
        playerPosition.y = 0;

        float distance = Vector3.Distance(
            monsterPosition,
            playerPosition
        );

        if (distance <= catchDistance)
        {
            Die();
        }
    }
    void Die()
    {
        playerCaught = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        Debug.Log("Player caught");
    }
    void OnGUI()
    {
        if (!playerCaught)
        return;

        GUI.color = new Color(0, 0, 0, 0.9f);
        GUI.DrawTexture(
            new Rect(0, 0, Screen.width, Screen.height),
            Texture2D.whiteTexture
        );

        GUI.color = Color.white;

        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 65;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.red;

        GUI.Label(
            new Rect(0, Screen.height / 2 - 130,
            Screen.width, 100),
            "MUTILATED",
            titleStyle
        );

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 25;

        if (GUI.Button(
            new Rect(
                Screen.width / 2 - 110,
                Screen.height / 2 + 20,
                220,
                60
            ),
            "Try Again?",
            buttonStyle
        ))
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}