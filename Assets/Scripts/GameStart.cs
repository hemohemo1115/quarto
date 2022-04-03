using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//　スタートボタンを押したら実行する
	public void StartLocalMatch()
    {
		SceneManager.LoadScene ("LocalMatch");
	}

    public void StartCpuMatch()
    {
		SceneManager.LoadScene ("CpuMatch");
	}
}
