using ImGuiNET;

using MoonSharp.Interpreter;

using WoLua.Lua.Docs;

namespace WoLua.Lua.Api.Script;

[MoonSharpUserData]
public class KeysApi: ApiBase {
	public KeysApi(ScriptContainer source) : base(source) { }

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
