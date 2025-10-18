using System.Diagnostics.CodeAnalysis;

using Dalamud.Bindings.ImGui;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua.Docs;

namespace VariableVixen.WoLua.Lua.Api.Script;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class KeysApi(ScriptContainer source): ApiBase(source) {

	[LuaDoc("Whether a control key is currently down.")]
	public bool Control
		=> ImGui.IsKeyDown(ImGuiKey.ModCtrl);
	[LuaDoc("Whether a control key is currently down.",
		"This is an alternative spelling for the `.Control` property.")]
	public bool Ctrl
		=> this.Control;

	[LuaDoc("Whether an alt key is currently down.")]
	public bool Alt
		=> ImGui.IsKeyDown(ImGuiKey.ModAlt);

	[LuaDoc("Whether a shift key is currently down.")]
	public bool Shift
		=> ImGui.IsKeyDown(ImGuiKey.ModShift);

}
