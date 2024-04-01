using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.player.GetComponent<PlayerMove>().PlayerTeleport(transform.position);
    }
}
