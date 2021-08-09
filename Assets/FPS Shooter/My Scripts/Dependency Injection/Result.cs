using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace S3
{
    public class Result : MonoBehaviour
    {
        private Caller caller;

        [Inject]
        public void Init(Caller _caller)
        {
            caller = _caller;
        }


    }
}

