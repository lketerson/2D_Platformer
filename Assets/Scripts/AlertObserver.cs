using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertObserver : MonoBehaviour
{

    PlayerCombat playerCombat;
    //public static AlertObserver AnimAlert;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlertObserve(string message)
    {
        switch (message)
        {
            case "AtackAnimEnd":
                playerCombat.IsAtacking = false;
                break;
            case "PlayerHitEnd":
                playerCombat.IsHitted = false;
                break;
        }

    }

}
