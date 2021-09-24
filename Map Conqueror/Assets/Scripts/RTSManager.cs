using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSManager : MonoBehaviour
{

    public static RTSManager instance;
    public List<RTSController> units = new List<RTSController>();

    public Rect box = new Rect();

    private void Awake()
	{
		if(instance != null)
		{
            Destroy(instance);
		}

        instance = this;
	}

    public void addUnit(RTSController unit) //FOR RTSCONTROLLER TO ADD ITSELF TO THE SCRIPT
	{
        units.Add(unit);
	}

    public void removeUnit(RTSController unit) //FOR RTSCONTROLLER TO REMOVE ITSELF TO THE SCRIPT
    {
        units.Remove(unit);
    }

    public RTSController getSelectedUnit(Vector3 pos)
	{
        foreach (RTSController item in units)
        {
            /*if (box.Contains(item.transform.position))
            {
                containUnit.Add(item);
            }*/

            if(item.GetComponent<Collider2D>().OverlapPoint(pos))
			{
                return item;
			}
        }

        return null;
    }

    //RETURN A LIST OF SELECTED UNITS ACCORDING TO START AND END POSITION OF MOUSE
    public List<RTSController>getSelectedUnit(Vector3 startPosition, Vector3 endPosition)
	{
        startPosition.z = 0;
        endPosition.z = 0;
        box.min = Vector2.Min(startPosition, endPosition);
        box.max = Vector2.Max(startPosition, endPosition);

        List<RTSController> containUnit = new List<RTSController>();
        
        foreach (RTSController item in units)
		{
            if(box.Contains(item.transform.position))
			{
                containUnit.Add(item);
			}
		}
        //Debug.Log(startPosition + "endPosition = " + endPosition);
        return containUnit;
    }

	public void deselectAllUnits() //ALL FORMS OF DESELECTING (LMB ON BACKGROUND, SELECTING 1 UNIT, ETC)
    {
        foreach (RTSController item in units)
        {
            item.SetSelected(false);
        }
    }
}
