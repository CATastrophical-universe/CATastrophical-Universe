using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActionTrigger : MonoBehaviour
{
    [SerializeField] KeyCode keyCode;
    public delegate void ShowTutorialStepHandler(KeyCode keyCode);
    public static event ShowTutorialStepHandler ShowTutorialStep;

    void OnTriggerEnter2D(Collider2D other) {
        ShowTutorialStep?.Invoke(keyCode);
    }
}
