using UnityEngine;
using UnityEngine.Networking;
using System;

namespace Rusty {
	public class RComponent
	{
		public static Option<T> GetL<T>(Is<GameObject> isObj, bool logCompNull) {
			return Rustify.NotNullL (isObj.Get ().GetComponent<T> (), logCompNull);
		}

		public static Option<T> GetL<T>(Option<GameObject> optObj, bool logCompNull) {
			if (optObj.IsSome ()) {
				return RComponent.GetL<T> (optObj.ToIs (), logCompNull);
			}
			return Option<T>.None ();
		}

		public static Option<T> GetL<T>(Is<NetworkInstanceId> isNetId, bool logObjNull, bool logCompNull) {
			return RComponent.GetL<T>(RGameObject.GetL (isNetId, logObjNull), logCompNull);
		}

		public static Option<T> GetL<T>(Option<NetworkInstanceId> optNetId, bool logObjNull, bool logCompNull) {
			return RComponent.GetL<T> (RGameObject.GetL(optNetId, logObjNull), logCompNull);
		}

		public static Option<T> Get<T>(Is<GameObject> isObj, bool logCompNull) {
			return GetL<T> (isObj, logCompNull);
		}

		public static Option<T> Get<T>(Option<GameObject> optObj, bool logCompNull) {
			return GetL<T> (optObj, logCompNull);
		}

		public static Option<T> Get<T>(Is<NetworkInstanceId> isNetId, bool logObjNull, bool logCompNull) {
			return GetL<T> (isNetId, logObjNull, logCompNull);
		}

		public static Option<T> Get<T>(Option<NetworkInstanceId> optNetId, bool logObjNull, bool logCompNull) {
			return GetL<T> (optNetId, logObjNull, logCompNull);
		}
	}
}

