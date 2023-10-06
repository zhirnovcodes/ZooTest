using System;

public interface IResourceManager : IDisposable
{
    T GetFromPool<E, T>(E name) where E : Enum where T: class;
    T GetFromPool<E, T>(E name, out bool wasInstantiated) where E : Enum where T : class;

    T InstantiatePrefab<E, T>(E name) where E : Enum where T : class;
}
