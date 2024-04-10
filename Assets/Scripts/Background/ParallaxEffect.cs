using System;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField][Range(-1.0f, 1.0f)] float parallaxMultiplier;
    [SerializeField] GameObject _camera;
    [SerializeField] float repeatOffset;

    private Vector3 cameraPos;

    void Start() {
        cameraPos = _camera.transform.position;
    }

    void LateUpdate() {
        UpdateParallax();
    }

    public void UpdateParallax() {
        Vector3 cameraMovement = _camera.transform.position - cameraPos;
        transform.position += cameraMovement * parallaxMultiplier;
        cameraPos = _camera.transform.position;

        if (Mathf.Abs(_camera.transform.position.x - transform.position.x) >= repeatOffset) {
            float offsetX = 0f;

            if (repeatOffset > 0f)
                offsetX =  (_camera.transform.position - transform.position).x % repeatOffset;

            transform.position = new Vector3(_camera.transform.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }

    #if UNITY_INCLUDE_TESTS
    
        public void SetCameraGameObject(GameObject c) { _camera = c; }
        public void SetParallaxMultiplier(float parallaxMultiplier) {  this.parallaxMultiplier = parallaxMultiplier; }
        public void SetRepeatOffset(float repeatOffset) {  this.repeatOffset = repeatOffset; }

    #endif
}