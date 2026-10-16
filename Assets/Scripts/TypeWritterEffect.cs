using System.Collections;
using TMPro;
using UnityEngine;
public class TypewriterEffect : MonoBehaviour
{
    [Header("Text Settings")]
    public TMP_Text textComponent;
    [TextArea(10, 25)]
    public string fullText =
        "EMPHREAL MENTAL HOSPITAL\n" +
        "Patient Escape Protocol - Classified\n\n" +

        "You don't remember how you got here.\n\n" +

        "The doors are locked. The halls are empty.\n\n" +
        "At least... they should be.\n\n" +

        "YOUR OBJECTIVE\n\n" +

        "Find the 5 hidden keys scattered throughout " +
        "the hallways.\n\n" +

        "Locate the keycard.\n\n" +
        "Unlock the exit.\n\n" +

        "And whatever you do...\n\n" +

        "DO NOT LET HIM FIND YOU.\n\n" +

        "He cannot see you.\n\n" +
        "But he can hear you.\n\n" +

        "Every footstep. Every breath. Every door you open.\n\n" +

        "He is listening.\n\n" +

        "If you hear something moving nearby, RUN.\n\n" +

        "If you hear him getting closer...\n\n" +

        "HIDE INSIDE A LOCKER.\n\n" +

        "Don't move.\n\n" +
        "Don't breathe too loudly.\n\n" +

        "No matter what you hear outside...\n\n" +

        "DO NOT COME OUT UNTIL IT'S SILENT.\n\n" +

        "You have been warned.\n\n" +

        "He is blind.\n\n" +
        "But he always knows when someone is there.\n\n\n" +
        "STAY QUIET.\n\n" +

        "He's listening.";
    [Header("Typing Speed")]
    public float lettersPerSecond = 20f;
    public float delayBeforeTyping = 1.5f;
    [Header("Options")]
    public bool playOnStart = true;
    private Coroutine typingCoroutine;
    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }
        if (textComponent == null)
        {
            Debug.LogError(
                "TypewriterEffect needs a TextMeshPro component!"
            );
            return;
        }
        textComponent.text = "";
        if (playOnStart)
        {
            typingCoroutine = StartCoroutine(PlayTypewriter());
        }
    }
    IEnumerator PlayTypewriter()
    {
        yield return new WaitForSeconds(delayBeforeTyping);
        textComponent.text = "";
        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(
                1f / Mathf.Max(lettersPerSecond, 1f)
            );
        }
        typingCoroutine = null;
    }
    public void RestartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }
        if (textComponent != null)
        {
            typingCoroutine = StartCoroutine(PlayTypewriter());
        }
    }
    public void ShowFullText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        if (textComponent != null)
        {
            textComponent.text = fullText;
        }
    }
}