using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Window

    {
        private int _formLeg;

    private int _heigthLeg;
    private int _lengthLeg;
    private int _lengthTop;
        private int _lengthTop1;
        private int _heigthTop;
        


        private int _topOffset = 2;
    private int[] _topPointsArray = new int[2];

    private int[] _xLegArray = new int[4];
    private int[] _yLegArray = new int[4];

    public int[] TopPointsArray
    {
        get { return _topPointsArray; }
    }

    public int[] XLegArray
    {
        get { return _xLegArray; }
    }

    public int[] YLegArray
    {
        get { return _yLegArray; }
    }

    public int HeigthLeg
    {
        get { return _heigthLeg; }
        set
        {
            if (value < 200 || value > 600)
            {
                throw new ArgumentOutOfRangeException("Высота ножки табуретки должна быть от 200 до 600!");
            }
            _heigthLeg = value;
        }
    }

    public int LengthLeg
    {
        get { return _lengthLeg; }
        set
        {
            //if (value < 10 || value > 35)
            //{
            //    throw new ArgumentOutOfRangeException("Длина стороны или радиус основания ножки должен быть от 10 до 35!");
            //}
            _lengthLeg = value;
        }
    }

    public int HeigthTop
    {
        get { return _heigthTop; }
        set
        {
            if (value < 10 || value > 50)
            {
                throw new ArgumentOutOfRangeException("Высота крышки должена быть от 10 до 20!");
            }
            _heigthTop = value;
        }
    }


    public int LengthTop
    {
        get { return _lengthTop; }
        set
        {
            if (value < 250 || value > 400)
            {
                throw new ArgumentOutOfRangeException("Длина стороны или радиус крышки должен быть от 250 до 400!");
            }
            _lengthTop = value;
        }

    }
        public int LengthTop1
        {
            get { return LengthTop/2; }
            set
            {
                if (value < 250 || value > 400)
                {
                    throw new ArgumentOutOfRangeException("Длина стороны или радиус крышки должен быть от 250 до 400!");
                }
                _lengthTop1 = value;
            }

        }

        public int FormLegs
    {
        get { return _formLeg; }
        set
        {
            if (value != 0 && value != 1)
            {
                throw new ArgumentOutOfRangeException("Не верная форма основания табуретки!");
            }
            _formLeg = value;
        }
    }
    public Window()
    {
    }

    public void InitPoints()
    {
        //InitSquareTopPoints();
        //InitSquareLegsPoints();
        InitCircleTopPoints();
        InitCircleLegsPoints();
    }

    private void InitSquareTopPoints()
    {
        _topPointsArray[0] = -(_lengthTop / 2);
        _topPointsArray[1] = -(_lengthTop / 2);
    }

    private void InitSquareLegsPoints()
    {
        _topOffset = _lengthTop / 100 * 10;

        _xLegArray[0] = -(_lengthTop / 2) + _topOffset;
        _yLegArray[0] = -(_lengthTop / 2) + _topOffset;

        _xLegArray[1] = -(_lengthTop / 2) + _topOffset;
        _yLegArray[1] = (_lengthTop / 2) - _topOffset;

        _xLegArray[2] = (_lengthTop / 2) - _topOffset;
        _yLegArray[2] = (_lengthTop / 2) - _topOffset;

        _xLegArray[3] = (_lengthTop / 2) - _topOffset;
        _yLegArray[3] = -(_lengthTop / 2) + _topOffset;
    }

    private void InitCircleTopPoints()
    {
        _topPointsArray[0] = 0;
        _topPointsArray[1] = 0;
    }

    private void InitCircleLegsPoints()
    {
        _topOffset = _lengthTop / 100 * 20;
        _xLegArray[0] = -(_lengthTop / 2) + _topOffset;
        _yLegArray[0] = -(_lengthTop / 2) + _topOffset;

        _xLegArray[1] = -(_lengthTop / 2) + _topOffset;
        _yLegArray[1] = (_lengthTop / 2) - _topOffset;

        _xLegArray[2] = (_lengthTop / 2) - _topOffset;
        _yLegArray[2] = (_lengthTop / 2) - _topOffset;

        _xLegArray[3] = (_lengthTop / 2) - _topOffset;
        _yLegArray[3] = -(_lengthTop / 2) + _topOffset;
    }
}
}
