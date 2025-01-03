using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharecterAnimator;

[RequireComponent(typeof(CharecterAnimator))]

[RequireComponent(typeof(Charecter))]

public class TrainerController : MonoBehaviour, Interactable, ISavable
{
    [SerializeField] GameObject exclimation;
    [SerializeField]  protected GameObject fov;
    [SerializeField] Dialogue dialogue;
    [SerializeField] Dialogue dialogueAfterBattle;

    public Party creatureParty { get; private set; }

    Charecter charecter;

    protected bool battleLost = false;

    private void Awake()
    {
        creatureParty = GetComponent<Party>();
        charecter = GetComponent<Charecter>();
    }

    private void Start()
    {
        SetFovRotation(charecter.Animator.DefultDirection);
    }

    public IEnumerator Interact(Transform initer)
    {
        charecter.LookTowards(initer.position);

        if (!battleLost)
        {
            yield return DialogueManager.Instance.ShowDialogue(dialogue);
            GameController.Instance.StartTrainerBattle(this);

        }
        else
        {
            yield return DialogueManager.Instance.ShowDialogue(dialogueAfterBattle);
        }
    }

    public virtual void BattleLost()
    {
        battleLost = true;
        fov.gameObject.SetActive(false);
    }
    public IEnumerator TriggerTrainerBattles(PlayerController player)
    {
        // show exclamation
        exclimation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        exclimation.SetActive(false);

        //walk twords player
        var diff = player.transform.position - transform.position;
        var movevec = diff - diff.normalized;
        movevec = new Vector2(Mathf.Round(movevec.x), Mathf.Round(movevec.y));

        yield return charecter.Move(movevec);

        yield return DialogueManager.Instance.ShowDialogue(dialogue);
        GameController.Instance.StartTrainerBattle(this);
    }
        
    public void SetFovRotation(FacingDirection dir)
    {
        float angle = 0f;
        if (dir == FacingDirection.Right)
            angle = 90f;
       else if (dir == FacingDirection.Up)
            angle = 180f;
        else if (dir == FacingDirection.Left)
            angle = 270f;

        fov.transform.eulerAngles = new Vector3(0, 0, angle);

    }

    public object CaptureState()
    {
        return battleLost;
    }

    public void RestoreState(object state)
    {
        battleLost = (bool)state;
        if (battleLost)
            fov.gameObject.SetActive(false);
    }
}
