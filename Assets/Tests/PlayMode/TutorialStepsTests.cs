using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TutorialStepsTests
{
    static KeyCode[] _keyCodes = new KeyCode[] { KeyCode.Space, KeyCode.A, KeyCode.D };

    [Test]
    public void CreateKeyTest([ValueSource("_keyCodes")] KeyCode keyCode) {
        Key newKey = new Key(KeyCode.Space.ToString(), keyCode, "Action");

        Assert.AreEqual(keyCode, newKey.GetKeyCode());
    }
}
