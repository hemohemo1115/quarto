using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    //ボードを定義
    private int[,] field = new int[4, 4];
    public Board board;
    public GameObject[] boardChildren;

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

    private void InitializeField()
    {
        for (int i = 0; i < 4;i++)
        {
            for (int j = 0; j < 4;j++)
            {
                field[i, j] = EMPTY;
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

    public void Move(GameObject piece, Vector3 position)
    {
        if(DoesPieceBig(piece))
        {
            piece.transform.position = position + new Vector3(0, 2.1f, 0);
        }
        else
        {
            piece.transform.position = position + new Vector3(0, 1.1f, 0);
        }
    }

}
