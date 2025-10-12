@echo off
chcp 65001 >nul
setlocal enabledelayedexpansion

echo ═══════════════════════════════════════════════════════════
echo     Примери: Работа с обекти, Итератори и Компаратори
echo ═══════════════════════════════════════════════════════════
echo.

set examples[0]=Primer01-BasicIterator:Основни итератори с числа
set examples[1]=Primer02-IComparable:Ученици и оценки (IComparable)
set examples[2]=Primer03-IComparer:Сортиране на ученици (IComparer)
set examples[3]=Primer04-ShoppingList:Списък за пазаруване
set examples[4]=Primer05-Contacts:Телефонен указател

for /L %%i in (0,1,4) do (
    for /f "tokens=1,2 delims=:" %%a in ("!examples[%%i]!") do (
        echo.
        echo ╔════════════════════════════════════════════════╗
        echo ║ %%b
        echo ╚════════════════════════════════════════════════╝
        echo.
        
        if exist "%%a" (
            cd %%a
            dotnet run --verbosity quiet
            if !errorlevel! equ 0 (
                echo.
                echo ✓ Примерът приключи успешно
            ) else (
                echo.
                echo ✗ Грешка при изпълнение
            )
            cd ..
        ) else (
            echo ✗ Директорията '%%a' не съществува
        )
        
        echo.
        echo ────────────────────────────────────────────────
        echo.
        echo Натиснете произволен клавиш за следващия пример...
        pause >nul
    )
)

echo.
echo ═══════════════════════════════════════════════════════════
echo Всички примери са изпълнени!
echo ═══════════════════════════════════════════════════════════
pause
