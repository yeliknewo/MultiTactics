using System;

public class Option<T>
{
	private T value;
	private bool some;

	public static Option<T> None() {
		return new Option<T> ();
	}

	public static Option<T> Some(T newValue) {
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

