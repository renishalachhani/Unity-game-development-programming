using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth; //Salud máxima del tanque enemigo
    int currentHealth; //Salud actual del tanque enemigo   

    [SerializeField]
    Slider slider;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        currentHealth = maxHealth;
        slider.maxValue = maxHealth; //maxValue es una variable propia de la clase Slider
        slider.value = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ShellEnemy"))
        {
            TakeDamage(collision.collider.GetComponent<Shell>().damagePlayer);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount; //currentHealth al inicio tiene 100 por cada impacto le quito 20
        //currentHealth = currentHealth - amount;
        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Death();
        }
    }   

    void Death()
    {
        gameManager.GameOver();
    }
}
