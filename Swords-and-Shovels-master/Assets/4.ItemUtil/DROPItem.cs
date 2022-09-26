using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DROPItem : MonoBehaviour
{
    public string itemCode;
    bool selectable = false;

    //아이템 코드에 따른 드롭 효과
    public void Set(string _itemCode)
    {
        itemCode = _itemCode;
        //스프라이트 바꾸고 효과 바꾸고
        selectable = false;

    }

    public void Drop(Vector3 dropPos)
    {
        //dropPosition에 드롭되는 이미지가 출력
    }

    IEnumerator Follow()
    {
        //포물선을 따라간다.
        selectable = true;
        yield break;
    }

    private void Update()
    {
        if (selectable)
        {
            //유저가 주변에 있으면 획득 후 selectable = false;
        }
    }

}
