using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class JumpscareManager : MonoBehaviour
{
    [Header("Jumpscare Images")]
    public Image jumpscareImage;
    public Sprite[] jumpscareImages;
    [Header("Jumpscare Sound")]
    public AudioSource audioSource;
    public AudioClip jumpscareSound;
    [Header("Timing Settings")]
    public float displayDuration = 2f;
    public float minimumGap = 60f;
    public float maximumGap = 120f;
    private int lastImageIndex = -1;
    private bool isPlaying = false;
    void Start()
    {
        if (jumpscareImage != null)
        {
            jumpscareImage.gameObject.SetActive(false);
        }
        StartCoroutine(RandomJumpscareLoop());
    }
    IEnumerator RandomJumpscareLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minimumGap, maximumGap);
            Debug.Log("Next jumpscare in " + waitTime + " seconds!");
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(PlayRandomJumpscare());
        }
    }
    IEnumerator PlayRandomJumpscare()
    {
        if (isPlaying)
            yield break;
        if (jumpscareImage == null ||
            jumpscareImages == null ||
            jumpscareImages.Length == 0)
        {
            Debug.LogError("Jumpscare images or UI are missing!");
            yield break;
        }
        isPlaying = true;
        int selectedIndex;
        if (jumpscareImages.Length == 1)
        {
            selectedIndex = 0;
        }
        else
        {
            do
            {
                selectedIndex = Random.Range(0, jumpscareImages.Length);
            }
            while (selectedIndex == lastImageIndex);
        }
        lastImageIndex = selectedIndex;
        jumpscareImage.sprite = jumpscareImages[selectedIndex];
        jumpscareImage.gameObject.SetActive(true);
        Debug.Log("JUMPSCARE!");
        if (audioSource != null && jumpscareSound != null)
        {
            audioSource.PlayOneShot(jumpscareSound);
        }
        yield return new WaitForSecondsRealtime(displayDuration);
        jumpscareImage.gameObject.SetActive(false);
        isPlaying = false;
    }
}
