using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharNavigation : MonoBehaviour
{
    public Transform goal;
    private Canvas loading_scene;
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        loading_scene = GameObject.FindGameObjectWithTag("loading_scene").GetComponent<Canvas>();
    }
    private void Update()
    {
        if (Vector3.Magnitude(gameObject.transform.position - goal.position) < 10) {
            endGame(gameObject.tag);
        }
    }
    public void endGame(string tag) {
        loading_scene.enabled = true;
        Text text = loading_scene.GetComponentInChildren<Text>();
        if (tag == "yankee")
        {
            Debug.Log("You Win");
            text.text = "You Win";
        }
        else{
            if (tag == "Enemy")
            {
                text.text = "You Lose";
                Debug.Log("You lose");
                
            }
            else {
                Debug.Log("Error in end game");
            }
        }
    }
}

