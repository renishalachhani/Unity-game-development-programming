using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Manager")]
    [SerializeField]
    Transform[] positions;
    [SerializeField]
    GameObject tankEnemyPrefab;
    private float time = 12;

    [Header("UI GameOver")]
    [SerializeField]
    GameObject panelGameOver;

    [Header("UI Victory")]
    [SerializeField]
    GameObject panelVictory;
    [SerializeField]
    TextMeshProUGUI textUI;
    [SerializeField]
    int numTanksVictory; //Establece cuantos tanques tenemos que eliminar para ganar
    private int numTankEnemy; //Contabilizar el número de tanques eliminados

    private bool gameOver,
                 victory;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateTankEnemy", time, time);
    }

    void CreateTankEnemy()
    {
        if(gameOver == true || victory == true)
        {
            return;
        }

        int n = Random.Range(0, positions.Length);//Coloca los tanques en posiciones aleatorias del array
        Instantiate(tankEnemyPrefab, positions[n].position, positions[n].rotation);
    }

    public void AddEnemyUI()
    {
        numTankEnemy++;
        int remainingTanks = numTanksVictory - numTankEnemy;

        textUI.text = "Tanks destroyed: " + numTankEnemy + "\nRemaining Tanks: " + remainingTanks;

        if(numTankEnemy == numTanksVictory)
        {
            Victory();
        }
    }

    void Victory()
    {
        victory = true;
        textUI.enabled = false;
        panelVictory.SetActive(true);
    }

    public void GameOver()
    {
        textUI.enabled = false;
        gameOver = true;
        panelGameOver.SetActive(true);
    }
}
