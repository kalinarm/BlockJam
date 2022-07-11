using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class Transaction
    {
        bool success = false;
        string error;

        public bool IsSuccess { get => success;}
        public string Error { get => error; }

        public Transaction()
        {
            success = false;
        }

        public void Success()
        {
            success = true;
        }
        public void Fail(string errorMessage)
        {
            error = errorMessage;
        }
    }
}

