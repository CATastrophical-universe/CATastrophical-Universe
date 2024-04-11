using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using UnityEngine.TestTools;
using System.Collections;

public class MovementTests
{
	private GameObject playerObject;
	private PlayerMovement playerMovement;
	private Rigidbody2D rb;
	private Transform groundCheck;
	private LayerMask groundLayer;

	[SetUp]
	public void Setup()
	{
		playerObject = new GameObject("PlayerObject");
		playerMovement = playerObject.AddComponent<PlayerMovement>();
		rb = playerObject.AddComponent<Rigidbody2D>();
		groundCheck = new GameObject().transform;

		// Set up mocks if necessary (skipping complicated Unity specific stuff, focus on principle)
		groundLayer = LayerMask.NameToLayer("Ground");

		// Use reflection or exposed functions to set up private fields if necessary
		typeof(PlayerMovement).GetField("rb", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, rb);
		typeof(PlayerMovement).GetField("groundCheck", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, groundCheck);
		typeof(PlayerMovement).GetField("groundLayer", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, groundLayer);
	}

	[UnityTest]
	public IEnumerator DoesPlayerMoveRight()
	{
		playerMovement.SetHorizontal(1f);

		yield return new WaitForSeconds(1);

		Debug.Log(playerObject.transform.position.x);

		Assert.IsTrue(playerObject.transform.position.x > 0f);
	}

	[UnityTest]
	public IEnumerator DoesPlayerMoveLeft()
	{
		playerMovement.SetHorizontal(-1f);

		yield return new WaitForSeconds(1);

		Assert.IsTrue(playerObject.transform.position.x < 0f);
	}

	[TearDown]
	public void TearDown()
	{
		Object.DestroyImmediate(playerObject);
	}
}
