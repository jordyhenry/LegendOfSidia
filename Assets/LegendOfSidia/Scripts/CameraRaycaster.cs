using UnityEngine;

namespace LegendOfSidia
{
    public class CameraRaycaster : MonoBehaviour
    {
        public Camera raycastCamera;
        public LayerMask raycastLayer;
        public float rayDistance = 30f;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo = new RaycastHit();

                if (Physics.Raycast(ray, out hitInfo, rayDistance, raycastLayer))
                {
                    Tile tile = hitInfo.collider.GetComponent<Tile>();
                    if (tile)
                        Debug.Log(tile.transform.position);
                }
            }
        }
    }
}