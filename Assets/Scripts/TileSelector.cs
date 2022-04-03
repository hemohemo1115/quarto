using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileSelector : MonoBehaviour
{
    public GameObject tileHighlightPrefab;
    private GameObject tileHighlight;
    

    // Start is called before the first frame update
    void Start()
    {
        Vector2 gridPoint = Geometry.GridPoint(0, 0);
        Vector3 point = Geometry.PointFromGrid(gridPoint);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 position = hit.collider.gameObject.transform.position;
            //Vector2 gridPoint = Geometry.GridFromPoint(position);
            //GameObject selectedPiece = hit.collider.gameObject;
            
            //ローカル対戦の処理
            if(SceneManager.GetActiveScene().name == "LocalMatch")
            {
                if(GameController.instance.DoesGameObjectBelongBoard(hit.collider.gameObject))
                {
                    tileHighlight.SetActive(true);
                    tileHighlight.transform.position = position + new Vector3(0, 0.1f, 0);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject selectedPiece = hit.collider.gameObject;
                    //Debug.Log(selectedPiece);
                    if (selectedPiece.tag == "Piece" && !(GameController.instance.DoesPieceInBoard(selectedPiece)))
                    {
                        GameController.instance.SelectPiece(selectedPiece);
                        //Debug.Log("selected");
                        // Reference Point 1: add ExitState call here later
                        GameController.instance.NextPlayer();
                        ExitState(selectedPiece);
                    }
                }
            }
            else if(SceneManager.GetActiveScene().name == "CpuMatch")
            {
                if(GameControllerCpu.instance.DoesGameObjectBelongBoard(hit.collider.gameObject))
                {
                    tileHighlight.SetActive(true);
                    tileHighlight.transform.position = position + new Vector3(0, 0.1f, 0);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject selectedPiece = hit.collider.gameObject;
                    //Debug.Log(selectedPiece);
                    if (selectedPiece.tag == "Piece" && !(GameControllerCpu.instance.DoesPieceInBoard(selectedPiece)))
                    {
                        GameControllerCpu.instance.SelectPiece(selectedPiece);
                        Cpu.instance.RemovePieceInList(selectedPiece);
                        //Debug.Log("selected");
                        // Reference Point 1: add ExitState call here later
                        GameControllerCpu.instance.NextPlayer();
                        this.enabled = false;
                        Cpu.instance.CpuPlay(selectedPiece);
                    }
                }
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
    }

    public void EnterState()
    {
        enabled = true;
    }

    private void ExitState(GameObject movingPiece)
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        MoveSelector move = GetComponent<MoveSelector>();
        move.EnterState(movingPiece);
    }

}
