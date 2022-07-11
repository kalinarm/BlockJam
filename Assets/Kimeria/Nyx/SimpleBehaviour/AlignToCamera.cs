using UnityEngine;
using System.Collections;

namespace Kimeria.Nyx.SimpleBehaviours
{
	public class AlignToCamera : MonoBehaviour
	{
		public bool invert = false;
		// Use this for initialization
		void Awake()
		{
			apply();
		}

		// Update is called once per frame
		void Update()
		{
			apply();
		}

		void apply()
		{
			if (invert)
			{
				Vector3 p = Camera.main.transform.position - transform.position;
				transform.LookAt(transform.position - p, transform.up);
			}
			else
			{
				transform.LookAt(Camera.main.transform, transform.up);
			}
		}
	}
}

