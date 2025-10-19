using System.Diagnostics.CodeAnalysis;

using Dalamud.Game.Gui.Toast;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Lua;
using VariableVixen.WoLua.Lua.Docs;

namespace VariableVixen.WoLua.Lua.Api.Game;

[MoonSharpUserData]
[SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "It doesn't matter")]
public class ToastApi(ScriptContainer source): ApiBase(source) {

	[LuaDoc("Display a normal-style toast for the short duration, around two seconds.",
		"Toasts have built-in durations set by the game itself, and cannot be given custom durations.")]
	public void Short(string text) {
		if (this.Disposed)
			return;

		Service.Toast.ShowNormal(text, new ToastOptions() { Speed = ToastSpeed.Fast });
	}
	[LuaDoc("Display a normal-style toast for the long duration, around four seconds.",
		"Toasts have built-in durations set by the game itself, and cannot be given custom durations.")]
	public void Long(string text) {
		if (this.Disposed)
			return;

		Service.Toast.ShowNormal(text, new ToastOptions() { Speed = ToastSpeed.Slow });
	}

	[LuaDoc("Display an error-style toast.",
		"Toasts have built-in durations set by the game itself, and cannot be given custom durations.")]
	public void Error(string text) {
		if (this.Disposed)
			return;

		Service.Toast.ShowError(text);
	}
	
	[LuaDoc("Display a quest-style toast with a checkmark.",
		"Toasts have built-in durations set by the game itself, and cannot be given custom durations.",
		"By default, this will play the usual ding sound, but if `true` is passed as the second argument, that will be suppressed.")]
	public void TaskComplete(string text, bool silent = false) {
		if (this.Disposed)
			return;

		Service.Toast.ShowQuest(text, new QuestToastOptions() { DisplayCheckmark = true, PlaySound = !silent });
	}
	[LuaDoc("Display a quest-style toast, using a custom icon ID.",
		"Toasts have built-in durations set by the game itself, and cannot be given custom durations.",
		"By default, this will play the usual ding sound, but if `true` is passed as the third argument, that will be suppressed.")]
	public void TaskComplete(string text, uint icon, bool silent = false) {
		if (this.Disposed)
			return;

		Service.Toast.ShowQuest(text, new QuestToastOptions() { IconId = icon, PlaySound = !silent });
	}

}
