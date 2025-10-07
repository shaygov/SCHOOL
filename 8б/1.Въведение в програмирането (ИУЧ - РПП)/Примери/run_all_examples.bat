@echo off
REM Скрипт за изпълнение на всички C# примери (Windows)
REM Автор: AI Assistant

echo === C# Примери - Изпълнение на всички проекти ===
echo.

REM Функция за изпълнение на проект
:run_example
set project_name=%1
set description=%2

echo ----------------------------------------
echo Изпълнявам: %project_name%
echo Описание: %description%
echo ----------------------------------------

if exist "%project_name%" (
    cd "%project_name%"
    echo Компилиране и изпълнение...
    echo.
    
    REM Изпълняваме проекта
    echo. | dotnet run
    
    echo.
    echo ✅ %project_name% завършен успешно!
    cd ..
) else (
    echo ❌ Папката %project_name% не съществува!
)

echo.
echo Натиснете Enter за продължение към следващия пример...
pause >nul
echo.
goto :eof

REM Изпълняваме всички примери
call :run_example "Example01_HelloWorld" "Първа конзолна програма - основни команди за извеждане"
call :run_example "Example02_ConsoleCommands" "Конзолни команди - WriteLine, Write, Clear"
call :run_example "Example03_UserInput" "Потребителски вход - четене и парсиране на данни"
call :run_example "Example04_DataTypes" "Типове данни - string, int, double, bool, char"
call :run_example "Example05_Calculator" "Прост калкулатор - математически операции"
call :run_example "Example06_Conditions" "Условни оператори - if/else логика"
call :run_example "Example07_Loops" "Цикли - for, while, do-while"
call :run_example "Example08_GuessGame" "Игра 'Познай числото' - комбиниране на концепции"
call :run_example "Example09_AreaCalculator" "Калкулатор за площ - практическо приложение"
call :run_example "Example10_Template" "Празен шаблон за нови примери"

echo 🎉 Всички примери са изпълнени успешно!
echo.
echo Благодарим за използването на C# примерите!
echo За повече информация вижте README_PROJECTS.md
pause
