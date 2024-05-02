using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIO_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject atomPrefab;
    [SerializeField] private TextMeshProUGUI currentMassText;
    private Queue<GameObject> atomPool = new Queue<GameObject>();
    private List<GameObject> activeAtoms = new List<GameObject>();
    private int poolMaxAliveCount = 10;
    private float maxSpawnDistance = 1000f;
    private int patternIndex;
    private int characterMass;
    private int waitedMass;

    //init game mode
    void Start()
    {
        Cursor.visible = false;
        SetPattern();

        patternIndex = Random.Range(0,2);
        AIO_CharacterController character = Instantiate(characterPrefab).GetComponent<AIO_CharacterController>();
        character.SetSpriteByPattern(patternIndex);

        InitializeAtomPool(poolMaxAliveCount);
        ActivateAtomsFromPool();
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }

    void SetPattern()
    {
        switch (patternIndex)
        {
            case 0: waitedMass = 1650;
            break;
            case 1: waitedMass = 1501550;
            break;
            case 2: waitedMass = 35050;
            break;
        }
    }

    public int GetPatternIndex()
    {
        return patternIndex;
    }

    public void AddPlayerMass(int massToAdd)
    {
        characterMass += massToAdd;
        currentMassText.text = "Masse : " + characterMass;
        CheckMassForVictory();
    }

    void CheckMassForVictory()
    {
        Debug.Log("waited mass = " + waitedMass);
        if(characterMass == waitedMass)
        {
            SceneManager.LoadScene("Lobby");
            PlayerPrefs.SetInt("MiniGame8", 1);
            Debug.Log("winned");
        }
        else if(characterMass > waitedMass)
        {
            LOB_Timer.instance.RemoveTimer(20);
            Debug.Log("loosed");
        }
    }

    //select spawn translation
    Vector3 GetRandomPositionOutsideScreen()
    {
        Camera mainCamera = Camera.main;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        int side = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0:
                spawnPosition.x = bottomLeft.x - Random.Range(1f, maxSpawnDistance);
                spawnPosition.y = Random.Range(bottomLeft.y, topRight.y);
                break;
            case 1:
                spawnPosition.x = topRight.x + Random.Range(1f, maxSpawnDistance);
                spawnPosition.y = Random.Range(bottomLeft.y, topRight.y);
                break;
            case 2:
                spawnPosition.x = Random.Range(bottomLeft.x, topRight.x);
                spawnPosition.y = bottomLeft.y - Random.Range(1f, maxSpawnDistance);
                break;
            case 3:
                spawnPosition.x = Random.Range(bottomLeft.x, topRight.x);
                spawnPosition.y = topRight.y + Random.Range(1f, maxSpawnDistance);
                break;
        }

        return spawnPosition;
    }

    void InitializeAtomPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject atom = Instantiate(atomPrefab, GetRandomPositionOutsideScreen(), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            atomPool.Enqueue(atom);
        }
    }

    void ActivateAtomsFromPool()
    {
        while (atomPool.Count > 0 && activeAtoms.Count < poolMaxAliveCount)
        {
            GameObject atom = atomPool.Dequeue();
            atom.transform.position = GetRandomPositionOutsideScreen(); 
            activeAtoms.Add(atom);
        }
    }

    public void ReturnToPool(GameObject atom)
    {
        atomPool.Enqueue(atom);
        activeAtoms.Remove(atom);
        ReuseAtom();
    }

    private void ReuseAtom()
    {
        while (activeAtoms.Count < poolMaxAliveCount && atomPool.Count > 0)
        {
            GameObject atom = atomPool.Dequeue();
            atom.GetComponent<AIO_AtomMovement>().RandomizeAtom();
            atom.transform.position = GetRandomPositionOutsideScreen(); 
            activeAtoms.Add(atom);
        }
    }
        public List<GameObject> GetActiveAtoms()
    {
        return activeAtoms;
    }
}
