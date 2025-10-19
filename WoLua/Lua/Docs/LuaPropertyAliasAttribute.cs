using System;

namespace VariableVixen.WoLua.Lua.Docs;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
internal class LuaPropertyAliasAttribute: LuaDocAttribute {
	public LuaPropertyAliasAttribute(string propName) {
		this.Description = $"This is an alias for `.{propName}`.";
	}
}
