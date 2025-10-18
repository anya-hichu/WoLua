using System.Numerics;

using Dalamud.Bindings.ImGui;

namespace VariableVixen.WoLua.Ui;

public static class ImUtils {

	public static void Textline(string? text = null, uint spacing = 1, bool centred = false, Vector4? colour = null) {
		if (spacing > 0) {
			for (uint i = 0; i < spacing; ++i)
				ImGui.Spacing();
		}
		if (colour is not null)
			ImGui.PushStyleColor(ImGuiCol.Text, colour.Value);

		if (centred) {
			float width = ImGui.CalcTextSize(text).X;
			float offset = (ImGui.GetContentRegionAvail().X / 2) - (width / 2);
			ImGui.SetCursorPosX(offset);
		}
		ImGui.TextUnformatted(text ?? string.Empty);

		if (colour is not null)
			ImGui.PopStyleColor();
		if (spacing > 0) {
			for (uint i = 0; i < spacing; ++i)
				ImGui.Spacing();
		}
	}

	public static void Separator(uint spacing = 1) {
		if (spacing > 0) {
			for (uint i = 0; i < spacing; ++i)
				ImGui.Spacing();
		}
		ImGui.Separator();
		if (spacing > 0) {
			for (uint i = 0; i < spacing; ++i)
				ImGui.Spacing();
		}
	}

	public static bool Section(string title) => ImGui.CollapsingHeader(title, ImGuiTreeNodeFlags.None);

}
