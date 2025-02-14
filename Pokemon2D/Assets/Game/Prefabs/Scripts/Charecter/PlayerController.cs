
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Creature;



public class PlayerController : MonoBehaviour, ISavable,ISwitchable
{
    private Vector2 input;

    private Charecter charecter;

    public  PlayerController i;

    public Sprite openedChestSprite;

    private BuddyController buddyController;

    private followOnContact followOnContactController;

    public Party creatureparty { get; private set; }


    public bool playerActive = true;

    public Text swapText;

    public GameObject blocker;

    public GameObject torch;

    public GameObject freezeItem;

    public CharecterAnimator theanim;

    public Rigidbody2D theRb;

    public bool isholding;

    public bool hasSnailEggs;

    public bool  hasFishBones1, hasFishBones2, hasFishBones3, hasFishBones4,hasFishBones5, hasFishBones6;

    public bool hasIceWater;

    public bool ignoreInput = false;

    public bool isInTrigger;

    public bool isInWater;


    private void Awake()
    {
        
        i = this;

      theRb = gameObject.GetComponent<Rigidbody2D>();
        creatureparty = GetComponent<Party>();

        charecter = GetComponent<Charecter>();        
    }

    private void Start()
    {  
       // controllerScript.enabled = true;
       
    }
  

    public void HandleUpdate()
    {


        if (!playerActive) return;
        if (ignoreInput) return;
        if (!charecter.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // remove diagonal movement
           // if (input.x != 0) input.y = 0;

           // if (input != Vector2.zero)
           // {
           //     StartCoroutine(charecter.Move(input, OnMoveOver));
           // }

            if (buddyController != null )
            {
               // if (CharecterSwap.istogether == true) 
                {
                    buddyController.Follow(transform.position);
                }
                
               
            }

           // if (followOnContactController != null)
           // {
                // if (CharecterSwap.istogether == true) 
               // {
               //     followOnContactController.Follow(transform.position);
               // }


         //   }

            if (Math.Abs(input.x) == 1 || Math.Abs(input.y) == 1) 
           StartCoroutine(charecter.Move(input, OnMoveOver));


            
        }

            charecter.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Interact());
        }


        }

    IEnumerator Interact()
    {
        var facingDir = new Vector3(charecter.Animator.MoveX, charecter.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        //Debug.Log($"{charecter.Animator.MoveX} {charecter.Animator.MoveY}");

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, GameLayers.I.InteractableLayer);
       // Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);
        if (collider != null)
        {
            if (collider.gameObject.GetComponent<Torch>() != null)
            {
                collider.gameObject.SetActive(false);
               
                
                theanim.IsSHoldingTorch = true;
            }

            if (collider.gameObject.GetComponent<FreezeItem>() != null)
            {

                this.gameObject.AddComponent<FreezeItem>();
                freezeItem = collider.gameObject;
                freezeItem.transform.parent = this.transform;
                theanim.IsSHoldingFreezeItem = true;
                GetComponent<FreezeItem>().player = this;
                GetComponent<FreezeItem>().item = this.gameObject;
                Destroy(freezeItem);


            } else
            {
                theanim.IsSHoldingFreezeItem = false;

            }
            yield return collider.GetComponent<Interactable>()?.Interact(transform);
        }
     

        collider = Physics2D.OverlapCircle(interactPos, 0.2f, (GameLayers.I.SpawnObject | GameLayers.I.MoveableObjects));
        if (collider != null)
        {
            //IEnumerator co = collider.GetComponent<MovableObject>().Move(facingDir);
            if(collider.TryGetComponent<MovableObject>(out MovableObject co) )
            {
                yield return co.Move(facingDir);
            }
           
        }
        
    }

    IPlayerTriggerable currentlyInTrigger;

    private void OnMoveOver()
    {
      var colliders =  Physics2D.OverlapCircleAll(transform.position - new Vector3(0, charecter.OffsetY), 0.2f, GameLayers.I.TriggerableLayers);

        IPlayerTriggerable triggerable = null;
        foreach (var collider in colliders)
        {
             triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {

                if (triggerable == currentlyInTrigger && !triggerable.triggerRepeatedly)
                    break;


               triggerable.OnPlayerTriggered(this);
                currentlyInTrigger = triggerable;
                break;
            }
        }

        if (colliders.Count() == 0 || triggerable != currentlyInTrigger)
            currentlyInTrigger = null;
    }

    public object CaptureState()
    {
        var saveData = new PlayerSaveData()
        {
            pos = new float[] { transform.position.x, transform.position.y },
            creatures = creatureparty.Creatures.Select(p => p.GetSaveData()).ToList()
        };

        float[] pos = new float[] {transform.position.x, transform.position.y};
        return saveData;
    }

    public void RestoreState(object state)
    {
        var savedata = (PlayerSaveData)state;

        var pos = savedata.pos;
        transform.position = new Vector3(pos[0], pos[1]);

      creatureparty.Creatures = savedata.creatures.Select(s => new Creature(s)).ToList();
    }

    public void SetBuddy(BuddyController buddy)
    {
        buddyController = buddy;
     
    }

    public void OnSwitch(bool isSwitched)
    {
        playerActive = isSwitched;

       // controllerScript.enabled = false;
       // inventoryScript.enabled = false;
       // buddyScript.enabled= true;
    }

    public void IsSeperated()
    {
        // turn off buddy script
       buddyController.GetComponent<BuddyController>().enabled = false;

        swapText.text = "Stay";
    }

 

    public void IsTogether()
    {
        // turn on buddy script
        swapText.text = "Follow";
        buddyController.GetComponent<BuddyController>().enabled = true;
    }

    public Charecter Charecter => charecter;

    public string Name { get;  set; }

    public Transform thecurrentChar => this.transform;
}




[Serializable]
public class PlayerSaveData
{
    public float[] pos;
    public List<CreatureSaveData> creatures;
}
