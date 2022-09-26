using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_opendoor : MonoBehaviour
{
    public GameObject[] ncps;
    public Animator Door;
    bool firsttime = true;

    // Update is called once per frame
    void Update()
    {
        if (!firsttime)
            return;

        for(int i = 0; i<ncps.Length; i++)
        {
            if (ncps[i].activeSelf)
            {
                return;
            }
        }
        OpenDoor();
        firsttime = false;
    }
    void OpenDoor()
    {
        Door.Play("Open");
    }
}
