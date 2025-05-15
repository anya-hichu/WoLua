using System;

namespace WoLua.Lua.Docs;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
internal class AsLuaTypeAttribute(string luaType): Attribute {
	public string LuaName { get; } = luaType;

	public AsLuaTypeAttribute(LuaType luaType) : this(luaType.LuaName()) { }
}
