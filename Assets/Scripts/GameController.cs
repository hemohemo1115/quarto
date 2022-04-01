using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    //ボードを定義
    private GameObject[,] field = new GameObject[4, 4];
    public Board board;
    public GameObject[] boardChildren;
    public GameObject empty;

    //各駒の数字を定義
    private const int EMPTY = 0;
    private const int BROWN = 2;
    private const int ORCHER = 3;
    private const int BIG = 5;
    private const int SMOLE = 7;
    private const int DENT = 11;
    private const int NODENT = 13;
    private const int CUBE = 17;
    private const int CYLINDER = 19;
    //private const int BrownBigDentCube = BROWN * BIG * DENT * CUBE;
    //private const int BrownBigDentCylinder = BROWN * BIG * DENT * CYLINDER;
    //private const int BrownBigCube = BROWN * BIG * NODENT * CUBE;
    //private const int BrownBigCylinder = BROWN * BIG * NODENT * CYLINDER;
    //private const int BrownSmoleDentCube = BROWN * SMOLE * DENT * CUBE;
    //private const int BrownSmoleDentCylinder = BROWN * SMOLE * DENT * CYLINDER;
    /*private const int BrownSmoleCube = BROWN * SMOLE * NODENT * CUBE;
    private const int BrownSmoleCylinder = BROWN * SMOLE * NODENT * CYLINDER;
    private const int OrcherBigDentCube = ORCHER * BIG * DENT * CUBE;
    private const int OrcherBigDentCylinder = ORCHER * BIG * DENT * CYLINDER;
    private const int OrcherBigCube = ORCHER * BIG * NODENT * CUBE;
    private const int OrcherBigCylinder = ORCHER * BIG * NODENT * CYLINDER;
    private const int OrcherSmoleDentCube = ORCHER * SMOLE * DENT * CUBE;
    private const int OrcherSmoleDentCylinder = ORCHER * SMOLE * DENT * CYLINDER;
    private const int OrcherSmoleCylinder = ORCHER * SMOLE * NODENT * CYLINDER;
    private const int OrcherSmoleCube = ORCHER * SMOLE * NODENT * CYLINDER;
    */ 
    //カメラ情報
    private Camera camera_object;
    private RaycastHit hit;

    //prefabs
    public GameObject CubeSmallOcherPlane;
    public GameObject CubeSmallBrownPlane;
    public GameObject CubeSmallOcherHole;
    public GameObject CubeSmallBrownHole;
    public GameObject CubeBigOcherPlane;
    public GameObject CubeBigBrownPlane;
    public GameObject CubeBigOcherHole;
    public GameObject CubeBigBrownHole;
    public GameObject CylinderSmallOcherPlane;
    public GameObject CylinderSmallBrownPlane;
    public GameObject CylinderSmallOcherHole;
    public GameObject CylinderSmallBrownHole;
    public GameObject CylinderBigOcherPlane;
    public GameObject CylinderBigBrownPlane;
    public GameObject CylinderBigOcherHole;
    public GameObject CylinderBigBrownHole;

    private GameObject Piece;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //カメラ情報を取得
        camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();

        //フィールド初期化
        InitializeField();

        //Boardの子供を取得
        int childrenLength = board.transform.childCount;
        boardChildren = new GameObject[childrenLength];
        Debug.Log(childrenLength);
        //ボードの子を順番に配列に格納
        for (var i = 0; i < childrenLength; ++i)
        {
            boardChildren[i] = board.transform.GetChild(i).gameObject;
        }
        Debug.Log(boardChildren);

        //DebugField();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeField()
    {
        for (int i = 0; i < 4;i++)
        {
            for (int j = 0; j < 4;j++)
            {
                field[i, j] = empty;
            }
        }
    }

    private void DebugField()
    {
        for (int i = 0; i < 4;i++)
        {
            for (int j = 0; j < 4;j++)
            {
                Debug.Log("(i,j) = (" + i + "," + j + ") = " + field[i, j]);
            }
        }
    }

    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }

    public bool DoesGameObjectBelongBoard(GameObject space)
    {
        for (var i = 0; i < boardChildren.Length; ++i)
        {
            if(boardChildren[i] == space)
            {
                return true;
            }
        }
        return false;
    }

    public bool DoesPieceCube(GameObject piece)
    {
        return piece.name.Contains("Cube");
    }

    public bool DoesPieceCylinder(GameObject piece)
    {
        return piece.name.Contains("Cylinder");
    }

    public bool DoesPieceBig(GameObject piece)
    {
        return piece.name.Contains("Big");
    }

    public bool DoesPieceSmall(GameObject piece)
    {
        return piece.name.Contains("Small");
    }

    public bool DoesPieceOcher(GameObject piece)
    {
        return piece.name.Contains("Ocher");
    }

    public bool DoesPieceBrown(GameObject piece)
    {
        return piece.name.Contains("Brown");
    }

    public bool DoesPieceHole(GameObject piece)
    {
        return piece.name.Contains("Hole");
    }

    public bool DoesPiecePlane(GameObject piece)
    {
        return piece.name.Contains("Plane");
    }

    public bool DoesPieceInBoard(GameObject piece)
    {
        if(piece.transform.position.x < 11 && piece.transform.position.x > -11
         && piece.transform.position.z < 11 && piece.transform.position.z > -11)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Move(GameObject piece, GameObject selectedSpace)
    {
        if(DoesPieceBig(piece))
        {
            piece.transform.position = selectedSpace.transform.position + new Vector3(0, 2.1f, 0);
        }
        else
        {
            piece.transform.position = selectedSpace.transform.position + new Vector3(0, 1.1f, 0);
        }
        RecordPiece(piece, selectedSpace);
    }

    public void RecordPiece(GameObject piece, GameObject selectedSpace)
    {
        switch(selectedSpace.name)
        {
            case "Space":
                field[0,0] = piece;
                break;
            case "Space1":
                field[0,1] = piece;
                break;
            case "Space2":
                field[0,2] = piece;
                break;
            case "Space3":
                field[0,3] = piece;
                break;
            case "Space4":
                field[1,0] = piece;
                break;
            case "Space5":
                field[1,1] = piece;
                break;
            case "Space6":
                field[1,2] = piece;
                break;
            case "Space7":
                field[1,3] = piece;
                break;
            case "Space8":
                field[2,0] = piece;
                break;
            case "Space9":
                field[2,1] = piece;
                break;
            case "Space10":
                field[2,2] = piece;
                break;
            case "Space11":
                field[2,3] = piece;
                break;
            case "Space12":
                field[3,0] = piece;
                break;
            case "Space13":
                field[3,1] = piece;
                break;
            case "Space14":
                field[3,2] = piece;
                break;
            case "Space15":
                field[3,3] = piece;
                break;
            default:
                Debug.Log("Record Piece error");
                break;
        }
    }

    public bool DoesQuarto()
    {
        //行チェック
        for(int i = 0;i < 4;i++)
        {
            if((DoesPieceCube(field[i,0]) && DoesPieceCube(field[i,1]) && DoesPieceCube(field[i,2]) && DoesPieceCube(field[i,3])) || 
            (DoesPieceCylinder(field[i,0]) && DoesPieceCylinder(field[i,1]) && DoesPieceCylinder(field[i,2]) && DoesPieceCylinder(field[i,3])) || 
            (DoesPieceBig(field[i,0]) && DoesPieceBig(field[i,1]) && DoesPieceBig(field[i,2]) && DoesPieceBig(field[i,3])) || 
            (DoesPieceSmall(field[i,0]) && DoesPieceSmall(field[i,1]) && DoesPieceSmall(field[i,2]) && DoesPieceSmall(field[i,3])) || 
            (DoesPieceOcher(field[i,0]) && DoesPieceOcher(field[i,1]) && DoesPieceOcher(field[i,2]) && DoesPieceOcher(field[i,3])) || 
            (DoesPieceBrown(field[i,0]) && DoesPieceBrown(field[i,1]) && DoesPieceBrown(field[i,2]) && DoesPieceBrown(field[i,3])) || 
            (DoesPieceHole(field[i,0]) && DoesPieceHole(field[i,1]) && DoesPieceHole(field[i,2]) && DoesPieceHole(field[i,3])) || 
            (DoesPiecePlane(field[i,0]) && DoesPiecePlane(field[i,1]) && DoesPiecePlane(field[i,2]) && DoesPiecePlane(field[i,3])))
            {
                return true;
            }
        }
        //列チェック
        for(int i = 0;i < 4;i++)
        {
            if((DoesPieceCube(field[0,i]) && DoesPieceCube(field[1,i]) && DoesPieceCube(field[2,i]) && DoesPieceCube(field[3,i])) || 
            (DoesPieceCylinder(field[0,i]) && DoesPieceCylinder(field[1,i]) && DoesPieceCylinder(field[2,i]) && DoesPieceCylinder(field[3,i])) || 
            (DoesPieceBig(field[0,i]) && DoesPieceBig(field[1,i]) && DoesPieceBig(field[2,i]) && DoesPieceBig(field[3,i])) || 
            (DoesPieceSmall(field[0,i]) && DoesPieceSmall(field[1,i]) && DoesPieceSmall(field[2,i]) && DoesPieceSmall(field[3,i])) || 
            (DoesPieceOcher(field[0,i]) && DoesPieceOcher(field[1,i]) && DoesPieceOcher(field[2,i]) && DoesPieceOcher(field[3,i])) || 
            (DoesPieceBrown(field[0,i]) && DoesPieceBrown(field[1,i]) && DoesPieceBrown(field[2,i]) && DoesPieceBrown(field[3,i])) || 
            (DoesPieceHole(field[0,i]) && DoesPieceHole(field[1,i]) && DoesPieceHole(field[2,i]) && DoesPieceHole(field[3,i])) || 
            (DoesPiecePlane(field[0,i]) && DoesPiecePlane(field[1,i]) && DoesPiecePlane(field[2,i]) && DoesPiecePlane(field[3,i])))
            {
                return true;
            }
        }
        //斜めチェック1
        if((DoesPieceCube(field[0,0]) && DoesPieceCube(field[1,1]) && DoesPieceCube(field[2,2]) && DoesPieceCube(field[3,3])) || 
        (DoesPieceCylinder(field[0,0]) && DoesPieceCylinder(field[1,1]) && DoesPieceCylinder(field[2,2]) && DoesPieceCylinder(field[3,3])) || 
        (DoesPieceBig(field[0,0]) && DoesPieceBig(field[1,1]) && DoesPieceBig(field[2,2]) && DoesPieceBig(field[3,3])) || 
        (DoesPieceSmall(field[0,0]) && DoesPieceSmall(field[1,1]) && DoesPieceSmall(field[2,2]) && DoesPieceSmall(field[3,3])) || 
        (DoesPieceOcher(field[0,0]) && DoesPieceOcher(field[1,1]) && DoesPieceOcher(field[2,2]) && DoesPieceOcher(field[3,3])) || 
        (DoesPieceBrown(field[0,0]) && DoesPieceBrown(field[1,1]) && DoesPieceBrown(field[2,2]) && DoesPieceBrown(field[3,3])) || 
        (DoesPieceHole(field[0,0]) && DoesPieceHole(field[1,1]) && DoesPieceHole(field[2,2]) && DoesPieceHole(field[3,3])) || 
        (DoesPiecePlane(field[0,0]) && DoesPiecePlane(field[1,1]) && DoesPiecePlane(field[2,2]) && DoesPiecePlane(field[3,3])))
        {
            return true;
        }
        //斜めチェック2
        if((DoesPieceCube(field[0,3]) && DoesPieceCube(field[1,2]) && DoesPieceCube(field[2,1]) && DoesPieceCube(field[3,0])) || 
        (DoesPieceCylinder(field[0,3]) && DoesPieceCylinder(field[1,2]) && DoesPieceCylinder(field[2,1]) && DoesPieceCylinder(field[3,0])) || 
        (DoesPieceBig(field[0,3]) && DoesPieceBig(field[1,2]) && DoesPieceBig(field[2,1]) && DoesPieceBig(field[3,0])) || 
        (DoesPieceSmall(field[0,3]) && DoesPieceSmall(field[1,2]) && DoesPieceSmall(field[2,1]) && DoesPieceSmall(field[3,0])) || 
        (DoesPieceOcher(field[0,3]) && DoesPieceOcher(field[1,2]) && DoesPieceOcher(field[2,1]) && DoesPieceOcher(field[3,0])) || 
        (DoesPieceBrown(field[0,3]) && DoesPieceBrown(field[1,2]) && DoesPieceBrown(field[2,1]) && DoesPieceBrown(field[3,0])) || 
        (DoesPieceHole(field[0,3]) && DoesPieceHole(field[1,2]) && DoesPieceHole(field[2,1]) && DoesPieceHole(field[3,0])) || 
        (DoesPiecePlane(field[0,3]) && DoesPiecePlane(field[1,2]) && DoesPiecePlane(field[2,1]) && DoesPiecePlane(field[3,0])))
        {
            return true;
        }
        return false;
    }
}
