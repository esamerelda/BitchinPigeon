using UnityEngine;
namespace Tools {
    public class Tools {
        public static bool GetIsLayerInLayerMask(GameObject obj, LayerMask layerMask) {
            return ((1 << obj.layer) & layerMask.value) > 0;
        }
        public static bool GetIsOnLayer(GameObject obj, string layerName) {
            return obj.layer == LayerMask.NameToLayer(layerName);
        }
        public static Ray ScreenPointToRay(Camera camera) {
            return camera.ScreenPointToRay(new Vector3(camera.pixelWidth / 2.0f, camera.pixelHeight / 2.0f, 0f));
        }
    }
}