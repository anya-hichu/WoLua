using System.Diagnostics.CodeAnalysis;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget.Internal;

internal class TitledContent {
	private string? title;
	private IDisplayWidget? content;
	public readonly IDisplayWidget Fallback;

	public TitledContent(string? name = null, IDisplayWidget? content = null, IDisplayWidget? fallback = null) {
		this.Fallback = fallback ?? new TextWidget();
		this.title = name;
		this.content = content;
	}

	[AllowNull]
	public string Title {
		get => this.title ?? string.Empty;
		set => this.title = string.IsNullOrEmpty(value) ? null : value;
	}

	[AllowNull]
	public IDisplayWidget Content {
		get => this.content ?? this.Fallback;
		set => this.content = value;
	}
}
