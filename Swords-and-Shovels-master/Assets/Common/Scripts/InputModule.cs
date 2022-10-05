using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{

    MainModule mainModule;

    // Start is called before the first frame update
    void Start()
    {
        mainModule = GetComponent<MainModule>();


    }

    // Update is called once per frame
    void Update()
    {

        if (!mainModule)
            return;



        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50))
            {

                LeftClick(hit.point);



            }
            else
            {
                Vector3 originCameraPosition = Camera.main.transform.position;
                Vector3 dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                Vector3 clickingPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin + dir / dir.y * (0 - originCameraPosition.y);

                LeftClick(clickingPosition);

            }







        }


        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            bool door = false;
            if (hit.collider.gameObject.tag == "Doorway")
            {
                door = true;
            }

            // If environment surface is clicked, invoke callbacks.
            if (Input.GetMouseButton(1))
            {
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    RightClick(doorway.position + doorway.forward * 10);
                }
                else
                {
                    RightClick(hit.point);
                }
            }





        }
    }


    public void RightClick(Vector3 destination)
    {
        mainModule.MoveTo(destination);
    }
    public void LeftClick(Vector3 target)
    {

        mainModule.TryAttack(target);




    }


}
