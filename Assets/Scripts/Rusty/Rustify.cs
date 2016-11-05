using UnityEngine;
using UnityEngine.Networking;
using System;

namespace Rusty {
	public class Rustify
	{
		public static Option<T> NotNullL<T>(T obj, bool logObjNull) {
			if (obj == null) {
				if (logObjNull) {
					Debug.LogError ("Obj is null");
				}
				return Option<T>.None();
			}
			return Option<T>.Some(obj);
		}

		public static Option<NetworkInstanceId> NetIdL(NetworkInstanceId netId, bool logInvalid, bool logEmpty) {
			if (netId == NetworkInstanceId.Invalid) {
				if (logInvalid) {
					Debug.LogError ("NetId is Invalid");
				}
				return Option<NetworkInstanceId>.None();
			}
			if (netId.IsEmpty ()) {
				if (logEmpty) {
					Debug.LogError ("NetId is Empty");
				}
				return Option<NetworkInstanceId>.None();
			}
			return Option<NetworkInstanceId>.Some(netId);
		}

		public static Option<T> NotNull<T>(T obj, bool logObjNull = true) {
			return NotNullL (obj, logObjNull);
		}

		public static Option<NetworkInstanceId> NetId(NetworkInstanceId netId, bool logInvalid = true, bool logEmpty = true) {
			return NetIdL (netId, logInvalid, logEmpty);
		}
	}

	public class Is<T>
	{
		private T value;

		internal static Is<T> Wrap(T newValue) {
			return new Is<T> (newValue);
		}

		private Is (T newValue)
		{
			if (newValue == null) {
				throw new Exception ("Is was passed null value");
			}
			this.value = newValue;
		}

		public T Get() {
			return this.value;
		}

		public Option<T> ToOpt() {
			return Option<T>.Some (value);
		}
	}

	public class Option<T>
	{
		private T value;
		private bool some;

		internal static Option<T> None() {
			return new Option<T> ();
		}

		internal static Option<T> Some(T newValue) {
			return new Option<T> (newValue);
		}

		private Option()
		{
			this.some = false;
		}

		private Option(T newValue) {
			if (newValue.Equals(null)) {
				throw new Exception ("Passed Null Value to Option");
			}
			this.value = newValue;
			this.some = true;
		}

		public bool IsSome() {
			return this.some;
		}

		public bool IsNone() {
			return this.some;
		}

		public T Unwrap() {
			if (this.IsSome ()) {
				return this.value;
			} else {
				throw new Exception ("Unwrapped a None");
			}
		}

		public Is<T> ToIs() {
			if (this.IsSome ()) {
				return Is<T>.Wrap (this.value);
			} else {
				throw new Exception ("ToIs value was none");
			}
		}
	}

}
