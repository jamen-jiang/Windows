﻿using System;
using Windows.Application.Shared.Enums;

namespace Windows.Application.Shared.Exception
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class ApiException : ApplicationException
    {
        private int _code;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code
        {
            get
            {
                return _code == 0 ? (int)ApiStatusEnum.Fail_App : _code;
            }
            set
            {
                _code = value;
            }
        }

        private string _message { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public override string Message
        {
            get
            {
                return string.IsNullOrEmpty(_message) ? base.Message : _message;
            }
        }
        /// <summary>
        /// 默认错误信息
        /// </summary>
        public ApiException() { }

        /// <summary>
        /// 错误代码
        /// </summary>
        /// <param name="code">代码</param>
        public ApiException(int code)
        {
            Code = code;
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        /// <param name="code">代码</param>
        public ApiException(ApiStatusEnum code)
        {
            Code = (int)code;
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public ApiException(string message)
        {
            _message = message;
        }
        /// <summary>
        /// 代码和信息
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="message">错误信息</param>
        public ApiException(int code, string message)
        {
            Code = code;
            _message = message;
        }
        /// <summary>
        /// 代码和信息
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="message">错误信息</param>
        public ApiException(ApiStatusEnum code, string message)
        {
            Code = (int)code;
            _message = message;
        }
    }
}
