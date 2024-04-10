using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParallaxEffectTests
{
    private GameObject _camera;
    private GameObject _parallaxObject;
    private ParallaxEffect _parallaxEffect;

    [SetUp]
    public void Setup() {
        // Create a scene with a camera and a GameObject with the ParallaxEffect script
        _camera = new GameObject("TestCamera");
        _parallaxObject = new GameObject("ParallaxObject");
        _parallaxObject.AddComponent<ParallaxEffect>();

        // Assign the camera to the ParallaxEffect script (assuming a public setter)
        _parallaxEffect = _parallaxObject.GetComponent<ParallaxEffect>();
        _parallaxEffect.SetCameraGameObject(_camera); // Assuming a private field with a setter

        // Set initial positions (modify these if needed for your test)
        _camera.transform.position = Vector3.zero;
        _parallaxObject.transform.position = Vector3.zero;
    }

    [TearDown]
    public void Teardown() {
        // Destroy GameObjects to avoid affecting subsequent tests
        Object.DestroyImmediate(_camera);
        Object.DestroyImmediate(_parallaxObject);
    }

    [UnityTest]
    public IEnumerator TestParallaxMovement() {
        float _customMultiplier = 0.5f;

        // Set a parallax multiplier (modify value as needed)
        _parallaxEffect.SetParallaxMultiplier(_customMultiplier);

        // Move the camera to simulate movement
        _camera.transform.position = new Vector3(5f, 0f, 0f);

        // Update the ParallaxEffect script
        _parallaxEffect.UpdateParallax();

        yield return new WaitForSeconds(1f);

        // Assert that the parallax object moved as expected
        float expectedX = _camera.transform.position.x - (_camera.transform.position.x - _parallaxObject.transform.position.x);
        Assert.AreEqual(expectedX, _parallaxObject.transform.position.x, 0.01f);
    }

    [Test]
    public void TestRepeatOffset() {
        float _customMultiplier = 1.0f;
        float _customOffset = 2.0f;

        // Set parallax multiplier and repeat offset (modify values as needed)
        _parallaxEffect.SetParallaxMultiplier(_customMultiplier);
        _parallaxEffect.SetRepeatOffset(_customOffset);

        // Move the camera beyond the repeat offset
        _camera.transform.position = new Vector3(3f, 0f, 0f);

        // Update the ParallaxEffect script
        _parallaxEffect.UpdateParallax();

        // Assert that the parallax object position wraps around the repeat offset
        float expectedX = _camera.transform.position.x - (_camera.transform.position.x - _parallaxObject.transform.position.x) % _customOffset;
        Assert.AreEqual(expectedX, _parallaxObject.transform.position.x, 0.01f);
    }
}
