using System;
using System.Runtime.InteropServices;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

namespace GUI
{
    internal class Kompas
    {
        /// <summary>
        /// 
        /// </summary>
        public KompasObject _Kompas;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        public void BuildWindow(WindowParametrs window)
        {
            StartAndConnectToKompas();

            if (_Kompas == null)
            {
                throw new Exception("Не возможно построить" +
                                    " деталь. Нет связи с Kompas.");
            }
            
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }
              
            var height = window.LengthHeight;
            var width = window.LengthWidth;
            var weigth = window.LengthWeight;
            var offset = 20;
            ksPart windowPart;
            ksEntity planeXoy;
            FirstSection(height, width,
                weigth, offset, out windowPart,
                out planeXoy);

            OpenFirstSection(window, height, width,
                weigth, offset, windowPart, planeXoy);

            if (window.SectionNumber >= 2)
            {
                DrawSecondSection(window, offset,
                    width, height, weigth,
                    windowPart, planeXoy);
            }

            if (window.SectionNumber >= 3)
            {
                DrawThirdSection(window, offset,
                    width, height, weigth,
                    windowPart, planeXoy);
            }

            if (window.SectionNumber >= 4)
            {
                DrawFourthSection(window, offset,
                    width, height, weigth,
                    windowPart, planeXoy);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="weigth"></param>
        /// <param name="offset"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void OpenFirstSection(WindowParametrs window,
            int height, int width, int weigth, int offset,
            ksPart windowPart, ksEntity planeXoy)
        {
            if (window.OpenSection == 1)
            {
                var xStartStepFirstOpenSectionExtrude = -width / 2
                    + offset;
                var yStartStepFirstOpenSectionExtrude = -height / 2 
                    + offset;
                var widthStepFirstOpenSectionExtrude = width / 2 
                    - offset * 2;
                var heighStepFirstOpenSectionExtrude = height 
                    - offset * 2;
                var weigthStepFirstOpenSectionExtrude = weigth;

                ksEntity stepPlaneOffsetFirstOpenSection =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionFirstOpenSectionExtrude =
                    stepPlaneOffsetFirstOpenSection.GetDefinition();
                planeOffsetDefinitionFirstOpenSectionExtrude
                    .SetPlane(planeXoy);
                planeOffsetDefinitionFirstOpenSectionExtrude.offset =
                    weigthStepFirstOpenSectionExtrude;
                stepPlaneOffsetFirstOpenSection.Create();
                ksEntity sketchFirstOpenSectionExtrude =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionFirstOpenSectionExtrude =
                    sketchFirstOpenSectionExtrude.GetDefinition();
                sketchDefinitionFirstOpenSectionExtrude.SetPlane(
                    stepPlaneOffsetFirstOpenSection);
                sketchFirstOpenSectionExtrude.Create();
                ksDocument2D document2DFirstOpenSectionExtrude = 
                    sketchDefinitionFirstOpenSectionExtrude.BeginEdit();

                DrawWindow(document2DFirstOpenSectionExtrude,
                    xStartStepFirstOpenSectionExtrude,
                    yStartStepFirstOpenSectionExtrude,
                    heighStepFirstOpenSectionExtrude,
                    widthStepFirstOpenSectionExtrude, null);

                sketchDefinitionFirstOpenSectionExtrude.EndEdit();
                Extrude(windowPart, sketchFirstOpenSectionExtrude,
                    weigthStepFirstOpenSectionExtrude,
                    (short)Direction_Type.dtNormal);

                var xStartStepFirstOpenSectionNotch = -width / 2 
                    + offset * 2;
                var yStartStepFirstOpenSectionNotch = -height / 2 
                    + offset * 2;
                var widthStepFirstOpenSectionNotch = width / 2 
                    - offset * 4;
                var heighStepFirstOpenSectionNotch = height 
                    - offset * 4;
                var weigthStepFirstOpenSectionNotch = weigth 
                    + offset + 4;

                ksEntity stepPlaneOffsetFirstOpenSectionNotch =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionFirstOpenSectionNotch =
                    stepPlaneOffsetFirstOpenSectionNotch.GetDefinition();
                planeOffsetDefinitionFirstOpenSectionNotch
                    .SetPlane(planeXoy);
                planeOffsetDefinitionFirstOpenSectionNotch.offset =
                    weigthStepFirstOpenSectionNotch;
                stepPlaneOffsetFirstOpenSectionNotch.Create();
                ksEntity sketchFirstOpenSectionNotch =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition
                    sketchDefinitionFirstOpenSectionNotch =
                        sketchFirstOpenSectionNotch.GetDefinition();
                sketchDefinitionFirstOpenSectionNotch.SetPlane(
                    stepPlaneOffsetFirstOpenSectionNotch);
                sketchFirstOpenSectionNotch.Create();
                ksDocument2D document2DFirstOpenSectionNotch = 
                    sketchDefinitionFirstOpenSectionNotch.BeginEdit();

                DrawWindow(document2DFirstOpenSectionNotch,
                    xStartStepFirstOpenSectionNotch,
                    yStartStepFirstOpenSectionNotch,
                    heighStepFirstOpenSectionNotch,
                    widthStepFirstOpenSectionNotch, null);

                sketchDefinitionFirstOpenSectionNotch.EndEdit();
                Notch(windowPart, sketchFirstOpenSectionNotch,
                    weigthStepFirstOpenSectionNotch);

                HandleLeftPosotionFirstSection(window, width, offset,
                    windowPart, stepPlaneOffsetFirstOpenSectionNotch);

                HandleRightPositionFirstSection(window, width, offset,
                    windowPart, stepPlaneOffsetFirstOpenSectionNotch);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="width"></param>
        /// <param name="offset"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetFirstOpenSectionNotch"></param>
        private void HandleLeftPosotionFirstSection(WindowParametrs window,
            int width, int offset, ksPart windowPart,
            ksEntity stepPlaneOffsetFirstOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Left)
            {
                var weightStepHandleLeftFirstSection = 10;
                var baseHandleLeftFirstSection = width;

                ksEntity sketchHandleLeftFirstSection =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleLeftFirstSection =
                    sketchHandleLeftFirstSection.GetDefinition();
                sketchDefinitionHandleLeftFirstSection.SetPlane(
                    stepPlaneOffsetFirstOpenSectionNotch);
                sketchHandleLeftFirstSection.Create();
                ksDocument2D document2DHandleLeftFirstSection = 
                    sketchDefinitionHandleLeftFirstSection.BeginEdit();
                DrawWindow(document2DHandleLeftFirstSection,
                    baseHandleLeftFirstSection / 22 - offset * 3,
                    baseHandleLeftFirstSection / 200.0,
                    baseHandleLeftFirstSection / 90.0,
                    -baseHandleLeftFirstSection / 70.0, 180);
                sketchDefinitionHandleLeftFirstSection.EndEdit();
                Extrude(windowPart, sketchHandleLeftFirstSection,
                    weightStepHandleLeftFirstSection,
                    (short)Direction_Type.dtNormal);

                var baseHandleLeftFirstSection1 = baseHandleLeftFirstSection * 2;
                ksEntity stepPlaneOffsetHandleLeftFirstSection1 =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleLeftFirstSection1 =
                        stepPlaneOffsetHandleLeftFirstSection1
                            .GetDefinition();
                planeOffsetDefinitionHandleLeftFirstSection1.SetPlane(
                    stepPlaneOffsetFirstOpenSectionNotch);
                planeOffsetDefinitionHandleLeftFirstSection1.offset =
                    weightStepHandleLeftFirstSection;
                stepPlaneOffsetHandleLeftFirstSection1.Create();
                ksEntity sketchHandleLeftFirstSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleLeftFirstSection1 =
                    sketchHandleLeftFirstSection1.GetDefinition();
                sketchDefinitionHandleLeftFirstSection1.SetPlane(
                    stepPlaneOffsetHandleLeftFirstSection1);
                sketchHandleLeftFirstSection1.Create();
                ksDocument2D document2DHandleLeftFirstSection1 =
                    sketchDefinitionHandleLeftFirstSection1.BeginEdit();

                DrawWindow(document2DHandleLeftFirstSection1,
                    baseHandleLeftFirstSection1 / 30 - offset * 3,
                    baseHandleLeftFirstSection1 / 170.0,
                    baseHandleLeftFirstSection1 / 85.0,
                    +baseHandleLeftFirstSection1 / 15.0, 180);
                sketchDefinitionHandleLeftFirstSection1.EndEdit();
                Extrude(windowPart, sketchHandleLeftFirstSection1,
                    weightStepHandleLeftFirstSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="width"></param>
        /// <param name="offset"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetFirstOpenSectionNotch"></param>
        private void HandleRightPositionFirstSection(WindowParametrs window,
            int width, int offset, ksPart windowPart,
            ksEntity stepPlaneOffsetFirstOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Right)
            {
                var weightStepHandleRightFirstSection = 10;
                var baseHandleRightFirstSection = width;

                ksEntity sketchHandleRightFirstSection =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleRightFirstSection =
                    sketchHandleRightFirstSection.GetDefinition();
                sketchDefinitionHandleRightFirstSection.SetPlane(
                    stepPlaneOffsetFirstOpenSectionNotch);
                sketchHandleRightFirstSection.Create();
                ksDocument2D document2DHandleRightFirstSection =
                    sketchDefinitionHandleRightFirstSection.BeginEdit();
                DrawWindow(document2DHandleRightFirstSection,
                    -baseHandleRightFirstSection / 2 + offset / 3 + offset,
                    baseHandleRightFirstSection / 200 - offset / 8,
                    baseHandleRightFirstSection / 90.0,
                    -baseHandleRightFirstSection / 70.0, 180);
                sketchDefinitionHandleRightFirstSection.EndEdit();
                Extrude(windowPart, sketchHandleRightFirstSection,
                    weightStepHandleRightFirstSection,
                    (short)Direction_Type.dtNormal);

                var baseHandleRightFirstSection1 = width;
                ksEntity stepPlaneOffsetHandleRightFirstSection1 =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleRightFirstSection1 =
                        stepPlaneOffsetHandleRightFirstSection1
                            .GetDefinition();
                planeOffsetDefinitionHandleRightFirstSection1.SetPlane(
                    stepPlaneOffsetFirstOpenSectionNotch);
                planeOffsetDefinitionHandleRightFirstSection1.offset =
                    weightStepHandleRightFirstSection;
                stepPlaneOffsetHandleRightFirstSection1.Create();
                ksEntity sketchHandleRightFirstSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleRightFirstSection1 =
                    sketchHandleRightFirstSection1.GetDefinition();
                sketchDefinitionHandleRightFirstSection1.SetPlane(
                    stepPlaneOffsetHandleRightFirstSection1);
                sketchHandleRightFirstSection1.Create();
                ksDocument2D document2DHandleRightFirstSection1 =
                    sketchDefinitionHandleRightFirstSection1.BeginEdit();

                DrawWindow(document2DHandleRightFirstSection1,
                    -baseHandleRightFirstSection1 / 2 + offset,
                    baseHandleRightFirstSection1 / 170.0,
                    baseHandleRightFirstSection1 / 45.0,
                    -baseHandleRightFirstSection1 / 9.0, 180);
                sketchDefinitionHandleRightFirstSection1.EndEdit();
                Extrude(windowPart, sketchHandleRightFirstSection1,
                    weightStepHandleRightFirstSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="weigth"></param>
        /// <param name="offset"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void FirstSection(int height, int width, 
            int weigth, int offset,
            out ksPart windowPart, out ksEntity planeXoy)
        {
            var xStartStepFirstSectionExtrude = -width / 2.0;
            var yStartStepFirstSectionExtrude = -height / 2.0;

            ksDocument3D document = _Kompas.Document3D();
            document.Create();
            windowPart = document.GetPart((short)
                Part_Type.pTop_Part);
            planeXoy = windowPart.GetDefaultEntity((short)
                Obj3dType.o3d_planeXOY);
            ksEntity sketchFirstSectionExtrude =windowPart.NewEntity((short)
                Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionFirstSectionExtrude =
                    sketchFirstSectionExtrude.GetDefinition();
            sketchDefinitionFirstSectionExtrude.SetPlane(planeXoy);
            sketchFirstSectionExtrude.Create();
            ksDocument2D
                document2D = sketchDefinitionFirstSectionExtrude.BeginEdit();
            DrawWindow(document2D, xStartStepFirstSectionExtrude,
                yStartStepFirstSectionExtrude, height,
                width / 2.0, null);

            sketchDefinitionFirstSectionExtrude.EndEdit();
            Extrude(windowPart, sketchFirstSectionExtrude,
                weigth, (short)Direction_Type.dtNormal);

            var xStartStepFirstSectionNotch = -width / 2 + offset;
            var yStartStepFirstSectionNotch = -height / 2 + offset;
            var widthStepFirstSectionNotch = width / 2 - offset * 2;
            var heighStepFirstSectionNotch = height - offset * 2;
            var weigthStepFirstSectionNotch = weigth + offset;

            ksEntity stepPlaneOffsetFirstSection =
                windowPart.NewEntity((short)Obj3dType
                    .o3d_planeOffset);
            ksPlaneOffsetDefinition
                planeOffsetDefinitionFirstSectionNotch =
                    stepPlaneOffsetFirstSection.GetDefinition();
            planeOffsetDefinitionFirstSectionNotch
                .SetPlane(planeXoy);
            planeOffsetDefinitionFirstSectionNotch.offset = 
                weigthStepFirstSectionNotch;
            stepPlaneOffsetFirstSection.Create();
            ksEntity sketchFirstSrctionNotch =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionFirstSectionNotch =
                    sketchFirstSrctionNotch.GetDefinition();
            sketchDefinitionFirstSectionNotch.SetPlane(
                stepPlaneOffsetFirstSection);
            sketchFirstSrctionNotch.Create();
            ksDocument2D docment2DFirstSectionNotch = 
                sketchDefinitionFirstSectionNotch.BeginEdit();

            DrawWindow(docment2DFirstSectionNotch,
                xStartStepFirstSectionNotch, yStartStepFirstSectionNotch,
                heighStepFirstSectionNotch, widthStepFirstSectionNotch, null);

            sketchDefinitionFirstSectionNotch.EndEdit();
            Notch(windowPart, sketchFirstSrctionNotch,
                weigthStepFirstSectionNotch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="weigth"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void DrawFourthSection(WindowParametrs window,
            int offset, int width, int height, int weigth,
            ksPart windowPart,
            ksEntity planeXoy)
        {
            var xStartStepFourthSectionExtrude = -width * 2.0;
            var yStartStepFourthSectionExtrude = -height / 2;
            var weigthStepFourthSectionExtrude = weigth;

            ksEntity sketchStepFourthSectionExtrude =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionFourthSectionExtrude =
                    sketchStepFourthSectionExtrude.GetDefinition();
            sketchDefinitionFourthSectionExtrude.SetPlane(planeXoy);
            sketchStepFourthSectionExtrude.Create();
            ksDocument2D
                document2DFourthSectionExtrude =
                    sketchDefinitionFourthSectionExtrude.BeginEdit();
            DrawWindow(document2DFourthSectionExtrude,
                xStartStepFourthSectionExtrude,
                yStartStepFourthSectionExtrude, height,
                width / 2.0, 0);
            sketchDefinitionFourthSectionExtrude.EndEdit();
            Extrude(windowPart, sketchStepFourthSectionExtrude,
                weigthStepFourthSectionExtrude,
                (short)Direction_Type.dtNormal);

            var offsetFourthSectionNotch = offset + 20;
            var xStartStepFourthSectionNotch = -width * 2
                + offset;
            var yStartStepFourthSectionNotch = -height / 2
                + offsetFourthSectionNotch - offset;
            var widthStepFourthSectionNotch = width / 2
                - offsetFourthSectionNotch;
            var heighStepFourthSectionNotch = height - 
                offsetFourthSectionNotch * 2
                + offsetFourthSectionNotch;
            var weigthStepFourthSectionNotch = weigth + 100;

            ksEntity sketchStepFourthSectionNotch =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinitionFourthSectionNotch1 = 
                sketchStepFourthSectionNotch.GetDefinition();
            ksEntity stepPlaneOffsetFourthSectionNotch =
                windowPart.NewEntity((short)Obj3dType
                    .o3d_planeOffset);
            ksPlaneOffsetDefinition planeOffsetDefinitionFourthSectionNotch =
                stepPlaneOffsetFourthSectionNotch.GetDefinition();
            planeOffsetDefinitionFourthSectionNotch
                .SetPlane(planeXoy);
            planeOffsetDefinitionFourthSectionNotch.offset = 
                weigthStepFourthSectionExtrude;
            stepPlaneOffsetFourthSectionNotch.Create();
            ksEntity sketchFourthSectionNotch =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionFourthSectionNotch =
                    sketchFourthSectionNotch.GetDefinition();
            sketchDefinitionFourthSectionNotch.SetPlane(
                stepPlaneOffsetFourthSectionNotch);
            sketchFourthSectionNotch.Create();
            ksDocument2D document2DFourthSectionNotch = 
                sketchDefinitionFourthSectionNotch.BeginEdit();

            DrawWindow(document2DFourthSectionNotch,
                xStartStepFourthSectionNotch, yStartStepFourthSectionNotch,
                heighStepFourthSectionNotch,
                widthStepFourthSectionNotch, null);

            sketchDefinitionFourthSectionNotch1.EndEdit();
            sketchDefinitionFourthSectionNotch.EndEdit();
            Notch(windowPart, sketchFourthSectionNotch,
                weigthStepFourthSectionNotch);
            OpenFourthSection(window, offset, width,
                height, weigth, windowPart, planeXoy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="weigth"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void OpenFourthSection(WindowParametrs window, int offset,
            int width, int height, int weigth, ksPart windowPart,
            ksEntity planeXoy)
        {
            if (window.OpenSection == 4)
            {
                var xStartStepFourthOpenSectionExtrude = -width * 2 + offset;
                var yStartStepFourthOpenSectionExtrude = -height / 2 + offset;
                var widthStepFourthOpenSectionExtrude = width / 2 - offset * 2;
                var heighStepFourthOpenSectionExtrude = height - offset * 2;
                var weigthStepFourthOpenSectionExtrude = weigth;


                ksEntity stepPlaneOffsetFourthOpenSectionExtrude =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionFourthOpenSectionExtrude =
                    stepPlaneOffsetFourthOpenSectionExtrude.GetDefinition();
                planeOffsetDefinitionFourthOpenSectionExtrude
                    .SetPlane(planeXoy);
                planeOffsetDefinitionFourthOpenSectionExtrude.offset =
                    weigthStepFourthOpenSectionExtrude;
                stepPlaneOffsetFourthOpenSectionExtrude.Create();
                ksEntity sketchFourthOpenSectionExtrude =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionFourthOpenSectionExtrude =
                    sketchFourthOpenSectionExtrude.GetDefinition();
                sketchDefinitionFourthOpenSectionExtrude.SetPlane(
                    stepPlaneOffsetFourthOpenSectionExtrude);
                sketchFourthOpenSectionExtrude.Create();
                ksDocument2D document2DFourthOpenSectionExtrude = 
                    sketchDefinitionFourthOpenSectionExtrude.BeginEdit();

                DrawWindow(document2DFourthOpenSectionExtrude,
                    xStartStepFourthOpenSectionExtrude,
                    yStartStepFourthOpenSectionExtrude,
                    heighStepFourthOpenSectionExtrude,
                    widthStepFourthOpenSectionExtrude, null);

                sketchDefinitionFourthOpenSectionExtrude.EndEdit();
                Extrude(windowPart, sketchFourthOpenSectionExtrude,
                    weigthStepFourthOpenSectionExtrude,
                    (short)Direction_Type.dtNormal);

                var xStartStepFourthOpenSectionNotch = -width * 2
                    + offset * 2;
                var yStartStepFourthOpenSectionNotch = -height / 2
                    + offset * 2;
                var widthStepFourthOpenSectionNotch = width / 2
                    - offset * 4;
                var heighStepFourthOpenSectionNotch = height
                    - offset * 4;
                var weigthStepFourthOpenSectionNotch = weigth
                    + offset + 4;

                ksEntity stepPlaneOffsetFourthOpenSectionNotch =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionFourthOpenSectionNotch =
                    stepPlaneOffsetFourthOpenSectionNotch.GetDefinition();
                planeOffsetDefinitionFourthOpenSectionNotch
                    .SetPlane(planeXoy);
                planeOffsetDefinitionFourthOpenSectionNotch.offset =
                    weigthStepFourthOpenSectionNotch;
                stepPlaneOffsetFourthOpenSectionNotch.Create();
                ksEntity sketchFourthOpenSectionNotch =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition
                    sketchDefinitionFourthOpenSectionNotch =
                        sketchFourthOpenSectionNotch.GetDefinition();
                sketchDefinitionFourthOpenSectionNotch.SetPlane(
                    stepPlaneOffsetFourthOpenSectionNotch);
                sketchFourthOpenSectionNotch.Create();
                ksDocument2D document2DFourthOpenSectionNotch = 
                    sketchDefinitionFourthOpenSectionNotch.BeginEdit();

                DrawWindow(document2DFourthOpenSectionNotch,
                    xStartStepFourthOpenSectionNotch,
                    yStartStepFourthOpenSectionNotch,
                    heighStepFourthOpenSectionNotch,
                    widthStepFourthOpenSectionNotch,
                    null);

                sketchDefinitionFourthOpenSectionNotch.EndEdit();

                Notch(windowPart, sketchFourthOpenSectionNotch,
                    weigthStepFourthOpenSectionNotch);

                HandleLeftPositionFourthSection(window, width, windowPart,
                    stepPlaneOffsetFourthOpenSectionNotch);

                HandleRigthPositionFourthSection(window, offset,
                    width, windowPart,
                    stepPlaneOffsetFourthOpenSectionNotch);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetFourthOpenSectionNotch"></param>
        private void HandleRigthPositionFourthSection(WindowParametrs window,
            int offset,int width, ksPart windowPart, 
            ksEntity stepPlaneOffsetFourthOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Right)
            {
                var weightStepHandleRightFourthSection = 10;
                var baseHandleRightFourthSection = width;
                
                ksEntity sketchHandleRightFourthSection =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleRightFourthSection =
                    sketchHandleRightFourthSection.GetDefinition(); 
                sketchDefinitionHandleRightFourthSection.SetPlane(
                    stepPlaneOffsetFourthOpenSectionNotch); 
                sketchHandleRightFourthSection.Create();
                ksDocument2D document2DHandleRightFourthSection =
                    sketchDefinitionHandleRightFourthSection.BeginEdit();
                DrawWindow(document2DHandleRightFourthSection,
                    -baseHandleRightFourthSection - 545,
                    baseHandleRightFourthSection / 200 - offset / 8,
                    baseHandleRightFourthSection / 90.0,
                    -baseHandleRightFourthSection / 70.0, 180);
                sketchDefinitionHandleRightFourthSection.EndEdit();
                Extrude(windowPart, sketchHandleRightFourthSection,
                    weightStepHandleRightFourthSection,
                    (short)Direction_Type.dtNormal);
               
                var baseHandleRightFourthSection1 = width;
                ksEntity stepPlaneOffsetHandleRightFourthSection1 =
                    windowPart.NewEntity((short)
                    Obj3dType.o3d_planeOffset); 
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleRightFourthSection1 =
                        stepPlaneOffsetHandleRightFourthSection1
                            .GetDefinition();
                planeOffsetDefinitionHandleRightFourthSection1.SetPlane(
                    stepPlaneOffsetFourthOpenSectionNotch);
                planeOffsetDefinitionHandleRightFourthSection1.offset =
                    weightStepHandleRightFourthSection;
                stepPlaneOffsetHandleRightFourthSection1.Create(); 
                ksEntity sketchHandleRightFourthSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch); 
                ksSketchDefinition sketchDefinitionHandleRightFourthSection1 =
                    sketchHandleRightFourthSection1.GetDefinition();
                sketchDefinitionHandleRightFourthSection1.SetPlane(
                    stepPlaneOffsetHandleRightFourthSection1); 
                sketchHandleRightFourthSection1.Create(); 
                ksDocument2D document2DHandleRightFourthSection1 =
                    sketchDefinitionHandleRightFourthSection1.BeginEdit();

                DrawWindow(document2DHandleRightFourthSection1,
                    -baseHandleRightFourthSection - 550,
                    baseHandleRightFourthSection1 / 170.0,
                    baseHandleRightFourthSection1 / 45.0,
                    -baseHandleRightFourthSection1 / 9.0, 180);

                sketchDefinitionHandleRightFourthSection1.EndEdit();

                Extrude(windowPart, sketchHandleRightFourthSection1,
                    weightStepHandleRightFourthSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="width"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetFourthOpenSectionNotch"></param>
        private void HandleLeftPositionFourthSection(WindowParametrs window,
            int width, ksPart windowPart,
            ksEntity stepPlaneOffsetFourthOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Left)
            {
                var weightStepHandleLeftFourthSection = 10;
                var baseHandleLeftFourthSection = width;

                ksEntity sketchHandleLeftFourthSection =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleLeftFourthSection =
                    sketchHandleLeftFourthSection.GetDefinition(); 
                sketchDefinitionHandleLeftFourthSection.SetPlane(
                    stepPlaneOffsetFourthOpenSectionNotch); 
                sketchHandleLeftFourthSection.Create();
                ksDocument2D document2DHandleLeftFourthSection =
                    sketchDefinitionHandleLeftFourthSection.BeginEdit();
                DrawWindow(document2DHandleLeftFourthSection,
                    -baseHandleLeftFourthSection - 320,
                    baseHandleLeftFourthSection / 200.0,
                    baseHandleLeftFourthSection / 90.0,
                    -baseHandleLeftFourthSection / 70.0, 180);
                sketchDefinitionHandleLeftFourthSection.EndEdit();
                Extrude(windowPart, sketchHandleLeftFourthSection,
                    weightStepHandleLeftFourthSection,
                    (short)Direction_Type.dtNormal);
               
                var baseHandleLeftFourthSection1 = baseHandleLeftFourthSection * 2;
                ksEntity stepPlaneOffsetHandleLeftFourthSection1 =
                    windowPart.NewEntity((short)
                    Obj3dType.o3d_planeOffset); 
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleLeftFourthSection1 =
                        stepPlaneOffsetHandleLeftFourthSection1
                            .GetDefinition(); 
                planeOffsetDefinitionHandleLeftFourthSection1.SetPlane(
                    stepPlaneOffsetFourthOpenSectionNotch); 
                planeOffsetDefinitionHandleLeftFourthSection1.offset =
                    weightStepHandleLeftFourthSection; 
                stepPlaneOffsetHandleLeftFourthSection1.Create(); 
                ksEntity sketchHandleLeftFourthSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleLeftFourthSection1 =
                    sketchHandleLeftFourthSection1.GetDefinition(); 
                sketchDefinitionHandleLeftFourthSection1.SetPlane(
                    stepPlaneOffsetHandleLeftFourthSection1); 
                sketchHandleLeftFourthSection1.Create(); 
                ksDocument2D document2DHandleLeftFourthSection1 =
                    sketchDefinitionHandleLeftFourthSection1.BeginEdit();

                DrawWindow(document2DHandleLeftFourthSection1,
                    -baseHandleLeftFourthSection - 305,
                    baseHandleLeftFourthSection1 / 170.0,
                    baseHandleLeftFourthSection1 / 85.0,
                    +baseHandleLeftFourthSection1 / 15.0, 180);

                sketchDefinitionHandleLeftFourthSection1.EndEdit();

                Extrude(windowPart, sketchHandleLeftFourthSection1, 
                    weightStepHandleLeftFourthSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="weigth"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void DrawThirdSection(WindowParametrs window,
            int offset, int width, int height, int weigth,
            ksPart windowPart,
            ksEntity planeXoy)
        {
            var xStartStepThirdSectionExtrude = -width - width / 2;
            var yStartStepThirdSectionExtrude = -height / 2;
            var weigthStepThirdSectionExtrude = weigth;

            ksEntity sketchStepThirdSectionExtrude =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionThirdSectionExtrude =
                    sketchStepThirdSectionExtrude.GetDefinition();
            sketchDefinitionThirdSectionExtrude.SetPlane(planeXoy);
            sketchStepThirdSectionExtrude.Create();
            ksDocument2D
                document2DThirdSectionExtrude =
                    sketchDefinitionThirdSectionExtrude.BeginEdit();
            DrawWindow(document2DThirdSectionExtrude,
                xStartStepThirdSectionExtrude,
                yStartStepThirdSectionExtrude, height, width / 2.0, 0);
            sketchDefinitionThirdSectionExtrude.EndEdit();
            Extrude(windowPart, sketchStepThirdSectionExtrude,
                weigthStepThirdSectionExtrude,
                (short)Direction_Type.dtNormal);

            var offsetThirdSectionNotch = offset + 20;
            var xStartStepThirdSectionNotch = -width - width / 2
                + offset;
            var yStartStepThirdSectionNotch = -height / 2
                + offsetThirdSectionNotch - offset;
            var widthStepThirdSectionNotch = width / 2
                - offsetThirdSectionNotch;
            var heighStepThirdSectionNotch = height - offsetThirdSectionNotch *
                2 + offsetThirdSectionNotch;
            var weigthStepThirdSectionNotch = weigth + 100;

            ksEntity sketchStepThirdSectionNotch =
                windowPart.NewEntity((short)
                Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinitionThirdSectionNotch = 
                sketchStepThirdSectionNotch.GetDefinition();
            ksEntity stepPlaneOffsetThirdSectionNotch =
                windowPart.NewEntity((short)
                Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition planeOffsetDefinitionThirdSectionNotch =
                stepPlaneOffsetThirdSectionNotch.GetDefinition();
            planeOffsetDefinitionThirdSectionNotch.SetPlane(planeXoy);
            planeOffsetDefinitionThirdSectionNotch.offset = 
                weigthStepThirdSectionNotch;
            stepPlaneOffsetThirdSectionNotch.Create();
            ksEntity sketchThirdSectionNotch1 =
                windowPart.NewEntity((short)
                Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionThirdSectionNotch1 =
                    sketchThirdSectionNotch1.GetDefinition();
            sketchDefinitionThirdSectionNotch1.SetPlane(
                stepPlaneOffsetThirdSectionNotch);
            sketchThirdSectionNotch1.Create();
            ksDocument2D document2DThirdSectionNotch1 = 
                sketchDefinitionThirdSectionNotch1.BeginEdit();

            DrawWindow(document2DThirdSectionNotch1,
                xStartStepThirdSectionNotch,
                yStartStepThirdSectionNotch,
                heighStepThirdSectionNotch,
                widthStepThirdSectionNotch, null);

            sketchDefinitionThirdSectionNotch.EndEdit();
            sketchDefinitionThirdSectionNotch1.EndEdit();
            Notch(windowPart, sketchThirdSectionNotch1,
                weigthStepThirdSectionNotch);
            OpenThirdSection(window, offset, width,
                height, weigth, windowPart, planeXoy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="weigth"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void OpenThirdSection(WindowParametrs window, int offset,
            int width, int height, int weigth, 
            ksPart windowPart, ksEntity planeXoy)
        {
            if (window.OpenSection == 3)
            {
                var xStartStepThirdOpenSectionExtrude = -width - width / 2
                    + offset;
                var yStartStepThirdOpenSectionExtrude = -height / 2 
                    + offset;
                var widthStepThirdOpenSectionExtrude = width / 2 
                    - offset * 2;
                var heighStepThirdOpenSectionExtrude = height 
                    - offset * 2;
                var weigthStepThirdOpenSectionExtrude = weigth;
                
                ksEntity stepPlaneOffsetThirdOpenSectionExtrude =
                    windowPart.NewEntity((short)Obj3dType.o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionThirdOpenSectionExtrude =
                    stepPlaneOffsetThirdOpenSectionExtrude.GetDefinition();
                planeOffsetDefinitionThirdOpenSectionExtrude
                    .SetPlane(planeXoy);
                planeOffsetDefinitionThirdOpenSectionExtrude.offset =
                    weigthStepThirdOpenSectionExtrude;
                stepPlaneOffsetThirdOpenSectionExtrude.Create();
                ksEntity sketchThirdOpenSectionExtrude =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionThirdOpenSectionExtrude =
                    sketchThirdOpenSectionExtrude.GetDefinition();
                sketchDefinitionThirdOpenSectionExtrude.SetPlane(
                    stepPlaneOffsetThirdOpenSectionExtrude);
                sketchThirdOpenSectionExtrude.Create();
                ksDocument2D document2DThirdOpenSectionExtrude = 
                    sketchDefinitionThirdOpenSectionExtrude.BeginEdit();

                DrawWindow(document2DThirdOpenSectionExtrude,
                    xStartStepThirdOpenSectionExtrude,
                    yStartStepThirdOpenSectionExtrude,
                    heighStepThirdOpenSectionExtrude,
                    widthStepThirdOpenSectionExtrude, null);

                sketchDefinitionThirdOpenSectionExtrude.EndEdit();

                Extrude(windowPart, sketchThirdOpenSectionExtrude,
                    weigthStepThirdOpenSectionExtrude,
                    (short)Direction_Type.dtNormal);

                var xStartStepThirdOpenSectionNotch = -width - width / 2
                    + offset * 2;
                var yStartStepThirdOpenSectionNotch = -height / 2
                    + offset * 2;
                var widthStepThirdOpenSectionNotch = width / 2
                    - offset * 4;
                var heighStepThirdOpenSectionNotch = height
                    - offset * 4;
                var weigthStepThirdOpenSectionNotch = weigth
                    + offset + 4;
                
                ksEntity stepPlaneOffsetThirdOpenSectionNotch =
                    windowPart.NewEntity((short)
                    Obj3dType.o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionThirdOpenSectionNotch =
                    stepPlaneOffsetThirdOpenSectionNotch.GetDefinition();
                planeOffsetDefinitionThirdOpenSectionNotch.SetPlane(planeXoy);
                planeOffsetDefinitionThirdOpenSectionNotch.offset =
                    weigthStepThirdOpenSectionNotch;
                stepPlaneOffsetThirdOpenSectionNotch.Create();
                ksEntity sketchThirdOpenSectionNotch =
                    windowPart.NewEntity((short)
                    Obj3dType.o3d_sketch);
                ksSketchDefinition
                    sketchDefinitionThirdOpenSectionNotch =
                        sketchThirdOpenSectionNotch.GetDefinition();
                sketchDefinitionThirdOpenSectionNotch.SetPlane(
                    stepPlaneOffsetThirdOpenSectionNotch);
                sketchThirdOpenSectionNotch.Create();
                ksDocument2D document2DThirdOpenSectionNotch = 
                    sketchDefinitionThirdOpenSectionNotch.BeginEdit();

                DrawWindow(document2DThirdOpenSectionNotch,
                    xStartStepThirdOpenSectionNotch,
                    yStartStepThirdOpenSectionNotch,
                    heighStepThirdOpenSectionNotch,
                    widthStepThirdOpenSectionNotch, null);

                sketchDefinitionThirdOpenSectionNotch.EndEdit();
                Notch(windowPart, sketchThirdOpenSectionNotch,
                    weigthStepThirdOpenSectionNotch);
                HandleLeftpositionThirdSection(window, width, windowPart,
                    stepPlaneOffsetThirdOpenSectionNotch);
                HandleRigthPositionThirdSection(window, offset, width,
                    windowPart, stepPlaneOffsetThirdOpenSectionNotch);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetThirdOpenSectionNotch"></param>
        private void HandleRigthPositionThirdSection(WindowParametrs window,
            int offset,int width, ksPart windowPart,
            ksEntity stepPlaneOffsetThirdOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Right)
            {
                var weightStepHandleRightThirdSection = 10;
                var baseHandleRightThirdSection = width;
                
                ksEntity sketchHandleRightThirdSection =
                    windowPart.NewEntity((short)
                    Obj3dType.o3d_sketch); 
                ksSketchDefinition sketchDefinitionHandleRightThirdSection =
                    sketchHandleRightThirdSection.GetDefinition(); 
                sketchDefinitionHandleRightThirdSection.SetPlane(
                    stepPlaneOffsetThirdOpenSectionNotch); 
                sketchHandleRightThirdSection.Create(); 
                ksDocument2D document2DHandleRightThirdSection =
                    sketchDefinitionHandleRightThirdSection.BeginEdit();
                DrawWindow(document2DHandleRightThirdSection,
                    -baseHandleRightThirdSection - 261,
                    baseHandleRightThirdSection / 200 - offset / 8,
                    baseHandleRightThirdSection / 90.0,
                    -baseHandleRightThirdSection / 70.0, 180);
                sketchDefinitionHandleRightThirdSection.EndEdit();
                Extrude(windowPart, sketchHandleRightThirdSection,
                    weightStepHandleRightThirdSection,
                    (short)Direction_Type.dtNormal);

                var baseHandleRightThirdSection1 = width;
                ksEntity stepPlaneOffsetHandleRightThirdSection1 =
                    windowPart.NewEntity((short)
                    Obj3dType.o3d_planeOffset); 
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleRightThirdSection1 =
                        stepPlaneOffsetHandleRightThirdSection1
                            .GetDefinition();  
                planeOffsetDefinitionHandleRightThirdSection1.SetPlane(
                    stepPlaneOffsetThirdOpenSectionNotch); 
                planeOffsetDefinitionHandleRightThirdSection1.offset =
                    weightStepHandleRightThirdSection; 
                stepPlaneOffsetHandleRightThirdSection1.Create(); 
                ksEntity sketchHandleRightThirdSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);  
                ksSketchDefinition sketchDefinitionHandleRightThirdSection1 =
                    sketchHandleRightThirdSection1.GetDefinition(); 
                sketchDefinitionHandleRightThirdSection1.SetPlane(
                    stepPlaneOffsetHandleRightThirdSection1);
                sketchHandleRightThirdSection1.Create();
                ksDocument2D document2DHandleRightThirdSection1 =
                    sketchDefinitionHandleRightThirdSection1.BeginEdit();

                DrawWindow(document2DHandleRightThirdSection1,
                    -baseHandleRightThirdSection - 265,
                    baseHandleRightThirdSection1 / 170.0,
                    baseHandleRightThirdSection1 / 45.0,
                    -baseHandleRightThirdSection1 / 9.0, 180);
                sketchDefinitionHandleRightThirdSection1.EndEdit();
                Extrude(windowPart, sketchHandleRightThirdSection1,
                    weightStepHandleRightThirdSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="width"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetThirdOpenSectionNotch"></param>
        private void HandleLeftpositionThirdSection(WindowParametrs window,
            int width, ksPart windowPart,
            ksEntity stepPlaneOffsetThirdOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Left)
            {
                var weightStepHandleLeftThirdSection = 10;
                var baseHandleLeftThirdSection = width;
                
                ksEntity sketchHandleLeftThirdSection =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch); 
                ksSketchDefinition sketchDefinitionHandleLeftThirdSection =
                    sketchHandleLeftThirdSection.GetDefinition(); 
                sketchDefinitionHandleLeftThirdSection.SetPlane(
                    stepPlaneOffsetThirdOpenSectionNotch); 
                sketchHandleLeftThirdSection.Create(); 
                ksDocument2D document2DHandleLeftThirdSection = 
                    sketchDefinitionHandleLeftThirdSection.BeginEdit();
                DrawWindow(document2DHandleLeftThirdSection,
                    -baseHandleLeftThirdSection - 35,
                    baseHandleLeftThirdSection / 200.0, 
                    baseHandleLeftThirdSection / 90.0,
                    -baseHandleLeftThirdSection / 70.0, 180);
                sketchDefinitionHandleLeftThirdSection.EndEdit();
                Extrude(windowPart, sketchHandleLeftThirdSection,
                    weightStepHandleLeftThirdSection,
                    (short)Direction_Type.dtNormal);

                var baseHandleLeftThirdSection1 = baseHandleLeftThirdSection * 2;
                ksEntity stepPlaneOffsetHandleLeftThirdSection =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleLeftThirdSection =
                        stepPlaneOffsetHandleLeftThirdSection
                            .GetDefinition(); 
                planeOffsetDefinitionHandleLeftThirdSection.SetPlane(
                    stepPlaneOffsetThirdOpenSectionNotch); 
                planeOffsetDefinitionHandleLeftThirdSection.offset =
                    weightStepHandleLeftThirdSection; 
                stepPlaneOffsetHandleLeftThirdSection.Create(); 
                ksEntity sketchHandleLeftThirdSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch); 
                ksSketchDefinition sketchDefinitionHandleLeftThirdSection1 =
                    sketchHandleLeftThirdSection1.GetDefinition();
                sketchDefinitionHandleLeftThirdSection1.SetPlane(
                    stepPlaneOffsetHandleLeftThirdSection); 
                sketchHandleLeftThirdSection1.Create(); 
                ksDocument2D document2DHandleLeftThirdSection1 =
                    sketchDefinitionHandleLeftThirdSection1.BeginEdit();

                DrawWindow(document2DHandleLeftThirdSection1,
                    -baseHandleLeftThirdSection - 20,
                    baseHandleLeftThirdSection1 / 170.0,
                    baseHandleLeftThirdSection1 / 85.0,
                    +baseHandleLeftThirdSection1 / 15.0, 180);
                sketchDefinitionHandleLeftThirdSection1.EndEdit();
                Extrude(windowPart, sketchHandleLeftThirdSection1, 
                    weightStepHandleLeftThirdSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="weigth"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void DrawSecondSection(WindowParametrs window,
            int offset, int width, int height, int weigth,
            ksPart windowPart,
            ksEntity planeXoy)
        {
            var xStartStepSecondSectionExtrude = -width;
            var yStartStepSecondSectionExtrude = -height / 2;
            var weigthStepSecondSectionExtrude = weigth;

            ksEntity sketchStepSecondSectionExtrude =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionSecondSectionExtrude =
                    sketchStepSecondSectionExtrude.GetDefinition();
            sketchDefinitionSecondSectionExtrude.SetPlane(planeXoy);
            sketchStepSecondSectionExtrude.Create();
            ksDocument2D
                document2DSecondSectionExtrude =
                    sketchDefinitionSecondSectionExtrude.BeginEdit();
            DrawWindow(document2DSecondSectionExtrude,
                xStartStepSecondSectionExtrude,
                yStartStepSecondSectionExtrude, height,
                width / 2.0, 0);
            sketchDefinitionSecondSectionExtrude.EndEdit();
            Extrude(windowPart, sketchStepSecondSectionExtrude,
                weigthStepSecondSectionExtrude,
                (short)Direction_Type.dtNormal);

            var offsetSecondSectionNotch = offset + 20;
            var xStartStepSecondSectionNotch = -width
                + offset;
            var yStartStepSecondSectionNotch = -height / 2
                + offsetSecondSectionNotch - offset;
            var widthStepSecondSectionNotch = width / 2
                - offsetSecondSectionNotch;
            var heighStepSecondSectionNotch = height
                - offsetSecondSectionNotch * 2 + offsetSecondSectionNotch;
            var weigthStepSecondSectionNotch = weigth + 100;

            ksEntity sketchStepSecondSectionNotch =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinitionSecondSectionNotch =
                sketchStepSecondSectionNotch.GetDefinition();
            ksEntity stepPlaneOffsetSecondSectionNotch =
                windowPart.NewEntity((short)Obj3dType
                    .o3d_planeOffset);
            ksPlaneOffsetDefinition planeOffsetDefinitionSecondSectionNotch =
                stepPlaneOffsetSecondSectionNotch.GetDefinition();
            planeOffsetDefinitionSecondSectionNotch
                .SetPlane(planeXoy);
            planeOffsetDefinitionSecondSectionNotch.offset =
                weigthStepSecondSectionNotch;
            stepPlaneOffsetSecondSectionNotch.Create();
            ksEntity sketchSecondSectionNotch =
                windowPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition
                sketchDefinitionSecondSectionNotch1 =
                    sketchSecondSectionNotch.GetDefinition();
            sketchDefinitionSecondSectionNotch1.SetPlane(
                stepPlaneOffsetSecondSectionNotch);
            sketchSecondSectionNotch.Create();
            ksDocument2D document2DSecondSectionNotch1 =
                sketchDefinitionSecondSectionNotch1.BeginEdit();

            DrawWindow(document2DSecondSectionNotch1,
                xStartStepSecondSectionNotch,
                yStartStepSecondSectionNotch,
                heighStepSecondSectionNotch,
                widthStepSecondSectionNotch, null);

            sketchDefinitionSecondSectionNotch.EndEdit();
            sketchDefinitionSecondSectionNotch1.EndEdit();
            Notch(windowPart, sketchSecondSectionNotch,
                weigthStepSecondSectionNotch);

            OpenSecondSection(window, offset, width, 
                height, weigth, windowPart, planeXoy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="weigth"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void OpenSecondSection(WindowParametrs window, int offset,
            int width, int height, int weigth,
            ksPart windowPart, ksEntity planeXoy)
        {
            if (window.OpenSection == 2)
            {
                var xStartStepSecondOpenSectionExtrude = -width + offset;
                var yStartStepSecondOpenSectionExtrude = -height / 2
                    + offset;
                var widthStepSecondOpenSectionExtrude = width / 2
                    - offset * 2;
                var heighStepSecondOpenSectionExtrude = height
                    - offset * 2;
                var weigthStepSecondOpenSectionExtrude = weigth;

                ksEntity stepPlaneOffsetSecondOpenSectionExtrude =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionSecondOpenSectionExtrude =
                    stepPlaneOffsetSecondOpenSectionExtrude.GetDefinition();
                planeOffsetDefinitionSecondOpenSectionExtrude
                    .SetPlane(planeXoy);
                planeOffsetDefinitionSecondOpenSectionExtrude.offset =
                    weigthStepSecondOpenSectionExtrude;
                stepPlaneOffsetSecondOpenSectionExtrude.Create();
                ksEntity sketchSecondOpenSectionExtrude =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionSecondOpenSectionExtrude =
                    sketchSecondOpenSectionExtrude.GetDefinition();
                sketchDefinitionSecondOpenSectionExtrude.SetPlane(
                    stepPlaneOffsetSecondOpenSectionExtrude);
                sketchSecondOpenSectionExtrude.Create();
                ksDocument2D document2DSecondOpenSectionExtrude =
                    sketchDefinitionSecondOpenSectionExtrude.BeginEdit();

                DrawWindow(document2DSecondOpenSectionExtrude,
                    xStartStepSecondOpenSectionExtrude,
                    yStartStepSecondOpenSectionExtrude,
                    heighStepSecondOpenSectionExtrude,
                    widthStepSecondOpenSectionExtrude, null);

                sketchDefinitionSecondOpenSectionExtrude.EndEdit();
                Extrude(windowPart, sketchSecondOpenSectionExtrude,
                    weigthStepSecondOpenSectionExtrude,
                    (short)Direction_Type.dtNormal);

                var xStartStepSecondOpenSectionNotch = -width
                    + offset * 2;
                var yStartStepSecondOpenSectionNotch = -height / 2
                    + offset * 2;
                var widthStepSecondOpenSectionNotch = width / 2
                    - offset * 4;
                var heighStepSecondOpenSectionNotch = height
                    - offset * 4;
                var weigthStepSecondOpenSectionNotch = weigth
                    + offset + 4;

                ksEntity stepPlaneOffsetSecondOpenSectionNotch =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset);
                ksPlaneOffsetDefinition planeOffsetDefinitionSecondOpenSectionNotch =
                    stepPlaneOffsetSecondOpenSectionNotch.GetDefinition();
                planeOffsetDefinitionSecondOpenSectionNotch
                    .SetPlane(planeXoy);
                planeOffsetDefinitionSecondOpenSectionNotch.offset =
                    weigthStepSecondOpenSectionNotch;
                stepPlaneOffsetSecondOpenSectionNotch.Create();
                ksEntity sketchSecondOpenSectionNotch =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition
                    sketchDefinitionSecondOpenSectionNotch =
                        sketchSecondOpenSectionNotch.GetDefinition();
                sketchDefinitionSecondOpenSectionNotch.SetPlane(
                    stepPlaneOffsetSecondOpenSectionNotch);
                sketchSecondOpenSectionNotch.Create();
                ksDocument2D document2DSecondOpenSectionNotch =
                    sketchDefinitionSecondOpenSectionNotch.BeginEdit();

                DrawWindow(document2DSecondOpenSectionNotch,
                    xStartStepSecondOpenSectionNotch,
                    yStartStepSecondOpenSectionNotch,
                    heighStepSecondOpenSectionNotch,
                    widthStepSecondOpenSectionNotch, null);

                sketchDefinitionSecondOpenSectionNotch.EndEdit();
                Notch(windowPart, sketchSecondOpenSectionNotch,
                    weigthStepSecondOpenSectionNotch);

                HandleLeftPositionSecondSection(window, width, windowPart,
                    stepPlaneOffsetSecondOpenSectionNotch);
                HandleRigthPositionSecondSection(window, offset, width,
                    windowPart, stepPlaneOffsetSecondOpenSectionNotch);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetSecondOpenSectionNotch"></param>
        private void HandleRigthPositionSecondSection(WindowParametrs window,
            int offset, int width, ksPart windowPart, 
            ksEntity stepPlaneOffsetSecondOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Right)
            {
                var weightStepHandleRightSecondSection = 10;
                var baseHandleRightSecondSection = width;
                
                ksEntity sketchHandleRightSecondSection =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch); 
                ksSketchDefinition sketchDefinitionHandleRightSecondSection =
                    sketchHandleRightSecondSection.GetDefinition(); 
                sketchDefinitionHandleRightSecondSection.SetPlane(
                    stepPlaneOffsetSecondOpenSectionNotch); 
                sketchHandleRightSecondSection.Create(); 
                ksDocument2D document2DHandleRightSecondSection =
                    sketchDefinitionHandleRightSecondSection.BeginEdit();
                DrawWindow(document2DHandleRightSecondSection,
                    -baseHandleRightSecondSection + 26,
                    baseHandleRightSecondSection / 200 - offset / 8,
                    baseHandleRightSecondSection / 90.0,
                    -baseHandleRightSecondSection / 70.0, 180);
                sketchDefinitionHandleRightSecondSection.EndEdit();
                Extrude(windowPart, sketchHandleRightSecondSection,
                    weightStepHandleRightSecondSection,
                    (short)Direction_Type.dtNormal);

                var baseHandleRightSecondSection1 = width;
                ksEntity stepPlaneOffsetHandleRightSecondSection =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset); 
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleRightSecondSection =
                        stepPlaneOffsetHandleRightSecondSection
                            .GetDefinition();
                planeOffsetDefinitionHandleRightSecondSection.SetPlane(
                    stepPlaneOffsetSecondOpenSectionNotch); 
                planeOffsetDefinitionHandleRightSecondSection.offset =
                    weightStepHandleRightSecondSection; 
                stepPlaneOffsetHandleRightSecondSection.Create();
                ksEntity sketchHandleRightSecondSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);  
                ksSketchDefinition sketchDefinitionHandleRightSecondSection1 =
                    sketchHandleRightSecondSection1.GetDefinition(); 
                sketchDefinitionHandleRightSecondSection1.SetPlane(
                    stepPlaneOffsetHandleRightSecondSection); 
                sketchHandleRightSecondSection1.Create(); 
                ksDocument2D document2DHandleRightSecondSection1 =
                    sketchDefinitionHandleRightSecondSection1.BeginEdit();

                DrawWindow(document2DHandleRightSecondSection1,
                    -baseHandleRightSecondSection + 20,
                    baseHandleRightSecondSection1 / 170.0,
                    baseHandleRightSecondSection1 / 45.0,
                    -baseHandleRightSecondSection1 / 9.0, 180);
                sketchDefinitionHandleRightSecondSection1.EndEdit();
                Extrude(windowPart, sketchHandleRightSecondSection1,
                    weightStepHandleRightSecondSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="width"></param>
        /// <param name="windowPart"></param>
        /// <param name="stepPlaneOffsetSecondOpenSectionNotch"></param>
        private void HandleLeftPositionSecondSection(WindowParametrs window,
            int width, ksPart windowPart,
            ksEntity stepPlaneOffsetSecondOpenSectionNotch)
        {
            if (window.HandlePosition == HandlePosition.Left)
            {
                var weightStepHandleLeftSecondSection = 10;
                var baseHandleLeftSecondSection = width;


                ksEntity sketchHandleLeftSecondSection =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDefinitionHandleLeftSecondSection =
                    sketchHandleLeftSecondSection.GetDefinition(); 
                sketchDefinitionHandleLeftSecondSection.SetPlane(
                    stepPlaneOffsetSecondOpenSectionNotch); 
                sketchHandleLeftSecondSection.Create(); 
                ksDocument2D document2DHandleLeftSecondSection =
                    sketchDefinitionHandleLeftSecondSection.BeginEdit();
                DrawWindow(document2DHandleLeftSecondSection,
                    -baseHandleLeftSecondSection + 251,
                    baseHandleLeftSecondSection / 200.0,
                    baseHandleLeftSecondSection / 90.0,
                    -baseHandleLeftSecondSection / 70.0, 180);
                sketchDefinitionHandleLeftSecondSection.EndEdit();
                Extrude(windowPart, sketchHandleLeftSecondSection,
                    weightStepHandleLeftSecondSection,
                    (short)Direction_Type.dtNormal);

                var baseHandleLeftSecondSection1 = baseHandleLeftSecondSection * 2;
                ksEntity stepPlaneOffsetHandleLeftSecondSection =
                    windowPart.NewEntity((short)Obj3dType
                        .o3d_planeOffset); 
                ksPlaneOffsetDefinition
                    planeOffsetDefinitionHandleLeftSecondSection =
                        stepPlaneOffsetHandleLeftSecondSection
                            .GetDefinition();
                planeOffsetDefinitionHandleLeftSecondSection.SetPlane(
                    stepPlaneOffsetSecondOpenSectionNotch);
                planeOffsetDefinitionHandleLeftSecondSection.offset =
                    weightStepHandleLeftSecondSection;
                stepPlaneOffsetHandleLeftSecondSection.Create(); 
                ksEntity sketchHandleLeftSecondSection1 =
                    windowPart.NewEntity((short)Obj3dType.o3d_sketch); 
                ksSketchDefinition sketchDefinitionHandleLeftSecondSection1 =
                    sketchHandleLeftSecondSection1.GetDefinition(); 
                sketchDefinitionHandleLeftSecondSection1.SetPlane(
                    stepPlaneOffsetHandleLeftSecondSection); 
                sketchHandleLeftSecondSection1.Create(); 
                ksDocument2D document2DHandleLeftSecondSection1 =
                    sketchDefinitionHandleLeftSecondSection1.BeginEdit();

                DrawWindow(document2DHandleLeftSecondSection1,
                    -baseHandleLeftSecondSection + 265,
                    baseHandleLeftSecondSection1 / 170.0,
                    baseHandleLeftSecondSection1 / 85.0,
                    +baseHandleLeftSecondSection1 / 15.0, 180);
                sketchDefinitionHandleLeftSecondSection1.EndEdit();
                Extrude(windowPart, sketchHandleLeftSecondSection1,
                    weightStepHandleLeftSecondSection,
                    (short)Direction_Type.dtNormal);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="doc2D"></param>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="heigth"></param>
        /// <param name="width"></param>
        /// <param name="ang"></param>
        private void DrawWindow(ksDocument2D doc2D, double xStart,
            double yStart, double heigth,
            double width, double? ang)
        {
            var param =
                (ksRectangleParam) _Kompas.GetParamStruct((short) 
                StructType2DEnum.ko_RectangleParam);

            param.x = xStart;
            param.y = yStart;
            param.width = width;
            param.height = heigth;

            if (ang != null)
                param.ang = (double) ang;

            param.style = 1;
            doc2D.ksRectangle(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="sketch"></param>
        /// <param name="length"></param>
        /// <param name="type"></param>
        private static void Extrude(ksPart part, ksEntity sketch, 
            int length, short type)
        {
            ksEntity extrude = part.NewEntity((short)
                Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition extrDef = 
                extrude.GetDefinition();
            extrDef.directionType = type;
            extrDef.SetSketch(sketch);
            ksExtrusionParam extrudeParam = 
                extrDef.ExtrusionParam();
            extrudeParam.depthNormal = length;
            extrude.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="sketch"></param>
        /// <param name="heigth"></param>
        private static void Notch(ksPart part, ksEntity sketch, 
            int heigth)
        {
            ksEntity cutExtrude = part.NewEntity((short) 
                Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutextrDef = 
                cutExtrude.GetDefinition();
            cutextrDef.directionType =
                (short) Direction_Type.dtNormal;
            cutextrDef.SetSketch(sketch);
            ksExtrusionParam cutExtrParam = 
                cutextrDef.ExtrusionParam();
            cutExtrParam.depthNormal = heigth;
            cutExtrude.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartAndConnectToKompas()
        {
            try
            {
                if (_Kompas != null)
                {
                    _Kompas.Visible = true;
                    _Kompas.ActivateControllerAPI();
                }

                if (_Kompas == null)
                {
                    var t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _Kompas = (KompasObject) Activator.CreateInstance(t);

                    StartAndConnectToKompas();

                    if (_Kompas == null) throw new Exception("Нет связи с Kompas3D.");
                }
            }
            catch (COMException)
            {
                _Kompas = null;
                StartAndConnectToKompas();
            }
        }
    }
}