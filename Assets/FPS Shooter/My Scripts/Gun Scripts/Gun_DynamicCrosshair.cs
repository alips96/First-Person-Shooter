using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_DynamicCrosshair : MonoBehaviour
    {
        private Gun_Master gunMaster;
        public Transform canvasDynamicCrosshair;
        private Transform playerTransform;
        private Transform weaponCamera;
        private float playerSpeed;
        private float nextCaptureTime;
        private float captureInterval = 0.5f;
        private Vector3 lastPosition;
        public Animator crosshairAnimator;
        public string weaponCameraName;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CapturePlayerSpeed();
            ApplySpeedToAnimation();
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();
            playerTransform = GameManager_Refrences._player.transform;
            FindWeaponCamera(playerTransform);
            SetCameraOnDynamicCrosshairCanvas();
            SetPlaneDistanceOnDynamicCrosshairCanvas();
        }

        void CapturePlayerSpeed()
        {
            if(Time.time > nextCaptureTime)
            {
                nextCaptureTime = Time.time + captureInterval;
                playerSpeed = (playerTransform.position - lastPosition).magnitude / captureInterval; //magnitute turns the distance in vector3 into a float number
                lastPosition = playerTransform.position;
                gunMaster.CallEventSpeedCaprtured(playerSpeed);
            }
        }

        void ApplySpeedToAnimation()
        {
            if (crosshairAnimator != null)
                crosshairAnimator.SetFloat("Speed", playerSpeed);
        }

        void FindWeaponCamera(Transform transformToSearchThrough)
        {
            //We have already set up the rifle correctly but it is for the other items to be set automatically
            if(transformToSearchThrough != null)
            {
                if(transformToSearchThrough.name == weaponCameraName)
                {
                    weaponCamera = transformToSearchThrough;
                    return; //when we found that we wont need to check any more
                }

                foreach (Transform child in transformToSearchThrough)
                {
                    FindWeaponCamera(child);//it will case the whole tree to be setup
                }
            }
        }

        void SetCameraOnDynamicCrosshairCanvas()
        {
            if(canvasDynamicCrosshair != null && weaponCamera != null)
            {
                canvasDynamicCrosshair.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                canvasDynamicCrosshair.GetComponent<Canvas>().worldCamera = weaponCamera.GetComponent<Camera>();
            }
        }

        void SetPlaneDistanceOnDynamicCrosshairCanvas()
        {
            if (canvasDynamicCrosshair != null)
                canvasDynamicCrosshair.GetComponent<Canvas>().planeDistance = 1;
        }
    }
}

