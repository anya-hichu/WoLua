using System;

namespace VariableVixen.WoLua.Lua.Docs;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
internal class LuaMethodAliasAttribute: LuaDocAttribute {
	public LuaMethodAliasAttribute(string methodName) {
		this.Description = $"This is an alias for `.{methodName}()`.";
	}
}
