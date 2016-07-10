using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.GeoAnalyst;
using System.IO;

using ESRI.ArcGIS.Analyst3D;
using System.Windows.Forms;

namespace PipeLine.Helper
{
    public class AnimationTracksHelper
    {

        /// <summary>
        /// 创建动画轨迹
        /// </summary>
        public static IAnimationTrack CreateAnimationTrack(IScene pScene, string trackName)
        {
            IAnimationTrack pAnimationTrack = null; ;
            IAnimationType pAnimationType = new AnimationTypeCamera();

            IAnimationTracks pAnimationTracks = pScene as IAnimationTracks;
            pAnimationTracks.FindTrack(trackName, out pAnimationTrack);
            if ((pAnimationTrack == null) || (pAnimationTracks.TrackCount == 0))
            {
                pAnimationTrack = new AnimationTrack();
                pAnimationTrack.Name = trackName;
                pAnimationTrack.AnimationType = pAnimationType;
                pAnimationTrack.AttachObject(pScene.SceneGraph.ActiveViewer.Camera);
                pAnimationTrack.ApplyToAllViewers = true;
                pAnimationTrack.EvenTimeStamps = false;

                pAnimationTracks.AddTrack(pAnimationTrack);
            }
            return pAnimationTrack;
        }

        /// <summary>
        /// 删除动画轨迹
        /// </summary>
        public static void RemoveAnimationTrack(IScene pScene, string trackName)
        {
            try
            {
                IAnimationTrack pAnimationTrack;
                IAnimationTracks pAnimationTracks = pScene as IAnimationTracks;
                pAnimationTracks.FindTrack(trackName, out pAnimationTrack);
                pAnimationTracks.RemoveTrack(pAnimationTrack);
                pAnimationTrack.RemoveAllKeyframes();
                DevExpress.XtraEditors.XtraMessageBox.Show("成功删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception)
            {
                 DevExpress.XtraEditors.XtraMessageBox.Show("无动画数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
           
        }
        /// <summary>
        /// 播放当前动画轨迹
        /// </summary>
        public static void PlayAnimationTrack(IScene pScene, double duration)
        {
            IViewers3D pViewers3D = pScene.SceneGraph as IViewers3D;
            IAnimationTracks ptracks = pScene as IAnimationTracks;
            DateTime st, et;
            TimeSpan span;
            double elapsedTime;
            int i;
            int numCycles = 1;
            if (ptracks.TrackCount == 0)
            {

               // MessageBoxHelper.ShowMessageBox("The animation file does not have any tracks to play.");
                return;
            }
            for (i = 1; i == numCycles; i++)
            {
                st = DateTime.Now;
                do
                {
                    et = DateTime.Now;
                    span = et.Subtract(st);
                    elapsedTime = span.TotalSeconds;
                    if (elapsedTime > duration)
                    {
                        elapsedTime = duration;
                    }
                    ptracks.ApplyTracks(null, elapsedTime, duration);
                    pViewers3D.RefreshViewers();
                }
                while (elapsedTime < duration);

            }
        }

        /// <summary>
        /// 创建动画帧
        /// </summary>
        public static void CreateKeyFrame(IScene pScene, IAnimationTrack pAnimTrack, string sName)
        {
            // Get the GlobeCamera properties
            ICamera pCamera = pScene.SceneGraph.ActiveViewer.Camera;

            // Create a New GlobeCamera Keyframe
            IKeyframe pKeyframe = new Bookmark3DClass() ;
          
            //Assign the properties of the GlobeCamera to the Keyframe
            pKeyframe.CaptureProperties(pScene, pCamera);
            // Assign Keyframe Properties
            pKeyframe.Name = sName + (pAnimTrack.KeyframeCount + 1);
            // Insert the GlobeCamera Keyframe in the Animation Track
            pAnimTrack.InsertKeyframe(pKeyframe, pAnimTrack.KeyframeCount + 1);
            pAnimTrack.EvenTimeStamps = true;
            pAnimTrack.ResetTimeStamps();
        }

        /// <summary>
        /// 删除动画帧
        /// </summary>
        public void RemoveKeyFrame(IAnimationTrack pAnimationTrack, string keyName)
        {
            for (int index = 0; index < pAnimationTrack.KeyframeCount; index++)
            {
                if (pAnimationTrack.get_Keyframe(index).Name == keyName)
                {
                    pAnimationTrack.RemoveKeyframe(index);
                    break;
                }
            }
        }

        /// <summary>
        /// 删除当前轨迹的所有帧
        /// </summary>
        public void RemoveAllKeyFrame(IAnimationTrack pAnimationTrack)
        {
            pAnimationTrack.RemoveAllKeyframes();
        }

        public static void LoadAnimationFile(ref   IScene pScene, string AnimationFile)
        {
            IBasicScene basicScene = pScene as IBasicScene;
            try
            {

                basicScene.LoadAnimation(AnimationFile);

            }
            catch (Exception ex)
            {
                //If file doesn't exist, create it for the user...
                basicScene.SaveAnimation(AnimationFile);
                basicScene.LoadAnimation(AnimationFile);
            }
        }


        public static void SaveAnimationFile(IScene pScene, string AnimationFile)
        {
            IBasicScene basicScene = pScene as IBasicScene;
            basicScene.SaveAnimation(AnimationFile);
        }

    }
}
