using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;

namespace RDR2CS.oldcode
{
    public class Menu
    {
        public static void SetupStyle()
        {
            ImGuiStylePtr style = ImGui.GetStyle();
            style.Colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.50f, 0.50f, 0.50f, 1.00f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.13f, 0.14f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.13f, 0.14f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.13f, 0.14f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.38f, 0.38f, 0.38f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.67f, 0.67f, 0.67f, 0.39f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.08f, 0.08f, 0.09f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.08f, 0.08f, 0.09f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.00f, 0.00f, 0.51f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 0.53f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.31f, 0.31f, 0.31f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.41f, 0.41f, 0.41f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.51f, 0.51f, 0.51f, 1.00f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.11f, 0.64f, 0.92f, 1.00f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.11f, 0.64f, 0.92f, 1.00f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.08f, 0.50f, 0.72f, 1.00f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.38f, 0.38f, 0.38f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.67f, 0.67f, 0.67f, 0.39f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.22f, 0.22f, 0.22f, 1.00f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.67f, 0.67f, 0.67f, 0.39f);
            style.Colors[(int)ImGuiCol.Separator] = style.Colors[(int)ImGuiCol.Border];
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.41f, 0.42f, 0.44f, 1.00f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.29f, 0.30f, 0.31f, 0.67f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.08f, 0.08f, 0.09f, 0.83f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.33f, 0.34f, 0.36f, 0.83f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.23f, 0.23f, 0.24f, 1.00f);
            style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.08f, 0.08f, 0.09f, 1.00f);
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.13f, 0.14f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.11f, 0.64f, 0.92f, 1.00f);
            style.Colors[(int)ImGuiCol.NavHighlight] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.35f);
            style.GrabRounding = style.FrameRounding = style.ChildRounding = style.WindowRounding = 2.3f;
        }
    }
}
