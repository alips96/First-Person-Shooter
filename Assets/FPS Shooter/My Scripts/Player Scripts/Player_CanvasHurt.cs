using UnityEngine;
using System.Collections;


namespace S3
{
public class Player_CanvasHurt : MonoBehaviour {
		public GameObject HurtCanvas;
		private Player_Master playerMaster;
		private float secondsTillHide = 2;

		void OnEnable(){
			SetInitialRefrences ();
			playerMaster.EventPlayerHealthDeduction += TurnOnHurtEffect;
		}

		void OnDisable(){
			playerMaster.EventPlayerHealthDeduction -= TurnOnHurtEffect;
		}

		void SetInitialRefrences(){
			playerMaster = GetComponent<Player_Master> ();
		}

		void TurnOnHurtEffect(int dummy) //dummy is not needed but we should pass on something so we pass this but will never use it
        {
			if (HurtCanvas != null) {
				StopAllCoroutines ();
				HurtCanvas.SetActive (true);
				StartCoroutine (ResetHealthCanvas ());
			}
		}

		IEnumerator ResetHealthCanvas(){
			yield return new WaitForSeconds (secondsTillHide);
			HurtCanvas.SetActive (false);
		}
}

}