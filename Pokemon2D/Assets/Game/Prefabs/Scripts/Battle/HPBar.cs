using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;


    public bool IsUpdating { get; internal set; }

    public void SetHP(float hpnormalized)
    {
        health.transform.localScale = new Vector3(hpnormalized, .45f);
    }

    public IEnumerator SetHPSmooth(float newHP)
    {
        IsUpdating = true;
        float curHP = health.transform.localScale.x;
        float changeAmt = curHP - newHP;

        while (curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changeAmt * Time.deltaTime;
            health.transform.localScale = new Vector3(curHP, .45f);
            yield return null;
        }

        health.transform.localScale = new Vector3(newHP, .45f);

        IsUpdating = false;
    }
}
