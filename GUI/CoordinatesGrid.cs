using System.Collections.Generic;

namespace GUI
{
    public class CoordinatesGrid
    {
        /// <summary>
        ///     Объект с параметрами окна
        /// </summary>
        private readonly WindowParametrs _windowsParametrs;

        /// <summary>
        ///     Отступ
        /// </summary>
        private readonly int _offset;

        /// <summary>
        ///     Сетка координат
        /// </summary>
        private readonly Dictionary<int, Dictionary<string, double>> _grid =
            new Dictionary<int, Dictionary<string, double>>();

        /// <summary>
        ///     Инициализация координат для всех секций
        /// </summary>
        private void InitSectionsCoordinations()
        {
            if (_windowsParametrs.SectionNumber >= 1)
            {
                _grid.Add(1, InitFirstSection());
            }

            if (_windowsParametrs.SectionNumber >= 2)
            {
                _grid.Add(2, InitSecondSection());
            }

            if (_windowsParametrs.SectionNumber >= 3)
            {
                _grid.Add(3, InitThirdSection());
            }

            if (_windowsParametrs.SectionNumber >= 4)
            {
                _grid.Add(4, InitFourthSection());
            }
        }

        /// <summary>
        ///     Инициализация координат для первой секции
        /// </summary>
        /// <returns>возвращает координаты 1 секции</returns>
        private Dictionary<string, double> InitFirstSection()
        {
            return new Dictionary<string, double>
            {
                {"xStartExtrude", -_windowsParametrs.LengthWidth / 2.0},
                {"yStartExtrude", -_windowsParametrs.LengthHeight / 2.0},
                {"xStartNotch", -_windowsParametrs.LengthWidth / 2 + _offset},
                {"yStartNotch", -_windowsParametrs.LengthHeight / 2 + _offset}
            };
        }

        /// <summary>
        ///     Инициализация координат для второй секции
        /// </summary>
        /// <returns>возвращает координаты 2 секции</returns>
        private Dictionary<string, double> InitSecondSection()
        {
            return new Dictionary<string, double>
            {
                {"xStartExtrude", -_windowsParametrs.LengthWidth},
                {"yStartExtrude", -_windowsParametrs.LengthHeight / 2.0},
                {"xStartNotch", -_windowsParametrs.LengthWidth + _offset},
                {"yStartNotch", -_windowsParametrs.LengthHeight / 2 + 20}
            };
        }

        /// <summary>
        ///     Инициализация координат для третьей секции
        /// </summary>
        /// <returns>возвращает координаты 3 секции</returns>
        private Dictionary<string, double> InitThirdSection()
        {
            return new Dictionary<string, double>
            {
                {"xStartExtrude", -_windowsParametrs.LengthWidth 
                - _windowsParametrs.LengthWidth / 2},
                {"yStartExtrude", -_windowsParametrs.LengthHeight / 2.0},
                {"xStartNotch", -_windowsParametrs.LengthWidth 
                - _windowsParametrs.LengthWidth / 2 + _offset},
                {"yStartNotch", -_windowsParametrs.LengthHeight / 2 + 20}
            };
        }

        /// <summary>
        ///     Инициализация координат для четвертой секции
        /// </summary>
        /// <returns>возвращает координаты 4 секции</returns>
        private Dictionary<string, double> InitFourthSection()
        {
            return new Dictionary<string, double>
            {
                {"xStartExtrude", -_windowsParametrs.LengthWidth * 2.0},
                {"yStartExtrude", -_windowsParametrs.LengthHeight / 2.0},
                {"xStartNotch", -_windowsParametrs.LengthWidth * 2 + _offset},
                {"yStartNotch", -_windowsParametrs.LengthHeight / 2 + _offset}
            };
        }

