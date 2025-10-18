using System.Collections.Generic;
using System.Linq;

using MoonSharp.Interpreter;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class ContentGroup: IDisplayWidget { // TODO: luadocs
	private List<IDisplayWidget> content = [];

	public ContentGroup Clear() {
		this.content.Clear();
		return this;
	}
	public ContentGroup Push(DynValue content) {
		this.content.Add(ScriptWidgetApi.FromLuaValue(content));
		return this;
	}
	public ContentGroup Add(List<DynValue> content) {
		this.content.AddRange(content.Select(ScriptWidgetApi.FromLuaValue));
		return this;
	}
	public ContentGroup Set(List<DynValue> content) {
		this.content = content.Select(ScriptWidgetApi.FromLuaValue).ToList();
		return this;
	}
	
	[MoonSharpHidden]
	public void Render(ScriptContainer script) {
		foreach (IDisplayWidget widget in this.content.ToArray())
			widget.Render(script);
	}
}
