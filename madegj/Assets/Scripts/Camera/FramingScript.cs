using Unity.Cinemachine;
using UnityEngine;

namespace Camera
{
    /// <summary>
    ///     Script to frame the two protags
    /// </summary>
    public class FramingScript : MonoBehaviour
    {
        [Header("Depends")]

        [SerializeField]
        private Transform protag1;

        [SerializeField]
        private Transform protag2;

        [SerializeField]
        private CinemachineCamera virtualCamera;

        [Header("Settings")]

        [SerializeField]
        private float margin;

        [SerializeField]
        private float minOrthographicSize;

        [SerializeField]
        private float lerpSpeed;

        private float desiredOrthographicSize;
        private Vector2 desiredPosition;

        private void Update()
        {
            if (protag1 == null || protag2 == null)
            {
                return;
            }

            // Calculate target bounds
            var bounds = new Bounds();
            bounds.Encapsulate(protag1.position);
            bounds.Encapsulate(protag2.position);

            Vector3 cameraSize = bounds.size;
            cameraSize.x += margin;
            cameraSize.y += margin;

            desiredPosition = bounds.center;

            // Resize camera
            float heightBoundSize = cameraSize.y / 2;
            float widthBoundSize = cameraSize.x / virtualCamera.Lens.Aspect / 2;
            desiredOrthographicSize = Mathf.Max(heightBoundSize, widthBoundSize, minOrthographicSize);

            // Lerp
            float t = 1 - Mathf.Pow(0.001f, Time.deltaTime * lerpSpeed);

            Vector3 pos = virtualCamera.transform.position;
            Vector3 finalCameraPos = Vector3.Lerp(pos, desiredPosition, t);
            finalCameraPos.z = -10;
            virtualCamera.transform.position = finalCameraPos;

            virtualCamera.Lens.OrthographicSize =
                Mathf.Lerp(virtualCamera.Lens.OrthographicSize, desiredOrthographicSize, t);
        }
    }
}