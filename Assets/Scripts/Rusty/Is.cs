using System;

public class Is<T>
{
	private T value;

	public static Is<T> Wrap(T newValue) {
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
}

