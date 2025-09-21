using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop2D : MonoBehaviour
{
    public int pieceID;
    Vector3 offset;
    BoxCollider2D collider;
    DropArea lastDropArea = null;

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void OnMouseDown()
    {
        offset = MouseWorldPosition() - transform.position;
        // Clear previous drop area's occupied state when picking up
        if (lastDropArea != null)
        {
            lastDropArea.isOccupied = false;
            lastDropArea = null;
        }
    }

    void OnMouseDrag()
    {
        // Debug.DrawLine(transform.position, MouseWorldPosition(), Color.green);
        transform.position = MouseWorldPosition() + offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.01f);
    }

    void OnMouseUp()
    {
        collider.enabled = false;
        Vector3 mouseWorldPos = MouseWorldPosition();
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
        if (hit != null && hit.CompareTag("DropArea") && !hit.GetComponent<DropArea>().isOccupied)
        {
            Debug.Log(hit.name);
            transform.position = hit.transform.position + new Vector3(0, 0, -0.01f);
            lastDropArea = hit.GetComponent<DropArea>();
            AudioHandler.instance.PlaySFX("PuzzleClick");
            lastDropArea.isOccupied = true;
            if (lastDropArea.pieceID == pieceID)
            {
                collider.enabled = false;
                lastDropArea.isCorrect = true;
                return;
            }
        }
        collider.enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
