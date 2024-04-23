using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_GameManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> GoodSprite = new();
    [SerializeField] private List<Sprite> BadSprite = new();
    [SerializeField] private List<Image> imageButton = new();
    public static int nbGoodprite;
    void Start()
    {
        SetGoodSprite();
        SetBadSprite();
    }

    private void SetGoodSprite()
    {
        nbGoodprite = Random.Range(1, 4);
        print(nbGoodprite);
        for (int i = 0; i < nbGoodprite; i++)
        {
            int random = Random.Range(0, imageButton.Count);
            int random2 = Random.Range(0, GoodSprite.Count);
            if (imageButton[random].sprite == null)
            {
                imageButton[random].sprite = GoodSprite[random2];
                imageButton[random].transform.parent.GetComponent<S_OnClick>().isGoodSprite = true;
                GoodSprite.RemoveAt(random2);
            }
            else
            {
                i--;
            }
        }
    }

    private void SetBadSprite()
    {
        for (int i = 0; i < imageButton.Count; i++)
        {
            if (imageButton[i].sprite == null)
            {
                int random = Random.Range(0, BadSprite.Count);
                imageButton[i].sprite = BadSprite[random];
                BadSprite.RemoveAt(random);
            }
        }
    }
}
