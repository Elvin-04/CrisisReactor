using System.Collections.Generic;
using UnityEngine;

public class AIO_AttractionComponent : MonoBehaviour
{
    private float maxAttractionForce = 1.0f;

    private AIO_GameManager gameManager;
    private List<GameObject> atomsInZone = new List<GameObject>();

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AIO_GameManager>();
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        atomsInZone.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        atomsInZone.Remove(other.gameObject);
    }

    void Update()
    {
        if(atomsInZone.Count > 0)
        {
            foreach (GameObject atom in atomsInZone)
            {
                if(!atom.GetComponent<AIO_AtomMovement>().GetIsSwapping())
                {
                    Vector3 direction = gameObject.transform.position - atom.transform.position;
                    float distance = direction.magnitude;
                    float attractionStrength = maxAttractionForce / (distance * distance);
                    Vector3 force = direction.normalized * attractionStrength;
                    Vector3 newPosition = atom.transform.position + force * Time.deltaTime;

                    atom.transform.position = newPosition;

                        if (Vector3.Distance(atom.transform.position, gameObject.transform.position) <= 0.25f)
                        {
                            AbsrobAtom(atom);
                        }
                }
            }
        }
    }

    void AbsrobAtom(GameObject atom)
    {
        gameManager.AddPlayerMass(atom.GetComponent<AIO_AtomMovement>().GetMass());
        gameManager.ReturnToPool(atom);
    }
}
