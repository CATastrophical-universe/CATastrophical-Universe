using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialStepUi;
    [SerializeField] private Image actionImage;
    [SerializeField] private TMP_Text actionText;
    [SerializeField] private Key[] keys;
    [SerializeField] private UnityEvent showTutorialStep;
    [SerializeField] private UnityEvent hideTutorialStep;

    private KeyCode _currentKey = KeyCode.None;

    void Start() {
        if (keys.Length > 0) {
            ShowTutorialStep(keys[0].GetKeyCode());
        }
    }

    void Update() {
        if (_currentKey != KeyCode.None && Input.GetKeyDown(_currentKey)) {
            StepCompleted();
        }
    }

    public void ShowTutorialStep(KeyCode keyCode) {
        if (tutorialStepUi.activeSelf) { HideTutorialStep(); }

        Key key = GetKey(keyCode);

        if (key.GetTexture() != null && actionImage != null)
            actionImage.sprite = key.GetTexture();
        if (key.GetAction() != null && actionText != null)
            actionText.text = key.GetAction();

        _currentKey = keyCode;

        tutorialStepUi.SetActive(true);

        if (showTutorialStep != null)
            showTutorialStep.Invoke();
    }

    public void HideTutorialStep() {
        _currentKey = KeyCode.None;

        if (hideTutorialStep != null)
            hideTutorialStep.Invoke();

        tutorialStepUi.SetActive(false);
    }

    public void StepCompleted() {
        int newIndex = GetKeyIndex(_currentKey) + 1;
        HideTutorialStep();

        if (keys.Length > newIndex) {
            ShowTutorialStep(keys[newIndex].GetKeyCode());
        }
    }

    private Key GetKey(KeyCode keyCode) {
        foreach (Key key in keys) {
            if (key.GetKeyCode() == keyCode) {
                return key;
            }
        }

        throw new System.Exception(String.Format("Key {0} was not Found!", keyCode));
    }

    private int GetKeyIndex(KeyCode keyCode) {
        int i = 0;

        foreach (Key key in keys) {
            if (key.GetKeyCode() == keyCode) {
                return i;
            }

            i++;
        }

        return i;
    }

    #if UNITY_INCLUDE_TESTS

        public void SetKeys(Key[] keys) { this.keys = keys; }
        public void SetTutorialStepUi (GameObject tutorialStepUi) { this.tutorialStepUi = tutorialStepUi; }
        public void SetActionText (TMP_Text actionText) { this.actionText = actionText; }

    #endif
}
