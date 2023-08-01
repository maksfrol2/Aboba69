using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField] private Image[] healthBar;

    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        ChangeHealth();
    }

    public void TakeDamage(float amount)
    {
        //damage logic
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("dead");
            Dead();
        }


    }
    public void ChangeHealth()
    {
        //healthbar logic
        for (int i = 0; i < healthBar.Length; i++)
            if (i < health)
            {
                healthBar[i].color = Color.red;
            }
            else
            {
                healthBar[i].color = Color.black;
            }
    }

    public void Dead()
    {
        //reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("reset");
    }
}

