using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class Level1Exit : MonoBehaviour
{
    [Header("Key Settings")]
    public int requiredKeys = 5;
    [Header("UI Settings")]
    public TMP_Text exitPrompt;
    [Header("Scene Transition")]
    public GameObject blackScreen;
    public string nextSceneName = "Level2";
    public float blackScreenWait = 2f;
    private bool playerNearby = false;
    private bool isTransitioning = false;
    void Start()
    {
        if (exitPrompt != null)
        {
            exitPrompt.gameObject.SetActive(false);
        }
        if (blackScreen != null)
        {
            blackScreen.SetActive(false);
        }
    }
    void Update()
    {
        if (!playerNearby || isTransitioning)
            return;
        int currentKeys = 0;
        if (KeyCounter.Instance != null)
        {
            currentKeys = KeyCounter.Instance.keysCollected;
        }
        if (currentKeys < requiredKeys)
        {
            if (exitPrompt != null)
            {
                exitPrompt.text = "FIND ALL THE KEYS";
            }
            return;
        }
        if (exitPrompt != null)
        {
            exitPrompt.text = "PRESS Y TO UNLOCK DOOR";
        }
        if (Keyboard.current != null &&
            Keyboard.current.yKey.wasPressedThisFrame)
        {
            Debug.Log("Y PRESSED! UNLOCKING EXIT!");
            StartCoroutine(LoadNextLevel());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (exitPrompt != null)
            {
                exitPrompt.gameObject.SetActive(true);
            }
            Debug.Log("Player reached the exit!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (exitPrompt != null)
            {
                exitPrompt.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator LoadNextLevel()
    {
        isTransitioning = true;
        if (exitPrompt != null)
        {
            exitPrompt.gameObject.SetActive(false);
        }
        if (blackScreen != null)
        {
            blackScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("Black Screen is not assigned!");
            yield break;
        }
        Debug.Log("Loading Level 2...");
        yield return new WaitForSecondsRealtime(blackScreenWait);
        SceneManager.LoadScene(nextSceneName);
    }
}