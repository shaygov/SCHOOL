-- Създаване на база данни и таблици без връзки. Първични ключове.
-- Клас: 11б

-- 1) Изтриване при нужда, за да може скриптът да се стартира многократно
DROP DATABASE IF EXISTS school_11b;

-- 2) Създаване на база с подходяща кодировка за български
CREATE DATABASE school_11b CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;

-- 3) Използване на базата
USE school_11b;

-- 4) Таблица STUDENTS (без връзки към други таблици)
CREATE TABLE students (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY, -- първичен ключ с автоинкремент
    first_name VARCHAR(50) NOT NULL,
    last_name  VARCHAR(50) NOT NULL,
    email      VARCHAR(100) UNIQUE,             -- по избор: уникален имейл
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 5) Таблица COURSES (собствен първичен ключ)
CREATE TABLE courses (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY, -- първичен ключ с автоинкремент
    title VARCHAR(100) NOT NULL,
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 6) Демонстрационни записи (по желание)
INSERT INTO students (first_name, last_name, email) VALUES
    ('Ivan',  'Petrov', 'ivan.petrov@example.com'),
    ('Maria', 'Georgieva', 'maria.georgieva@example.com'),
    ('Georgi','Ivanov', NULL);

INSERT INTO courses (title, description) VALUES
    ('Databases 101', 'Въведение в бази от данни'),
    ('Intro to SQL', 'Базови SQL команди: SELECT, INSERT, UPDATE, DELETE');

-- 7) Примери за заявки
-- Всички ученици
SELECT * FROM students;
-- Курс по ключ
SELECT * FROM courses WHERE id = 1;

-- Край

