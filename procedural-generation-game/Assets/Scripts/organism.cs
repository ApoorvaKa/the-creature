using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organism : MonoBehaviour
{
    GameObject player;
    public float speed;
    private float idleSpeed;
    //bool inRange;
    public float range;
    private float distance;
    Vector2 target;
    private float timer = 100;
    LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        speed = speed + Random.Range(-3f, 3f);
        idleSpeed = speed / 10;
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target);
        if (distance < 1 || timer < 0)
        {
            target = new Vector2(transform.position.x + Random.Range(-3f, 3f), transform.position.y + Random.Range(-3f, 3f));
            timer = 1000;
        }
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < range)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, target, -1 * idleSpeed * Time.deltaTime);
            timer += -1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int number = (int) (speed * speed);
        if(collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<scoring>().Scored(10);
            //collision.gameObject.GetComponent<scoring>().Scored(number);
            Destroy(gameObject);

        }
    }
}
