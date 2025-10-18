using System;

namespace VariableVixen.WoLua.Lua.Api;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
internal class NoAutoDisposeAttribute: Attribute {
	public bool Value { get; init; }
	public NoAutoDisposeAttribute(bool value) => this.Value = value;
	public NoAutoDisposeAttribute() : this(true) { }
}
