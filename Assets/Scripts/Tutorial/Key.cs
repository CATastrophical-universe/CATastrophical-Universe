using System;
using UnityEngine;

[System.Serializable]
public class Key
{
    [SerializeField] Sprite texture;
    [SerializeField] String keyName;
    [SerializeField] KeyCode keyCode;
    [SerializeField] String action;

    public Sprite GetTexture() { return texture; }
    public String GetKeyName() { return keyName; }
    public KeyCode GetKeyCode() { return keyCode; }
    public String GetAction() { return action; }

    #if UNITY_INCLUDE_TESTS
    
        public Key(String keyName, KeyCode keyCode, String action) {
            this.keyName = keyName;
            this.keyCode = keyCode;
            this.action = action;
        }

    #endif
}
