using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AppleAuthExtension
{
    /// <summary>
    /// サポートされていないプラットフォームで実行しようとした時に投げられる例外
    /// </summary>
    public class AppleAuthNotSupportedException : Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AppleAuthNotSupportedException()
            : base("このプラットフォームはサポートされていません")
        {
        }
    }
}
