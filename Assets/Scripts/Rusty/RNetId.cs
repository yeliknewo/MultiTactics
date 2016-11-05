using UnityEngine;
using UnityEngine.Networking;

namespace Rusty {
	public class RNetId
	{
		public static Option<NetworkInstanceId> GetL(Is<NetworkIdentity> isNetIdent, bool logInvalid, bool logEmpty) {
			return Rustify.NetIdL (isNetIdent.Get ().netId, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> GetL(Option<NetworkIdentity> optNetIdent, bool logInvalid, bool logEmpty) {
			if (optNetIdent.IsSome ()) {
				return RNetId.GetL(optNetIdent.ToIs(), logInvalid, logEmpty);
			}
			return None ();
		}

		public static Option<NetworkInstanceId> GetL(Is<GameObject> isObj, bool logCompNull, bool logInvalid, bool logEmpty) {
			return RNetId.GetL(RComponent.GetL<NetworkIdentity>(isObj, logCompNull), logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> GetL(Option<GameObject> optObj, bool logCompNull, bool logInvalid, bool logEmpty) {
			if (optObj.IsSome ()) {
				return RNetId.GetL (RComponent.GetL<NetworkIdentity> (optObj.ToIs (), logCompNull), logInvalid, logEmpty);
			}
			return None ();
		}

		private static Option<NetworkInstanceId> None() {
			return Option<NetworkInstanceId>.None ();
		}

		public static Option<NetworkInstanceId> Get(Is<NetworkIdentity> isNetIdent, bool logInvalid = false, bool logEmpty = false) {
			return GetL (isNetIdent, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get(Option<NetworkIdentity> optNetIdent, bool logInvalid = false, bool logEmpty = false) {
			return GetL (optNetIdent, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get(Is<GameObject> isObj, bool logCompNull = false, bool logInvalid = false, bool logEmpty = false) {
			return GetL (isObj, logCompNull, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get(Option<GameObject> optObj, bool logCompNull = false, bool logInvalid = false, bool logEmpty = false) {
			return GetL (optObj, logCompNull, logInvalid, logEmpty);
		}
	}
}