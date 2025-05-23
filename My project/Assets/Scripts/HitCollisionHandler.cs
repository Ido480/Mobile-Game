using UnityEngine;

public class HitCollisionHandler : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tag of the attacking collider (e.g., LeftFoot)")]
    public string weaponTag = "LeftFoot";

    [Header("Sound")]
    [Tooltip("Sound to play when Remy gets hit (Adam’s attack)")]
    public AudioClip getHitSound;

    private HitFeedback hitFeedback;
    private Animator animator;

    void Awake()
    {
        hitFeedback = Object.FindFirstObjectByType<HitFeedback>();
        if (hitFeedback == null)
            Debug.LogWarning("No HitFeedback found in scene!");

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("No Animator found on this GameObject!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(weaponTag))
        {
            // Shake + flash
            hitFeedback?.TriggerHit();

            // Play hit-react animation
            animator?.SetTrigger("HitReact");

            // Play hit sound via SoundManager
            SoundManager.Instance?.PlaySound(getHitSound);
        }
    }
}
