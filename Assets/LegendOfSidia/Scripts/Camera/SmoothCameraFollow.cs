using UnityEngine;

namespace LegendOfSidia { 
    public class SmoothCameraFollow : MonoBehaviour
    {
        public Transform target;
        public float distance = 5f;
        public Vector2 cameraDistanceMinMax;

        private void Update()
        {
            if (!target || !target.hasChanged) return;

            distance += Input.mouseScrollDelta.y;
            distance = Mathf.Clamp(distance, cameraDistanceMinMax.x, cameraDistanceMinMax.y);


            Vector3 newCameraPos = target.position + (Vector3.up * distance);
            transform.position = Vector3.Lerp(transform.position, newCameraPos, Time.deltaTime);
        }
    }
}