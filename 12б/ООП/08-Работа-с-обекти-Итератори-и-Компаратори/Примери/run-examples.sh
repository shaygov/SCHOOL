#!/bin/bash

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

echo -e "${BLUE}═══════════════════════════════════════════════════════════${NC}"
echo -e "${BLUE}    Примери: Работа с обекти, Итератори и Компаратори     ${NC}"
echo -e "${BLUE}═══════════════════════════════════════════════════════════${NC}"
echo

examples=(
    "Primer01-BasicIterator:Основни итератори с числа"
    "Primer02-IComparable:Ученици и оценки (IComparable)"
    "Primer03-IComparer:Сортиране на ученици (IComparer)"
    "Primer04-ShoppingList:Списък за пазаруване"
    "Primer05-Contacts:Телефонен указател"
)

run_example() {
    local dir=$1
    local description=$2
    
    echo -e "\n${YELLOW}╔════════════════════════════════════════════════╗${NC}"
    echo -e "${YELLOW}║${NC} ${GREEN}$description${NC}"
    echo -e "${YELLOW}╚════════════════════════════════════════════════╝${NC}\n"
    
    if [ -d "$dir" ]; then
        cd "$dir"
        dotnet run --verbosity quiet
        local exit_code=$?
        cd ..
        
        if [ $exit_code -eq 0 ]; then
            echo -e "\n${GREEN}✓ Примерът приключи успешно${NC}"
        else
            echo -e "\n${RED}✗ Грешка при изпълнение${NC}"
        fi
    else
        echo -e "${RED}✗ Директорията '$dir' не съществува${NC}"
    fi
    
    echo -e "\n${BLUE}────────────────────────────────────────────────${NC}"
}

if [ "$1" == "" ]; then
    for example in "${examples[@]}"; do
        IFS=':' read -r dir description <<< "$example"
        run_example "$dir" "$description"
        
        echo -e "\n${YELLOW}Натиснете Enter за следващия пример...${NC}"
        read
    done
else
    example_num=$1
    if [ $example_num -ge 1 ] && [ $example_num -le ${#examples[@]} ]; then
        example=${examples[$((example_num-1))]}
        IFS=':' read -r dir description <<< "$example"
        run_example "$dir" "$description"
    else
        echo -e "${RED}Невалиден номер на пример. Използвайте число от 1 до ${#examples[@]}.${NC}"
    fi
fi

echo -e "\n${BLUE}═══════════════════════════════════════════════════════════${NC}"
echo -e "${GREEN}Всички примери са изпълнени!${NC}"
echo -e "${BLUE}═══════════════════════════════════════════════════════════${NC}"
