using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFader : MonoBehaviour
{
    Image image;

    public static WhiteFader i;

    private void Awake()
    {
        i = GetComponent<WhiteFader>();
        image = GetComponent<Image>();
    }

    public IEnumerator FadeIn(float time)
    {
        yield return image.DOFade(1f, time).WaitForCompletion();
    }

    public IEnumerator FadeOut(float time)
    {
        yield return image.DOFade(0f, time).WaitForCompletion();
    }
}
