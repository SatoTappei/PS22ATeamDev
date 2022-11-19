using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnRestartButton()
    { 
        SceneManager.LoadScene("");//タイトルまたはmainシーンの名前を入れる
    }

    //ゲームを終了するための関数
    public void OnQuitButton()
    {
        Application.Quit(); //ゲームを終了する
    }
}
