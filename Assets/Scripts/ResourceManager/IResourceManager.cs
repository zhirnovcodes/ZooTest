using System;

public interface IResourceManager : IDisposable
{
    T GetFromPool<E, T>(E name) where E : Enum where T: class;
}
