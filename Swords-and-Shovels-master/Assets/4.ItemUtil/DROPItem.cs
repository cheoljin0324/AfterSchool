using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DROPItem : MonoBehaviour
{
    public string itemCode;
    bool selectable = false;

    //������ �ڵ忡 ���� ��� ȿ��
    public void Set(string _itemCode)
    {
        itemCode = _itemCode;
        //��������Ʈ �ٲٰ� ȿ�� �ٲٰ�
        selectable = false;

    }

    public void Drop(Vector3 dropPos)
    {
        //dropPosition�� ��ӵǴ� �̹����� ���
    }

    IEnumerator Follow()
    {
        //�������� ���󰣴�.
        selectable = true;
        yield break;
    }

    private void Update()
    {
        if (selectable)
        {
            //������ �ֺ��� ������ ȹ�� �� selectable = false;
        }
    }

}
