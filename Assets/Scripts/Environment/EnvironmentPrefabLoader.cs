using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Prosodiya
{
    [ExecuteInEditMode]
    public class EnvironmentPrefabLoader : MonoBehaviour
    {
        public List<GameObject> PrefabsToLoad = new List<GameObject>();
        public bool ExecuteInEditMode = true;

        void Awake()
        {
            if(Application.isPlaying || ExecuteInEditMode) LoadPrefabsAsChild();
        }

        /// <summary>
        /// Instantiates the prefabs as a child of the GameObject this script is attached to
        /// </summary>
        public void LoadPrefabsAsChild()
        {
            foreach (var pref in PrefabsToLoad)
            {
                var go = Instantiate(pref);
#if UNITY_EDITOR
                foreach (Transform child in transform)
                {
                    if (child.name.Contains(pref.name))
                    {
                        DestroyImmediate(child.gameObject);
                    }
                }
#endif
                if(go) go.transform.parent = transform;
            }
        }
    }

}
