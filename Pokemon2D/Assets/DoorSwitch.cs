using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField]
    public GameObject switchUp;
    [SerializeField]
     public GameObject switchDown;

    public bool isPressed = false;

    public bool ispresuresensative = false;

    public bool isBlock;

    public bool isPlayer;

    private DoorSetActive theDoor;


    [HideInInspector]
    public DoorSwitch instance;

    private void Awake()
    {
        instance= this;
    }
    private void Update()
    {
        
    }
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchUp.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
          
            if ((other.tag == "Player" && isPlayer == true))
            {
              gameObject.GetComponent<SpriteRenderer>().sprite = switchDown.GetComponent<SpriteRenderer>().sprite;
              isPressed = true;
            }
       
        
          else if (isBlock == true)
            {

                gameObject.GetComponent<SpriteRenderer>().sprite = switchDown.GetComponent<SpriteRenderer>().sprite;
                isPressed = true;

            } 


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (ispresuresensative)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchUp.GetComponent<SpriteRenderer>().sprite;

            isPressed = false;
        }
    }
}
