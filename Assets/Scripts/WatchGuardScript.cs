using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGuardScript : MonoBehaviour {

    Animator animate;
    public bool playerCaught;

    void Start()
    {
        animate = GetComponent<Animator>();
    }

    void Update()
    {
        if(playerCaught)
            GameController.playerFound = true;

        if (GameController.playerFound == true)
            animate.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                {
                    playerCaught = true;
                    break;
                }
            default:
                break;
        }
    }
}
