using System.Collections.Generic;
using UnityEngine;
using VDFramework;

namespace Utility
{
	public class EnsureSingleInstance : BetterMonoBehaviour
	{
		public static List<int> loadedGUIDs = new List<int>();

		[SerializeField]
		private int guid;

		[SerializeField]
		private GameObject singleInstanceParent;

		private bool isValid = false;

		private void Awake()
		{
			if (loadedGUIDs.Contains(guid))
			{
				Destroy(gameObject);
				return;
			}
			
			loadedGUIDs.Add(guid);
			isValid = true;

			if (transform.childCount > 0)
			{
				transform.GetChild(0).gameObject.SetActive(true);
			}
			else if (singleInstanceParent != null)
			{
				singleInstanceParent.SetActive(true);
			}
		}

		private void OnDestroy()
		{
			if (isValid)
			{
				loadedGUIDs.Remove(guid);
			}
		}
	}
}