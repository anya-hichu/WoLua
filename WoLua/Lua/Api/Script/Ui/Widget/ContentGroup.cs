using System.Collections.Generic;

using MoonSharp.Interpreter;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class ContentGroup: IDisplayWidget { // TODO: luadocs
	private readonly List<IDisplayWidget> content = [];

	public ContentGroup Add(DynValue content) {
		this.content.Add(ScriptWidgetApi.FromLuaValue(content));
		return this;
	}
	
	[MoonSharpHidden]
	public void Render(ScriptContainer script) {
		foreach (IDisplayWidget widget in this.content.ToArray())
			widget.Render(script);
	}
}
