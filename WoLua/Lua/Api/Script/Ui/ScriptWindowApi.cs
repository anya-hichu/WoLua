using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Constants;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class ScriptWindowApi: ApiBase { // TODO: luadocs

	[MoonSharpHidden]
	public ScriptWindowApi(ScriptContainer source) : base(source) {
		this.ScriptWindow = new(source) {
			Namespace = this.Id,
			WindowName = this.DisplayName,
		};
	}

	private string? windowTitle = null;
	internal ScriptWindow ScriptWindow { get; set; }

	public string Id => "ScriptWindow_" + this.Owner.InternalName;
	public string Title {
		get => this.windowTitle ?? string.Empty;
		set {
			this.windowTitle = string.IsNullOrWhiteSpace(value) ? null : value;
			this.ScriptWindow.WindowName = this.DisplayName;
		}
	}
	public string DisplayName {
		get {
			string title = this.Title;
			return Plugin.Name + ": " + (string.IsNullOrWhiteSpace(title)
				? this.Owner.PrettyName
				: this.Owner.PrettyName + ": " + title);
		}
	}


	public bool Visible {
		get => this.ScriptWindow.IsOpen;
		set {
			this.ScriptWindow.IsOpen = value;
			this.Owner.Log($"Set script window to {(value ? "visible" : "hidden")}", LogTag.ScriptWindow);
		}
	}
	public void Show() => this.Visible = true;
	public void Hide() => this.Visible = false;
	public void Toggle() => this.Visible = !this.Visible;

	public bool AutoResize {
		get => this.ScriptWindow.AutoResize;
		set => this.ScriptWindow.AutoResize = value;
	}

	public Dimensions MinSize => this.ScriptWindow.MinSize;
	public Dimensions Size => this.ScriptWindow.DefaultSize;
	public Dimensions MaxSize => this.ScriptWindow.MaxSize;

	public bool Empty => this.ScriptWindow.Empty;

	public void SetContent(List<DynValue> contents) {
		this.ScriptWindow.content = contents.Select(ScriptWidgetApi.FromLuaValue).ToArray();
		this.Owner.Log($"Set window content list to {this.ScriptWindow.content.Length} elements", LogTag.ScriptWindow);
	}

}
