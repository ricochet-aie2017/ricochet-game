using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public Text killCount;
    //public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    GameObject player;
    PlayerActor playerActor;

    bool isDead;
    bool damaged;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerActor = player.GetComponent<PlayerActor>();
        currentHealth = startingHealth;
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        killCount.text = (playerActor.killPoints).ToString();

	    if(damaged)
        {
            damageImage.color = flashColour;
        }	
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead)
        {
            //Death();
        }
    }

    void Death()
    {
        isDead = true;

        //playerMovement.enabled = false;
    }
}
