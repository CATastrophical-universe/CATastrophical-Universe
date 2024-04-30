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
    public bool unable = false;

    // Events for teleport
    public delegate void TeleportEventHandler(int worldNum);
    public static event TeleportEventHandler OnTeleport;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability"))
        {
            Teleport(Overlaps());
        } 

        if (Input.GetButtonUp("Ability"))
        {
            ChangeStates();
        }
    }
    public void Teleport(bool overlaps)
    {
        if (!overlaps)
        {
            playerTransform.position = new Vector3(playerTransform.position.x,
                playerTransform.position.y - tpDistance * worldNum, playerTransform.position.z);

            cameraTransform.position = new Vector3(cameraTransform.position.x,
                cameraTransform.position.y - tpDistance * worldNum, cameraTransform.position.z);

            worldNum *= -1;

            OnTeleport?.Invoke(worldNum);
        } else
        {
            unable = true;
            ChangeStates();
        }
        
    }
    public void ChangeStates()
    {
        if (playerRenderer.color == Color.black && unable)
        {
            playerRenderer.color = Color.red;
        }else if(unable)
        {
            playerRenderer.color = Color.black;
            unable = false;
        }
    }
    public bool Overlaps()
    {
        return Physics2D.OverlapCircle(new Vector3(playerTransform.position.x,
                playerTransform.position.y - tpDistance * worldNum, playerTransform.position.z), 0.2f, groundLayer);
    }

    #if UNITY_INCLUDE_TESTS

        public void SetPlayerAndCameraTransform(Transform p, Transform c) {
            playerTransform = p;
            cameraTransform = c;
        }
        public float GetPlayerTransform() { return playerTransform.position.y; }
        public void SetPlayerRenderer(SpriteRenderer p)
        {
            playerRenderer = p;
        }
        public Color GetPlayerRenderer() { return playerRenderer.color; }

#endif
}
