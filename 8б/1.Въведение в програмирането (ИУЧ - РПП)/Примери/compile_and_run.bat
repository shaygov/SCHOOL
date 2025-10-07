@echo off
REM Скрипт за компилиране и изпълнение на C# примери за Windows
REM Използване: compile_and_run.bat [име_на_файла.cs]

echo === C# Компилатор и Изпълнител за Windows ===

REM Проверка дали е подаден аргумент
if "%1"=="" (
    echo Използване: %0 [име_на_файла.cs]
    echo.
    echo Налични примери:
    dir *.cs /b | findstr /v Template
    goto :end
)

REM Име на файла
set FILENAME=%1

REM Проверка дали файлът съществува
if not exist "%FILENAME%" (
    echo Грешка: Файлът '%FILENAME%' не съществува!
    goto :end
)

REM Име на изпълнимия файл (без разширението)
for %%F in ("%FILENAME%") do set EXECUTABLE=%%~nF

echo Компилиране на %FILENAME%...

REM Компилиране
csc /out:"%EXECUTABLE%.exe" "%FILENAME%"

REM Проверка дали компилирането е успешно
if %ERRORLEVEL% EQU 0 (
    echo ✅ Компилирането е успешно!
    echo Изпълнение на програмата...
    echo ================================
    
    REM Изпълнение
    "%EXECUTABLE%.exe"
    
    echo ================================
    echo ✅ Програмата приключи!
    
    REM Изтриване на изпълнимия файл
    del "%EXECUTABLE%.exe" >nul 2>&1
    echo 🧹 Изпълнимият файл е изтрит.
) else (
    echo ❌ Грешка при компилиране!
    echo Проверете дали имате инсталиран .NET Framework SDK
)

:end
pause
