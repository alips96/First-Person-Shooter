using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Chapter1
{
    public class Welcome : MonoBehaviour
    {
        public string welcomeMessage = "Welcome!!!";
        public string welcomeMessage2 = "Welcome again!";
        private Text WelcomeText;
        private Text WelcomeText2;//the seccond one for more practice!
        public GameObject CanvasContoller;
        //private float DelayTime = 3.5f;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrence();
            WelcomeMEssage();
        }

        void WelcomeMEssage()
        {
            if (WelcomeText != null)
                WelcomeText.text = welcomeMessage;
            else
                Debug.LogWarning("there is no uI text assigned to the welcomeText");

            if (WelcomeText2 != null)
                WelcomeText2.text = welcomeMessage2;
            else
                Debug.LogWarning("there is no uI text assigned to the welcomeText2");

            StartCoroutine(DissapearCanvas(3.5f));
        }

        void SetInitialRefrence()
        {
            WelcomeText = GameObject.Find("TextWelcome").GetComponent<Text>();
            WelcomeText2 = GameObject.Find("TextWelcome2").GetComponent<Text>();
            //CanvasContoller = GameObject.Find("Canvas");
        }

        IEnumerator DissapearCanvas(float wait)
        {
            yield return new WaitForSeconds(wait);
            if (CanvasContoller != null)
                CanvasContoller.SetActive(false);
            else
                Debug.LogWarning("Nothing has been assinged to the canvas controller");
        }
    }
}