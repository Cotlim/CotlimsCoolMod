using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Projectiles
{
    // This example is similar to the Wooden Arrow projectile
    public class FinalBlasterLoadProjectile : ModProjectile
    {
        public override string Texture => "CotlimsCoolMod/Content/Projectiles/FinalBlasterWeapon/FinalBlasterLoadProjectile";

        private int _projectileCount;

        public const float _aimResponsiveness = 0.12f;

        private int _projectilesLeft = 3;

        private SoundStyle? _blastShoot;

        private SoundStyle? _blastLoad;

        private SoundStyle? _blastShoot2;

        private SoundStyle? _blastLoad2;

        private float _speedUp;

        private Player _owner;

        private bool _firstTime = true;

        private Vector2 Rot45PlusPosOffset => new Vector2(-26f, -10f).RotatedBy(-MathHelper.PiOver2).RotatedBy(Projectile.velocity.ToRotation());

        private Vector2 Rot45MinusPosOffset => new Vector2(26f, -10f).RotatedBy(-MathHelper.PiOver2).RotatedBy(Projectile.velocity.ToRotation());

        private float UpdatesPerFrame => Projectile.extraUpdates + 1;

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private float Pitch
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        private bool IsItDownBeat
        {
            get => Projectile.ai[2] == 1;
            set => Projectile.ai[2] = value ? 1 : 0;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            //writer.WriteVector2(Projectile.velocity);
            //writer.Write(Projectile.rotation);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            //Projectile.velocity = reader.ReadVector2();
            //Projectile.rotation = reader.ReadSingle();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.tileCollide = false;
            Projectile.hide = true;
            ProjectileID.Sets.DontAttachHideToAlpha[Type] = true;
            Projectile.extraUpdates = 1;
            Projectile.height = 38;
            Projectile.width = 38;
            Projectile.Opacity = 0.3f;


        }

        private void LoadSounds()
        {
            _blastShoot ??= new SoundStyle($"{nameof(CotlimsCoolMod)}/Assets/Sounds/FinalBlastShoot")
            {
                Volume = 0.5f,
                Pitch = Pitch,
            };
            _blastLoad ??= new SoundStyle($"{nameof(CotlimsCoolMod)}/Assets/Sounds/FinalBlastLoad")
            {
                Volume = 0.5f,
                Pitch = Pitch,
            };

            _blastShoot2 ??= new SoundStyle($"{nameof(CotlimsCoolMod)}/Assets/Sounds/FinalBlastShoot2")
            {
                Volume = 0.5f,
                Pitch = Pitch,
            };
            _blastLoad2 ??= new SoundStyle($"{nameof(CotlimsCoolMod)}/Assets/Sounds/FinalBlastLoad2")
            {
                Volume = 0.5f,
                Pitch = Pitch,
            };
        }

        public override void AI()
        {
            // First time logic
            if (_firstTime)
            {
                _firstTime = false;
                //Setting sounds of blaster
                LoadSounds();

                SoundEngine.PlaySound(IsItDownBeat ? _blastLoad2 : _blastLoad, Projectile.position);

                _projectileCount = 3;
                _projectilesLeft = _projectileCount;

                _speedUp = (float)Math.Pow(2f, Pitch);

                _owner = Main.player[Projectile.owner];
            }

            // Check if owner use/can use this weapon
            if (!_owner.channel || _owner.dead || !_owner.active)
            {
                _owner.itemTime = (int)(Timer - 60);
                _owner.itemAnimation = _owner.itemTime;
                Projectile.Kill();
                return;
            }

            // Cancel projectile movement
            Projectile.position -= Projectile.velocity;

            // Making owner using item until projectile is killed
            _owner.itemTime = 2;
            _owner.itemAnimation = 2;

            //Decrement timer
            Timer -= _speedUp / UpdatesPerFrame;

            // Setting some useful variables
            Vector2 rrp = _owner.RotatedRelativePoint(_owner.MountedCenter, true);

            if (Projectile.owner == Main.myPlayer)
            {
                // Updating projectile position
                UpdatePosition(rrp, _owner);
            }

            //Phase controller
            if (Timer > 58)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    UpdateRotation(rrp, _owner);
                    Projectile.netUpdate = true;
                }
                // Setting projectile rotation in direction of velocity
                Projectile.rotation = Projectile.velocity.ToRotation();

                // Changing direction of owner and item
                _owner.ChangeDir(Projectile.direction);
                _owner.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

            }
            else if (Timer <= 58 && Timer > 0)
            {
                //Making beams invisible
                Projectile.Opacity = 0;

                //Spawning shoot projectiles with complicated logic
                if (Timer <= 58 - (_projectileCount - _projectilesLeft) * 120f * 3f / 16f && _projectilesLeft > 0)
                {
                    if (Projectile.owner == Main.myPlayer)
                    {
                        var rot45Plus = Projectile.velocity.RotatedBy(MathHelper.PiOver4);
                        var rot45Minus = Projectile.velocity.RotatedBy(-MathHelper.PiOver4);

                        var rot45PlusPos = Projectile.Center + Rot45PlusPosOffset;
                        var rot45MinusPos = Projectile.Center + Rot45MinusPosOffset;

                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                        Projectile.velocity, ModContent.ProjectileType<FinalBlasterShootProjectile>(),
                        Projectile.damage, Projectile.knockBack, Projectile.owner,
                        ai0: 180, ai1: Pitch);

                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), rot45PlusPos,
                        rot45Plus, ModContent.ProjectileType<FinalBlasterShootProjectile>(),
                        Projectile.damage, Projectile.knockBack, Projectile.owner,
                        ai0: 180, ai1: Pitch);

                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), rot45MinusPos,
                        rot45Minus, ModContent.ProjectileType<FinalBlasterShootProjectile>(),
                        Projectile.damage, Projectile.knockBack, Projectile.owner,
                        ai0: 180, ai1: Pitch);
                    }
                    SoundEngine.PlaySound(IsItDownBeat ? _blastShoot2 : _blastShoot, rrp);
                    _projectilesLeft--;
                }
            }
            if (Timer <= 0)
            {
                _owner.itemTime = 2;
                _owner.itemAnimation = 2;
                Projectile.Kill();
            }

        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player owner = Main.player[Projectile.owner];

            //If the beam doesn't have a defined direction, don't draw anything.
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }

            //Setting some useful variables
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 drawScale = new Vector2(Projectile.scale);
            Vector2 rrp = owner.RotatedRelativePoint(owner.MountedCenter, true);
            float maxBeamLength = 460;
            Vector2 rotationVector = Projectile.rotation.ToRotationVector2();

            //Setting variables for drawing beams
            Vector2 startPosition = rotationVector * 68 + rrp - Main.screenPosition;
            Vector2 startPositionRotated45Plus = startPosition + Rot45PlusPosOffset * 1.55f;
            Vector2 startPositionRotated45Minus = startPosition + Rot45MinusPosOffset * 1.55f;

            Vector2 endPosition = startPosition + rotationVector * maxBeamLength;
            Vector2 endPositionRotated45Plus = startPositionRotated45Plus + rotationVector.RotatedBy(MathHelper.PiOver4) * maxBeamLength;
            Vector2 endPositionRotated45Minus = startPositionRotated45Minus + rotationVector.RotatedBy(-MathHelper.PiOver4) * maxBeamLength;

            // Draw the beams.
            DrawBeam(Main.spriteBatch, texture, startPosition, endPosition, drawScale, lightColor * Projectile.Opacity);
            DrawBeam(Main.spriteBatch, texture, startPositionRotated45Plus, endPositionRotated45Plus, drawScale, lightColor * Projectile.Opacity);
            DrawBeam(Main.spriteBatch, texture, startPositionRotated45Minus, endPositionRotated45Minus, drawScale, lightColor * Projectile.Opacity);

            // Returning false prevents Terraria from trying to draw the Projectile itself.
            return false;
        }
        /*
        private float PerformBeamHitscan(float visualBeamLength, Vector2 direction)
        {
            float[] laserScanResults = new float[3];
            Collision.LaserScan(Owner.Center, Vector2.Normalize(direction), 1f, visualBeamLength, laserScanResults);
            float averageLengthSample = 0f;
            for (int i = 0; i < laserScanResults.Length; ++i)
            {
                averageLengthSample += laserScanResults[i];
            }
            averageLengthSample /= 3;
            return averageLengthSample;
        }
        */

        private void UpdateRotation(Vector2 source, Player owner)
        {
            //Getting normalised vector from player to mouse 
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);

            if (aim.HasNaNs())
            {
                aim = -Vector2.UnitY;
            }

            //Lerping aim(so changing aim is slower)
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, _aimResponsiveness / UpdatesPerFrame));

            //Checking if aim is different from frame before
            if (aim.ToRotation() != Projectile.rotation)
            {
                Projectile.velocity = aim * Projectile.velocity.Length();

                Projectile.netUpdate = true;
            }
        }
        private void UpdatePosition(Vector2 source, Player owner)
        {
            Projectile.Center = Projectile.rotation.ToRotationVector2() * 48 + source;

            if (!Collision.CanHitLine(owner.Center, 0, 0, Projectile.Center, 0, 0))
            {
                Projectile.Center = owner.Center;
            }
        }


        private void DrawBeam(SpriteBatch spriteBatch, Texture2D texture, Vector2 startPosition, Vector2 endPosition, Vector2 drawScale, Color beamColor)
        {

            void WarningLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
            {
                color = beamColor;
                switch (stage)
                {
                    case 0:
                        distCovered = 0;
                        frame = new Rectangle(0, 1, 38, 38);
                        origin = frame.Size() / 2f;
                        break;
                    case 1:
                        frame = new Rectangle(0, 1, 38, 38);
                        distCovered = 38;
                        origin = new Vector2(frame.Width / 2, 0f);
                        break;
                    case 2:
                        distCovered = 38;
                        frame = new Rectangle(0, 80, 38, 38);
                        origin = new Vector2(frame.Width / 2, 0f);
                        break;
                    default:
                        distCovered = 9999f;
                        frame = Rectangle.Empty;
                        origin = Vector2.Zero;
                        color = Color.Transparent;
                        break;
                }
            }
            Utils.LaserLineFraming lineFraming = new Utils.LaserLineFraming(WarningLaserDraw);

            // c_1 is an unnamed decompiled variable which is the render color of the beam drawn by DelegateMethods.RainbowLaserDraw.
            DelegateMethods.c_1 = beamColor;
            Utils.DrawLaser(spriteBatch, texture, startPosition, endPosition, drawScale, lineFraming);
        }


    }
}