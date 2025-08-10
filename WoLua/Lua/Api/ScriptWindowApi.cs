using System.Diagnostics.CodeAnalysis;

using MoonSharp.Interpreter;

namespace VariableVixen.WoLua.Lua.Api;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class ScriptWindowApi(ScriptContainer source): ApiBase(source) {

	public string DisplayName {
		get {
			string title = this.Title;
			return string.IsNullOrWhiteSpace(title)
				? this.Owner.PrettyName
				: this.Owner.PrettyName + ": " + title;
		}
	}
	public string Id => "ScriptWindow_" + this.Owner.InternalName;

	public string Title {
		get => this.windowTitle ?? string.Empty;
		set => this.windowTitle = string.IsNullOrWhiteSpace(value) ? null : value;
	}
	private string? windowTitle = null;

	public bool Visible { get; set; }
	public void Show() => this.Visible = true;
	public void Hide() => this.Visible = false;
	public void Toggle() => this.Visible = !this.Visible;

	public bool Empty => true;

	// However the content is exposed, it needs to be usable as a table to set values, but setting individual values OR replacing the whole thing needs to go through validation somehow.
	// There are three meaningful values (two types) that can be used: strings are printed directly, boolean FALSE makes a small vertical spacer, and boolean TRUE makes a separator line.

	internal void Draw() {
		if (!this.Visible)
			return;
		if (this.Empty)
			return;

		// TODO once content storage is sorted, this should render the ENTIRE SCRIPT WINDOW as long as the content isn't empty
	}

}
