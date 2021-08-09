// We can not type the global variables in the inspector! so we need to assign something to them


using UnityEngine;
using System.Collections;


namespace S3
{
    public class GameManager_Refrences : MonoBehaviour
    {
        public string playerTag;
        public static string _playerTag;

        public string enemyTag;
        public static string _enemyTag;

        public static GameObject _player;

        void OnEnable()
        {
            if (playerTag == "")
            {
                Debug.LogWarning("Please type in the name of the player tag in the GameManager_Refrences " +
                 "slot in the inspector or the GTGD S3 system will not work");
            }

            if (enemyTag == "")
            {
                Debug.LogWarning("Please type in the name of the enemy tag in the GameManager_Refrences " +
                 "slot in the inspector or the GTGD S3 system will not work");
            }

            _playerTag = playerTag;
            _enemyTag = enemyTag;
            _player = GameObject.FindGameObjectWithTag(_playerTag);
        }
    }

}