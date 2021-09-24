using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float shootCooldown = 0.5f;
	protected Shooter enemy;
	private float currentCooldown = 0;
	public bool immortal = false;
	[SerializeField] protected float health;
	[SerializeField] protected float maxHealth;
	[SerializeField] protected float damage;
	[SerializeField] protected GameObject healthBar;
	[SerializeField] protected Image healthBarFill;
	[SerializeField] private GameObject bulletObj;
	[SerializeField] private Color bulletColor;
	[SerializeField] private GameObject gunSFX;

	protected virtual void Start()
	{
		health = maxHealth;
	}

	protected void ShootEnemy() //protected is that we choose this code to be visible only by inherited classes.
	{
		//Instantiate(bullet, this.transform.position, Quaternion.identity); //Spawn bullet from player.
		if (enemy == null) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (enemy.transform.position - this.transform.position).normalized);

        // If it hits something...
        if (hit.collider != null)
        {
			if(hit.collider.tag == enemy.tag)
			{
				//Debug.Log(hit.collider.name);
				currentCooldown = shootCooldown;
				hit.collider.GetComponent<Shooter>().Hit(damage);
				Instantiate(bulletObj).GetComponent<Bullet>().Setup(bulletColor, this.transform.position, hit.collider.transform.position);
				GameObject sfx = Instantiate(gunSFX);
				Destroy(sfx, sfx.GetComponent<AudioSource>().clip.length);
			}

			else
			{
				enemy = null; //null means empty. This line will reset the unit's targets if lost sight.
			}
        }
    }

	protected virtual void Update()
	{
		if(currentCooldown > 0)
		{
			currentCooldown -= Time.deltaTime;
		}

		if (enemy == null)
		{
			FindEnemy();
		}

		if (currentCooldown <= 0)
		{
			if (enemy == null) return;
			ShootEnemy();
		}
	}

	public void Hit(float damage)
	{
		if(immortal == false)
		{
			health -= damage;
		}

		if(health <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	protected virtual void FindEnemy()
	{
		Debug.Log("ShooterFindEnemy");
	}
}
