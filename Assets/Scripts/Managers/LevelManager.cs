using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] GameObject[] topLevelLights;
    [SerializeField] GameObject[] bottomLevelLights;
    
    void Start()
    {
        Player_Ability.OnTeleport += HandleTeleportEvent;
	}

    void OnDisable()
    {
        Player_Ability.OnTeleport -= HandleTeleportEvent;
    }

    void HandleTeleportEvent(int worldNum) {
        if (worldNum == 1) {
            ToggleLights(bottomLevelLights, false);
            ToggleLights(topLevelLights, true);
        }
        else if (worldNum == -1) {
            ToggleLights(topLevelLights, false);
            ToggleLights(bottomLevelLights, true);
        }
    }

    void ToggleLights(GameObject[] lights, bool value) {
        foreach(GameObject light in lights) {
            light.SetActive(value);
        }
    }
}
