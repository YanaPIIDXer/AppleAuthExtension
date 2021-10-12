using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AppleAuth;
using AppleAuth.Interfaces;

namespace AppleAuthExtension
{
    /// <summary>
    /// 認証エラーの例外
    /// </summary>
    public class AppleAuthErrorException : Exception
    {
        /// <summary>
        /// エラーインタフェース
        /// </summary>
        public IAppleError Error { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="error">エラーインタフェース</param>
        public AppleAuthErrorException(IAppleError error)
            : base("認証エラー")
        {
            Error = error;
        }
    }
}
