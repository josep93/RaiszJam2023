using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundScript : MonoBehaviour
{
    RoundEvent roundEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateRound()
    {
        //Deactivate Hud

        TreeScript.current.UpdateBonuses();
        roundEvent.Run();
    }

    public void EndRound()
    {
        //Activate Hud

    }
}
