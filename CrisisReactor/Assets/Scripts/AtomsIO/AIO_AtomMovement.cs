using System.Collections.Generic;
using UnityEngine;

public class AIO_AtomMovement : MonoBehaviour
{
    [SerializeField] private List<Sprite> atomsSprites = new List<Sprite>();
    private float moveSpeed = 5f;
    private float rotationSpeed = 200f;
    private Quaternion targetRotation;
    private float randomizedAngle;
    private int mass;
    private Vector3 forwardDirection = Vector3.right;

    void Awake()
    {
        RandomizeAtom();
        forwardDirection = new Vector3(1, 1, 1) * Random.Range(-1f,1f);
    }

    public int GetMass()
    {
        return mass;
    }

    public void RandomizeAtom()
    {
        int randomizedIndex = Random.Range(0, 5);
        GetComponent<SpriteRenderer>().sprite = atomsSprites[randomizedIndex];
        moveSpeed = Random.Range(0.3f, 2f);
        rotationSpeed = Random.Range(25f, 125f);

        switch (randomizedIndex)
        {
            case 0: mass = 50;
                break;
            case 1: mass = 500;
                break;
            case 2: mass = 5000;
                break;
            case 3: mass = 50000;
                break;
            case 4: mass = 500000;
                break;
            case 5: mass = 1000;
                break;
        }
        RandomizeDirection();
        transform.localScale = new Vector3(1, 1, 1) * Random.Range(0.1f, 0.5f);
    }

    void RandomizeDirection()
    {
        targetRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        forwardDirection = transform.right;
        Invoke("RandomizeDirection", Random.Range(2f, 5f));
    }
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += forwardDirection * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x , transform.position.y, 0f);
    }
}
