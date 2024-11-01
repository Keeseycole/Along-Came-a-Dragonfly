using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, Interactable, ISavable
{
    [SerializeField] ItemBase item;

    public bool Used { get;  set; } = false;

    public bool isPhysicalPickup = false;

    [SerializeField] PlayerController player;

    [SerializeField] Creature playermoves;

 


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        playermoves = player.GetComponentInParent<Party>().creatures[0];

    
    }
    public object CaptureState()
    {
        return Used;
    }

    public void RestoreState(object state)
    {
        Used = (bool)state;

        if(Used)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public IEnumerator Interact(Transform initer)
    {
        if(isPhysicalPickup)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.parent = player.transform;

            
        }
        if (!Used)
        {
            string PlayermoveToLearn = null;



           if (item is PmItem playerMoves) 
            {
                playermoves.Moves.Add(new Move(playerMoves.getPlayerMoves()));
                PlayermoveToLearn = playerMoves.getPlayerMoves().name;


            } else
            {
                initer.GetComponent<Inventory>().AddItem(item);
            }
           

            Used = true;
            
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            string playerName = initer.GetComponent<PlayerController>().Name;


            if (string.IsNullOrEmpty(PlayermoveToLearn) == false)
            {
                yield return DialogueManager.Instance.ShowDialogText($" {playerName} learned {PlayermoveToLearn}");

            }
            else
            {
                yield return DialogueManager.Instance.ShowDialogText($" {playerName} found {item.Name}");
            }

            //GameController.Instance.StateMachine.Pop();
        }
    }

   
}
