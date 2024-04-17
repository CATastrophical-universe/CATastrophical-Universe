using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using UnityEngine.TestTools;
using System.Collections;

public class MovementTests
{
	private GameObject playerObject;
    private GameObject groundObject;
	private PlayerMovement playerMovement;
	private Rigidbody2D rb;
	private GameObject groundCheck;

	[SetUp]
	public void Setup()
	{
        // Set up mocks if necessary (skipping complicated Unity specific stuff, focus on principle)
		LayerMask groundLayer = 1 << 6;
        Debug.Log(string.Format("{0} {1}", (int)groundLayer.value, LayerMask.NameToLayer("Ground")));

        groundObject = new GameObject("Ground");
        groundObject.AddComponent<BoxCollider2D>();
        groundObject.transform.position -= new Vector3(0f, 10f, 0f);
        groundObject.transform.localScale = new Vector2(10f, 1f);
        groundObject.layer = groundLayer;

		playerObject = new GameObject("PlayerObject");
        playerObject.AddComponent<BoxCollider2D>();
        rb = playerObject.AddComponent<Rigidbody2D>();
        playerMovement = playerObject.AddComponent<PlayerMovement>();

		groundCheck = new GameObject("GroundCheck");
        groundCheck.transform.SetParent(playerObject.transform);
        groundCheck.transform.position = new Vector3(0f, -0.6f, 0f);

		// Use reflection or exposed functions to set up private fields if necessary
		typeof(PlayerMovement).GetField("rb", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, rb);
		typeof(PlayerMovement).GetField("groundCheck", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, groundCheck.transform);
		typeof(PlayerMovement).GetField("groundLayer", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, groundLayer);
        // playerMovement.SetGroundLayer("Ground");
	}

	[UnityTest]
    [Timeout(10000)]
	public IEnumerator DoesPlayerMoveRight()
	{
        yield return new WaitWhile(() => !playerMovement.IsGrounded());

		playerMovement.SetHorizontal(1f);

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
        Object.DestroyImmediate(groundCheck);
        Object.DestroyImmediate(groundObject);
	}
}
