using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        //When Game starts health will be at max
        health = maxHealth;
    }

    //function called to take damage and "amount" is how much damage is taken
    public void TakeDamage(int amount)
    {
        health -= amount;
        // if damage takes player to zero then player will be destroyed
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
