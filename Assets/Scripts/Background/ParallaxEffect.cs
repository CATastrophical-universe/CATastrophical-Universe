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
        Vector3 cameraMovement = _camera.transform.position - cameraPos;
        transform.position += cameraMovement * parallaxMultiplier;
        cameraPos = _camera.transform.position;

        if (Mathf.Abs(_camera.transform.position.x - transform.position.x) >= repeatOffset) {
            float offsetX = (_camera.transform.position - transform.position).x % repeatOffset;

            transform.position = new Vector3(_camera.transform.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }
}