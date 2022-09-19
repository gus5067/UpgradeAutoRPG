using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntKing : Grunt
{

    public void Heal()
    {
        StartCoroutine(HealRoutine());
    }
    IEnumerator HealRoutine()
    {
        for(float i = 0.0f; i < 4f; i += 0.01f)
        {
            this.hpController.hp += 0.25f;
            this.hpController.CheckHpChange();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
