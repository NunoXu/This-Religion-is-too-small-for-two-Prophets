using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Player1 : Player
    {
        
        void Update()
        {
            //Get input from controls
            float z = Input.GetAxisRaw("Horizontal");
            float x = -(Input.GetAxisRaw("Vertical"));
            inputVec = new Vector3(x, 0, z);

            //Apply inputs to animator
            animator.SetFloat("Input X", z);
            animator.SetFloat("Input Z", -(x));

            if (x != 0 || z != 0)  //if there is some input
            {
                //set that character is moving
                animator.SetBool("Moving", true);
                animator.SetBool("Running", true);
            }
            else
            {
                //character is not moving
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Attack1Trigger");
                StartCoroutine(COStunPause(1.2f));
            }

            if (Input.GetButtonDown("MeteorSpell1"))
            {
                invokers[0].Invoke();
            }
            if (Input.GetButtonDown("HeatWaveSpell1"))
            {
                invokers[1].Invoke();
            }
            if (Input.GetButtonDown("ThunderSpell1"))
            {
                invokers[2].Invoke();
            }
            UpdateMovement();  //update character position and facing
        }

        public IEnumerator COStunPause(float pauseTime)
        {
            yield return new WaitForSeconds(pauseTime);
        }

        //converts control input vectors into camera facing vectors
        void GetCameraRelativeMovement()
        {
            Transform cameraTransform = Camera.main.transform;

            // Forward vector relative to the camera along the x-z plane   
            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;

            // Right vector relative to the camera
            // Always orthogonal to the forward vector
            Vector3 right = new Vector3(forward.z, 0, -forward.x);

            //directional inputs
            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");

            // Target direction relative to the camera
            targetDirection = h * right + v * forward;
            if (hasSacrifice)
            {
                animator.speed = 1 - CurrentAnimal().Weight() ;
            }
            else
            {
                animator.speed = 1;
            }
        }

        //face character along input direction
        void RotateTowardMovementDirection()
        {
            if (inputVec != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
            }
        }
        
                void UpdateMovement()
                {
                    //get movement input from controls
                    Vector3 motion = inputVec;
                    //reduce input for diagonal movement
                    motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? .7f : 1;

                    RotateTowardMovementDirection();
                    GetCameraRelativeMovement();
                }
        
    }
}
