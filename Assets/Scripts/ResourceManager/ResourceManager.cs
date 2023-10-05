using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceManager : IResourceManager
{
    // TODO type
    private Dictionary<object, ObjectPool<GameObject>> ObjectPools = new Dictionary<object, ObjectPool<GameObject>>();

    public void Dispose()
    {
        foreach (var pool in ObjectPools.Values)
        {
            pool.Dispose();
        }

        ObjectPools.Clear();
    }

    public T GetFromPool<E, T>(E name) where E : Enum where T : class
    {
        var enumType = typeof(E);

        if (!ObjectPools.ContainsKey(name))
        {
            Func<GameObject> getFunc = () => InstantiatePrefab<E, GameObject>(name);
            var pool = new ObjectPool<GameObject>(getFunc);
            ObjectPools[name] = pool;
        }

        var resultObject = ObjectPools[name].Get();

        if (typeof(T).Equals(typeof(GameObject)))
        {
            return resultObject as T;
        }

        var resultComponent = resultObject.GetComponent<T>();
        return resultComponent;
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
