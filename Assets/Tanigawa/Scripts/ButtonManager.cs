using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] string _sceneName = "Title";
    public void OnRestartButton()
    {
        Debug.Log("���X�^�[�g�{�^����������܂����B");
        SceneManager.LoadScene(_sceneName);//�^�C�g���܂���main�V�[���̖��O������
    }

    //�Q�[�����I�����邽�߂̊֐�
    public void OnQuitButton()
    {
        Application.Quit(); //�Q�[�����I������
    }
}
