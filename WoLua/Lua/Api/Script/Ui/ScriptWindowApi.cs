using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Constants;
using VariableVixen.WoLua.Lua.Docs;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class ScriptWindowApi: ApiBase {

	[MoonSharpHidden]
	public ScriptWindowApi(ScriptContainer source) : base(source) {
		this.ScriptWindow = new(source) {
			Namespace = this.Id,
			WindowName = this.DisplayName,
		};
	}

	private string? windowTitle = null;
	internal ScriptWindow ScriptWindow { get; set; }

	[LuaDoc("The unique ID of this script's window. All scripts have a single window with their own ID.")]
	public string Id => "ScriptWindow_" + this.Owner.InternalName;

	[LuaDoc("The custom title of this script's window.",
		"You can use this to indicate what the window is currently being used for if your script changes what's displayed for different purposes.")]
	public string Title {
		get => this.windowTitle ?? string.Empty;
		set {
			this.windowTitle = string.IsNullOrWhiteSpace(value) ? null : value;
			this.ScriptWindow.WindowName = this.DisplayName;
		}
	}

	[LuaDoc("The calculated display title of the script's window. This is what the user sees in the window's title bar.")]
	public string DisplayName {
		get {
			string title = this.Title;
			return Plugin.Name + ": " + (string.IsNullOrWhiteSpace(title)
				? this.Owner.PrettyName
				: this.Owner.PrettyName + ": " + title);
		}
	}

	[LuaDoc("Whether or not the script window is currently visible.",
		"Please note that script windows without any content (`.Empty == true`) will not appear even if set to be visible.")]
	public bool Visible {
		get => this.ScriptWindow.IsOpen;
		set {
			this.ScriptWindow.IsOpen = value;
			this.Owner.Log($"Set script window to {(value ? "visible" : "hidden")}", LogTag.ScriptWindow);
		}
	}
	[LuaDoc("Show the script window. This is equivalent to setting `.Visible = true`.")]
	public void Show() => this.Visible = true;
	[LuaDoc("Hide the script window. This is equivalent to setting `.Visible = false`.")]
	public void Hide() => this.Visible = false;
	[LuaDoc("Toggle the script window. This is equivalent to inverting the value of `.Visible`.")]
	public void Toggle() => this.Visible = !this.Visible;

	[LuaDoc("Whether the script window has any content assigned to it. Script windows with no content are not drawn, even if set to be visible.")]
	public bool Empty => this.ScriptWindow.Empty;

	[LuaDoc("Whether the script window should automatically resize to fit its content, within the `.MinSize` and `.MaxSize` constraints.",
		"If this is enabled, then `.DefaultSize` will be ignored.")]
	public bool AutoResize {
		get => this.ScriptWindow.AutoResize;
		set => this.ScriptWindow.AutoResize = value;
	}

	[LuaDoc("The minimum dimensions allowed for the script window.",
		"If autoresize is enabled (`.AutoResize == true`) then the window will never shrink to be smaller than this size to fit its content."
		+ " If autoresize is disabled, the user cannot resize the window to be smaller than this.",
		"This size is itself limited to never be smaller than the window title or larger than the entire monitor."
		+ " Values of zero or less will be auto-calculated to a generally-reasonable default.")]
	public Dimensions MinSize => this.ScriptWindow.MinSize;
	[LuaDoc("The initial size of the script window, applied only the first time it's opened AND when autoresize is disabled.",
		"Once the script window has been opened, Dalamud saves its size and position and will reapply it in the future.",
		"If `.AutoResize == true` then this value is ignored, because the script window will automatically size itself to fit its content.",
		"This value is still subject to the `.MinSize` and `.MaxSize` constraints, even if they are auto-calculated.")]
	public Dimensions DefaultSize => this.ScriptWindow.DefaultSize;
	[LuaDoc("The maximum dimensions allowed for the script window.",
		"If autoresize is enabled (`.AutoResize == true`) then the window will never expand to be bigger than this size to fit its content."
		+ "If autoresize is disabled, the user cannot resize the window to be smaller than this.",
		"This size is itself limited to never be smaller than the window title or larger than the entire monitor."
		+ " Values of zero or less will be auto-calculated to a generally-reasonable default.")]
	public Dimensions MaxSize => this.ScriptWindow.MaxSize;

	[LuaDoc("Removes all content from the script window. This is equivalent to calling `.SetContent({})`.",
		"Empty script windows are not drawn, even when set to be visible.")]
	public void ClearContent() {
		this.ScriptWindow.content = [];
		this.Owner.Log($"Cleared window contents", LogTag.ScriptWindow);
	}
	[LuaDoc("Replaces all content for the script window with the given items.",
		"The value `true` will render a visible horizontal bar in the middle of the spacer, while `false` will insert only empty space."
		+ " Other primitives will be converted to strings if necessary, then printed as ordinary text.",
		"For more advanced UIs, you can use the `Script.Ui.Widget.*` functions to create and customise display widgets."
		+ " Widgets in this list will be rendered according to their content and settings.")]
	public void SetContent(List<DynValue> contents) {
		this.ScriptWindow.content = contents.Select(ScriptWidgetApi.FromLuaValue).ToArray();
		this.Owner.Log($"Set window content list to {this.ScriptWindow.content.Length} elements", LogTag.ScriptWindow);
	}
	[LuaDoc("Append a list of content to the script window, without removing what's already there.",
		"This function takes the same types of content (primitives and widgets) that `.SetContent()` does, and values are treated the same way.")]
	public void AddContent(List<DynValue> contents) {
		this.ScriptWindow.content = this.ScriptWindow.content.Concat(contents.Select(ScriptWidgetApi.FromLuaValue)).ToArray();
		this.Owner.Log($"Set window content list to {this.ScriptWindow.content.Length} elements", LogTag.ScriptWindow);
	}
	[LuaDoc("Append a single item to the content of the script window, without removing what's already there."
		+ " This is equivalent to calling `.AddContent({ item })`.",
		"This function takes the same types of content (primitives and widgets) that `.SetContent()` does, and values are treated the same way.")]
	public void PushContent(DynValue item) {
		this.ScriptWindow.content = this.ScriptWindow.content.Append(ScriptWidgetApi.FromLuaValue(item)).ToArray();
		this.Owner.Log($"Set window content list to {this.ScriptWindow.content.Length} elements", LogTag.ScriptWindow);
	}

}
