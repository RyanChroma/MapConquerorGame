using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RTSController : Shooter
{
    public float speed = 10f;
    Vector2 lastClickedPos;
    bool moving;
    private bool isSelected = false;
    private Rigidbody2D rid2D; //This is GetComponent<Rigidbody2d>()
    [SerializeField] private GameObject selecter;
    
    private void OnDestroy()
	{
        RTSManager.instance.removeUnit(this);
	}

	protected override void Start()
	{
        base.Start();
        RTSManager.instance.addUnit(this);
        rid2D = this.GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            immortal = true;
        }

        base.Update();
        setDestination();
        Move();
        healthBarFill.fillAmount = health / maxHealth;
    }

    private void setDestination() //SET TARGET'S DESTINATION (FOR UNIT TO KNOW WHERE TO GO TO)
	{
        if (isSelected == false)
        {
            return; //return means stop all of this function.
        }

        if (Input.GetMouseButtonDown(1))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
    }

    private void Move() //MOVE TO TARGET (MOVING MECHANIC)
	{
        if (moving && Vector2.Distance((Vector2)transform.position, lastClickedPos) >= 0.1f)
        {
            rid2D.velocity = (lastClickedPos - (Vector2)transform.position).normalized * speed;
            //Debug.Log(lastClickedPos.normalized);
            /*float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);*/
        }
        else
        {
            moving = false;
            rid2D.velocity = Vector2.zero;
        }
    }

    public void SetSelected(bool isSelect) //SetActive selecter on isSelected. Turn on/off selecter
	{
        isSelected = isSelect;
        selecter.SetActive(isSelect);
        healthBar.SetActive(isSelected || health < maxHealth); //Will show HP when less than max. (ALWAYS)
	}

    protected override void FindEnemy()
    {
        int shortestIndex = -1;
        float shortestDistance = float.PositiveInfinity;
        List<EnemyController> Units = EnemyManager.instance.units;
        for (int i = 0; i < Units.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (Units[i].transform.position - this.transform.position).normalized);

            // If it hits something...
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Enemy")
                {
                    if (shortestDistance > Vector3.Distance(Units[i].transform.position, this.transform.position))
                    {
                        shortestDistance = Vector3.Distance(Units[i].transform.position, this.transform.position);
                        shortestIndex = i;
                    }
                }
            }
        }

        if (shortestIndex != -1)
        {
            enemy = Units[shortestIndex].GetComponent<Shooter>();
        }
    }
}