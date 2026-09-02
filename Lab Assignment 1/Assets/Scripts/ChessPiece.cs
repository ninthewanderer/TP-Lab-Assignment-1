using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

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
    private enum ColorTint { Red, Yellow, Green, Cyan, Blue, White }    
    
    [SerializeField] private ColorTint colorTint;
    private Color tint;

    [SerializeField] private ChessPieceType chessPiece;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
