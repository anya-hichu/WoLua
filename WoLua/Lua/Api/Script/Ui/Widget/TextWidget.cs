using System.Diagnostics.CodeAnalysis;

using Dalamud.Bindings.ImGui;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Api.Script.Ui.Widget.Internal;
using VariableVixen.WoLua.Ui;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class TextWidget: IDisplayWidget { // TODO: luadocs
	private string? content;
	private TextAlignment alignment;

	[AllowNull]
	public string Text {
		get => this.content ?? string.Empty;
		set => this.content = string.IsNullOrEmpty(value) ? null : value;
	}
	public TextWidget SetText(string? text) {
		this.Text = text;
		return this;
	}

	public bool Dim { get; set; }
	public TextWidget SetDim(bool dim = true) {
		this.Dim = dim;
		return this;
	}

	public TextWidget AlignLeft() {
		this.alignment = TextAlignment.Normal;
		return this;
	}
	public TextWidget Indent() {
		this.alignment = TextAlignment.Indented;
		return this;
	}
	public TextWidget Center() {
		this.alignment = TextAlignment.Centred;
		return this;
	}
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
				ImGui.TextUnformatted(this.Text);
				break;
			case TextAlignment.Indented:
				ImGui.Indent();
				ImGui.TextUnformatted(this.Text);
				ImGui.Unindent();
				break;
			case TextAlignment.Centred:
				ImUtils.Textline(this.Text, 0, true);
				break;
			case TextAlignment.Right:
				ImGui.SetCursorPosX(ImGui.GetContentRegionAvail().X - ImGui.CalcTextSize(this.Text).X);
				ImGui.TextUnformatted(this.Text);
				break;
		}

		if (this.Dim)
			ImGui.EndDisabled();
	}

	public TextWidget(string? text = null) {
		this.Text = text;
	}

	public static implicit operator TextWidget(string text) => new(text);

}
