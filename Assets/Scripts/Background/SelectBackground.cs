using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBackground : MonoBehaviour
{
    [Header("Backgrounds")]
    [SerializeField] GameObject background1;
    [SerializeField] GameObject background2;

    void Start()
    {
        Player_Ability.OnTeleport += HandleTeleportEvent;
	}

    void OnDisable()
    {
        Player_Ability.OnTeleport -= HandleTeleportEvent;
    }

    void HandleTeleportEvent(int worldNum)
    {
        background1.SetActive(worldNum == 1);
        background2.SetActive(worldNum == -1);
    }
}
