using System;

namespace WoLua.Lua.Api;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
internal class WipeOnDisposeAttribute(bool value): Attribute {
	public bool Value { get; init; } = value;

	public WipeOnDisposeAttribute() : this(true) { }
}
