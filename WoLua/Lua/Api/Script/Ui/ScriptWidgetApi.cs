using System.Diagnostics.CodeAnalysis;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class ScriptWidgetApi(ScriptContainer source): ApiBase(source) { // TODO: luadocs

	public TextWidget Text(string? text = null) => new(text);

	public TextWidget Header(string? text = null) => this.Text(text).Center();
	public TextWidget SubHeader(string? text = null) => this.Text(text).SetDim().Indent();

	public SeparatorWidget Separator() => new();
	public SeparatorWidget Spacer() => this.Separator().SetBar(false);

	public ContentGroup Group() => new();

	public CollapsableWidget CollapsableSection(string title, IDisplayWidget content) => new(title, content);
	public CollapsableWidget CollapsableSection(string title, string content) => new(title, new TextWidget(content));
	public CollapsableWidget Section(string title, IDisplayWidget content) => this.CollapsableSection(title, content);
	public CollapsableWidget Section(string title, string content) => this.CollapsableSection(title, content);

	public static IDisplayWidget FromLuaValue(DynValue dv) => dv.Type switch {
		DataType.Boolean => new SeparatorWidget().SetBar(dv.Boolean),
		DataType.UserData => dv.UserData.Object is IDisplayWidget widget ? widget : new TextWidget(ToUsefulString(dv)),
		DataType.Number => new TextWidget(dv.Number.ToString()),
		DataType.String => new TextWidget(dv.String),
		_ => new TextWidget(ToUsefulString(dv)),
	};

}
