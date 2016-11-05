using UnityEngine;
using UnityEngine.Networking;

namespace Rusty {
	public class RGameObject
	{
		public static Is<GameObject> GetL<T>(Is<T> isComp) where T: NetworkBehaviour {
			return Is<GameObject>.Wrap(isComp.Get ().gameObject);
		}

		public static Option<GameObject> GetL<T>(Option<T> optComp) where T: NetworkBehaviour {
			if (optComp.IsSome ()) {
				return RGameObject.GetL(optComp.ToIs()).ToOpt();
			}
			return None ();
		}

		public static Option<GameObject> GetL(Is<NetworkInstanceId> isNetId, bool logObjNull) {
			return Rustify.NotNullL (ClientScene.FindLocalObject(isNetId.Get ()), logObjNull);
		}

		public static Option<GameObject> GetL(Option<NetworkInstanceId> optNetId, bool logObjNull) {
			if (optNetId.IsSome ()) {
				return Rustify.NotNullL(ClientScene.FindLocalObject (optNetId.Unwrap ()), logObjNull);
			}
			return None ();
		}

		private static Option<GameObject> None() {
			return Option<GameObject>.None ();
		}

		public static Is<GameObject> Get<T>(Is<T> isComp) where T: NetworkBehaviour {
			return GetL (isComp);
		}

		public static Option<GameObject> Get<T>(Option<T> optComp) where T: NetworkBehaviour {
			return GetL (optComp);
		}

		public static Option<GameObject> Get(Is<NetworkInstanceId> isNetId, bool logObjNull = false) {
			return GetL (isNetId, logObjNull);
		}

		public static Option<GameObject> Get(Option<NetworkInstanceId> optNetId, bool logObjNull = false) {
			return GetL (optNetId, logObjNull);
		}
	}
}