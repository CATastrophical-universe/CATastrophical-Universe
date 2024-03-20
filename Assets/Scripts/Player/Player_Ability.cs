using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ability : MonoBehaviour
{
    // Variables
    private int worldNum = 1;

    // References to components and objects
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundLayer; // Layer mask to define what is considered ground
    [SerializeField] private float tpDistance = 40;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability") && !Overlaps())
        {
            playerTransform.position = new Vector3(playerTransform.position.x,
                playerTransform.position.y - tpDistance * worldNum, playerTransform.position.z);

            cameraTransform.position = new Vector3(cameraTransform.position.x,
                cameraTransform.position.y - tpDistance * worldNum, cameraTransform.position.z);

            worldNum *= -1;
        } else if (Input.GetButtonDown("Ability"))
        {
            playerRenderer.color = Color.red;
        }

        if (Input.GetButtonUp("Ability"))
        {
            playerRenderer.color = Color.black;
        }
    }
    private bool Overlaps()
    {
        return Physics2D.OverlapCircle(new Vector3(playerTransform.position.x,
                playerTransform.position.y - tpDistance * worldNum, playerTransform.position.z), 0.2f, groundLayer);
    }
}
