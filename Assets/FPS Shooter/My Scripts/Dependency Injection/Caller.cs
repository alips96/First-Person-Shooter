using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace S3
{
    public class Caller : MonoBehaviour
    {
        [Inject]
        public void Print()
        {
            Debug.Log("Yes");
        }
    }
}

