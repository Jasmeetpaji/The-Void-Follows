using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip walkingSound;

    [Header("Settings")]
    public float stepInterval = 0.5f;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    private CharacterController controller;
    private AudioSource audioSource;

    private float stepTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity.y = 0f;

        bool isMoving = horizontalVelocity.magnitude > 0.15f;
        bool isGrounded = controller.isGrounded;

        if (isMoving && isGrounded)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                if (walkingSound != null)
                {
                    audioSource.PlayOneShot(
                        walkingSound,
                        volume
                    );
                }
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}