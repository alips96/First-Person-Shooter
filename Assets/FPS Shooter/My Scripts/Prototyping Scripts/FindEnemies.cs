using UnityEngine;
using System.Collections;

namespace Chapter1
{

    public class FindEnemies : MonoBehaviour
    {
        GameObject[] enemies;

        // Use this for initialization
        void Start()
        {
            SearchForEnemies();
        }

        void SearchForEnemies()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                foreach (GameObject go in enemies)
                {
                    Debug.Log(go.transform.name);
                    go.SetActive(false);
                }
            }
        }
    }
}