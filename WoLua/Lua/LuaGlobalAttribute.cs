using System;

namespace VariableVixen.WoLua.Lua;

[AttributeUsage(AttributeTargets.Property)]
internal class LuaGlobalAttribute: Attribute {
	public readonly string Name;
	public LuaGlobalAttribute(string name) => this.Name = name;
}
