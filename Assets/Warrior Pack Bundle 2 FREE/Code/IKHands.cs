using UnityEngine;
using System.Collections;

public class IKHands : MonoBehaviour {

	public Transform leftHandObj;
	public Transform rightHandObj;
	public Transform attachLeft;
	public Transform attachRight;

	public float leftHandPositionWeight;
	public float leftHandRotationWeight;
	public float rightHandPositionWeight;
	public float rightHandRotationWeight;
	
	private Animator animator;
	
	void Start() {
		animator = this.gameObject.GetComponent<Animator>();
	}
	
	void OnAnimatorIK(int layerIndex) {
		if(rightHandObj != null){
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand,rightHandPositionWeight);
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand,rightHandRotationWeight);     
			animator.SetIKPosition(AvatarIKGoal.RightHand,attachRight.position);                    
			animator.SetIKRotation(AvatarIKGoal.RightHand,attachRight.rotation);
		}
	}
}