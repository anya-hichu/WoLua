using System.Collections.Generic;
using System.Linq;

using Dalamud.Bindings.ImGui;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Docs;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class ContentGroup: IDisplayWidget {
	private List<IDisplayWidget> content = [];

	[LuaDoc("Whether this group indents ALL of its content. If enabled, all widgets within this group will be indented by one level.",
		"If this group is contained within another group that is also indented, the indentation is cumulative.")]
	public bool Indent { get; set; }
	[LuaChainSetterDoc(nameof(Indent))]
	public ContentGroup SetIndent(bool indent = true) {
		this.Indent = indent;
		return this;
	}

	[LuaDoc("Removes all content from this group, returning the widget for chaining. This is equivalent to calling `.SetContent({})`.",
		"Groups have no decorations, so empty groups will not render anything at all and will not take up any space.")]
	public ContentGroup ClearContent() {
		this.content.Clear();
		return this;
	}
	[LuaMethodAlias(nameof(ClearContent))] public ContentGroup Clear() => this.ClearContent();

	[LuaDoc("Replaces all content for this group, returning the widget for chaining.",
		"This method performs the same primitive-to-widget translation as `Script.Ui.Window.SetContent()`.")]
	public ContentGroup SetContent(List<DynValue> content) {
		this.content = content.Select(ScriptWidgetApi.FromLuaValue).ToList();
		return this;
	}
	[LuaMethodAlias(nameof(SetContent))] public ContentGroup Set(List<DynValue> content) => this.SetContent(content);

	[LuaDoc("Append a list of content to this group, without removing what's already there.",
		"This function takes the same types of content (primitives and widgets) that `.SetContent()` does, and values are treated the same way.")]
	public ContentGroup AddContent(List<DynValue> content) {
		this.content.AddRange(content.Select(ScriptWidgetApi.FromLuaValue));
		return this;
	}
	[LuaMethodAlias(nameof(AddContent))] public ContentGroup Add(List<DynValue> content) => this.AddContent(content);

	[LuaDoc("Append a single item to this group, without removing what's already there."
		+ " This is equivalent to calling `.AddContent({ item })`.",
		"This function takes the same types of content (primitives and widgets) that `.SetContent()` does, and values are treated the same way.")]
	public ContentGroup PushContent(DynValue content) {
		this.content.Add(ScriptWidgetApi.FromLuaValue(content));
		return this;
	}
	[LuaMethodAlias(nameof(PushContent))] public ContentGroup Push(DynValue content) => this.PushContent(content);

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
