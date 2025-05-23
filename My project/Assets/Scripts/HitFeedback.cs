using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HitFeedback : MonoBehaviour
{
    [Header("References")]
    public Transform cameraRig;        // assign your CameraRig here
    public Image hitFlashImage;        // assign the HitFlash UI Image here

    [Header("Shake Settings")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.3f;

    [Header("Flash Settings")]
    public float flashDuration = 0.3f;
    public float maxFlashAlpha = 0.5f;

    // Call this to trigger both effects
    public void TriggerHit()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCamera());
        StartCoroutine(FlashRed());
    }

    private IEnumerator ShakeCamera()
    {
        Vector3 originalPos = cameraRig.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            cameraRig.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraRig.localPosition = originalPos;
    }

    private IEnumerator FlashRed()
    {
        // Fade in
        float half = flashDuration * 0.5f;
        float t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, maxFlashAlpha, t / half);
            hitFlashImage.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }
        // Fade out
        t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(maxFlashAlpha, 0f, t / half);
            hitFlashImage.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }
        hitFlashImage.color = new Color(1f, 0f, 0f, 0f);
    }
}
