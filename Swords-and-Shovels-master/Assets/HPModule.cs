using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPModule : MonoBehaviour
{
    public int hp = 50;
    Animator anim;
    NPCController npcCon;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        npcCon = GetComponent<NPCController>();
    }

    public void Damage(int _damage)
    {
        StartCoroutine(Hitting(_damage));
    }

    IEnumerator Hitting(int _damage)
    {
        hp -= _damage;
        if (hp < 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            npcCon?.StopCoroutine(npcCon?.updateCoroutine);

            anim.Play("Hit");
            anim.Update(0);

            yield return new WaitUntil(() => !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"));

            npcCon.updateCoroutine = npcCon?.StartCoroutine(npcCon?.Tick());

        }
    }
}
