using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPModule : MonoBehaviour
{
    [SerializeField]
    Image hpbar;
    // Start is called before the first frame update
    private int firsthp;
    public int hp = 50;
    MainModule mainModule;

    private void Start()
    {
        mainModule = GetComponent<MainModule>();
        firsthp = hp;
    }


    public void Damage(int _damage)
    {

        if (mainModule.enabled)
            StartCoroutine(Hitting(_damage));
            hpbar.fillAmount = (float)hp / firsthp;

    }
    IEnumerator Hitting(int _damage)
    {


        hp -= _damage;
        if (hp < 1)
        {
            gameObject.SetActive(false);
            ItemSpawnManager.Instance.ItemSpawn("Void", gameObject.transform);
        }

        else
        {

            mainModule.Damage();
        
        
        }
        yield break;


    }
}
