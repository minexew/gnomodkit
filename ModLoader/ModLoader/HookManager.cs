﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModLoader
{
    public class HookManager
    {
        public delegate Game.GUI.Controls.Button AddButton(string label);

        public delegate void ExportMenuListInitHandler(Game.GUI.ImportExportMenu importExportMenu, Game.GUI.Controls.Manager manager, AddButton context);

        public HookManager()
        {
            instance = this;
        }

        public static int HookImportExportListInit(int Y, Game.GUI.ImportExportMenu importExportMenu, Game.GUI.Controls.Manager manager)
        {
            AddButton addButton = (string label) =>
            {
                var button = new Game.GUI.Controls.Button(importExportMenu.Manager);
                button.Init();
                Y += button.Margins.Top;
                button.Width = 200;
                button.Top = Y;
                button.Text = label;
                importExportMenu.panel_0.Add(button);
                Y += button.Height + button.Margins.Bottom;
                return button;
            };

            if (instance.ExportMenuListInit != null)
                instance.ExportMenuListInit(importExportMenu, manager, addButton);

            return Y;
        }

        public event ExportMenuListInitHandler ExportMenuListInit;

        private static HookManager instance;
    }
}
