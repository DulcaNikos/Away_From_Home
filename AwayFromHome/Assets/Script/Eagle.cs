using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    //Pause
    bool isPaused = false;
    //Reference to waypoints
    public List<Transform> points;
    //The int value for the next point index
    int nextpoint = 0;
    //The value of that applies to ID for changing
    int pointChangeValue = 1;
    //Speed of movement or lying
    public float speed = 2;

    //Facing Player
    [SerializeField] GameObject player;
    bool flip;

    void Update()
    {
        //Call the movetonextpoint
        MoveToNextPoint();

        //Facing Player
        FacingPlayer();
    }

    void FacingPlayer()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }

    void MoveToNextPoint()
    {
        //Transform the next point 
        Transform nextPoint = points[nextpoint];
        //Flip the enemy transform to look into the point's direction
        if (nextPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Move the enemy towards the goal point
        transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);
        //Check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, nextPoint.position) < 0.2f)
        {
            //Check if we are at the end of the line (make the change -1)
            if (nextpoint == points.Count - 1)
            {
                pointChangeValue = -1;
            }
            //Check if we are at the start of the line (make the change +1)
            if (nextpoint == 0)
            {
                pointChangeValue = 1;
            }
            //Apply the change of the nextpoint
            nextpoint += pointChangeValue;
        }
    }

    public void HandlePause()
    {
        if (isPaused)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
}
