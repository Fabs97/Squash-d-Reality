using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICookingTime : MonoBehaviour
{

    [SerializeField] private RawImage[] Ingredients;
    

    // Start is called before the first frame update
    void Start()
    {
        setImageActive(false);
    }

    public void setImages(List<Ingredient> ingredientsImages)
    {
        int i = 0;
        for (i = 0; i < ingredientsImages.Count; i++)
        {
            Ingredients[i].texture = ingredientsImages[i].image;
            Ingredients[i].gameObject.SetActive(true);
        }

        for (int j = i; j < Ingredients.Length; j++)
        {
            Ingredients[i].gameObject.SetActive(false);
        }
    }

    public void setImageActive(bool value)
    {
        for (int i = 0; i < Ingredients.Length; i++)
        {
            Ingredients[i].gameObject.SetActive(value);
        }
    }
}
