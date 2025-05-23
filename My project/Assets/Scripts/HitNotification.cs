using System.Collections;
using UnityEngine;
using TMPro;   // or TMPro if using TextMeshPro

public class HitNotification : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text notifText;                   // assign HitNotifText here
    // public TextMeshProUGUI notifText;     // if you prefer TMP

    [Header("Settings")]
    public float displayDuration = 2f;
    public float fadeSpeed = 4f;

    private bool isShowing = false;         //  guard flag
    private Coroutine showRoutine;

    /// <summary>
    /// Shows the pop-up only if one isn't already in progress.
    /// </summary>
    public void ShowHitNotif()
    {
        if (isShowing) return;             //  skip if already showing

        // Optional: pick a random comic word
        string[] words = { "Pow!", "Pew!", "Bam!", "Wham!" };
        notifText.text = words[Random.Range(0, words.Length)];

        // Start the notification coroutine
        showRoutine = StartCoroutine(DoNotification());
    }

    private IEnumerator DoNotification()
    {
        isShowing = true;

        // Fade in
        float t = 0f;
        Color baseColor = notifText.color;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            notifText.color = new Color(baseColor.r, baseColor.g, baseColor.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        // Hold
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            notifText.color = new Color(baseColor.r, baseColor.g, baseColor.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        // Reset
        notifText.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0f);
        isShowing = false;                  //  allow next hit to trigger
    }
}
