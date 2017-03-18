using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {
    // Is this mine
    public bool isMine;

    // Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

	// Use this for initialization
	void Start () {
        // Rendomlt decide if it's a mine or not
        isMine = Random.value < 0.15;

        // Register in Grid
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Grid.elements[x, y] = this;

        /*// Testing
        isMine = false;
        loadTexture(1);*/
	}
	
    // Load another Texture
    public void loadTexture(int adjacentCount)
    {
        if (isMine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }

    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    void OnMouseUpAsButton()
    {
        // It's a mine
        if (isMine)
        {
            // Todo : Uncover all mines
            Grid.uncoverMines();

            // game over
            print("YOU LOSE!!");
        }
        // not a mine
        else
        {
            // Todo : show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Grid.adjacentMines(x, y));

            // Todo : uncover area without mines
            Grid.floodFill(x, y, new bool[Grid.w, Grid.h]);

            // Todo : find out if the game was won 
            if (Grid.isFinished())
                print("You win");
        }
    }
	/* // Update is called once per frame
	void Update () {
		
	} */
}
