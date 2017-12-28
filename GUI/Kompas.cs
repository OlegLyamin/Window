using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

namespace GUI
{
    /// <summary>
    /// </summary>
    internal class Kompas
    {
        /// <summary>
        ///     объект компас
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        ///     метод для построения окна в компасе
        /// </summary>
        /// <param name="window"></param>
        public void BuildWindow(WindowParametrs window)
        {
            StartAndConnectToKompas();

            if (_kompas == null)
            {
                throw new Exception("Не возможно построить" +
                                    " деталь. Нет связи с Kompas.");
            }
                
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }
                
            var coordinatesGrid = new CoordinatesGrid(window).Grid;

            var height = window.LengthHeight;
            var width = window.LengthWidth;
            var weigth = window.LengthWeight;
            var offset = 20;

            var widthNotch = width / 2 - offset * 2;
            var heighNotch = height - offset * 2;
            var weigthNotch = weigth + 100;

            ksDocument3D document = _kompas.Document3D();
            document.Create();
            ksPart windowPart = document.GetPart((short)
                Part_Type.pTop_Part);

            ksEntity planeXoy = windowPart.GetDefaultEntity((short)
                Obj3dType.o3d_planeXOY);


            DrawAllSections(window, windowPart, planeXoy, height, width, weigth, coordinatesGrid, widthNotch, heighNotch,
                weigthNotch);

            if (window.OpenSection != 0)
            {
                DrawOpenSection(windowPart, planeXoy, coordinatesGrid, weigth, window);
            }
           
        }

        /// <summary>
        ///     Построение ручки
        /// </summary>
        /// <param name="window"></param>
        /// <param name="coordinatesGrid"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        private void DrawHandle(WindowParametrs window, Dictionary<int, Dictionary<string, double>> coordinatesGrid, ksPart windowPart, ksEntity planeXoy)
        {
            var xStartPartOne = coordinatesGrid[6]["xStartPartOne"];
            var yStartPartOne = coordinatesGrid[6]["yStartPartOne"];
            var heigthPartOne = coordinatesGrid[6]["HeigthPartOne"];
            var widthPartOne = coordinatesGrid[6]["WidthPartOne"];
            var weithPartOne = coordinatesGrid[6]["WeigthPartOne"];
            var planeOffsetOne = CreateOffsetPlane(windowPart, planeXoy, window.LengthWeight * 2);
            ExtrudeOdject(heigthPartOne, widthPartOne, weithPartOne, windowPart, planeOffsetOne, xStartPartOne, yStartPartOne,
                Obj3dType.o3d_bossExtrusion);

            var xStartPartTwo = coordinatesGrid[6]["xStartPartTwo"];
            var yStartPartTwo = coordinatesGrid[6]["yStartPartTwo"];
            var heigthPartTwo = coordinatesGrid[6]["HeigthPartTwo"];
            var widthPartTwo = coordinatesGrid[6]["WidthPartTwo"];
            var weithPartTwo = coordinatesGrid[6]["WeigthPartTwo"];
            var planeOffsetTwo = CreateOffsetPlane(windowPart, planeXoy, window.LengthWeight * 2 + weithPartOne);
            ExtrudeOdject(heigthPartTwo, widthPartTwo, weithPartTwo, windowPart, planeOffsetTwo, xStartPartTwo, yStartPartTwo,
                Obj3dType.o3d_bossExtrusion);
        }


        /// <summary>
        ///     Построение всех секций
        /// </summary>
        /// <param name="window"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="weigth"></param>
        /// <param name="coordinatesGrid"></param>
        /// <param name="widthNotch"></param>
        /// <param name="heighNotch"></param>
        /// <param name="weigthNotch"></param>
        private void DrawAllSections(WindowParametrs window, ksPart windowPart, ksEntity planeXoy, int height, int width,
            int weigth, Dictionary<int, Dictionary<string, double>> coordinatesGrid, int widthNotch, int heighNotch,
            int weigthNotch)
        {
            for (var i = 1; i <= window.SectionNumber; i++)
                DrawSection(windowPart, planeXoy, height, width / 2.0, weigth, coordinatesGrid[i]["xStartExtrude"],
                    coordinatesGrid[i]["yStartExtrude"], coordinatesGrid[i]["xStartNotch"],
                    coordinatesGrid[i]["yStartNotch"],
                    widthNotch, heighNotch, weigthNotch);
        }

