using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemBinder : MonoBehaviour
{
    public enum Character
    {
        Adam,
        Remy
    }

    [Header("Character")]
    public Character currentCharacter = Character.Adam;

    [Header("References")]
    public Animator animator;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Attacking.Enable();

        // If Remy, override bindings at runtime
        if (currentCharacter == Character.Remy)
        {
            controls.Attacking.LeftHook.ApplyBindingOverride(0, "<Keyboard>/a");
            controls.Attacking.RightHook.ApplyBindingOverride(0, "<Keyboard>/s");
            controls.Attacking.LeftKick.ApplyBindingOverride(0, "<Keyboard>/d");
            controls.Attacking.HitReact.ApplyBindingOverride(0, "<Keyboard>/f");
            controls.Attacking.RightKick.ApplyBindingOverride(0, "<Keyboard>/g");
            controls.Attacking.LeftBlock.ApplyBindingOverride(0, "<Keyboard>/h");
            controls.Attacking.RightBlock.ApplyBindingOverride(0, "<Keyboard>/j");
            controls.Attacking.KnockOut.ApplyBindingOverride(0, "<Keyboard>/k");
            controls.Attacking.Squat.ApplyBindingOverride(0, "<Keyboard>/l");

        }
        else
        {
            // Otherwise, keep Adam’s defaults
            controls.Attacking.LeftHook.RemoveBindingOverride(0);
            controls.Attacking.RightHook.RemoveBindingOverride(0);
            controls.Attacking.LeftKick.RemoveBindingOverride(0);
            controls.Attacking.HitReact.RemoveBindingOverride(0);
        }

        // Subscribe to all actions once
        controls.Attacking.LeftHook.performed += _ => animator.SetTrigger("LeftHook");
        controls.Attacking.RightHook.performed += _ => animator.SetTrigger("RightHook");
        controls.Attacking.LeftKick.performed += _ => animator.SetTrigger("LeftKick");
        controls.Attacking.HitReact.performed += _ => animator.SetTrigger("HitReact");
        controls.Attacking.RightKick.performed += _ => animator.SetTrigger("RightKick");
        controls.Attacking.LeftBlock.performed += _ => animator.SetTrigger("LeftBlock");
        controls.Attacking.RightBlock.performed += _ => animator.SetTrigger("RightBlock");
        controls.Attacking.KnockOut.performed += _ => animator.SetTrigger("KnockOut");
        controls.Attacking.Squat.performed += _ => animator.SetTrigger("Squat");
    }

    void OnEnable() => controls.Attacking.Enable();
    void OnDisable() => controls.Attacking.Disable();
}
