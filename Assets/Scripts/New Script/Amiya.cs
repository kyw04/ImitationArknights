using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amiya : Operator
{
    public float skillDelay;
    public float buffDuration;

    private float lastSkillTime;
    private float baseAttackSpeed;

    protected override void Update()
    {
        base.Update();

        if (skillDelay + lastSkillTime <= Time.time)
        {
            Skill();
        }
    }

    private void Skill()
    {
        StartCoroutine("Buff");
    }

    private IEnumerator Buff()
    {
        baseAttackSpeed = stats.attackSpeed;
        stats.attackSpeed = baseAttackSpeed * 0.5f;
        yield return new WaitForSeconds(buffDuration);
        stats.attackSpeed = baseAttackSpeed;
    }
}
