using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class MonsterAudio : MonoBehaviour
{
    [Header("Sound Clips")]
    public AudioClip walkingSound;
    public AudioClip screechSound;

    [Header("Footstep Settings")]
    public float walkingInterval = 0.65f;
    public float chasingInterval = 0.30f;

    [Header("Volume")]
    [Range(0f, 1f)]
    public float footstepVolume = 0.8f;

    [Range(0f, 1f)]
    public float screechVolume = 1f;

    private NavMeshAgent agent;
    private MonsterAI monsterAI;
    private AudioSource audioSource;

    private float footstepTimer = 0f;
    private bool wasChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        monsterAI = GetComponent<MonsterAI>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 1f;
    }

    void Update()
    {
        if (agent == null || monsterAI == null)
        return;

        bool chasing = monsterAI.IsChasing;

        if (chasing && !wasChasing)
        {
            if (screechSound != null)
            {
                audioSource.PlayOneShot(
                    screechSound,
                    screechVolume
                );
            }
        }
        
        wasChasing = chasing;

        if (agent.velocity.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;

            float interval = chasing 
            ? chasingInterval 
            : walkingInterval;

            if (footstepTimer <= 0f)
            {
                if (walkingSound != null)
                {
                    audioSource.PlayOneShot(
                        walkingSound,
                        footstepVolume
                    );
                }
                footstepTimer = interval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }
}