using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour {

    [Header("For randomizing the background image")]
    public bool randomize = false;
    public Sprite[] sprites;

	void Start ()
    {
		if (randomize)
        {
            if (sprites.Length > 0)
                GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            else
            {
                Debug.LogWarning("Cannot randomize with nothing in the sprite array!");
                return;
            }
        }
	}
	

}
