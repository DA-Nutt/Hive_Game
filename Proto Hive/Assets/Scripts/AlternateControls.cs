using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateControls : MonoBehaviour
{

    #region Variables
    //A filter to ensure the player can only select Territory tiles
    public LayerMask navMask;
    public InteractableObject focusObject;
    public HiveManager hiveManager;
    public Camera cam;
    public Territory highlightedTerritory;
    public Territory targetedTeritory;
    public ControllerState controllerState;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        controllerState = ControllerState.territorySelection;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player left clicks...
        if (Input.GetMouseButtonDown(0))
        {
            //...Shoot out a ray from the mouse
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //Store data about the object we hit
            RaycastHit hit;

            //If our ray hits something on our mask...
            if (Physics.Raycast(ray, out hit, 100, navMask))
            {
                if (hit.collider.CompareTag("ClickableObject"))
                {
                    //Grab a reference to the object we just clicked on
                    InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

                    //Set that selected tile as our focus
                    SetFocus(interactableObject);
                }

            }
            else //If the ray does not hit an interactable object...
            {
                RemoveFocus();
                controllerState = ControllerState.territorySelection;
            }
        }
    }

    #region Controller Methods
    private void SetFocus(InteractableObject newFocus)
    {
        focusObject = newFocus;
        highlightedTerritory = newFocus.gameObject.GetComponent<Territory>();
        highlightedTerritory.SelectTerritory();
        Debug.Log("Setting Focus To " + newFocus.name);
    }

    private void RemoveFocus()
    {
        if (focusObject != null)
        {
            Debug.Log("Defocusing On " + focusObject.name);
            focusObject = null;
        }
    }
    #endregion
}
