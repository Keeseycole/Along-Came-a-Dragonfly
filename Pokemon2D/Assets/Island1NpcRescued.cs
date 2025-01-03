using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island1NpcRescued : MonoBehaviour
{
    public GameObject npc1Island, npc1Rescued, npc2AfterRescue, npc2BeforeRescue;

    PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            npc1Island.SetActive(false);
            npc1Rescued.SetActive(true);
            npc2AfterRescue.SetActive(true);
            npc2BeforeRescue.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
