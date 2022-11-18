using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    void OnRestartButton()
    { 
        SceneManager.LoadScene("");//タイトルまたはmainシーンの名前を入れる
    }

    //ゲームを終了するための関数
    void QuitButton()
    {
        Application.Quit(); //ゲームを終了する
    }
}
