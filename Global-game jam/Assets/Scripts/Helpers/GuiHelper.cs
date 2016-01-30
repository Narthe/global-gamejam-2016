using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class GuiHelper
    {

        public static GameObject Instanciate(GameObject prefab, GameObject parent)
        {
            if (prefab == null)
            {
                return null;
            }

            GameObject child = (GameObject)Object.Instantiate(prefab);
            child.transform.SetParent(parent.transform, false);
            return child;
        }
        /// <summary>
        /// Detruit tous les enfants d'un GameObject
        /// </summary>
        /// <param name="container"></param>
        public static void ClearChilds(this GameObject container)
        {
            foreach (Transform t in container.transform)
            {
                Object.Destroy(t.gameObject, .1f);
            }
        }

        public static GameObject Instanciate(GameObject parent)
        {

            GameObject prefabToInstanciate = new GameObject();
            prefabToInstanciate.AddComponent<RectTransform>();
            prefabToInstanciate.AddComponent<CanvasRenderer>();



            GameObject child = (GameObject)Object.Instantiate(prefabToInstanciate);
            child.transform.SetParent(parent.transform, false);
            Object.Destroy(prefabToInstanciate);
            return child;
        }
    }
}
