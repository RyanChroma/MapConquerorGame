using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Shooter
{
    protected override void FindEnemy()
    {
        //Debug.Log("FindEnemy");
        int shortestIndex = -1;
        float shortestDistance = float.PositiveInfinity;
        List<RTSController> Units = RTSManager.instance.units;
        for (int i = 0; i < Units.Count; i++)
		{
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (Units[i].transform.position - this.transform.position).normalized);

            // If it hits something...
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Unit")
                {
                    if(shortestDistance > Vector3.Distance(Units[i].transform.position, this.transform.position))
					{
                        shortestDistance = Vector3.Distance(Units[i].transform.position, this.transform.position);
                        shortestIndex = i;
                    }
                }
            }
        }

        if(shortestIndex != -1)
		{
            //Debug.Log("FoundEnemy");
            enemy = Units[shortestIndex].GetComponent<Shooter>();
		}
    }

	protected override void Update()
	{
		base.Update();
        healthBarFill.fillAmount = health / maxHealth;
    }

	private void OnDestroy()
    {
        EnemyManager.instance.removeUnit(this);
    }

    protected override void Start()
    {
        base.Start();
        EnemyManager.instance.addUnit(this);
    }
}
