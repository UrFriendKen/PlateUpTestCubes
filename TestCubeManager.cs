using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestCubes
{
    public static class TestCubeManager
    {
        private static GameObject _testCubesHider;
        private static Dictionary<Type, GameObject> _testCubePrefabs;

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
            Type type = typeof(T);
            if (_testCubePrefabs == null)
            {
                _testCubePrefabs = new Dictionary<Type, GameObject>();
            }

            if (_testCubesHider == null)
            {
                _testCubesHider = new GameObject("Test Cube Container");
                _testCubesHider.SetActive(false);
            }

            if (!_testCubePrefabs.TryGetValue(type, out GameObject prefab))
            {
                prefab = GameObject.Instantiate(Main.Bundle.LoadAsset<GameObject>("TestCube"));
                prefab.name = $"{type.Name}_TestCube";
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

                _testCubePrefabs[type] = prefab;
            }

            return prefab;
        }
    }
}
