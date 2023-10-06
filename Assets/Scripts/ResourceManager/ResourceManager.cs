using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceManager : IResourceManager
{
    private Dictionary<Type, Dictionary<int, List<GameObject>>> ObjectPools = new Dictionary<Type, Dictionary<int, List<GameObject>>>();

    public void Dispose()
    {
        foreach (var dictionary in ObjectPools.Values)
        {
            foreach (var pool in dictionary.Values)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    var disposableItem = pool[i];
                    pool[i] = null;
                    GameObject.Destroy(disposableItem);
                }
            }
            dictionary.Clear();
        }

        ObjectPools.Clear();
    }

    public T GetFromPool<E, T>(E name, out bool wasInstantiated) where E : Enum where T : class
    {
        var enumType = typeof(E);

        if (!ObjectPools.ContainsKey(enumType))
        {
            ObjectPools.Add(enumType, new Dictionary<int, List<GameObject>>());
        }

        var dictionary = ObjectPools[enumType];
        // TODO leakage
        var nameInt = (int)(object)name;

        if (!dictionary.ContainsKey(nameInt))
        {
            var pool = new List<GameObject>();
            dictionary.Add(nameInt, pool);
        }

        var resultObject = GetFromPoolList(dictionary[nameInt], name, out wasInstantiated);

        if (typeof(T).Equals(typeof(GameObject)))
        {
            return resultObject as T;
        }

        var resultComponent = resultObject.GetComponent<T>();
        return resultComponent;
    }

    public T GetFromPool<E, T>(E name) where E : Enum where T : class
    {
        return GetFromPool<E, T>(name, out var wasInstatntiated);
    }

    private GameObject GetFromPoolList<E>(List<GameObject> pool, E name, out bool wasInstantiated) where E : Enum
    {
        wasInstantiated = false;

        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                var activeObject = obj;
                activeObject.SetActive(true);
                return activeObject;
            }
        }

        wasInstantiated = true;
        var result = InstantiatePrefab<E, GameObject>(name);
        pool.Add(result);

        return result;
    }

    public T InstantiatePrefab<E, T>(E name) where E : Enum where T : class
    {
        var folderName = typeof(E).Name;
        var fileName = name.ToString();
        var prefabPath = "Zoo/" + folderName + "/" + fileName;
        var prefab = Resources.Load<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError("Prefab not found at path: " + prefabPath);
            return null;
        }

        var gameObject = GameObject.Instantiate(prefab);

        if (typeof(GameObject).Equals(typeof(T)))
        {
            return gameObject as T;
        }

        var component = gameObject.GetComponent<T>();

        return component;
    }
}
