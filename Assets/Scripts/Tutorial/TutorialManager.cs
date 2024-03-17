using System;
using TMPro;
using UnityEditor.PackageManager;
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
            int newIndex = GetKeyIndex(_currentKey) + 1;
            HideTutorialStep();

            if (keys.Length > newIndex) {
                ShowTutorialStep(keys[newIndex].GetKeyCode());
            }
        }
    }

    public void ShowTutorialStep(KeyCode keyCode) {
        if (tutorialStepUi.activeSelf) { HideTutorialStep(); }

        Key key = GetKey(keyCode);

        actionImage.sprite = key.GetTexture();
        actionText.text = key.GetAction();

        _currentKey = keyCode;

        tutorialStepUi.SetActive(true);
        showTutorialStep.Invoke();
    }

    public void HideTutorialStep() {
        _currentKey = KeyCode.None;
        hideTutorialStep.Invoke();

        tutorialStepUi.SetActive(false);
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
}
