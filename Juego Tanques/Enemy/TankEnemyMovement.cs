using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemyMovement : MonoBehaviour
{
    //El GameObject que vamos a acosar
    private GameObject player;
    //El acosador
    NavMeshAgent agent;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Destiny();
    }

    void Destiny()
    {
        if (player == null)
        {
            return;
        }

        agent.SetDestination(player.transform.position);
    }
}
