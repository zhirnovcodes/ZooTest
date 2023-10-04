using System;

public interface IMortal
{
    event Action Died;
    bool IsAlive { get; }
}
