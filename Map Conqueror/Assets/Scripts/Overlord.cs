using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 mouseDownPosition;
    public Transform ToggleSelection;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //LEFT CLICK TO TOGGLE UNIT
        {
            RTSManager.instance.deselectAllUnits();

            mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (selectedObject != null)
            {
                RTSController unit = selectedObject.GetComponent<RTSController>();
                if (unit != null)
                {
                    unit.SetSelected(false);
                }
            }
            //RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.right, 1);
            //Debug.DrawRay(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.right, Color.white, 2, false);

            //RaycastHit2D hit = Physics2D.CircleCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f, Vector2.zero, 10);

            RTSController selectedUnit = RTSManager.instance.getSelectedUnit(mouseDownPosition);

            if(selectedUnit != null)
			{
                selectedUnit.SetSelected(true);
			}

            /*if (hit.collider != null)
            {
                Debug.Log("Hit");
                selectedObject = hit.collider.gameObject;
                RTSController unit = selectedObject.GetComponent<RTSController>();
                if (unit != null)
                {
                    unit.SetSelected(true);
                }
            }
            else
			{
                selectedObject = null;
			}*/
        }

        if(Input.GetMouseButton(0)) //&& Vector3.Distance(mouseDownPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 10) //IF HOLDING LMB, CHECK WHICH UNIT IS BEING SELECTED
		{
            if(Vector3.Distance(mouseDownPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.5f)
			{
                List<RTSController> selectedUnit = RTSManager.instance.getSelectedUnit(mouseDownPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                foreach (RTSController unit in selectedUnit)
                {
                    unit.SetSelected(true);
                }
            }
		}

        //SHOW RECTANGLE SELECTION IN GAME
        ToggleSelection.position = RTSManager.instance.box.center;
        ToggleSelection.localScale = RTSManager.instance.box.size;

        if(Input.GetMouseButtonUp(0)) //WHEN YOU ARE DONE HIGHLIGHTING THE UNITS, THE RECTANGLE WILL DISAPPEAR
		{
            RTSManager.instance.box.size = Vector2.zero;
		}
    }
}
