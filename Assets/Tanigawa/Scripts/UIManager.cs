using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Wave,Enemy関連
    //WaveプロパティでWaveの値のやり取りをする
    int _nowWave = 0;
    public int NowWave //Waveプロパティ
    {
        get => _nowWave;
        private set => _nowWave = value;
    }
    [Header("最大ウェーブ数")] public int _maxWave = 0;
    int _remainingWave = 0; //残りのWave数
    GameObject[] _enemyArray;   //敵を入れる配列

    //Text関連
    [SerializeField, Header("現在のウェーブのテキスト")] Text _nowWaveText;
    [SerializeField, Header("残りのウェーブ数のテキスト")] Text _remainingWaveText;
    [SerializeField, Header("残りの敵の数のテキスト")] Text _enemyCountText;

    //ポップメニュー関連
    [SerializeField, Header("ポップメニューのオブジェクト")] GameObject _popMenu;
    [SerializeField, Header("ポップTipsのオブジェクト")] GameObject _popTips;
    bool _onPopMenu;

    void Start()
    {
        //PopMenu 非表示
        _popMenu.SetActive(false);
        _onPopMenu = false;
        //PopTips 非表示
        _popTips.SetActive(false);
    }

    void Update()
    {
        //Popするメニュー
        OnPopMenu();
        OnPopTips();
    }

    //現在のウェーブを出力する関数
    public void OutputNowWave()
    {
        if (EnemyCount() == 0) //もし敵の数が０なら
        {
           　NowWave++; //ウェーブカウントアップ
        }
        _nowWaveText.text = "現在のWave：" + NowWave.ToString();  // Textに反映
    }

    //残りのウェーブを数えて出力する関数
    public void OutputRemainingWave() 
    {
        _remainingWave = _maxWave;  //残りのウェーブの初期値を設定
        if (_remainingWave != 0) //残りが０出ないとき
        {
            _remainingWave -= NowWave; //残りのウェーブを減らす
        }
        _remainingWaveText.text = "残りのWave：" + _remainingWave.ToString();  // Textに反映
    }

    //残りの敵の数を出力する関数
    public void OutputEnemyCount() 
    {
        _enemyCountText.text = "敵：" + EnemyCount().ToString(); // Textに反映
    }

    //敵を数えて数を返す関数
    public int EnemyCount() 
    {
        //敵を配列に格納
        _enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        //要素数を返す
        return _enemyArray.Length;
    }

    //PopMenuを操作するための関数
    void OnPopMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Escapeボタン押されたら
        {
            if (!_onPopMenu)    //bool型_onPopMeneがfalseなら
            {
                //PopMenu表示
                _popMenu.SetActive(true);
                _onPopMenu = true;
            }
            else
            {
                //PopMenu非表示
                _popMenu.SetActive(false);
                _onPopMenu = false;
            }
        }
    }

    //PopTipsを操作するための関数
    void OnPopTips() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Spaseボタンが押されている間
        {
            //PopMenu表示
            _popTips.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))  //Spaseボタンを話したら
        {
            //PopMenu表示
            _popTips.SetActive(false);
        }
    }
}
