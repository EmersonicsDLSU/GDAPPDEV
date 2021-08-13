using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwapScript : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentindex = 0;
    public List <GameObject> weaponsGameObject = new List<GameObject>();
    private PlayerStatistics playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        initializeGun();
    }
    private void initializeGun()
    {
        if (GameObject.FindGameObjectWithTag("Gun") != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Gun");
            weaponsGameObject.Add(go.transform.GetChild(0).gameObject);
            weaponsGameObject.Add(go.transform.GetChild(1).gameObject);
            weaponsGameObject.Add(go.transform.GetChild(2).gameObject);
        }
    }

    public void leftbutton()
    {
        weapons[currentindex].SetActive(false);
        weaponsGameObject[currentindex].SetActive(false);
        currentindex -= 1;
        if (currentindex < 0)
        {
            currentindex = weapons.Length - 1;
        }
        weapons[currentindex].SetActive(true);
        weaponsGameObject[currentindex].SetActive(true);
        ChangeWeaponary(currentindex);
    }
   public void rightbutton()
    {
        weapons[currentindex].SetActive(false);
        weaponsGameObject[currentindex].SetActive(false);
        currentindex += 1;
        if (currentindex > weapons.Length -  1)
        {
            currentindex = 0;
        }
        weapons[currentindex].SetActive(true);
        weaponsGameObject[currentindex].SetActive(true);
        ChangeWeaponary(currentindex);
    }

    private void ChangeWeaponary(int index)
    {
        switch(index)
        {
            case 0:
                playerStats.currentBullet = PlayerStatistics.sBlueBul;
                playerStats.currentGun = PlayerStatistics.sBlueGun;
                break;
            case 1:
                playerStats.currentBullet = PlayerStatistics.sGreenBul;
                playerStats.currentGun = PlayerStatistics.sGreenGun;
                break;
            case 2:
                playerStats.currentBullet = PlayerStatistics.sRedBul;
                playerStats.currentGun = PlayerStatistics.sRedGun;
                break;
        }
    }

}
