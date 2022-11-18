using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Wave,Enemy関連
    [SerializeField, Header("現在のウェーブ数")] int _nowWave = 1;
    [SerializeField, Header("最大ウェーブ数")] int _maxWave = 0;
    [SerializeField,Header("残りのウェーブ数")] int _remainingWave = 0;
    GameObject[] _enemyArray;

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
    }

    void Update()
    {
        //それぞれの数値を更新していく
        OutputNowWave();//現在のWave出力
        OutputRemainingWave();//残りのWave出力
        OutputEnemyCount();//敵の数出力

        //Popするメニュー
        OnPopMenu();
        OnPopTips();

    }

    //現在のウェーブを出力する関数
    void OutputNowWave()
    {
        if (EnemyCount() == 0) //もし敵の数が０なら
        {
            _nowWave++; //ウェーブカウントアップ
        }
        _nowWaveText.text = "現在のWave数：" + _nowWave.ToString();  // Textに反映
    }

    //残りのウェーブを数えて出力する関数
    void OutputRemainingWave() 
    {
        _remainingWave = _maxWave;  //残りのウェーブの初期値を設定
        if (_remainingWave != 0) //残りが０出ないとき
        {
            _remainingWave -= _nowWave; //残りのウェーブを減らす
        }
        _remainingWaveText.text = "残りのウェーブ数：" + _remainingWave.ToString();  // Textに反映
    }

    //残りの敵の数を出力する関数
    void OutputEnemyCount() 
    {
        _enemyCountText.text = "残りの敵：" + EnemyCount().ToString(); // Textに反映
    }

    //敵を数えて数を返す関数
    int EnemyCount() 
    {
        //敵を配列に格納
        _enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        //要素数を返す
        return _enemyArray.Length;
    }

    //PopMenuを操作するための関数
    void OnPopMenu()
    {
        if (Input.GetKey(KeyCode.Escape)) //Escapeボタン押されたら
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
        if (Input.GetKeyDown(KeyCode.Tab)) //Tabボタンが押されている間
        {
            //PopMenu表示
            _popTips.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))  //Tabボタンを話したら
        {
            //PopMenu表示
            _popTips.SetActive(false);
        }
    }
}
