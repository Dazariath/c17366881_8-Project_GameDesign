using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    [SerializeField] private string loadLevel;          //has which level is going to be loaded

    private void OnTriggerEnter(Collider other)         //if something with the Player tag enter the trigger then it ends the level
    {
        if (other.CompareTag("Player"))
        {            
                SceneManager.LoadScene(loadLevel);            
        }
    }
}
