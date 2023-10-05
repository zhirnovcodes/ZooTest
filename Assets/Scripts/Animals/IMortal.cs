using System;

public interface IMortal
{
    bool IsAlive { get; }
    void Kill();
}
