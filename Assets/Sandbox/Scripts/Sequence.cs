using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppleAuth.Enums;
using AppleAuthExtension;
using Cysharp.Threading.Tasks;
using AppleAuth.Interfaces;

/// <summary>
/// シーケンス
/// </summary>
public class Sequence : MonoBehaviour
{
    /// <summary>
    /// メイン画面
    /// </summary>
    [SerializeField]
    private MainScreen mainScreen = null;

    void Awake()
    {
        mainScreen.OnAuth = async () =>
        {
            try
            {
                var result = await AppleAuthExtension.AppleAuth.SignIn(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName, null, this.GetCancellationTokenOnDestroy());
                var credential = result as IAppleIDCredential;
                mainScreen.SetResultText(credential.IdentityToken.ToString());
            }
            catch (AppleAuthErrorException e)
            {
                mainScreen.SetResultText(e.Error.LocalizedDescription);
            }
            catch (AppleAuthNotSupportedException)
            {
                mainScreen.SetResultText("未対応");
            }
        };
    }
}
