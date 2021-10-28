using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player
    public GameObject player;
    public Rigidbody rb;

    //Speed
    float Speed = 25f;
    float PlayerMotion = 25f;

    //Jump
    int jumpTicks = 0;
    int maxJump = 15;

    //Shield
    public GameObject shieldPrefab;
    GameObject shield;
    bool hasShield = false;

    //Reset List
    List<GameObject> moved = new List<GameObject>();

    //Delay
    float delay = 1.5f;

    private void Update()
    {


        if (Input.GetButtonUp("Restart"))
        {
            restart(0);
            Debug.Log("Restart");
        }

        if (player.transform.position.y < -5)
        {
           restart(delay);
           Debug.Log("Fell Off");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(-Speed, 0, 0 * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            rb.AddForce(0, 0, PlayerMotion * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(0, 0, -PlayerMotion * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (jumpTicks > 0)
        {
            rb.AddForce(0, PlayerMotion * Time.deltaTime, 0, ForceMode.VelocityChange);
            jumpTicks--;
            Debug.Log(jumpTicks);
        }


        //Clamp
        if (rb.velocity.x < -50) //Forward Movement
        {
            rb.velocity = new Vector3(-50, rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.z < -25) //Left Movement
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -25);
        }

        if (rb.velocity.z > 25) //Right Movement
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 25);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.gameObject.CompareTag("ShieldPickUp"))
        {
            //Move PickUp Below Level
            Vector3 temp = collisionInfo.gameObject.transform.position;
            collisionInfo.gameObject.transform.position = new Vector3(temp.x, temp.y - 5, temp.z);

            moved.Add(collisionInfo.gameObject);

            //Add Shield
            temp = player.transform.position;
            shield = Instantiate(shieldPrefab, new Vector3(temp.x, 0, temp.z), Quaternion.identity);
            shield.transform.parent = player.transform;

            hasShield = true;
            Debug.Log("Got Shield");
        }

        if (collisionInfo.collider.tag == "Obstical")
        {
            if (hasShield) //If Player Has Shiled they keep going
            {

                Debug.Log("Protected");

                //Deal with Shield
                Destroy(shield);
                hasShield = false;
            }
            else
            { //else Fail
                enabled = false;
                Debug.Log("Dead");

                restart(delay);
            }
        }

        if (collisionInfo.gameObject.CompareTag("JumpPad"))
        {
            jumpTicks = maxJump;
            Debug.Log("Jump");
        }
    }

    void restart(float restartDelay)
    {
        FindObjectOfType<GameManager>().EndGame(restartDelay);
    }
}