        /// <summary>
        ///     Инициализация координат для ручки
        /// </summary>
        /// <returns>возвращает координаты ручки выбранной секции</returns>
        private Dictionary<string, double> InitHandle()
        {
            var handleCoordinations = new Dictionary<string, double>();
            var openSectionCorrdinations = InitOpenSection();

            handleCoordinations.Add("yStartPartOne", 
                _windowsParametrs.LengthHeight / 100 * 1);
            handleCoordinations.Add("WidthPartOne",
                _windowsParametrs.LengthWidth / 100 * 1);
            handleCoordinations.Add("HeigthPartOne",
                _windowsParametrs.LengthWidth / 100 * 1);
            handleCoordinations.Add("WeigthPartOne",
                openSectionCorrdinations["weigthSectionNotch"] / 100 * 10);

            handleCoordinations.Add("yStartPartTwo",
                _windowsParametrs.LengthHeight / 100.0 * 0.5);
            handleCoordinations.Add("WidthPartTwo", 
                _windowsParametrs.LengthWidth / 100 * 10);
            handleCoordinations.Add("HeigthPartTwo",
                _windowsParametrs.LengthWidth / 100 * 2);
            handleCoordinations.Add("WeigthPartTwo", 
                openSectionCorrdinations["weigthSectionNotch"]/100* 5);

            switch (_windowsParametrs.HandlePosition)
            {
                case HandlePosition.Right:
                    handleCoordinations.Add("xStartPartOne", 
                        openSectionCorrdinations["xStartExtrude"]
                            + _offset / 4.0);
                    handleCoordinations.Add("xStartPartTwo",
                        openSectionCorrdinations["xStartExtrude"]);
                    break;
                case HandlePosition.Left:
                    handleCoordinations.Add("xStartPartOne", 
                        openSectionCorrdinations["xStartExtrude"]/2-_offset*2);
                    handleCoordinations.Add("xStartPartTwo", 
                        openSectionCorrdinations["xStartExtrude"]/2-_offset*4);
                    break;
            }
            
            return handleCoordinations;
        }

