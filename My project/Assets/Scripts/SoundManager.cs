using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Play a 2D sound (non-positional), but only if no other clip is currently playing.
    /// </summary>
    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // NEW: don’t start a new one if we're still playing
        if (audioSource.isPlaying) return;

        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Play a 3D sound at a specific position.
    /// (This uses its own temporary AudioSource, so won’t be gated.)
    /// </summary>
    public void PlaySound(AudioClip clip, Vector3 position)
    {
        if (clip == null) return;
        AudioSource.PlayClipAtPoint(clip, position);
    }
}
