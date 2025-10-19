using System.Diagnostics.CodeAnalysis;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;
using VariableVixen.WoLua.Lua.Docs;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class ScriptWidgetApi(ScriptContainer source): ApiBase(source) {

	[LuaDoc("Creates a new text widget, which allows additional control over formatting of the text compared to a string primitive.")]
	public TextWidget Text(string? text = null) => new(text);

	[LuaDoc("A shortcut method to create a text widget and mark it to centre the text when rendered.",
		"This is equivalent to calling `.Text(content).Center()`.")]
	public TextWidget Centered(string? text = null) => this.Text(text).Center();

	[SkipDoc("spelling variant")]
	public TextWidget Centred(string? text = null) => this.Centered(text);

	[LuaDoc("A shortcut method to create a \"subheading\" of text, marking it to render centred and in a dimmer colour.",
		"This is equivelent to calling `.Text(content).SetDim().Indent()`.")]
	public TextWidget Subheader(string? text = null) => this.Text(text).SetDim().Indent();

	[LuaDoc("Creates a vertical spacer that will render a horizontal bar in the middle.",
		"This is equivalent to using a boolean literal `true` in any content list, or to calling `.Spacer().SetBar()`.")]
	public SeparatorWidget Separator() => new();
	[LuaDoc("Creates a vertical spacer without a horizontal bar in the middle.",
		"This is equivalent to using a boolean literal `false` in any content list, or to calling `.Separator().SetBar(false)`.")]
	public SeparatorWidget Spacer() => this.Separator().SetBar(false);

	[LuaDoc("Creates a \"content group\" widget."
		+ " This widget simply renders its contents without decoration, but allows you to use multiple widgets in places where only a single widget is allowed.")]
	public ContentGroup Group() => new();

	[LuaDoc("Creates a collapsable section, with a header that can be clicked on to toggle the display of the contents.",
		"If you need to draw multiple widgets in the contents, you can use a `.Group()` widget.")]
	public CollapsableWidget Section(string title, IDisplayWidget content) => new(title, content);
	[LuaDoc("A shortcut function to create a collapsable section with contents that are purely ordinary text.")]
	public CollapsableWidget Section(string title, string content) => new(title, new TextWidget(content));

	[SkipDoc("people are going to get confused about collapsable/collapsible and should use the above instead")]
	public CollapsableWidget CollapsableSection(string title, IDisplayWidget content) => this.Section(title, content);
	[SkipDoc("people are going to get confused about collapsable/collapsible and should use the above instead")]
	public CollapsableWidget CollapsableSection(string title, string content) => this.Section(title, content);

	public static IDisplayWidget FromLuaValue(DynValue dv) => dv.Type switch {
		DataType.Boolean => new SeparatorWidget().SetBar(dv.Boolean),
		DataType.UserData => dv.UserData.Object is IDisplayWidget widget ? widget : new TextWidget(ToUsefulString(dv)),
		DataType.Number => new TextWidget(dv.Number.ToString()),
		DataType.String => new TextWidget(dv.String),
		_ => new TextWidget(ToUsefulString(dv)),
	};

}
