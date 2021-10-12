using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using AppleAuth;
using AppleAuth.Interfaces;
using System.Threading;
using AppleAuth.Enums;
using AppleAuth.Native;

namespace AppleAuthExtension
{
    /// <summary>
    /// Apple認証
    /// </summary>
    public class AppleAuthenticate : MonoBehaviour
    {
        /// <summary>
        /// CompletionSource
        /// </summary>
        private UniTaskCompletionSource<ICredential> completionSource = null;

        /// <summary>
        /// 認証マネージャ
        /// </summary>
        private AppleAuthManager authManager = null;

        /// <summary>
        /// サインイン
        /// </summary>
        /// <param name="logInOptions">ログインオプション</param>
        /// <param name="nonce">ノンス</param>
        /// <param name="token">CancellationToken</param>
        /// <returns>ICredential</returns>
        public static UniTask<ICredential> SignIn(LoginOptions logInOptions, string nonce = null, CancellationToken token = default)
        {
            if (!AppleAuthManager.IsCurrentPlatformSupported) { throw new AppleAuthNotSupportedException(); }

            GameObject obj = new GameObject("AppleAuth");
            var auth = obj.AddComponent<AppleAuthenticate>();

            auth.completionSource = new UniTaskCompletionSource<ICredential>();
            auth.authManager = new AppleAuthManager(new PayloadDeserializer());
            auth.authManager.LoginWithAppleId(new AppleAuthLoginArgs(logInOptions, nonce), credential =>
            {
                auth.completionSource.TrySetResult(credential);
                Destroy(obj);
            },
            error =>
            {
                AppleAuthErrorException exception = new AppleAuthErrorException(error);
                auth.completionSource.TrySetException(exception);
                Destroy(obj);
                throw exception;
            });
            return auth.completionSource.Task;
        }

        void Update()
        {
            if (authManager != null)
            {
                authManager.Update();
            }
        }
    }
}

