using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraFollowTransform;
    public void SetupCameraFolloe(Transform cameraFollowTransform)
    {
        this.cameraFollowTransform = cameraFollowTransform;
    }
    void Update()
    {
        if(cameraFollowTransform != null)
        {
            transform.position = new Vector3
                (
                cameraFollowTransform.position.x,
                cameraFollowTransform.position.y,
                -10
                );
        }
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-0.5f, .5f) * magnitude;
            float y = Random.Range(-0.5f, .5f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
