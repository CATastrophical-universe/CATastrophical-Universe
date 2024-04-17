using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerAbilityTests
{
    private GameObject gameObject;
    private GameObject cameraObject;
    private SpriteRenderer renderer;
    private Player_Ability ability;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        cameraObject = new GameObject();
        renderer = gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<Player_Ability>();

        ability = gameObject.GetComponent<Player_Ability>();

        ability.SetPlayerRenderer(renderer);
        gameObject.transform.position = new Vector3(0f, 0f, 0f);
        cameraObject.transform.position = new Vector3(0f, 0f, 0f);
    }
    [TearDown]
    public void Teardown()
    {
        // Destroy GameObjects to avoid affecting subsequent tests
        Object.DestroyImmediate(gameObject);
        Object.DestroyImmediate(renderer);
    }
    [Test]
    public void ChangedStatesToBlack()
    {
        renderer.color = Color.red;
        ability.SetPlayerRenderer(renderer);
        ability.unable = true;

        ability.ChangeStates();

        Assert.AreEqual(Color.black, ability.GetPlayerRenderer());
    }
    [Test]
    public void ChangedStatesToRed()
    {
        renderer.color = Color.black;
        ability.SetPlayerRenderer(renderer);
        ability.unable = true;

        ability.ChangeStates();

        Assert.AreEqual(Color.red, ability.GetPlayerRenderer());
    }
    [Test]
    public void NotChangedStates()
    {
        renderer.color = Color.black;
        ability.SetPlayerRenderer(renderer);
        ability.unable = false;

        ability.ChangeStates();

        Assert.AreEqual(Color.black, ability.GetPlayerRenderer());
    }
    [Test]
    public void Teleported()
    {
        bool overlap = false;
        ability.SetPlayerAndCameraTransform(gameObject.transform, cameraObject.transform);

        ability.Teleport(overlap);

        Assert.AreEqual(-40f, ability.GetPlayerTransform());
    }
    [Test]
    public void NotTeleported()
    {
        bool overlap = true;
        ability.SetPlayerAndCameraTransform(gameObject.transform, cameraObject.transform);

        ability.Teleport(overlap);

        Assert.AreEqual(0, ability.GetPlayerTransform());
    }
}
