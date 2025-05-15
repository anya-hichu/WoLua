using System;

namespace WoLua.Lua;

[AttributeUsage(AttributeTargets.Property)]
internal class LuaGlobalAttribute(string name): Attribute {
	public readonly string Name = name;
}
