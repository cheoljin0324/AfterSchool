using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPModule : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp = 50;
    MainModule mainModule;

    private void Start()
    {
        mainModule = GetComponent<MainModule>();
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

            mainModule.Damage();
        
        
        }
        yield break;


    }
}
