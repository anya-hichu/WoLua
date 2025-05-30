using System;

namespace WoLua.Lua.Docs;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
internal class SkipDocAttribute: Attribute {
	public SkipDocAttribute(string justification) => _ = justification;
}
