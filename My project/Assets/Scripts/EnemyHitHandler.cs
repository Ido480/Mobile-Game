using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tag used for Remy's attack collider")]
    public string attackerWeaponTag = "RemyWeapon";

    [Header("Sound")]
    public AudioClip getHitSound2;

    private HitNotification hitNotification;

    void Awake()
    {
        hitNotification = Object.FindFirstObjectByType<HitNotification>();
        if (hitNotification == null)
            Debug.LogWarning("No HitNotification found in scene.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(attackerWeaponTag))
        {
            // 1) Show “Pow!”
            hitNotification?.ShowHitNotif();

            // 2) Play hit sound
            SoundManager.Instance?.PlaySound(getHitSound2, transform.position);

            // 3) Flash Remy's limb blue
            StartCoroutine(FlashLimbBlue(other, 0.2f));
        }
    }

    private IEnumerator FlashLimbBlue(Collider hitCollider, float duration)
    {
        // 1. Find all renderers on that collider and its children
        Renderer[] renderers = hitCollider.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning($"No Renderer found under {hitCollider.name}");
            yield break;
        }

        // 2. Cache original colors
        var originalColors = new List<Color>(renderers.Length);
        foreach (var rend in renderers)
        {
            // Make sure we have an instanced material
            rend.material = new Material(rend.material);
            originalColors.Add(rend.material.color);
            // Tint blue
            rend.material.color = Color.blue;
        }

        // 3. Wait
        yield return new WaitForSeconds(duration);

        // 4. Restore original colors
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
                renderers[i].material.color = originalColors[i];
        }
    }
}
