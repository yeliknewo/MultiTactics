using UnityEngine;
using UnityEngine.Networking;

namespace Rusty {
	public class RComponent
	{
		public static Option<T> GetL<T>(Vital<GameObject> vitObj, bool logCompNull) {
			return Rustify.NotNullL (vitObj.Get ().GetComponent<T> (), logCompNull);
		}

		public static Option<T> GetL<T>(Option<GameObject> optObj, bool logCompNull) {
			if (optObj.IsSome ()) {
				return RComponent.GetL<T> (optObj.ToVital (), logCompNull);
			}
			return Rustify.None<T>();
		}

		public static Option<T> GetL<T>(Vital<NetworkInstanceId> vitNetId, bool logObjNull, bool logCompNull) {
			return RComponent.GetL<T>(RGameObject.GetL (vitNetId, logObjNull), logCompNull);
		}

		public static Option<T> GetL<T>(Option<NetworkInstanceId> optNetId, bool logObjNull, bool logCompNull) {
			return RComponent.GetL<T> (RGameObject.GetL(optNetId, logObjNull), logCompNull);
		}

		public static Option<T> FindL<T>(bool logObjNull) where T: NetworkBehaviour {
			return Rustify.NotNullL (Object.FindObjectOfType<T>(), logObjNull);
		}

		public static Option<T> Get<T>(Vital<GameObject> vitObj, bool logCompNull = true) {
			return GetL<T> (vitObj, logCompNull);
		}

		public static Option<T> Get<T>(Option<GameObject> optObj, bool logCompNull = true) {
			return GetL<T> (optObj, logCompNull);
		}

		public static Option<T> Get<T>(Vital<NetworkInstanceId> vitNetId, bool logObjNull = true, bool logCompNull = true) {
			return GetL<T> (vitNetId, logObjNull, logCompNull);
		}

		public static Option<T> Get<T>(Option<NetworkInstanceId> optNetId, bool logObjNull = true, bool logCompNull = true) {
			return GetL<T> (optNetId, logObjNull, logCompNull);
		}

		public static Option<T> Find<T>(bool logObjNull = true) where T: NetworkBehaviour {
			return FindL<T> (logObjNull);
		}
	}
}

