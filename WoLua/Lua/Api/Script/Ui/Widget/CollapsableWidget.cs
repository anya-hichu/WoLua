using System.Diagnostics.CodeAnalysis;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Api.Script.Ui.Widget.Internal;
using VariableVixen.WoLua.Lua.Docs;
using VariableVixen.WoLua.Ui;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class CollapsableWidget: IDisplayWidget {
	private readonly TitledContent storage = new();

	[MoonSharpHidden]
	public CollapsableWidget(string title, IDisplayWidget content) {
		this.storage.Title = title;
		this.storage.Content = content;
	}

	[AllowNull]
	[LuaDoc("The title of this collapsable section. This is what the clickable header will display.")]
	public string Title {
		get => this.storage.Title;
		set => this.storage.Title = value;
	}
	[LuaChainSetterDoc(nameof(Title))]
	public CollapsableWidget SetTitle(string? title) {
		this.Title = title;
		return this;
	}

	[AllowNull]
	[LuaDoc("The widget that will be rendered when this section is open.",
		"Please note that this MUST be a widget - you cannot assign a primitive, due to technical limitations.")]
	public IDisplayWidget Content {
		get => this.storage.Content;
		set => this.storage.Content = value;
	}
	[LuaChainSetterDoc(nameof(Content), "However, it also allows you to use a lua primitive, which will be translated into a widget.")]
	public CollapsableWidget SetContent(DynValue content) {
		this.Content = ScriptWidgetApi.FromLuaValue(content);
		return this;
	}

	[MoonSharpHidden]
	public void Render(ScriptContainer script) {
		if (ImUtils.Section(this.Title))
			this.Content.Render(script);
	}

}
