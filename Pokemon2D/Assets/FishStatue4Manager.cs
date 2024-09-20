using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStatue4Manager : TRiggerableEvent
{
    public bool functionCalled = false;

    public List<GameObject> trigger;

    PlayerController thePlayer;

    Statue4Manager statue4man;

    public Fader fader;

    cutsceneManagerr cutscene;

    RebuiltStatues rebuiltStatus;

    [SerializeField] public GameObject triggerCutscene;


    public GameObject player;

    public GameObject buddy;

    public GameObject statue, emptystatue, pieces;


    // Start is called before the first frame update
    void Start()
    {

        statue4man = FindObjectOfType<Statue4Manager>();

        rebuiltStatus = FindObjectOfType<RebuiltStatues>();

        player = FindObjectOfType<PlayerController>().gameObject;

        buddy = FindObjectOfType<BuddyController>().gameObject;

        thePlayer = FindObjectOfType<PlayerController>();

        functionCalled = false;

        fader = FindObjectOfType<Fader>();

        statue = rebuiltStatus.transform.GetChild(3).gameObject;

        cutscene = FindObjectOfType<cutsceneManagerr>();

        triggerCutscene = cutscene.cutscenes[39].gameObject;

        emptystatue = statue4man.transform.GetChild(0).gameObject;

        pieces = statue4man.transform.GetChild(1).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.Count <= 0)

        {

            if (!functionCalled)
            {

                functionCalled = true;

                StartCoroutine(PlayCutscene());

            }
        }

    }


    public override IEnumerator PlayCutscene()
    {
        yield return fader.FadeIn(1f);

        yield return new WaitForSeconds(.2f);
        player.SetActive(false);
        buddy.SetActive(false);

        emptystatue.SetActive(false);
        pieces.SetActive(false);
        triggerCutscene.SetActive(true);

        yield return fader.FadeOut(2f);
        yield return new WaitForSeconds(5f);
        yield return fader.FadeIn(2f);

        triggerCutscene.SetActive(false);
        player.SetActive(true);
        buddy.SetActive(true);

        statue.SetActive(true);

        yield return fader.FadeOut(2f);

        this.gameObject.SetActive(false);
    }
}
