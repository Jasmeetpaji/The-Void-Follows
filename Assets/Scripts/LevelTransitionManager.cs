using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class LevelTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    public Image blackScreen;
    public string nextSceneName = "Level2";
    public float fadeDuration = 1.5f;
    public float blackScreenWait = 2f;
    private bool isTransitioning = false;
    public void StartTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(Transition());
        }
    }
    IEnumerator Transition()
    {
        isTransitioning = true;
        blackScreen.gameObject.SetActive(true);
        Color color = blackScreen.color;
        color.a = 0;
        blackScreen.color = color;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }
        color.a = 1f;
        blackScreen.color = color;
        yield return new WaitForSecondsRealtime(blackScreenWait);
        SceneManager.LoadScene(nextSceneName);
    }
}