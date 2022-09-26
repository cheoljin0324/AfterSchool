using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listfind : MonoBehaviour
{
    List<GameObject> listoOBJ = new List<GameObject>();
    private void Start()
    {
        List<GameObject> a = listoOBJ.FindAll(x => x.name == "blahblah");
    }
}
