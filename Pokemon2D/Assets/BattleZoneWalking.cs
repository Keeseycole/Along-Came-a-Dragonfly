using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZoneWalking : MonoBehaviour
{

    public void OnPlayerTriggered(PlayerController player)
    {
        if (UnityEngine.Random.Range(1, 101) <= 10)
        {
            player.Charecter.Animator.IsMoving = false;
            //Debug.Log("battle started");
            GameController.Instance.StartBattle(BattleTrigger.Grasslands);
        }
    }

    public void OnCompanionTriggered(CompanionController companion)
    {
        if (UnityEngine.Random.Range(1, 101) <= 10)
        {
            companion.Charecter.Animator.IsMoving = false;
            // Debug.Log("battle started");
            GameController.Instance.StartBattle(BattleTrigger.Grasslands);
        }
    }

    public bool triggerRepeatedly => true;
}
