using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void createBullet(ref GameObject bullet);
    public void onHit(GameObject enemyGo);
}
