using CMF;
using UnityEngine;
using VDFramework;

namespace PlayerControls
{
	public class DisableColliderOnDodge : BetterMonoBehaviour
	{
		[SerializeField]
		private Collider[] colliders;
		
		private Controller controller;

		private void Awake()
		{
			controller = GetComponent<Controller>();
		}

		private void OnEnable()
		{
			controller.OnDodge    += DisableCollider;
			controller.OnDodgeEnd += EnableCollider;
		}

		private void OnDisable()
		{
			controller.OnDodge    -= DisableCollider;
			controller.OnDodgeEnd -= EnableCollider;
		}

		private void DisableCollider()
		{
			foreach (Collider colliderObject in colliders)
			{
				colliderObject.enabled = false;
			}
		}

		private void EnableCollider()
		{
			foreach (Collider colliderObject in colliders)
			{
				colliderObject.enabled = true;
			}
		}
	}
}