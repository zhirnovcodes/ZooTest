using System;
using System.Collections.Generic;

public interface IPreyModel : IAnimal, IMortal
{
    void PlayDeadAnimation();

    void Enable();
    void Disable();
}