        /// <summary>
        ///     Построение открытой секции
        /// </summary>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        /// <param name="coordinatesGrid"></param>
        /// <param name="weigth"></param>
        /// <param name="window"></param>
        private void DrawOpenSection(ksPart windowPart, ksEntity planeXoy,
            Dictionary<int, Dictionary<string, double>> coordinatesGrid, int weigth, WindowParametrs window)
        {
            var planeOffset = CreateOffsetPlane(windowPart, planeXoy, weigth);
            DrawSection(windowPart, planeOffset, coordinatesGrid[5]["heighSectionExtrude"],
                coordinatesGrid[5]["widthSectionExtrude"],
                weigth, coordinatesGrid[5]["xStartExtrude"],
                coordinatesGrid[5]["yStartExtrude"], coordinatesGrid[5]["xStartNotch"],
                coordinatesGrid[5]["yStartNotch"],
                coordinatesGrid[5]["widthSectionNotch"], coordinatesGrid[5]["heightSectionNotch"],
                coordinatesGrid[5]["weigthSectionNotch"]);

            DrawHandle(window, coordinatesGrid, windowPart, planeXoy);
        }

        /// <summary>
        ///     Построение одной конкретной секции
        /// </summary>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="weigth"></param>
        /// <param name="xStartExtrude"></param>
        /// <param name="yStartExtrude"></param>
        /// <param name="xStartNotch"></param>
        /// <param name="yStartNotch"></param>
        /// <param name="widthNotch"></param>
        /// <param name="heighNotch"></param>
        /// <param name="weigthNotch"></param>
        private void DrawSection(ksPart windowPart, ksEntity planeXoy, double height, double width, double weigth,
            double xStartExtrude, double yStartExtrude, double xStartNotch, double yStartNotch, double widthNotch,
            double heighNotch, double weigthNotch)
        {
            ExtrudeOdject(height, width, weigth, windowPart, planeXoy, xStartExtrude, yStartExtrude,
                Obj3dType.o3d_bossExtrusion);
            ExtrudeOdject(heighNotch, widthNotch, weigthNotch, windowPart,
                CreateOffsetPlane(windowPart, planeXoy, weigthNotch), xStartNotch, yStartNotch,
                Obj3dType.o3d_cutExtrusion);
        }

        /// <summary>
        ///     Создание плоскости с отступом
        /// </summary>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        /// <param name="weigthNotch"></param>
        /// <returns></returns>
        private static ksEntity CreateOffsetPlane(ksPart windowPart, ksEntity planeXoy, double weigthNotch)
        {
            ksEntity planeOffsetSection =
                windowPart.NewEntity((short) Obj3dType
                    .o3d_planeOffset);
            ksPlaneOffsetDefinition
                planeOffsetDefinition =
                    planeOffsetSection.GetDefinition();
            planeOffsetDefinition.SetPlane(planeXoy);
            planeOffsetDefinition.offset =
                weigthNotch;
            planeOffsetSection.Create();
            return planeOffsetSection;
        }

        /// <summary>
        ///     Выдавливание конкретной секции
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="weigth"></param>
        /// <param name="windowPart"></param>
        /// <param name="planeXoy"></param>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="type"></param>
        private void ExtrudeOdject(double height, double width, double weigth, ksPart windowPart, ksEntity planeXoy,
            double xStart, double yStart, Obj3dType type)
        {
            ksEntity sketch = windowPart.NewEntity((short) Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinition = sketch.GetDefinition();
            sketchDefinition.SetPlane(planeXoy);
            sketch.Create();

            ksDocument2D document2D = sketchDefinition.BeginEdit();
            DrawRectangle(document2D, xStart, yStart, height, width, null);
            sketchDefinition.EndEdit();

            Extrusion(windowPart, sketch, weigth, type);
        }

        /// <summary>
        /// </summary>
        /// <param name="doc2D"></param>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="heigth"></param>
        /// <param name="width"></param>
        /// <param name="ang"></param>
        private void DrawRectangle(ksDocument2D doc2D, double xStart,
            double yStart, double heigth,
            double width, double? ang)
        {
            var param =
                (ksRectangleParam) _kompas.GetParamStruct((short)
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
        ///     метод выреза/выдавливания фигуры
        /// </summary>
        /// <param name="part"></param>
        /// <param name="sketch"></param>
        /// <param name="heigth"></param>
        /// <param name="type"></param>
        private static void Extrusion(ksPart part, ksEntity sketch, double heigth, Obj3dType type)
        {
            ksEntity cutExtrude = part.NewEntity((short) type);
            var cutextrDef = cutExtrude.GetDefinition();
            cutextrDef.directionType = (short) Direction_Type.dtNormal;
            cutextrDef.SetSketch(sketch);
            ksExtrusionParam cutExtrParam = cutextrDef.ExtrusionParam();
            cutExtrParam.depthNormal = heigth;
            cutExtrude.Create();
        }

        /// <summary>
        ///     метод для запуска и открытия компаса в одном окне
        /// </summary>
        private void StartAndConnectToKompas()
        {
            try
            {
                if (_kompas != null)
                {
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                }

                if (_kompas == null)
                {
                    var t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject) Activator.CreateInstance(t);

                    StartAndConnectToKompas();

                    if (_kompas == null) throw new Exception("Нет связи с Kompas3D.");
                }
            }
            catch (COMException)
            {
                _kompas = null;
                StartAndConnectToKompas();
            }
        }
    }
}