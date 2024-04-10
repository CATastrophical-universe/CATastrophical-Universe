using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class TutorialStepsTests
{
    static KeyCode[] _keyCodes = new KeyCode[] { KeyCode.Space, KeyCode.A, KeyCode.D };

    [Test, Order(0)]
    public void CreateKeyTest([ValueSource("_keyCodes")] KeyCode keyCode) {
        Key newKey = new Key(KeyCode.Space.ToString(), keyCode, "Action");

        Assert.AreEqual(keyCode, newKey.GetKeyCode());
    }

    [Test, Order(1)]
    public void ShowTutorialStepsTest([ValueSource("_keyCodes")] KeyCode keyCode) {
        Key[] _keys = new Key[] { new Key(KeyCode.Space.ToString(), keyCode, "Action") };

        GameObject _tutorialManagerObject = new GameObject("TutorialManager");
        GameObject _tutorialStepUI = new GameObject("TutorialStepUI");

        _tutorialStepUI.AddComponent<TMP_Text>();
        TMP_Text _actionText = _tutorialStepUI.GetComponent<TMP_Text>();

        _tutorialManagerObject.AddComponent<TutorialManager>();
        TutorialManager _tutorialManager = _tutorialManagerObject.GetComponent<TutorialManager>();

        _tutorialManager.SetKeys(_keys);
        _tutorialManager.SetTutorialStepUi(_tutorialStepUI);
        _tutorialManager.SetActionText(_actionText);
        
        _tutorialStepUI.SetActive(false);

        _tutorialManager.ShowTutorialStep(keyCode);

        Assert.IsTrue(_tutorialStepUI.activeSelf);
    }

    [UnityTest, Order(2)]
    public IEnumerator CompleteTutorialStepsTest() {
        ArrayList _keys = new ArrayList();

        foreach (KeyCode keyCode in _keyCodes)
            _keys.Add(new Key(KeyCode.Space.ToString(), keyCode, "Action"));

        GameObject _tutorialManagerObject = new GameObject("TutorialManager");
        GameObject _tutorialStepUI = new GameObject("TutorialStepUI");

        _tutorialStepUI.AddComponent<TMP_Text>();
        TMP_Text _actionText = _tutorialStepUI.GetComponent<TMP_Text>();

        _tutorialManagerObject.AddComponent<TutorialManager>();
        TutorialManager _tutorialManager = _tutorialManagerObject.GetComponent<TutorialManager>();

        _tutorialManager.SetTutorialStepUi(_tutorialStepUI);
        _tutorialManager.SetActionText(_actionText);
        _tutorialManager.SetKeys((Key[])_keys.ToArray(typeof(Key)));
        
        _tutorialStepUI.SetActive(false);

        yield return new WaitForSeconds(2);

        foreach (Key key in _keys) {
            _tutorialManager.StepCompleted();

            yield return new WaitForSeconds(1);
        }

        Assert.IsFalse(_tutorialStepUI.activeSelf);
    }
}
