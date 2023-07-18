using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace TestCubes
{
    public static class TestCubeManager
    {
        private static GameObject _testCubesHider;
        private static Dictionary<string, GameObject> _testCubePrefabs;

        /// <summary>
        /// Gets the cube prefab for a specified Type key. If prefab does not exist, creates a new cube prefab.
        /// </summary>
        /// <typeparam name="T">Type key. Usually your CustomGameDataObject type</typeparam>
        /// <param name="scaleX">X component of local scale</param>
        /// <param name="scaleY">Y component of local scale</param>
        /// <param name="scaleZ">Z component of local scale</param>
        /// <param name="material">Cube material</param>
        /// <param name="collider">If true, collider and navmesh enabled. Otherwise, disabled.</param>
        /// <returns>Prefab GameObject for instantiation</returns>
        public static GameObject GetPrefab<T>(float scaleX = 1f, float scaleY = 1f, float scaleZ = 1f, Material material = null, bool collider = true)
        {
            return GetPrefab(typeof(T).AssemblyQualifiedName,
                scaleX: scaleX,
                scaleY: scaleY,
                scaleZ: scaleZ,
                material: material,
                collider: collider);
        }

        /// <summary>
        /// Gets the cube prefab for a specified Identifier. If prefab does not exist, creates a new cube prefab.
        /// </summary>
        /// <param name="guid">Unique Object Identifier</param>
        /// <param name="scaleX">X component of local scale</param>
        /// <param name="scaleY">Y component of local scale</param>
        /// <param name="scaleZ">Z component of local scale</param>
        /// <param name="material">Cube material</param>
        /// <param name="collider">If true, collider and navmesh enabled. Otherwise, disabled.</param>
        /// <returns>Prefab GameObject for instantiation</returns>
        public static GameObject GetPrefab(string guid, float scaleX = 1f, float scaleY = 1f, float scaleZ = 1f, Material material = null, bool collider = true)
        {
            if (_testCubePrefabs == null)
            {
                _testCubePrefabs = new Dictionary<string, GameObject>();
            }

            if (_testCubesHider == null)
            {
                _testCubesHider = new GameObject("Test Cube Container");
                _testCubesHider.SetActive(false);
            }

            if (!_testCubePrefabs.TryGetValue(guid, out GameObject prefab))
            {
                prefab = GameObject.Instantiate(Main.Bundle.LoadAsset<GameObject>("TestCube"));
                prefab.name = $"{guid}_TestCube";
                prefab.transform.SetParent(_testCubesHider.transform);
                Transform anchor = prefab.transform.Find("Anchor");
                if (anchor != null)
                {
                    anchor.localScale = new Vector3(scaleX, scaleY, scaleZ);
                    Transform childCollider = anchor.Find("Cube")?.Find("Collider");
                    childCollider?.gameObject.SetActive(collider);

                    if (material == null)
                    {
                        material = MaterialUtils.GetExistingMaterial("Metal Dark");
                    }
                    MaterialUtils.ApplyMaterial(prefab, "Anchor/Cube", new Material[] { material });
                }

                _testCubePrefabs[guid] = prefab;
            }

            return prefab;
        }
    }
}
