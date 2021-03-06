﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    //where the projectile comes from
    public GameObject projectile;
	public string article;

	public SpriteRenderer spriteRenderer;

	public ShootingController shootingController;
	public Color activeColor;
	private Color defaultColor;
	// Use this for initialization
	void Start () {
		if (!spriteRenderer) {
			spriteRenderer = GetComponent<SpriteRenderer> ();
			defaultColor = spriteRenderer.color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //modify later - to button press
    private void OnMouseDown()
    {
		if (shootingController.isRecharging ()) {
			return;
		}
		spriteRenderer.color = activeColor;
		shootingController.shoot ();
		Transform cannonTip = shootingController.getNextTip ();

		//instantiate projectile
        GameObject tempProjectile;
        tempProjectile = Instantiate(projectile, cannonTip.transform.position, cannonTip.transform.rotation) as GameObject;
        //assign a tab to the projectile - a word that we want to match with enemy
		tempProjectile.name = article;

        //target position
		Vector2 position;
		if (shootingController.target) {
			position = shootingController.target.transform.position - cannonTip.transform.position;
		} else {
			position = gameObject.transform.forward;
		}
        //control the rigidbody component of projectile to shoot it
        Rigidbody2D tempRigidbody;
        tempRigidbody = tempProjectile.GetComponent<Rigidbody2D>();

        //shoot the projectile
        tempRigidbody.velocity = position * 10;


        //after 2 second projectile is automatically destroyed if not used
        Destroy(tempProjectile, 2.0f);
        
    }

	private void OnMouseUp() {
		spriteRenderer.color = defaultColor;
	}
}
