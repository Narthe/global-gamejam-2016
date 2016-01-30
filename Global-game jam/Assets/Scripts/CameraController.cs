using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {

        private Camera _camera;
        public Transform startingTarget;
        public float delayTracking;

        float TARGET_WIDTH = 1920f;
        float TARGET_HEIGHT = 1080f;
        int PIXELS_TO_UNITS = 30; // 1:1 ratio of pixels to units


        //Object to follow
        private Transform target;


        //Camera position
        private float xPos;
        private float yPos;
        private float zPos;

        void Start () {
            target = startingTarget;
            _camera = GetComponent<Camera>();

            xPos = transform.position.x;
            yPos = transform.position.y;
            zPos = transform.position.z;
        }
	
        // Update is called once per frame
        void Update () {

            //Define new position
            xPos = target.position.x;
            yPos = target.position.y;
           
            Vector3 newPosition = new Vector3(xPos, yPos, zPos);

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * delayTracking);

            UpdateCamera();
        }

        private void UpdateCamera()
        {
            float desiredRatio = TARGET_WIDTH / TARGET_HEIGHT;
            float currentRatio = (float)Screen.width / (float)Screen.height;
            if (currentRatio >= desiredRatio)
            {
                // Our resolution has plenty of width, so we just need to use the height to determine the camera size
                _camera.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS;
            }
            else
            {
                // Our camera needs to zoom out further than just fitting in the height of the image.
                // Determine how much bigger it needs to be, then apply that to our original algorithm.
                float differenceInSize = desiredRatio / currentRatio;
                _camera.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS * differenceInSize;
            }
        }

        public void RedefineTarget(Transform target){
            this.target = target;
        }

        public void RedefineTarget(string targetName){
            this.target = GameObject.Find(targetName).transform;
        }
    }
}
