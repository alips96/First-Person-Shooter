using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace S3
{
    public class GameManager_GoToMenuScene : MonoBehaviour
    {
        private GameManager_Master GameManagerMaster;

        void OnEnable()
        {
            SetInitialRefrences();
            GameManagerMaster.GoToMenuSceneEvent += GoToMenuScene;
        }

        void OnDisable()
        {
            GameManagerMaster.GoToMenuSceneEvent -= GoToMenuScene;
        }

        void SetInitialRefrences()
        {
            GameManagerMaster = GetComponent<GameManager_Master>();
        }

        void GoToMenuScene()
        {
            //Application.LoadLevel(0);
            SceneManager.LoadScene(1);
        }
    }

}
