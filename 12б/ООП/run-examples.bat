@echo off
chcp 65001 >nul
title C# ООП Примери за 12 клас

:menu
cls
echo ================================
echo    C# ООП Примери за 12 клас
echo ================================
echo.
echo Изберете урок за изпълнение:
echo.
echo 1. Дефиниране на класове
echo 2. Статични членове
echo 3. Наследяване на класове
echo 4. Абстракция и абстрактни класове
echo 5. Интерфейси
echo 6. Изпълни всички примери
echo 0. Изход
echo.
set /p choice="Въведете номер (0-6): "

if "%choice%"=="1" goto lesson1
if "%choice%"=="2" goto lesson2
if "%choice%"=="3" goto lesson3
if "%choice%"=="4" goto lesson4
if "%choice%"=="5" goto lesson5
if "%choice%"=="6" goto all
if "%choice%"=="0" goto exit
echo Невалиден избор!
pause
goto menu

:lesson1
echo.
echo === Урок 1: Дефиниране на класове ===
cd "01-Дефиниране-на-класове\Примери"
echo Компилиране...
csc *.cs
if errorlevel 1 (
    echo Грешка при компилиране!
    pause
    cd ..\..
    goto menu
)
echo Изпълнение:
Program.exe
echo.
cd ..\..
pause
goto menu

:lesson2
echo.
echo === Урок 2: Статични членове ===
cd "02-Статични-членове\Примери"
echo Компилиране...
csc *.cs
if errorlevel 1 (
    echo Грешка при компилиране!
    pause
    cd ..\..
    goto menu
)
echo Изпълнение:
Program.exe
echo.
cd ..\..
pause
goto menu

:lesson3
echo.
echo === Урок 3: Наследяване на класове ===
cd "03-Наследяване-на-класове\Примери"
echo Компилиране...
csc *.cs
if errorlevel 1 (
    echo Грешка при компилиране!
    pause
    cd ..\..
    goto menu
)
echo Изпълнение:
Program.exe
echo.
cd ..\..
pause
goto menu

:lesson4
echo.
echo === Урок 4: Абстракция и абстрактни класове ===
cd "04-Абстракция-и-абстрактни-класове\Примери"
echo Компилиране...
csc *.cs
if errorlevel 1 (
    echo Грешка при компилиране!
    pause
    cd ..\..
    goto menu
)
echo Изпълнение:
Program.exe
echo.
cd ..\..
pause
goto menu

:lesson5
echo.
echo === Урок 5: Интерфейси ===
cd "05-Интерфейси\Примери"
echo Компилиране...
csc *.cs
if errorlevel 1 (
    echo Грешка при компилиране!
    pause
    cd ..\..
    goto menu
)
echo Изпълнение:
Program.exe
echo.
cd ..\..
pause
goto menu

:all
echo.
echo === Изпълнение на всички примери ===
for /d %%d in (*) do (
    if exist "%%d\Примери\Program.cs" (
        echo === %%d ===
        cd "%%d\Примери"
        echo Компилиране...
        csc *.cs >nul 2>&1
        if exist "Program.exe" (
            echo Изпълнение:
            Program.exe
            echo.
            del Program.exe >nul 2>&1
        ) else (
            echo Грешка при компилиране!
        )
        cd ..\..
        echo ----------------------------------------
    )
)
pause
goto menu

:exit
echo.
echo Довиждане!
pause
exit
