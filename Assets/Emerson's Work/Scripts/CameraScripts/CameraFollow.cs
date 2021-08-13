using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private float offsetSmoothing = 2.0f;
    private float offset = 1.0f;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            if (player.transform.localScale.x > 0.0f)
            {
                playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, -10);
            }
            else
            {
                playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, -10);
            }
            this.transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
