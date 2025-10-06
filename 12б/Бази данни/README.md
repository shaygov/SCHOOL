# XAMPP: MySQL и phpMyAdmin (за ученици)

## Инсталация
- Инсталирайте XAMPP.
- Стартирайте XAMPP Control Panel и включете: Apache, MySQL.

## phpMyAdmin
- Отворете: http://localhost/phpmyadmin
- Вход: потребител `root`, парола (обикновено празна по подразбиране).

## База и потребител
- Създайте база: `school_db` (utf8mb4_general_ci).
- По желание: потребител `school_user` с парола и права за `school_db`.

## Бързи действия
- Създаване на таблица: изберете база → "Create table".
- Импорт на .sql: Import → изберете файл → Go.
- SQL конзола: SQL → напишете заявката → Go.

## Примерни заявки
```sql
CREATE TABLE Students (
  StudentId INT AUTO_INCREMENT PRIMARY KEY,
  FullName  VARCHAR(100) NOT NULL
);

INSERT INTO Students (FullName) VALUES ('Ivan Petrov'), ('Maria Ivanova');

SELECT * FROM Students;
```

## Клиент (по избор)
- Host: localhost, Port: 3306, User: root, Password: (ако е зададена), DB: school_db

## Зареждане на примерни данни (seed)
- В phpMyAdmin: изберете база `school_db` → Import → файл `01_seed_students_groups.sql` (в папката на урока) → Go.
