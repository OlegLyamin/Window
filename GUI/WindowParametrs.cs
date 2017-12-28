using System;

namespace GUI
{
    class WindowParametrs
    {
        /// <summary>
        /// объект высоты
        /// </summary>
        private int _lengthHeight;

        /// <summary>
        /// объект ширины
        /// </summary>
        private int _lengthWidth;

        /// <summary>
        /// объект толщины
        /// </summary>
        private int _lengthWeight;

        /// <summary>
        /// геттер сеттер высоты
        /// </summary>
        public int LengthHeight
        {
            get { return _lengthHeight; }
            set
            {
                if (value < 580 || value > 2755)
                {
                    throw new ArgumentOutOfRangeException("Не верно" +
                                                          " указана высота");
                }
                _lengthHeight = value;
            }
        }

        /// <summary>
        /// геттер сеттер ширины
        /// </summary>
        public int LengthWidth
        {
            get { return _lengthWidth; }
            set
            {
                if (value < 10 || value > 2670)
                {
                    throw new ArgumentOutOfRangeException("Не верно" +
                                                          " указана ширина");
                }
                _lengthWidth = value;
            }
        }

        /// <summary>
        /// геттер сеттер толщины
        /// </summary>
        public int LengthWeight
        {
            get { return _lengthWeight; }
            set
            {
                if (value < 24 || value > 48)
                {
                    throw new ArgumentOutOfRangeException("Не верно" +
                                                          " указана толщина");
                }
                _lengthWeight = value;
            }
        }

        /// <summary>
        /// Геттер сеттер номера секции
        /// </summary>
        public int SectionNumber { get; set; }

        /// <summary>
        /// Геттер сеттер положения ручки
        /// </summary>
        public HandlePosition HandlePosition { get; set; }

        /// <summary>
        /// Геттер сеттер открываемой секции
        /// </summary>
        public int OpenSection { get; set; }

        /// <summary>
        /// создание экземпляров 
        /// </summary>
        /// <param name="lengthHeight">высота</param>
        /// <param name="lengthWidth">ширина</param>
        /// <param name="lengthWeight">толщина</param>
        /// <param name="sectionNumber">номер секции</param>
        /// <param name="opensection"></param>
        public WindowParametrs(int lengthHeight, int lengthWidth,
            int lengthWeight, int sectionNumber, int opensection)
        {
            LengthHeight = lengthHeight;
            LengthWidth = lengthWidth;
            LengthWeight = lengthWeight;
            SectionNumber = sectionNumber;
            OpenSection = opensection;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public WindowParametrs()
        {
        }
    }
}
