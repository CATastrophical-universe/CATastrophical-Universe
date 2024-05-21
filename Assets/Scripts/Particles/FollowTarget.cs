using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] Vector3 offset;


    void OnEnable()
    {
        if (follow != null) {
            StartCoroutine(Follow());
        }
    }

    IEnumerator Follow() {
        while (true) {
            transform.position = follow.position +  offset;

            yield return new WaitForEndOfFrame();
        }
    }
}
