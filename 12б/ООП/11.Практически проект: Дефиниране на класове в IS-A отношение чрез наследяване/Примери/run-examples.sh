#!/bin/bash

echo "=== КОМПИЛИРАНЕ И ИЗПЪЛНЕНИЕ НА ПРОЕКТА ==="
echo ""

echo "Компилиране на проекта..."
dotnet build

if [ $? -eq 0 ]; then
    echo ""
    echo "Компилирането беше успешно!"
    echo ""
    echo "Изпълнение на програмата..."
    echo ""
    dotnet run
else
    echo ""
    echo "ГРЕШКА при компилиране!"
    echo "Проверете кода за грешки."
fi

echo ""
echo "=== КРАЙ ==="
