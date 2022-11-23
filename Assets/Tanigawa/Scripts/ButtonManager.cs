using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] string _sceneName = "Title";
    public void OnRestartButton()
    {
        Debug.Log("リスタートボタンが押されました。");
        SceneManager.LoadScene(_sceneName);//タイトルまたはmainシーンの名前を入れる
    }

    //ゲームを終了するための関数
    public void OnQuitButton()
    {
        Application.Quit(); //ゲームを終了する
    }
}
