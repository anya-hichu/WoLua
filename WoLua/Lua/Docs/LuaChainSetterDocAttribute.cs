using System;

namespace VariableVixen.WoLua.Lua.Docs;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
internal class LuaChainSetterDocAttribute: LuaDocAttribute {
	public LuaChainSetterDocAttribute(string propName, string moreInfo = "") {
		this.Description = $"This function sets the `.{propName}` property, then returns this object to allow for chaining. {moreInfo}".Trim();
	}
}
