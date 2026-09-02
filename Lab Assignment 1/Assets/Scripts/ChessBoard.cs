using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    // Draws the chessboard based on object's current position
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int x = 0; x < 9; x++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(0f, x, 0f), transform.position + new Vector3(8f, x, 0f));
            Gizmos.DrawLine(transform.position + new Vector3(x, 0f, 0f), transform.position + new Vector3(x, 8f, 0f));
        }
    }
}
