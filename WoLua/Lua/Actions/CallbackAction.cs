using System;
using System.Collections.ObjectModel;

using MoonSharp.Interpreter;

using WoLua.Constants;
using WoLua.Lua.Api;

namespace WoLua.Lua.Actions;

public class CallbackAction(DynValue callback, params DynValue[] arguments): ScriptAction {
	public DynValue Function { get; } = callback;
	private readonly DynValue[] arguments = arguments;
	public ReadOnlyCollection<DynValue> Arguments => Array.AsReadOnly(this.arguments);

	protected override void Process(ScriptContainer script) {
		script.Log(ApiBase.ToUsefulString(this.Function), LogTag.ActionCallback);
		try {
			script.Engine.Call(this.Function, this.arguments);
		}
		catch (ArgumentException e) {
			Service.Plugin.Error("Error in queued callback function", e, script.PrettyName);
		}
	}

	public override string ToString()
		=> $"Invoke: {ApiBase.ToUsefulString(this.Function, true)}";
}
