# 🐧 Документация за Linux/Ubuntu - ООП с C#

## 🚀 Бърз старт (5 минути)

### Стъпка 1: Инсталирайте .NET SDK
```bash
# Добавете Microsoft пакетния репозиторий
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Инсталирайте .NET 8.0 SDK
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

### Стъпка 2: Инсталирайте VS Code (препоръчвам)
```bash
# Чрез snap (най-лесно)
sudo snap install --classic code

# Или чрез apt
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > packages.microsoft.gpg
sudo install -o root -g root -m 644 packages.microsoft.gpg /etc/apt/trusted.gpg.d/
sudo sh -c 'echo "deb [arch=amd64,arm64,armhf signed-by=/etc/apt/trusted.gpg.d/packages.microsoft.gpg] https://packages.microsoft.com/repos/code stable main" > /etc/apt/sources.list.d/vscode.list'
sudo apt update
sudo apt install code
```

### Стъпка 3: Тествайте инсталацията
```bash
# Отворете терминал (Ctrl + Alt + T)
dotnet --version
# Трябва да видите нещо като: 8.0.xxx
```

## 📚 Как да работите с примерите

### Вариант 1: С VS Code (Най-лесно)
1. Отворете VS Code
2. Отворете папката с урока (File → Open Folder)
3. Изберете папката `01-Дефиниране-на-класове/Примери`
4. Натиснете `Ctrl + F5` за изпълнение
5. Готово! 🎉

### Вариант 2: С терминал
```bash
# Отидете в папката с примерите
cd /път/към/01-Дефиниране-на-класове/Примери

# Изпълнете
dotnet run
```

### Вариант 3: С bash скрипт (Най-бързо)
```bash
# Направете скрипта изпълним
chmod +x run-examples-universal.sh

# Стартирайте
./run-examples-universal.sh
```

### Вариант 4: С csc компилатор
```bash
# Отидете в папката с примерите
cd /път/към/01-Дефиниране-на-класове/Примери

# Компилирайте
csc *.cs

# Изпълнете
./Program
```


## 🎯 Структура на материалите

```
📁 01-Дефиниране-на-класове/
├── 📄 HTML документация
└── 📁 Примери/
    ├── Student.cs      (класът)
    ├── Program.cs      (главна програма)
    └── Example.csproj  (проектен файл за dotnet)
```

## 💡 Съвети за успех

### ✅ Добре:
- Четете HTML документацията първо
- Разгледайте кода преди да го изпълните
- Променяйте стойностите и вижте какво се случва
- Използвайте терминала за бързи команди

### ❌ Избягвайте:
- Да копирате кода без да го разбирате
- Да прескачате HTML документацията
- Да се притеснявате от грешките - те са част от ученето!

## 🔧 Често срещани проблеми

### Проблем: "dotnet: command not found"
**Решение:** 
```bash
# Проверете дали .NET е инсталиран
which dotnet

# Ако не е, инсталирайте отново
sudo apt-get install -y dotnet-sdk-8.0
```

### Проблем: "Permission denied" при изпълнение на скрипт
**Решение:**
```bash
chmod +x run-examples-universal.sh
```

### Проблем: VS Code не разпознава .cs файлове
**Решение:** Инсталирайте C# разширението в VS Code

### Проблем: "csc: command not found"
**Решение:** Използвайте `dotnet run` вместо `csc`

### Проблем: "No such file or directory"
**Решение:** Проверете пътищата - Linux използва `/` вместо `\`


## 🎮 Упражнения

След като разгледате всеки урок, опитайте:

1. **Променете стойностите** в Program.cs
2. **Добавете нови методи** в класовете
3. **Създайте нови обекти** с различни параметри
4. **Експериментирайте** с функционалността

## 🛠️ Полезни команди за Linux

### Основни команди:
```bash
# Проверка на версията
dotnet --version

# Компилиране и изпълнение
dotnet run

# Само компилиране
dotnet build

# Създаване на нов проект
dotnet new console -n MyProject

# Компилиране с csc
csc *.cs

# Изпълнение на компилираната програма
./Program

# Показване на файлове
ls -la *.cs

# Промяна на директория
cd /път/към/папка

# Показване на текуща директория
pwd
```

### Полезни bash команди:
```bash
# Търсене на файлове
find . -name "*.cs"

# Показване на съдържанието на файл
cat Program.cs

# Редактиране на файл в терминала
nano Program.cs
# или
vim Program.cs

# Копиране на файлове
cp Program.cs Program_backup.cs

# Преместване/преименуване
mv old_name.cs new_name.cs
```

### VS Code команди (Ctrl+Shift+P):
- **C#: Run** - изпълнява програмата
- **C#: Debug** - стартира debug режим
- **Terminal: New Terminal** - отваря терминал
- **File: Open Folder** - отваря папка

## 🎯 Инсталация на алтернативни IDE

### JetBrains Rider (Платена)
```bash
# Изтеглете от: https://www.jetbrains.com/rider/
# Или чрез snap
sudo snap install rider --classic
```

### Visual Studio Code (препоръчвам)
```bash
# Чрез snap
sudo snap install --classic code

# Или чрез apt (вижте по-горе)
```

### MonoDevelop (Безплатен)
```bash
sudo apt-get install monodevelop
```

## 📊 Сравнение на инструментите

| Инструмент | Безплатен | Лесно за начинаещи | Професионален | Linux поддръжка |
|------------|-----------|-------------------|---------------|-----------------|
| VS Code | ✅ | ✅ | ✅ | ✅ |
| JetBrains Rider | ❌ | ✅ | ✅ | ✅ |
| MonoDevelop | ✅ | ✅ | ❌ | ✅ |
| Терминал + dotnet | ✅ | ❌ | ✅ | ✅ |

## 🔄 Кръстосана платформена работа

### Съвместимост с Windows:
✅ **Работи отлично:**
- C# кодът е идентичен
- .NET командите са същите
- VS Code работи еднакво
- Git репозиториите са съвместими

⚠️ **Внимание:**
- Пътищата в скриптовете (Linux: `/`, Windows: `\`)
- Изпълними файлове (Linux: без разширение, Windows: `.exe`)

## 🏆 Цел

Целта е да научите:
- Какво е клас и обект
- Как се създават и използват
- Как работи наследяването
- Как се използват интерфейсите
- Как работи полиморфизмът

**Запомнете:** Програмирането е като ученето на музика - нужна е практика! 🎵

---
*Успех в ученето на Linux! 🐧*
