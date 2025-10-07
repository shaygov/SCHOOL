#!/bin/bash

# Скрипт за изпълнение на всички C# примери
# Автор: AI Assistant
# Дата: $(date)

echo "=== C# Примери - Изпълнение на всички проекти ==="
echo ""

# Функция за изпълнение на проект
run_example() {
    local project_name=$1
    local description=$2
    
    echo "----------------------------------------"
    echo "Изпълнявам: $project_name"
    echo "Описание: $description"
    echo "----------------------------------------"
    
    if [ -d "$project_name" ]; then
        cd "$project_name"
        echo "Компилиране и изпълнение..."
        echo ""
        
        # Изпълняваме проекта с автоматичен вход за ReadLine()
        echo "" | dotnet run
        
        echo ""
        echo "✅ $project_name завършен успешно!"
        cd ..
    else
        echo "❌ Папката $project_name не съществува!"
    fi
    
    echo ""
    echo "Натиснете Enter за продължение към следващия пример..."
    read -r
    echo ""
}

# Изпълняваме всички примери
run_example "Example01_HelloWorld" "Първа конзолна програма - основни команди за извеждане"
run_example "Example02_ConsoleCommands" "Конзолни команди - WriteLine, Write, Clear"
run_example "Example03_UserInput" "Потребителски вход - четене и парсиране на данни"
run_example "Example04_DataTypes" "Типове данни - string, int, double, bool, char"
run_example "Example05_Calculator" "Прост калкулатор - математически операции"
run_example "Example06_Conditions" "Условни оператори - if/else логика"
run_example "Example07_Loops" "Цикли - for, while, do-while"
run_example "Example08_GuessGame" "Игра 'Познай числото' - комбиниране на концепции"
run_example "Example09_AreaCalculator" "Калкулатор за площ - практическо приложение"
run_example "Example10_Template" "Празен шаблон за нови примери"

echo "🎉 Всички примери са изпълнени успешно!"
echo ""
echo "Благодарим за използването на C# примерите!"
echo "За повече информация вижте README_PROJECTS.md"
