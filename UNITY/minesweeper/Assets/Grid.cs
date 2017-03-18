using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    // The Grid
    public static int w = 10;
    public static int h = 13;
    public static Element[,] elements = new Element[w, h];

    // uncover all mines if the user clicked a mine
    public static void uncoverMines()
    {
        foreach (Element elem in elements)
            if (elem.isMine)
                elem.loadTexture(0);
    }

    // find whether an element is a mine
    public static bool mineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].isMine;
        return false;
    }

    // find count of adjacent mines
    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x, y + 1)) // top
            count++;
        if (mineAt(x - 1, y + 1)) // top left
            count++;
        if (mineAt(x + 1, y + 1)) // top right
            count++;
        if (mineAt(x - 1, y)) // left
            count++;
        if (mineAt(x + 1, y)) // right
            count++;
        if (mineAt(x, y - 1)) // bottom
            count++;
        if (mineAt(x - 1, y - 1)) // bottom left
            count++;
        if (mineAt(x + 1, y - 1)) // bottom right
            count++;

        return count;
    }

    // flood fill algo if user clicked something other than a mine
    public static void floodFill(int x, int y, bool[,] visited)
    {
        // in range
        if(x >= 0 && y >= 0 && x < w && y < h)
        {
            if (visited[x, y])
                return;

            // uncover element
            elements[x, y].loadTexture(adjacentMines(x, y));

            // if not empty cell, then return
            if (adjacentMines(x, y) > 0)
                return;

            visited[x, y] = true;

            // recursion
            floodFill(x, y + 1, visited); //top
            floodFill(x - 1, y + 1, visited); // top left
            floodFill(x + 1, y + 1, visited); // top right
            floodFill(x - 1, y, visited); // left
            floodFill(x + 1, y, visited); // right
            floodFill(x, y - 1, visited); // bottom
            floodFill(x - 1, y - 1, visited); // bottom left
            floodFill(x + 1, y - 1, visited); // bottom right
        }
    }

    // check if all elements other than mine are uncovered
    public static bool isFinished()
    {
        foreach (Element elem in elements)
            if (elem.isCovered() && !elem.isMine)
                return false;

        // else all uncovered elements are mine
        return true;
    }
}
