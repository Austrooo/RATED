using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    public DropArea[] dropArea;
    public GameObject pieceHolder;
    public GameObject puzzleArea;
    public GameObject piecePrefab;
    public void InitializePuzzleGame()
    {
        dropArea = new DropArea[puzzleArea.transform.childCount];
        for (int i = 0; i < puzzleArea.transform.childCount; i++)
        {
            dropArea[i] = puzzleArea.transform.GetChild(i).GetComponent<DropArea>();
            dropArea[i].isOccupied = false;
            dropArea[i].isCorrect = false;
        }
        foreach (Transform child in pieceHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ScatterPieces(Sprite[] sprites)
    {
        Debug.Log("scattering pieces");
        int index = 0;
        foreach (Sprite sprite in sprites)
        {
            GameObject piece = Instantiate(piecePrefab, pieceHolder.transform);
            piece.GetComponent<Image>().sprite = sprite;
            float randomX = Random.Range(startPos.x, endPos.x); // -850 950
            float randomY = Random.Range(endPos.y, startPos.y); // 420 -520
            piece.GetComponent<RectTransform>().anchoredPosition = new Vector3(randomX, randomY, -0.01f);
            piece.GetComponent<DragNDrop2D>().pieceID = index;
            piece.name = "Piece " + index;
            index++;
        }
    }

    public bool CheckIsPuzzleComplete()
    {
        foreach (DropArea area in dropArea)
        {
            if (!area.isCorrect) return false;
        }
        return true;
    }
}
