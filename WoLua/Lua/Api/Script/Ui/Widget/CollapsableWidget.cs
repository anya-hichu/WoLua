using System.Diagnostics.CodeAnalysis;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Api.Script.Ui.Widget.Internal;
using VariableVixen.WoLua.Ui;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class CollapsableWidget: IDisplayWidget { // TODO: luadocs
	private readonly TitledContent storage = new();

	public CollapsableWidget(string title, IDisplayWidget content) {
		this.storage.Title = title;
		this.storage.Content = content;
	}

	[AllowNull]
	public string Title {
		get => this.storage.Title;
		set => this.storage.Title = value;
	}
	public CollapsableWidget SetTitle(string? title) {
		this.Title = title;
		return this;
	}

	[AllowNull]
	public IDisplayWidget Content {
		get => this.storage.Content;
		set => this.storage.Content = value;
	}
	public CollapsableWidget SetContent(IDisplayWidget content) {
		this.Content = content;
		return this;
	}
	
	[MoonSharpHidden]
	public void Render(ScriptContainer script) {
		if (ImUtils.Section(this.Title))
			this.Content.Render(script);
	}

}
