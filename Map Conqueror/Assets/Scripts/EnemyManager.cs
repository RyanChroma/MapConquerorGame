using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager instance;
	public List<EnemyController> units = new List<EnemyController>();

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(instance);
		}

		instance = this;
	}

	public void addUnit(EnemyController unit) //FOR RTSCONTROLLER TO ADD ITSELF TO THE SCRIPT
	{
		units.Add(unit);
	}

	public void removeUnit(EnemyController unit) //FOR RTSCONTROLLER TO REMOVE ITSELF TO THE SCRIPT
	{
		units.Remove(unit);
	}
}
