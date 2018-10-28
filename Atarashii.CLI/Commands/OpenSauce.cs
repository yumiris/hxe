﻿using System;
using Atarashii.CLI.Common;
using Atarashii.CLI.Outputs;
using Atarashii.OpenSauce;

namespace Atarashii.CLI.Commands
{
    internal class OpenSauce : Command
    {
        public static void Initiate(string[] args)
        {
            Exit.IfNoArgs(args);
            Message.Show($"Invoked installation to '{args[0]}'", Message.Type.Info);

            var installer = new InstallerFactory(args[0]).Get();
            var installerState = installer.Verify();

            if (installerState.IsValid)
                Message.Show("Installer verification has passed.", Message.Type.Success);
            else
                Exit.WithError(installerState.Reason, 4);


            try
            {
                installer.Install();
                Message.Show("OpenSauce has been successfully installed.", Message.Type.Success);
            }
            catch (OpenSauceException e)
            {
                Exit.WithError(e.Message, 2);
            }
            catch (Exception e)
            {
                Exit.WithError(e.Message, 3);
            }
        }
    }
}