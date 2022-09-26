using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer;

    public Texture2D pointer;
    public Texture2D target;
    public Texture2D doorway;

    public EventVector3 OnClickEnvironment;
    public EventVector3 OnClickEnvironment2;


    private bool useDefaultCursor = false;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        useDefaultCursor = currentState == GameManager.GameState.PAUSED;
    }

    void Update()
    {
        if (useDefaultCursor)
        {
            Cursor.SetCursor(pointer, new Vector2(16, 16), CursorMode.Auto);
            return;
        }


        // Raycast into scene
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                OnClickEnvironment2.Invoke(hit.point);
            }
            else
            {
                Vector3 originCameraPosition = Camera.main.transform.position;
                Vector3 dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                Vector3 clickingpos = Camera.main.ScreenPointToRay(Input.mousePosition).origin + dir / dir.y * (0-originCameraPosition.y);

                

                OnClickEnvironment2.Invoke(clickingpos);
                    
            }
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50))
        {
            bool door = false;
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            // If environment surface is clicked, invoke callbacks.
            if (Input.GetMouseButtonDown(1))
            {
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorway.position + doorway.forward * 10);
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
