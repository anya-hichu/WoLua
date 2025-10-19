using System.Collections.Generic;
using System.Linq;

using Dalamud.Bindings.ImGui;

using MoonSharp.Interpreter;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class ContentGroup: IDisplayWidget { // TODO: luadocs
	private List<IDisplayWidget> content = [];

	public bool Indent { get; set; }
	public ContentGroup SetIndent(bool indent = true) {
		this.Indent = indent;
		return this;
	}

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
		if (this.Indent)
			ImGui.Indent();

		foreach (IDisplayWidget widget in this.content.ToArray())
			widget.Render(script);

		if (this.Indent)
			ImGui.Unindent();
	}
}
