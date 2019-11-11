using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerEngine))]
public class PlayerController : MonoBehaviour
{
    public Interaction focus;

    Camera cam;
    public LayerMask movementMask;
    PlayerEngine engine;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        engine = GetComponent<PlayerEngine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                engine.moveToPoint(hit.point);
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
              Interaction interaction = hit.collider.GetComponent<Interaction>();

                if (interaction != null)
                {
                    setFocus(interaction);
                }
            }
        }
    }

    void setFocus (Interaction newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
                focus = newFocus;

            focus.OnDefocused();
            engine.followTarget(newFocus);
        }

       
        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();

        focus = null;
        engine.stopFollowingTarget();
    }
}
