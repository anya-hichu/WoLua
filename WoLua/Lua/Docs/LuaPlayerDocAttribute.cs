using System;
using System.Collections.Generic;
using System.Linq;

namespace WoLua.Lua.Docs;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
internal partial class LuaPlayerDocAttribute: LuaDocAttribute {
	public LuaPlayerDocAttribute(params string[] help) {
		IEnumerable<string> lines = CleanLines(help);
		this.Description = CleanJoin(lines.Append("If the `Loaded` property is `false`, this returns `nil`."));
	}
}
