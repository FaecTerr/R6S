using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Security.Cryptography;

// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("Rainbow 7: Duck")]

// The author of the mod
[assembly: AssemblyCompany("FaecTerr")]

// The description of the mod
[assembly: AssemblyDescription("Just copy of R6S")]

// The mod's version
[assembly: AssemblyVersion("1.0.0.0")]

namespace DuckGame.R6S
{
    public class R6S : Mod
    {
        public static Thread tr;
        public static R6SMainUpdate upd;

        public static string version = "0.1.9e";


        // The mod's priority; this property controls the load order of the mod.
        public override Priority priority
		{
			get { return base.priority; }
		}

		// This function is run before all mods are finished loading.
		protected override void OnPreInitialize()
		{
			base.OnPreInitialize();
		}

		// This function is run after all mods are loaded.
		protected override void OnPostInitialize()
		{
			base.OnPostInitialize();
            copyLevels();
            PlayerStats.Load();
            PlayerStats.Save();
            PlayerStats.Update();
            tr = new Thread(wait);
            if (PlayerStats.keyBindings[22] == Keys.None)
            {
                PlayerStats.keyBindings[22] = Keys.B;
            }
            if (PlayerStats.keyBindings[23] == Keys.None)
            {
                PlayerStats.keyBindings[23] = Keys.X;
            }
            if (PlayerStats.keyBindings[24] == Keys.None)
            {
                PlayerStats.keyBindings[24] = Keys.Tab;
            }
            if (PlayerStats.keyBindings[25] == Keys.None)
            {
                PlayerStats.keyBindings[25] = Keys.Z;
            }
            tr.Start();
        }

        void wait()
        {
            while (Level.current == null || !(Level.current.ToString() == "DuckGame.TitleScreen") && !(Level.current.ToString() == "DuckGame.TeamSelect2"))
                Thread.Sleep(200);
            upd = new R6SMainUpdate();
            AutoUpdatables.Add(upd);
        }

        private static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            int iterations = (int)Math.Ceiling(first.Length / 8.0);
            using (FileStream fs = first.OpenRead())
            {
                using (FileStream fs2 = second.OpenRead())
                {
                    byte[] one = new byte[8];
                    byte[] two = new byte[8];
                    for (int i = 0; i < iterations; i++)
                    {
                        fs.Read(one, 0, 8);
                        fs2.Read(two, 0, 8);
                        if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private static void copyLevels()
        {
            string levelFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\R6SMaps");
            if (!Directory.Exists(levelFolder))
            {
                Directory.CreateDirectory(levelFolder);
            }
            foreach (string sourcePath in Directory.GetFiles(Mod.GetPath<R6S>("Levels")))
            {
                string destPath = Path.Combine(levelFolder, Path.GetFileName(sourcePath));
                bool file_exists = File.Exists(destPath);
                if (!file_exists || !R6S.FilesAreEqual(new FileInfo(sourcePath), new FileInfo(destPath)))
                {
                    if (file_exists)
                    {
                        File.Delete(destPath);
                    }
                    File.Copy(sourcePath, destPath);
                }
            }
        }
    }
}
