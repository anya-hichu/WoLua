using MoonSharp.Interpreter;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

public interface IDisplayWidget {
	[MoonSharpHidden]
	public void Render(ScriptContainer script);
}
