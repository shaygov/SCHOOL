@echo off
echo === КОМПИЛИРАНЕ И ИЗПЪЛНЕНИЕ НА ПРОЕКТА ===
echo.

echo Компилиране на проекта...
dotnet build

if %errorlevel% equ 0 (
    echo.
    echo Компилирането беше успешно!
    echo.
    echo Изпълнение на програмата...
    echo.
    dotnet run
) else (
    echo.
    echo ГРЕШКА при компилиране!
    echo Проверете кода за грешки.
)

echo.
echo === КРАЙ ===
pause
