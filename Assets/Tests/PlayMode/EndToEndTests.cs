using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Reflection;

public class EndToEndTests
{
    bool sceneLoaded = false;
    bool referencesSetup = false;
    private GameObject player;
    private PlayerMovement playerMovement;
    private Player_Ability playerAbility;
    private Rigidbody2D playerRigidbody;

    [OneTimeSetUp]
    public void OneTimeSetup() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Level_Tutorial");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        sceneLoaded = true;
    }

    void SetupReferences() {
        if (referencesSetup)
            return;

        Transform[] objects = Resources.FindObjectsOfTypeAll<Transform>();

        foreach (Transform t in objects) {
            if (t.name == "Player") {
                player = t.gameObject;
                playerMovement = t.GetComponent<PlayerMovement>();
                playerAbility = t.GetComponent<Player_Ability>();
                playerRigidbody = t.GetComponent<Rigidbody2D>();
            }
        }
    }

    [UnityTest]
    [Timeout(15000)]
    public IEnumerator Make_Sure_Player_Does_Not_Fall_Off_The_Map() {
        yield return new WaitWhile(() => sceneLoaded == false);

        SetupReferences();

        // Check if references are setup
        Assert.IsNotNull(player);
        Assert.IsNotNull(playerMovement);
        Assert.IsNotNull(playerAbility);
        Assert.IsNotNull(playerRigidbody);

        // Move player to left
        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, -1f);
        yield return new WaitWhile(() => player.transform.position.x > -7f);
        // Move player to left
        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, 1f);
        yield return new WaitWhile(() => player.transform.position.x < 1.5f);
        // Jump
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 12f);
        yield return new WaitWhile(() => player.transform.position.x < 16f);

        // Teleport
        playerAbility.Teleport(playerAbility.Overlaps());
        yield return new WaitWhile(() => player.transform.position.x < 34f);

        // Teleport
        playerAbility.Teleport(playerAbility.Overlaps());
        yield return new WaitWhile(() => player.transform.position.x < 54f);

        // Teleport
        playerAbility.Teleport(playerAbility.Overlaps());
        yield return new WaitWhile(() => player.transform.position.x < 60f);

        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 12f);
        yield return new WaitForSeconds(0.5f);
        playerAbility.Teleport(playerAbility.Overlaps());
        yield return new WaitForSeconds(5);

        Assert.GreaterOrEqual(player.transform.position.y, -50);
    }

    [UnityTest]
    [Timeout(15000)]
    public IEnumerator Walk_Player_To_The_Finish_Point_Through_Whole_Level() {
        yield return new WaitWhile(() => sceneLoaded == false);

        SetupReferences();

        // Check if references are setup
        Assert.IsNotNull(player);
        Assert.IsNotNull(playerMovement);
        Assert.IsNotNull(playerAbility);
        Assert.IsNotNull(playerRigidbody);

        // Move player to left
        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, -1f);
        yield return new WaitWhile(() => player.transform.position.x > -7f);
        // Move player to left
        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, 1f);
        yield return new WaitWhile(() => player.transform.position.x < 1.5f);
        // Jump
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 12f);
        yield return new WaitWhile(() => player.transform.position.x < 16f);

        // Teleport
        playerAbility.Teleport(playerAbility.Overlaps());
        yield return new WaitWhile(() => player.transform.position.x < 34f);

        // Teleport
        playerAbility.Teleport(playerAbility.Overlaps());
        yield return new WaitWhile(() => player.transform.position.x < 54f);

        // Teleport
        playerAbility.Teleport(playerAbility.Overlaps());
        yield return new WaitWhile(() => player.transform.position.x < 75f);

        Assert.AreEqual("Level_Tutorial", SceneManager.GetActiveScene().name);
    }
}
