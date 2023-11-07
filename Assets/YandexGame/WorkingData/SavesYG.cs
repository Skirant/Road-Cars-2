
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }

        public int Coins = 0;
        public int _price = 30;
        public int Weight = 10;
        public int HealthBarrir = 50;
        public int LevelNumber = 1;
        public float CarMassPrice = 100;
        public float CarMassMultiplier = 1f;
        public float ResellPricePrice = 100;
        public float ResellPriceMultiplier = 1.1f;
    }
}
