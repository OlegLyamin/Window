using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using KAPITypes;
using KompasAPI7;

namespace GUI
{
    class Kompas
    {
        public KompasObject _Kompas = null;

        public void RunKompas()
        {
            if (_Kompas == null)
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _Kompas = (KompasObject)Activator.CreateInstance(t);
            }

            if (_Kompas != null)
            {
                _Kompas.Visible = true;
                _Kompas.ActivateControllerAPI();
            }

            if (_Kompas == null) throw new Exception("Нет связи с Kompas3D.");
        }

        public void BuildStool(Window stool)
        {
            if (_Kompas == null) throw new Exception("Не возможно построить деталь. Нет связи с Kompas3D.");
            var height = 800; // высота
            var width = 500; // ширина
            var weigth = 20; //толщина

            var xStartStep1 = -width / 2;
            var yStartStep1 = -height / 2;


            ksDocument3D doc = _Kompas.Document3D();
            doc.Create();
            ksPart stoolPart = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part); //указатель на деталь
            ksEntity planeXoy = stoolPart.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY); //определим плоскость XY
            ksEntity sketch = stoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза
            ksSketchDefinition sd = sketch.GetDefinition(); //получим указатель на параметры эскиза
            sd.SetPlane(planeXoy); //зададим плоскость на которой создаем эскиз
            sketch.Create(); // создаем эскиз
            ksDocument2D doc2d = sd.BeginEdit(); //входим в режим редактирование эскиза
            DrawRectangle(stool, doc2d, xStartStep1, yStartStep1, height, width, null);
           // doc2d.ksCircle(stool.TopPointsArray[0], stool.TopPointsArray[1], stool.LengthTop, 1);
            sd.EndEdit(); //закончили редактировать эскиз
            Extrude(stool, stoolPart, sketch, weigth, (short)Direction_Type.dtNormal);

            //Notch(stool, stoolPart, sketch, stool.LengthTop1, (short)Direction_Type.dtNormal);

            ///Step 2
            ///
            var offset = 20;
            var xStartStep2 = -width/2 + offset;
            var yStartStep2 = -height/2 + offset;
            var widthStep2 = width/2 ;
            var heighStep2 = height  -offset * 2;
            var weigthStep2 = weigth * 2;

            ksEntity sketchStep2 = stoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза
            ksSketchDefinition step2 = sketchStep2.GetDefinition(); //получим указатель на параметры эскиза
            step2.SetPlane(planeXoy); //зададим плоскость на которой создаем эскиз
            sketchStep2.Create(); // создаем эскиз
            ksDocument2D doc2d2 = step2.BeginEdit(); //входим в режим редактирование эскиза
            DrawRectangle(stool, doc2d2, xStartStep2, yStartStep2, heighStep2, widthStep2, null);
            step2.EndEdit(); //закончили редактировать эскиз
            Extrude(stool, stoolPart, sketchStep2, weigthStep2, (short)Direction_Type.dtNormal);

            /// Step 3  
            /// 

            var offset3 = offset + 20;
            var xStartStep3 = -width / 2 + offset3;
            var yStartStep3 = -height / 2 + offset3;
            var widthStep3 = width / 2 - offset3 - offset;
            var heighStep3 = height - offset3 * 2;
            var weigthStep3 = 100;

            
            ksEntity step3PlaneOffset = stoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeOffset); //создаем переменную смещенной поверхности 
            ksPlaneOffsetDefinition pod3 = step3PlaneOffset.GetDefinition(); // получаем указатель на её настройки 
            pod3.SetPlane(planeXoy); // ХУ плоскость установим как исходную, чтобы отталкиватся от неё 
            pod3.offset = weigthStep2; //смещаемся на десять 
            step3PlaneOffset.Create(); // создаем саму плоскость 
            ksEntity sketch3 = stoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза 
            ksSketchDefinition sd3 = sketch3.GetDefinition(); //получим указатель на параметры эскиза 
            sd3.SetPlane(step3PlaneOffset); //зададим плоскость на которой создаем эскиз 
            sketch3.Create(); // создаем эскизa 
            ksDocument2D doc2d3 = sd3.BeginEdit();

            DrawRectangle(stool, doc2d3, xStartStep3, yStartStep3, heighStep3, widthStep3, null);

            DrawRectangle(stool, doc2d3, -xStartStep3+offset, -yStartStep3+ (offset), heighStep3+(offset*2), widthStep3, 180);
            sd3.EndEdit();
            Notch(stool, stoolPart, sketch3, weigthStep3, 11);

            /// Step 4
            ///     
            var weightStep4 = 10;
            var osn = 10;
            ksEntity sketch4 = stoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза 
            ksSketchDefinition sd4 = sketch4.GetDefinition(); //получим указатель на параметры эскиза 
            sd4.SetPlane(step3PlaneOffset); //зададим плоскость на которой создаем эскиз 
            sketch4.Create(); // создаем эскизa 
            ksDocument2D doc2d4 = sd4.BeginEdit();
            DrawRectangle(stool, doc2d4, osn/2, osn / 2, osn, osn, 180);
            sd3.EndEdit();
            Extrude(stool, stoolPart, sketch4, weightStep4, (short)Direction_Type.dtNormal);

            /// Step 5
            /// 
            var osn5 = osn * 2;
            ksEntity step5PlaneOffset = stoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeOffset); //создаем переменную смещенной поверхности 
            ksPlaneOffsetDefinition pod5 = step5PlaneOffset.GetDefinition(); // получаем указатель на её настройки 
            pod5.SetPlane(step3PlaneOffset); // ХУ плоскость установим как исходную, чтобы отталкиватся от неё 
            pod5.offset = weightStep4; //смещаемся на десять 
            step5PlaneOffset.Create(); // создаем саму плоскость 
            ksEntity sketch5 = stoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза 
            ksSketchDefinition sd5 = sketch5.GetDefinition(); //получим указатель на параметры эскиза 
            sd5.SetPlane(step5PlaneOffset); //зададим плоскость на которой создаем эскиз 
            sketch5.Create(); // создаем эскизa 
            ksDocument2D doc2d5 = sd5.BeginEdit();

            DrawRectangle(stool, doc2d5, osn5 / 2, osn5 / 2, osn5, osn5+100, 180);
            sd5.EndEdit();
            Extrude(stool, stoolPart, sketch5, weightStep4, (short)Direction_Type.dtNormal);

        }

        private void DrawRectangle(Window stool, ksDocument2D doc2d, double xStart, double yStart, double heigth, double width, double? ang)
        {
            ksRectangleParam param = (ksRectangleParam)_Kompas.GetParamStruct((short)StructType2DEnum.ko_RectangleParam);
            param.x = xStart;
            param.y = yStart;
            param.width = width;
            param.height = heigth;

            if (ang != null)
                param.ang = (double)ang;

            param.style = 1;
            doc2d.ksRectangle(param, 0);
        }





        private static void Extrude(Window stool, ksPart part, ksEntity sketch, int length, short type)
        {
            ksEntity extrude = part.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition extrDef = extrude.GetDefinition();
            extrDef.directionType = type;
            extrDef.SetSketch(sketch);
            ksExtrusionParam extrudeParam = extrDef.ExtrusionParam();
            extrudeParam.depthNormal = length;

            extrude.Create();
        }
        private static void Notch(Window stool, ksPart part, ksEntity sketch, int heigth, short type)
        {
            ksEntity cutExtrude = part.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutextrDef = cutExtrude.GetDefinition();
            cutextrDef.directionType = (short)Kompas6Constants3D.Direction_Type.dtNormal;
            cutextrDef.SetSketch(sketch);
            ksExtrusionParam cutExtrParam = cutextrDef.ExtrusionParam();
            cutExtrParam.depthNormal = heigth;
            cutExtrude.Create();
        }

    }
}
