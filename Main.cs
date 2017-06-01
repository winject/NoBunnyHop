using System;
using System.Drawing;
using CitizenFX;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NoBunnyHop
{
    public class NoBunnyHop : BaseScript
    {
        Cooldown coolDown = new Cooldown(0);
        Cooldown cleaner = new Cooldown(1);
        bool canRestore = false;

        public NoBunnyHop()
        {
            coolDown.MaximumFrame = 2;
            base.Tick += OnTick;
        }

        private async Task OnTick()
        {
            /// <summary>
            /// Function below will try to restore the number of jumps to 0 when it's possible
            /// </summary>                              
            if (cleaner.HasExpired)
            {
                if (canRestore)
                {
                    coolDown.ResetFrameCounter();
                }
                cleaner.Time = 12000;
            }

            /// <summary>
            /// Wait for player to be spawned and only with OnFoot status
            /// </summary>
            if (Game.PlayerPed.IsOnScreen && Game.PlayerPed.IsOnFoot) 
            {
                if (coolDown.HasExpired)
                {
                    canRestore = true;
                    if (OnKeyDown.Sprint && OnKeyDown.Jump) //classic bunnyhop (sprint+jump)
                    {
                        canRestore = false;
                        if (Game.PlayerPed.IsSprinting || Game.PlayerPed.IsWalking || Game.PlayerPed.IsJumping)
                        {
                            //Make him ragdoll or any thing that reminds him it's boring to bunnyhop
                            Game.PlayerPed.Task.ClearAll();
                        }
                    }
                    else if(OnKeyDown.Jump) // clever bunnyhop (run/walk + jump)
                    {
                        canRestore = false;
                        if(Game.PlayerPed.IsRunning || Game.PlayerPed.IsWalking) 
                        {
                            if(!Game.PlayerPed.IsClimbing || !Game.PlayerPed.IsVaulting) //prevent parkours boys from being flagged
                            {
                                if(coolDown.FrameCount < coolDown.MaximumFrame)
                                {
                                    cleaner.Time = 12000;
                                    coolDown.CountThisFrame();
                                }
                                else if(coolDown.FrameCount >= coolDown.MaximumFrame)
                                {
                                    coolDown.Time = 8000;
                                    coolDown.ResetFrameCounter();
                                }
                            }
                        }
                    }
                }
                else if(!coolDown.HasExpired)
                {
                    Game.DisableControlThisFrame(2, CitizenFX.Core.Control.Jump); //rip jumping
                }
            }
            await Task.FromResult(0);
        }
    }
}