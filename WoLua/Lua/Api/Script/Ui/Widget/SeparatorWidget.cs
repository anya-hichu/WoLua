using Dalamud.Bindings.ImGui;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Docs;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui.Widget;

[MoonSharpUserData]
public class SeparatorWidget: IDisplayWidget {

	[LuaDoc("Whether this widget will draw a visible horizontal line across the middle of the space.")]
	public bool Bar { get; set; }
	[LuaChainSetterDoc(nameof(Bar))]
	public SeparatorWidget SetBar(bool bar = true) {
		this.Bar = bar;
		return this;
	}

	[MoonSharpHidden]
	public void Render(ScriptContainer script) {
		ImGui.Spacing();
		ImGui.Spacing();

		if (this.Bar)
			ImGui.Separator();
		else
			ImGui.Spacing();

		ImGui.Spacing();
		ImGui.Spacing();
	}
}
