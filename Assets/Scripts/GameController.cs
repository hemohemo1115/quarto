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
    private string[] check = new string[4];

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
        //InitializeField();

        //Boardの子供を取得
        int childrenLength = board.transform.childCount;
        boardChildren = new GameObject[childrenLength];
        Debug.Log(childrenLength);
        // 0～個数-1までの子を順番に配列に格納
        for (var i = 0; i < childrenLength; ++i)
        {
            boardChildren[i] = board.transform.GetChild(i).gameObject;
        }
        Debug.Log(boardChildren);

        DebugField();
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetMouseButtonDown(0))
        {
            //マウスのポジションを取得してRayに代入
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition);
 
            //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            if (Physics.Raycast(ray, out hit))
            {
                //x,zの値を取得
                float x = (int)hit.collider.gameObject.transform.position.x;
                float z = (int)hit.collider.gameObject.transform.position.z;
                //Debug.Log(x);
                //Debug.Log(z);

                if (hit.collider.gameObject.tag == "Piece")
                {
                    Piece = hit.collider.gameObject;
                }
                else 
                {
                    if (Piece != null)
                    {
                        Vector3 pos = hit.collider.gameObject.transform.position;
                        Debug.Log(Piece.transform.localScale);
                        pos.y += Piece.transform.localScale.y/2;
                        Piece.transform.position = pos;
                        Piece = null;
                    }
                }


                //GameObject space = Instantiate(OrcherBigCube);
                //space.transform.position = hit.collider.gameObject.transform.position;
            }

        }*/
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
                Debug.Log(field[0,0]);
                break;
            case "Space1":
                field[0,1] = piece;
                Debug.Log(field[0,1]);
                break;
            case "Space2":
                field[0,2] = piece;
                Debug.Log(field[0,2]);
                break;
            case "Space3":
                field[0,3] = piece;
                Debug.Log(field[0,3]);
                break;
            case "Space4":
                field[1,0] = piece;
                Debug.Log(field[1,0]);
                break;
            case "Space5":
                field[1,1] = piece;
                Debug.Log(field[1,1]);
                break;
            case "Space6":
                field[1,2] = piece;
                Debug.Log(field[1,2]);
                break;
            case "Space7":
                field[1,3] = piece;
                Debug.Log(field[1,3]);
                break;
            case "Space8":
                field[2,0] = piece;
                Debug.Log(field[2,0]);
                break;
            case "Space9":
                field[2,1] = piece;
                Debug.Log(field[2,1]);
                break;
            case "Space10":
                field[2,2] = piece;
                Debug.Log(field[2,2]);
                break;
            case "Space11":
                field[2,3] = piece;
                Debug.Log(field[2,3]);
                break;
            case "Space12":
                field[3,0] = piece;
                Debug.Log(field[3,0]);
                break;
            case "Space13":
                field[3,1] = piece;
                Debug.Log(field[3,1]);
                break;
            case "Space14":
                field[3,2] = piece;
                Debug.Log(field[3,2]);
                break;
            case "Space15":
                field[3,3] = piece;
                Debug.Log(field[3,3]);
                break;
            default:
                Debug.Log("Record Piece error");
                break;
        }
    }

    public bool DoesPieceLineCube(GameObject field)
    {
        //行チェック
        for(int i = 0;i < 4;i++)
        {
            if(DoesPieceCube(field[i,0]) && DoesPieceCube(field[i,1]) && DoesPieceCube(field[i,2]) && DoesPieceCube(field[i,3]))
            {
                return true;
            }
        }
        //列チェック
        for(int i = 0;i < 4;i++)
        {
            if(DoesPieceCube(field[0,i]) && DoesPieceCube(field[1,i]) && DoesPieceCube(field[2,i]) && DoesPieceCube(field[3,i]))
            {
                return true;
            }
        }
        //斜めチェック1
        if(DoesPieceCube(field[0,0]) && DoesPieceCube(field[1,1]) && DoesPieceCube(field[2,2]) && DoesPieceCube(field[3,3]))
        {
            return true;
        }
        //斜めチェック2
        if(DoesPieceCube(field[0,3]) && DoesPieceCube(field[1,2]) && DoesPieceCube(field[2,1]) && DoesPieceCube(field[3,0]))
        {
            return true;
        }
        return false;
    }

    public bool DoesQuarto(GameObject field)
    {
        for(int i = 0; i < 4;i++)
        {
            if(DoesPieceCube(field[i,0]) &&
                
                
            DoesPieceCylinder(field[i,j]) || DoesPieceBig(field[i,j]) || DoesPieceSmall(field[i,j]) || 
            DoesPieceOcher(field[i,j]) || DoesPieceBrown(field[i,j]) || DoesPieceHole(field[i,j]) || DoesPiecePlane(field[i,j]))
            {
                return true;
            }
        }
        for(i = 0;j < 4;i++)
        {

        }
        for(i = 0;i < 0;i++)
        {
            if(DoesPieceCube(field[i,j]) || DoesPieceCylinder(field[i,j]) || DoesPieceBig(field[i,j]) || DoesPieceSmall(field[i,j]) || 
                DoesPieceOcher(field[i,j]) || DoesPieceBrown(field[i,j]) || DoesPieceHole(field[i,j]) || DoesPiecePlane(field[i,j]))
                {
                    return true;
                }
        }
    }
}
