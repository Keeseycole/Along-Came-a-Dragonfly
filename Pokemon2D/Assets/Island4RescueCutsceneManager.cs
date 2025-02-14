using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island4RescueCutsceneManager : TRiggerableEvent
{
    public bool functionCalled = false;

    public Fader fader;

    [SerializeField] public GameObject island4RescueCutscene;

    public List<GameObject> enemies;

    PlayerController thePlayer;
    BuddyController theBuddy;


    cutsceneManagerr cutscenes;

    public GameObject npc1, npc2;

    public GameObject waterKey;



    // Start is called before the first frame update
    void Start()
    {
        cutscenes = FindObjectOfType<cutsceneManagerr>();

        island4RescueCutscene = cutscenes.cutscenes[76].gameObject;


        thePlayer = FindObjectOfType<PlayerController>();
        theBuddy = FindObjectOfType<BuddyController>();
        functionCalled = false;
        fader = FindObjectOfType<Fader>();

    }
    // Update is called once per frame
    void Update()
    {
        if (enemies.Count <= 0)
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

        thePlayer.gameObject.SetActive(false);
        theBuddy.gameObject.SetActive(false);

        island4RescueCutscene.SetActive(true);
        npc1.SetActive(false);
        yield return fader.FadeOut(4f);

        yield return new WaitForSeconds(10f);

        yield return fader.FadeIn(2f);
        island4RescueCutscene.SetActive(false);
        thePlayer.gameObject.SetActive(true);
        theBuddy.gameObject.SetActive(true);
        waterKey.SetActive(true);
        npc2.SetActive(true);

        yield return fader.FadeOut(2f);


        this.gameObject.SetActive(false);
    }
}
