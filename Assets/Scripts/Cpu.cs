using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpu : MonoBehaviour
{
    public static Cpu instance;
    private GameObject movingPiece;
    private GameObject selectedSpace;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPiece()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        List<GameObject> pieces = GameControllerCpu.instance.Pieces;
        GameObject selectedPiece = pieces[Random.Range(0, pieces.Count)];
        GameControllerCpu.instance.SelectPiece(selectedPiece);
        RemovePieceInList(selectedPiece);
        GameControllerCpu.instance.NextPlayer();
        MoveSelector move = GameControllerCpu.instance.board.GetComponent<MoveSelector>();
        move.EnterState(selectedPiece);
    }

    public void MovePiece(GameObject selectedPiece)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        while(true)
        {
            selectedSpace = GameControllerCpu.instance.boardChildren[Random.Range(1, 17)]; //ランダムでスペースを選択
            if(!(GameControllerCpu.instance.DoesPieceInSpace(selectedSpace)))
            {
                GameControllerCpu.instance.Move(selectedPiece, selectedSpace);
                GameControllerCpu.instance.DeselectPieceCpuMatch(selectedPiece);
                break;
            }
        }
    }

    public void CpuFirstPlay()
    {
        StartCoroutine(DelayCoroutine());
        SelectPiece();
    }

    public void CpuPlay(GameObject selectedPiece)
    {
        StartCoroutine(DelayCoroutine());
        MovePiece(selectedPiece);
        SelectPiece();
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(2);
        //Debug.Log("2秒経過");
    }

    public void RemovePieceInList(GameObject selectedPiece)
    {
        GameControllerCpu.instance.Pieces.Remove(selectedPiece);
    }
}