        /// <summary>
        ///     Инициализация координат для открытой секции
        /// </summary>
        /// <returns>возвращает координаты выбранной открытой секции</returns>
        private Dictionary<string, double> InitOpenSection()
        {
            var openSectionCoordinations = new Dictionary<string, double>();
            switch (_windowsParametrs.OpenSection)
            {
                case 1:
                    openSectionCoordinations.Add("xStartExtrude",
                        -_windowsParametrs.LengthWidth / 2 + _offset);
                    openSectionCoordinations.Add("yStartExtrude",
                        -_windowsParametrs.LengthHeight / 2 + _offset);
                    openSectionCoordinations.Add("xStartNotch",
                        -_windowsParametrs.LengthWidth / 2 + _offset * 2);
                    openSectionCoordinations.Add("yStartNotch", 
                        -_windowsParametrs.LengthHeight / 2 + _offset * 2);
                    openSectionCoordinations.Add("widthSectionExtrude",
                        _windowsParametrs.LengthWidth / 2 - _offset * 2);
                    openSectionCoordinations.Add("heighSectionExtrude",
                        _windowsParametrs.LengthHeight - _offset * 2);
                    openSectionCoordinations.Add("widthSectionNotch",
                        _windowsParametrs.LengthWidth / 2 - _offset * 4);
                    openSectionCoordinations.Add("heightSectionNotch",
                        _windowsParametrs.LengthHeight - _offset * 4);
                    openSectionCoordinations.Add("weigthSectionNotch",
                        _windowsParametrs.LengthWeight);
                    break;
                case 2:
                    openSectionCoordinations.Add("xStartExtrude", 
                        -_windowsParametrs.LengthWidth + _offset);
                    openSectionCoordinations.Add("yStartExtrude",
                        -_windowsParametrs.LengthHeight / 2 + _offset);
                    openSectionCoordinations.Add("xStartNotch",
                        -_windowsParametrs.LengthWidth + _offset * 2);
                    openSectionCoordinations.Add("yStartNotch",
                        -_windowsParametrs.LengthHeight / 2 + _offset * 2);
                    openSectionCoordinations.Add("widthSectionExtrude",
                        _windowsParametrs.LengthWidth / 2 - _offset * 2);
                    openSectionCoordinations.Add("heighSectionExtrude",
                        _windowsParametrs.LengthHeight - _offset * 2);
                    openSectionCoordinations.Add("widthSectionNotch",
                        _windowsParametrs.LengthWidth / 2 - _offset * 4);
                    openSectionCoordinations.Add("heightSectionNotch",
                        _windowsParametrs.LengthHeight - _offset * 4);
                    openSectionCoordinations.Add("weigthSectionNotch",
                        _windowsParametrs.LengthWeight + _offset + 4);
                    break;
                case 3:
                    openSectionCoordinations.Add("xStartExtrude", 
                        -_windowsParametrs.LengthWidth
                            - _windowsParametrs.LengthWidth / 2 + _offset);
                    openSectionCoordinations.Add("yStartExtrude",
                        -_windowsParametrs.LengthHeight / 2 + _offset);
                    openSectionCoordinations.Add("xStartNotch",
                        -_windowsParametrs.LengthWidth 
                          - _windowsParametrs.LengthWidth / 2 + _offset * 2);
                    openSectionCoordinations.Add("yStartNotch",
                        -_windowsParametrs.LengthHeight / 2 + _offset * 2);
                    openSectionCoordinations.Add("widthSectionExtrude",
                        _windowsParametrs.LengthWidth / 2 - _offset * 2);
                    openSectionCoordinations.Add("heighSectionExtrude",
                        _windowsParametrs.LengthHeight - _offset * 2);
                    openSectionCoordinations.Add("widthSectionNotch",
                        _windowsParametrs.LengthWidth / 2 - _offset * 4);
                    openSectionCoordinations.Add("heightSectionNotch",
                        _windowsParametrs.LengthHeight - _offset * 4);
                    openSectionCoordinations.Add("weigthSectionNotch",
                        _windowsParametrs.LengthWeight + _offset + 4);
                    break;
                case 4:
                    openSectionCoordinations.Add("xStartExtrude",
                        -_windowsParametrs.LengthWidth * 2 + _offset);
                    openSectionCoordinations.Add("yStartExtrude",
                        -_windowsParametrs.LengthHeight / 2 + _offset);
                    openSectionCoordinations.Add("xStartNotch",
                        -_windowsParametrs.LengthWidth * 2 + _offset * 2);
                    openSectionCoordinations.Add("yStartNotch",
                        -_windowsParametrs.LengthHeight / 2 + _offset * 2);
                    openSectionCoordinations.Add("widthSectionExtrude",
                        _windowsParametrs.LengthWidth / 2 - _offset * 2);
                    openSectionCoordinations.Add("heighSectionExtrude",
                        _windowsParametrs.LengthHeight - _offset * 2);
                    openSectionCoordinations.Add("widthSectionNotch",
                        _windowsParametrs.LengthWidth / 2 - _offset * 4);
                    openSectionCoordinations.Add("heightSectionNotch",
                        _windowsParametrs.LengthHeight - _offset * 4);
                    openSectionCoordinations.Add("weigthSectionNotch",
                        _windowsParametrs.LengthWeight + _offset + 4);
                    break;
            }

            return openSectionCoordinations;
        }

        /// <summary>
        ///     Конструктор класса для сетки координат
        /// </summary>
        /// <param name="windowsParametrs"></param>
        public CoordinatesGrid(WindowParametrs windowsParametrs)
        {
            _windowsParametrs = windowsParametrs;
            _offset = 20;

            InitSectionsCoordinations();

            _grid.Add(5, InitOpenSection());

            if (_windowsParametrs.OpenSection != 0)
            {
                _grid.Add(6, InitHandle());
            }

        }

        /// <summary>
        ///     Геттер для сетки координат
        /// </summary>
        public Dictionary<int, Dictionary<string, double>> Grid => _grid;
    }
}
