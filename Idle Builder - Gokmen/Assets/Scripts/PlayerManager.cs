using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
SEARCH,
PICK,
BUILD
}
public class PlayerManager : MonoBehaviour
{
    Animator workerAnimator;
    [SerializeField] public State currentState;
    [SerializeField] private Transform rockCarryPosition;
    public Transform targetRock;
    public GameObject building;
    public float speed = 2f;
    [SerializeField] private float stopDistance = 1.5f;

    void Start()
    {
        workerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.SEARCH:
                Search();
                break;
            case State.PICK:
                Pick();
                break;
            case State.BUILD:
                Invoke("Build", 1f);
                break;
        }
    }

    void OnCollisionEnter(Collision other) 
    {

        switch (other.gameObject.tag)
        {
            case "Crusher":
                Invoke("EnableRock", 1f);
                break;
            case "Building":
                Invoke("DisableRock", 1f);
                break;
        }
    }

    void Search()
    {
        FindRock();

        if (!targetRock) return;

        var targetPos = targetRock.position; // position of the targeted rock

        targetPos.y = 0f; // to keep them on the floor

        var distanceToRock = Vector3.Distance(transform.position, targetPos);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        SmoothFollow(targetPos, 100f * speed);
        

        if (distanceToRock < stopDistance)
        {
            workerAnimator.SetTrigger("Pick");
            currentState = State.PICK;
        }
    }

    void Pick()
    {
        if (targetRock)
        {
            Invoke("TakeRock", 1f);
        }

        currentState = State.BUILD;
    }

    void Build()
    {
        var distBetweenBuilding = Vector3.Distance(transform.position, building.transform.position);

        transform.position = Vector3.MoveTowards(transform.position, building.transform.position, speed * Time.deltaTime);

        SmoothFollow(building.transform.position, 100f * speed);

        if (distBetweenBuilding < stopDistance)
        {
            workerAnimator.SetTrigger("Pick");
            Invoke("PutRock", 1f);

        }
    }

    void TakeRock()
    {
        targetRock.parent = rockCarryPosition;
        targetRock.position = rockCarryPosition.position;
        workerAnimator.SetTrigger("Walk");
    }

    void PutRock()
    {
        if (targetRock)
        {
            Destroy(targetRock.gameObject);
            targetRock=null;
            currentState = State.SEARCH;
        }
    }


    void FindRock()
    {
        // If no rock on the scene it returns null
        if (targetRock || Rock.Instance.rockPieces.Count <= 0) return;

        // choosing a rock from list
        targetRock = Rock.Instance.rockPieces[0];

        // Removing a rock piece from the list so that more than one workers wont go to the same piece
        Rock.Instance.rockPieces.Remove(targetRock);
    }

    // method for smooth turns and movement
    private void SmoothFollow(Vector3 target, float smoothSpeed)
    {
        Vector3 direction = target - transform.position;

        if (direction != Vector3.zero)
        {
           transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), smoothSpeed * Time.deltaTime); 
        }
            
    }
}