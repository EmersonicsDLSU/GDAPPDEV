using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterAnimations
{
    void movementAnimation();
    void idleAnimation();
    void moveUpAnimation();
    void attackAnimation(bool attack = true);
    void deadAnimation();
    void destroyObj();
}
