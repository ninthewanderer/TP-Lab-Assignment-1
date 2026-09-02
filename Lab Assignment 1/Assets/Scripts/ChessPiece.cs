using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Net.WebSockets;

[ExecuteInEditMode]
public class ChessPiece : MonoBehaviour
{

    Vector3[] verticalPoints = new Vector3[]
    {
        new Vector3 (0, -1, 0),  new Vector3 (0, 1, 0)   
    };

    Vector3[] verticalUpPoints = new Vector3[]
    {
        new Vector3 (0, 0, 0)  , new Vector3(0, 1, 0) 
    };
    Vector3[] horizontalPoints = new Vector3[]
    {
        new Vector3 (-1, 0, 0),  new Vector3 (1, 0, 0)   
    };
    Vector3[] diagonalPoints = new Vector3[]
    {
        new Vector3 (-1, -1, 0),  new Vector3 (1, 1, 0), new Vector3 (-1, 1, 0), new Vector3 (1, -1, 0)   
    };

    private enum ChessPieceType { Pawn, Rook, Knight, Bishop, Queen, King }
    private enum ColorTint { Red, Yellow, Green, Cyan, Blue, White , Custom }    
    
    [SerializeField] private ColorTint colorTint;
    public Color tint;
    public Vector3 handlePosition = new Vector3(1f, 0f, 0f);

    [SerializeField] private ChessPieceType chessPiece;

    private void OnDrawGizmos() {
        tint = colorTint switch
        {
            ColorTint.Red => Color.red,
            ColorTint.Yellow => Color.yellow,
            ColorTint.Green => Color.green,
            ColorTint.Cyan => Color.cyan,
            ColorTint.Blue => Color.blue,
            ColorTint.White => Color.white,
            _ => Color.white
        };

        if (handlePosition != new Vector3(0f, 0f, 0f))
        {
            tint = new Color(handlePosition.x, handlePosition.y, handlePosition.z);
            colorTint = ColorTint.Custom;
        }

        Gizmos.DrawIcon(transform.position, chessPiece.ToString(), true, tint);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3[] points;
        switch (chessPiece)
        {

            case ChessPieceType.Pawn:
                points = new Vector3[]
                {
                    // draw line one vertical
                   this.transform.position,
                    new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z)

                };
                Gizmos.DrawLineList(points);
                break;
            case ChessPieceType.Rook:
                points = new Vector3[]
                {
                    // draw line horizontal and vertical
                   this.transform.position,
                    new Vector3(this.transform.position.x, this.transform.position.y + 8, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x + 8, this.transform.position.y, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 8, this.transform.position.y, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x, this.transform.position.y - 8, this.transform.position.z)
                };
                Gizmos.DrawLineList(points);
                break;
            case ChessPieceType.Knight:
                Gizmos.DrawCube(this.transform.position + new Vector3(2, 1, 0), new Vector3 (1, 1, 1));
                Gizmos.DrawCube(this.transform.position + new Vector3(1, 2, 0), new Vector3 (1, 1, 1));
                Gizmos.DrawCube(this.transform.position + new Vector3(-1, 2, 0), new Vector3 (1, 1, 1));
                Gizmos.DrawCube(this.transform.position + new Vector3(-2, 1, 0), new Vector3 (1, 1, 1));
                Gizmos.DrawCube(this.transform.position + new Vector3(-2, -1, 0), new Vector3 (1, 1, 1));
                Gizmos.DrawCube(this.transform.position + new Vector3(-1, -2, 0), new Vector3 (1, 1, 1));
                Gizmos.DrawCube(this.transform.position + new Vector3(1, -2, 0), new Vector3 (1, 1, 1));
                Gizmos.DrawCube(this.transform.position + new Vector3(2, -1, 0), new Vector3 (1, 1, 1));    

                // highlight squares in an L shape
                break;
            case ChessPieceType.Bishop:
                points = new Vector3[]
                {
                    // draw line diagonal
                    this.transform.position,
                    new Vector3(this.transform.position.x + 8, this.transform.position.y + 8, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 8, this.transform.position.y - 8, this.transform.position.z),

                    this.transform.position,
                    new Vector3(this.transform.position.x + 8, this.transform.position.y - 8, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 8, this.transform.position.y + 8, this.transform.position.z)
                };
                Gizmos.DrawLineList(points);
                break;
            case ChessPieceType.Queen:
                points = new Vector3[]
                {
                    // draw line diagonal & vertical & horizontal
                    this.transform.position,
                    new Vector3(this.transform.position.x, this.transform.position.y + 8, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x + 8, this.transform.position.y, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 8, this.transform.position.y, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x, this.transform.position.y - 8, this.transform.position.z),

                    this.transform.position,
                    new Vector3(this.transform.position.x + 8, this.transform.position.y + 8, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 8, this.transform.position.y - 8, this.transform.position.z),

                    this.transform.position,
                    new Vector3(this.transform.position.x + 8, this.transform.position.y - 8, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 8, this.transform.position.y + 8, this.transform.position.z)
                };
                Gizmos.DrawLineList(points);
                break;
            case ChessPieceType.King:
                points = new Vector3[]
                {
                    // draw line diagonal & vertical & horizontal
                    this.transform.position,
                    new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 1, this.transform.position.y - 1, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x + 1, this.transform.position.y - 1, this.transform.position.z),
                    this.transform.position,
                    new Vector3(this.transform.position.x - 1, this.transform.position.y + 1, this.transform.position.z)
                };
                Gizmos.DrawLineList(points);
                break;
            default:
                break;
        }
    }
}

[CustomEditor(typeof(ChessPiece))]
public class ChessPieceEditor : Editor
{
    // makes a handle that can be used to make a custom color on the chess piece
    public void OnSceneGUI()
    {
        ChessPiece piece = (ChessPiece)target;
        Transform transform = piece.transform;

        // calculate handle's world position relative to the chess piece
        Vector3 worldHandlePos = transform.position + piece.handlePosition;

        // sets color of the handle to the current color of the piece 
        Handles.color = new Color(piece.handlePosition.x, piece.handlePosition.y, piece.handlePosition.z);

        // tracks for scene changes
        EditorGUI.BeginChangeCheck();

        // draws the free move handle
        Vector3 newPos = Handles.FreeMoveHandle(piece.handlePosition, 0.25f, Vector3.zero, Handles.CircleHandleCap);

        // if the handle is dragged, update position & log changes
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(piece, "Change piece color via handle");

            // converts world position into local position (color)
            Vector3 colorOffset = newPos - transform.position;

            // clamps RGB values
            colorOffset.x = Mathf.Clamp01(colorOffset.x);
            colorOffset.y = Mathf.Clamp01(colorOffset.y);
            colorOffset.z = Mathf.Clamp01(colorOffset.z);

            // changes handlePosition variable to the new color
            piece.handlePosition = colorOffset;
            EditorUtility.SetDirty(piece);
        }
    }
}
