using System.Diagnostics.CodeAnalysis;

using Dalamud.Bindings.ImGui;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Api.Script.Ui.Widget.Internal;
using VariableVixen.WoLua.Lua.Docs;
using VariableVixen.WoLua.Ui;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class TextWidget: IDisplayWidget {
	private string? content;
	private TextAlignment alignment;

	[NotNull]
	[LuaDoc("The contents of this text widget. Assigning `nil` is the same as assigning an empty string, but the value when reading will never be `nil`.")]
	public string? Text {
		get => this.content ?? string.Empty;
		set => this.content = string.IsNullOrEmpty(value) ? null : value;
	}
	[LuaChainSetterDoc(nameof(Text))]
	public TextWidget SetText(string? text) {
		this.Text = text;
		return this;
	}

	[LuaDoc("Whether to render this text in a dimmer colour, similar to an aside.")]
	public bool Dim { get; set; }
	[LuaChainSetterDoc(nameof(Dim))]
	public TextWidget SetDim(bool dim = true) {
		this.Dim = dim;
		return this;
	}

	[LuaDoc("Sets the content to be aligned to the left. If this widget is within a group that is marked to be indented, that indentation will still be respected.")]
	public TextWidget AlignLeft() {
		this.alignment = TextAlignment.Normal;
		return this;
	}
	[LuaDoc("Sets the content to be indented one extra level. Often used to indicate an aside.")]
	public TextWidget Indent() {
		this.alignment = TextAlignment.Indented;
		return this;
	}
	[LuaDoc("Sets the content to be rendered in the centre.",
		"Please note that this does not align the text as a formatted document would, but rather draws the entire block of text in the horizontal centre of the available space.",
		"Multi-line text (either by including a line break or by the window being too narrow) will not look right.")]
	public TextWidget Center() {
		this.alignment = TextAlignment.Centred;
		return this;
	}
	[LuaDoc("Sets the content to be rendered on the far right of the available space.",
		"Please note that this does not align the text as a formatted document would, but rather draws the entire block of text on the right side of the available space.",
		"Multi-line text (either by including a line break or by the window being too narrow) will not look right.")]
	public TextWidget AlignRight() {
		this.alignment = TextAlignment.Right;
		return this;
	}

	[MoonSharpHidden]
	public void Render(ScriptContainer script) {
		if (this.Dim)
			ImGui.BeginDisabled();

		switch (this.alignment) {
			case TextAlignment.Normal:
				ImUtils.Textline(this.Text);
				break;
			case TextAlignment.Indented:
				ImGui.Indent();
				ImUtils.Textline(this.Text);
				ImGui.Unindent();
				break;
			case TextAlignment.Centred:
				ImUtils.Textline(this.Text, centred: true);
				break;
			case TextAlignment.Right:
				ImGui.SetCursorPosX(ImGui.GetContentRegionAvail().X - ImGui.CalcTextSize(this.Text).X);
				ImUtils.Textline(this.Text);
				break;
		}

		if (this.Dim)
			ImGui.EndDisabled();
	}

	[MoonSharpHidden]
	public TextWidget(string? text = null) {
		this.Text = text;
	}

	public static implicit operator TextWidget(string text) => new(text);

}
