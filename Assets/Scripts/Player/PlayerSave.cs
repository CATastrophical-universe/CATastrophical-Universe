using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        return new SaveData {
            Position_X = transform.position.x,
            Position_Y = transform.position.y
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        transform.position = new Vector3(saveData.Position_X, saveData.Position_Y, 0f);
    }

    [Serializable]
    private struct SaveData
    {
        public float Position_X;
        public float Position_Y;
    }
}
