using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O_MoveOnde : MonoBehaviour
{
    [SerializeField] private List<Image> onde = new();
    [SerializeField] private GameObject ondes;
    [SerializeField] private float speed;
    private int indexMove;
    private int timer;

    private void Start()
    {
        indexMove = onde.Count - 1;
    }
    void FixedUpdate()
    {
        //Move();
    }

    private void Move()
    {
        ondes.transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        timer++;
        if (timer >= 100)
        {
            timer = 0;
            if (indexMove >= onde.Count - 1)
            {
                onde[onde.Count - 1].transform.position = onde[0].transform.position;
                onde[onde.Count - 1].transform.position -= new Vector3(200, 0, 0);
                indexMove--;    
            }
            else
            {
                onde[indexMove].transform.position = onde[indexMove + 1].transform.position;
                onde[indexMove].transform.position -= new Vector3(200, 0, 0);
                if (indexMove <= 0)
                    indexMove = onde.Count - 1;
                else
                    indexMove--;
            }
        }
    }
}
