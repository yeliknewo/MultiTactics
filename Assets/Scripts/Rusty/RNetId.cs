using UnityEngine;
using UnityEngine.Networking;

namespace Rusty {
	public class RNetId
	{
		public static Option<NetworkInstanceId> GetL(Vital<NetworkIdentity> vitNetIdent, bool logInvalid, bool logEmpty) {
			return Rustify.NetIdL (vitNetIdent.Get ().netId, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> GetL(Option<NetworkIdentity> optNetIdent, bool logInvalid, bool logEmpty) {
			if (optNetIdent.IsSome ()) {
				return Rustify.NetIdL (optNetIdent.Unwrap ().netId, logInvalid, logEmpty);
			}
			return Rustify.NoneL<NetworkInstanceId> ();
		}

		public static Option<NetworkInstanceId> GetL<T>(Vital<T> vitComp, bool logInvalid, bool logEmpty) where T: NetworkBehaviour {
			return Rustify.NetIdL (vitComp.Get ().netId, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> GetL<T>(Option<T> optComp, bool logInvalid, bool logEmpty) where T: NetworkBehaviour {
			if (optComp.IsSome ()) {
				return RNetId.GetL(optComp.ToVital(), logInvalid, logEmpty);
			}
			return None ();
		}

		public static Option<NetworkInstanceId> GetL(Vital<GameObject> vitObj, bool logCompNull, bool logInvalid, bool logEmpty) {
			return RNetId.GetL(RComponent.GetL<NetworkIdentity>(vitObj, logCompNull), logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> GetL(Option<GameObject> optObj, bool logCompNull, bool logInvalid, bool logEmpty) {
			if (optObj.IsSome ()) {
				return RNetId.GetL (RComponent.GetL<NetworkIdentity> (optObj.ToVital (), logCompNull), logInvalid, logEmpty);
			}
			return None ();
		}

		private static Option<NetworkInstanceId> None() {
			return Rustify.None<NetworkInstanceId> ();
		}

		public static Option<NetworkInstanceId> Get(Vital<NetworkIdentity> vitNetIdent, bool logInvalid = true, bool logEmpty = true) {
			return GetL (vitNetIdent, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get(Option<NetworkIdentity> optNetIdent, bool logInvalid = true, bool logEmpty = true) {
			return GetL (optNetIdent, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get<T>(Vital<T> vitComp, bool logInvalid = true, bool logEmpty = true) where T: NetworkBehaviour {
			return GetL (vitComp, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get<T>(Option<T> optComp, bool logInvalid = true, bool logEmpty = true) where T: NetworkBehaviour {
			return GetL (optComp, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get(Vital<GameObject> vitObj, bool logCompNull = true, bool logInvalid = true, bool logEmpty = true) {
			return GetL (vitObj, logCompNull, logInvalid, logEmpty);
		}

		public static Option<NetworkInstanceId> Get(Option<GameObject> optObj, bool logCompNull = true, bool logInvalid = true, bool logEmpty = true) {
			return GetL (optObj, logCompNull, logInvalid, logEmpty);
		}
	}
}