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
		LayerMask groundLayer = LayerMask.NameToLayer("Ground");

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
		playerMovement.SetGroundLayer(groundLayer);
	}

	[UnityTest]
    [Timeout(15000)]
	public IEnumerator DoesPlayerMoveRight()
	{
        yield return new WaitWhile(() => !playerMovement.IsGrounded());

        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, 1f);

        yield return new WaitForFixedUpdate();

		Assert.IsTrue(playerObject.transform.position.x > 0f);
	}

	[UnityTest]
	public IEnumerator DoesPlayerMoveLeft()
	{
		yield return new WaitWhile(() => !playerMovement.IsGrounded());

        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, -1f);

        yield return new WaitForFixedUpdate();

		Assert.IsTrue(playerObject.transform.position.x < 0f);
	}

    [UnityTest]
    public IEnumerator DoesPlayerFlipRight()
    {
        yield return new WaitWhile(() => !playerMovement.IsGrounded());

        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, 1f);

        yield return new WaitForFixedUpdate();

        Assert.IsTrue(playerObject.transform.localScale.x > 0f);
    }

    [UnityTest]
    public IEnumerator DoesPlayerFlipLeft()
    {
        yield return new WaitWhile(() => !playerMovement.IsGrounded());

        typeof(PlayerMovement).GetField("horizontal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playerMovement, -1f);

        yield return new WaitForFixedUpdate();

        Assert.IsTrue(playerObject.transform.localScale.x < 0f);
    }

    [UnityTest]
    public IEnumerator IsGroundedWhenJumping()
    {
        yield return new WaitWhile(() => !playerMovement.IsGrounded());

        rb.velocity = new Vector2(0f, 10f);

        yield return new WaitWhile(() => rb.velocity.y > 0f);

        Assert.IsFalse(playerMovement.IsGrounded());
    }

	[TearDown]
	public void TearDown()
	{
		Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(groundCheck);
        Object.DestroyImmediate(groundObject);
	}
}
