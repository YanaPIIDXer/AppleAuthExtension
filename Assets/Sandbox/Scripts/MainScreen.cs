using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// メイン画面
/// </summary>
public class MainScreen : MonoBehaviour
{
    /// <summary>
    /// 結果のテキスト
    /// </summary>
    [SerializeField]
    private Text resultText = null;

    /// <summary>
    /// 認証ボタン
    /// </summary>
    [SerializeField]
    private Button authButton = null;

    /// <summary>
    /// 認証ボタンが押された
    /// </summary>
    public Action OnAuth { set; private get; }

    /// <summary>
    /// 結果のテキストを設定
    /// </summary>
    /// <param name="text">テキスト</param>
    public void SetResultText(string text)
    {
        resultText.text = text;
        authButton.interactable = true;
    }

    /// <summary>
    /// 認証ボタンが押された
    /// </summary>
    public void OnClickAuthButton()
    {
        authButton.interactable = false;
        OnAuth?.Invoke();
    }
}
