using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float startingHealth = 100.0f;
    public float currentHealth;
    public Image damageImage;
    public Text killCount;
    public Text medKitCount;
    //public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public Slider healthSlider;
    public Image fillImage;
    public Color fullHealthColour = Color.green;
    public Color zeroHealthColour = Color.red;

    public Image mKRed;
    public Image mKGrey;

    public int startingMedkits = 2;
    public int currentMedkits;
    public float healCooldown = 5.0f;
    public float healTimer = 0.0f;

    GameObject player;
    PlayerActor playerActor;

    bool isDead;
    bool damaged;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerActor = player.GetComponent<PlayerActor>();
        currentHealth = startingHealth;
        currentMedkits = startingMedkits;
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        healTimer += Time.deltaTime;
        SetHealthUI();

        // Medkit Heal
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal();
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }	
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        killCount.text = (playerActor.killPoints).ToString();
        medKitCount.text = (currentMedkits).ToString();
    }

    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        //healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Heal()
    {
        // simple health regen; one kit give full heal
        if (currentMedkits > 0 && healTimer >= healCooldown)
        {
            currentMedkits--;
            healTimer = 0.0f;
            currentHealth = startingHealth;
            //healthSlider.value = currentHealth;
            //print("Health: " + currentHealth + "; Medkits: " + currentMedkits + ";");
        }
    }

    void Death()
    {
        isDead = true;
        GlobalVariables.score = playerActor.killPoints;
        SceneManager.LoadScene(2);
        //playerMovement.enabled = false;
    }
 
    void SetHealthUI()
    {
        // Set the slider's value appropriately.
        healthSlider.value = currentHealth;
        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(zeroHealthColour, fullHealthColour, currentHealth / startingHealth);

        if(healTimer < healCooldown && currentMedkits < startingHealth)
        {
            mKRed.enabled = false;
            mKGrey.enabled = true;
        }
        else
        {
            mKRed.enabled = true;
            mKGrey.enabled = false;
        }
    }
}
