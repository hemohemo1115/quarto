using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveSelector : MonoBehaviour
{
    //public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    //public GameObject attackLocationPrefab;

    private GameObject tileHighlight;
    private GameObject movingPiece;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)),
                        Quaternion.identity, gameObject.transform);
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
            //Debug.Log(position);
            //スペースを光らせるために分岐
            if(GameController.instance.DoesGameObjectBelongBoard(hit.collider.gameObject))
            {
                tileHighlight.SetActive(true);
                tileHighlight.transform.position = position + new Vector3(0, 0.1f, 0);
            }
            //移動先を選択
            if (Input.GetMouseButtonDown(0))
            {
                GameObject selectedSpace = hit.collider.gameObject;
                Debug.Log(selectedSpace);
                if(GameController.instance.DoesGameObjectBelongBoard(selectedSpace) && !(GameController.instance.DoesPieceInBoard(movingPiece)))
                {
                    GameController.instance.Move(movingPiece, selectedSpace);
                }
                ExitState();
            }

        }
        else
        {
            tileHighlight.SetActive(false);
        }

    }

    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        this.enabled = true;
    }

    private void ExitState()
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        GameController.instance.DeselectPiece(movingPiece);
        movingPiece = null;
        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();
    }


}
