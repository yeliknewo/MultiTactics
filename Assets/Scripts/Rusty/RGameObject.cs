using UnityEngine;
using UnityEngine.Networking;

namespace Rusty {
	public class RGameObject
	{
		public static Vital<GameObject> GetL<T>(Vital<T> vitComp) where T: NetworkBehaviour {
			return Rustify.ToVital(vitComp.Get ().gameObject);
		}

		public static Option<GameObject> GetL<T>(Option<T> optComp) where T: NetworkBehaviour {
			if (optComp.IsSome ()) {
				return RGameObject.GetL(optComp.ToVital()).ToOpt();
			}
			return None ();
		}

		public static Option<GameObject> GetL(Vital<NetworkInstanceId> vitNetId, bool logObjNull) {
			return Rustify.NotNullL (ClientScene.FindLocalObject(vitNetId.Get ()), logObjNull);
		}

		public static Option<GameObject> GetL(Option<NetworkInstanceId> optNetId, bool logObjNull) {
			if (optNetId.IsSome ()) {
				return Rustify.NotNullL(ClientScene.FindLocalObject (optNetId.Unwrap ()), logObjNull);
			}
			return None ();
		}

		public static Vital<GameObject> InstantiateL(Vital<GameObject> vitObjPrefab) {
			return Rustify.ToVital (GameObject.Instantiate<GameObject> (vitObjPrefab.Get()));
		}

		public static Option<GameObject> InstantiateL(Option<GameObject> optObjPrefab) {
			if (optObjPrefab.IsSome ()) {
				return InstantiateL (optObjPrefab.ToVital ()).ToOpt();
			}
			return None ();
		}

		private static Option<GameObject> None() {
			return Rustify.None<GameObject>();
		}

		public static Vital<GameObject> Get<T>(Vital<T> vitComp) where T: NetworkBehaviour {
			return GetL (vitComp);
		}

		public static Option<GameObject> Get<T>(Option<T> optComp) where T: NetworkBehaviour {
			return GetL (optComp);
		}

		public static Option<GameObject> Get(Vital<NetworkInstanceId> vitNetId, bool logObjNull = true) {
			return GetL (vitNetId, logObjNull);
		}

		public static Option<GameObject> Get(Option<NetworkInstanceId> optNetId, bool logObjNull = true) {
			return GetL (optNetId, logObjNull);
		}

		public static Vital<GameObject> Instantiate(Vital<GameObject> vitObjPrefab) {
			return RGameObject.InstantiateL (vitObjPrefab);
		}
	}
}