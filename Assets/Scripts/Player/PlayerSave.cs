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
            Position = transform.position
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        transform.position = saveData.Position;
    }

    [Serializable]
    private struct SaveData
    {
        public Vector3 Position;
    }
}
