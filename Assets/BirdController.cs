using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    Vector2 birdStartPosition;
    // public string[] speed = { "Slow", "Medium", "Fast","VeryFast" };
    public enum Speed
    {
       Slow,Medium,Fast,VeryFast
    }
    public Speed speed;
    public float force;
    public int score;
    public float maximumDragDistance;
    public int collisionCount;
    private int countOfMonsters;
    private void Awake()
    {
        
        rb=GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        speed = Speed.Medium;
        rb.isKinematic = true;
        birdStartPosition = rb.position;
        GameObject[] Monster = GameObject.FindGameObjectsWithTag("Monster");
        countOfMonsters = Monster.Length;

    }

    // Update is called once per frame
    void Update()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        string sceneName = SceneManager.GetActiveScene().name;
     
        if ((Input.GetKeyDown(KeyCode.Space)) && sceneName!="Level2") // Condition to move next level( || countOfMonsters == score)
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
        if(Input.GetKeyDown(KeyCode.Backspace)) // Condition to move previous level
        {
            SceneManager.LoadScene(sceneIndex -1);
        }
        
       
       
    }
   
    public void Score()
    {
        score = score + 1;
        if(SceneManager.GetActiveScene().name=="Level2")
            print("Score" +score);
    }
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 currentPosition = rb.position;
        Vector2 direction = birdStartPosition - currentPosition;
       
        direction.Normalize();
        
        rb.isKinematic = false;
        switch (speed)
        {
            case Speed.Slow:
                force = Random.Range(100f, 500f);
                break;
            case Speed.Medium:
                force = Random.Range(500f, 700f);
                break;
            case Speed.Fast:
                force = Random.Range(700f, 1500f);
                break;
            case Speed.VeryFast:
                force = Random.Range(1000f, 1800f);
                break;
            default:
                break;
        }
        //print("Force: "+force);
        rb.AddForce(direction * force);
    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition); //Mouse Position converted to WorldPoint
        Vector2 desiredPosition = mousePosition;
        if(desiredPosition.x>birdStartPosition.x)
        {
            desiredPosition.x = birdStartPosition.x;
        }
        float distance =Vector2.Distance(desiredPosition,birdStartPosition);
        if(distance>maximumDragDistance)
        {
            Vector2 direction = desiredPosition - birdStartPosition;
            direction.Normalize();
            desiredPosition = birdStartPosition+(direction*maximumDragDistance);
        }
        rb.position = desiredPosition;
        //transform.position = new Vector3(mousePosition.x, mousePosition.y,transform.position.z);     

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Crate")
            collisionCount++;
        StartCoroutine(ResetAfterDelay());
    }
    IEnumerator ResetAfterDelay()// It is a coroutine function
    {
        yield return new WaitForSeconds(3f);
        //Debug.Log("This is a Coroutine Function");
        rb.position = birdStartPosition;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }
   
    
}
//Put force randomly
//Design the Enivornment

