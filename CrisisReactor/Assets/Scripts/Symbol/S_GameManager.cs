using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_GameManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> GoodSprite = new();
    [SerializeField] private List<Sprite> BadSprite = new();
    [SerializeField] private List<Image> imageButton = new();
    private int nbGoodprite;
    void Start()
    {
        SetGoodSprite();
        SetBadSprite();
    }

    private void SetGoodSprite()
    {
        nbGoodprite = Random.Range(1, 4);
        for (int i = 0; i < nbGoodprite; i++)
        {
            int random = Random.Range(0, imageButton.Count);
            int random2 = Random.Range(0, GoodSprite.Count);
            if (imageButton[random].sprite != null || GoodSprite[random2] == null)
            {
                continue;
            }
            imageButton[random].sprite = GoodSprite[random2];
            GoodSprite[random2] = null;
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
