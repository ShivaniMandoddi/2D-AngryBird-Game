using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite deadMonster;
    private string scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
       
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
       GameObject player = GameObject.FindWithTag("Player");
        BirdController birdController = collision.gameObject.GetComponent<BirdController>();
        
        if(birdController!=null || collision.gameObject.tag == "Crate")
        {
            //Destroy(gameObject);
            if (gameObject.GetComponent<SpriteRenderer>().sprite != deadMonster)
                MonsterDeath();
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == deadMonster)
                //player.SendMessage("MonsterDead");
                print("Trying to kill dead Monster");

        }
    }

    private void MonsterDeath()
    {
        // gameObject.SetActive(false);\
        GameObject player = GameObject.FindWithTag("Player");
        gameObject.GetComponent<SpriteRenderer>().sprite = deadMonster;
        //if(scene=="Level2") // Condition to update the score of Bird
        player.SendMessage("Score");
        
    }
    //level1: more monsters and crates . Check for preventing multiple deaths 
    //level2: more monsters and crates. When the collision happens, Print the score
    //create different scenes
    // I want you to create 
}
