-- Seeder за ученици от 11б клас
-- База данни: school_11b

USE school_11b;

-- Изчистване на съществуващи данни (по желание)
-- DELETE FROM students;

-- Вмъкване на ученици от 11б клас
INSERT INTO students (first_name, last_name, email) VALUES
    ('Ава', 'Ахмед', 'ava.ahmed@school11b.bg'),
    ('Айлин', 'Ибрахим', 'aylin.ibrahim@school11b.bg'),
    ('Ахмед', 'Кехайов', 'ahmed.kehajov@school11b.bg'),
    ('Ахмед', 'Робев', 'ahmed.robev@school11b.bg'),
    ('Ибрахим', 'Бичков', 'ibrahim.bichkov@school11b.bg'),
    ('Исмаил', 'Шериф', 'ismail.sherif@school11b.bg'),
    ('Исме', 'Мустафа', 'isme.mustafa@school11b.bg'),
    ('Муса', 'Кьоров', 'musa.kyorov@school11b.bg'),
    ('Муса', 'Сабри', 'musa.sabri@school11b.bg'),
    ('Мустафа', 'Шериф', 'mustafa.sherif@school11b.bg'),
    ('Риза', 'Терзи', 'riza.terzi@school11b.bg'),
    ('Сабие', 'Авдикова', 'sabie.avdikova@school11b.bg'),
    ('Сабит', 'Айредин', 'sabit.ayredin@school11b.bg'),
    ('Сабит', 'Саабит', 'sabit.saabit@school11b.bg'),
    ('Сабиха', 'Бозева', 'sabiha.bozeva@school11b.bg'),
    ('Сабри', 'Мухтарски', 'sabri.muhtarski@school11b.bg'),
    ('Сайде', 'Бекир', 'sayde.bekir@school11b.bg'),
    ('Салих', 'Мехмед', 'salih.mehmed@school11b.bg'),
    ('Фатиме', 'Кимова', 'fatime.kimova@school11b.bg'),
    ('Фатиме', 'Гугурева', 'fatime.gugureva@school11b.bg'),
    ('Хатидже', 'Багренска', 'hatidje.bagrenska@school11b.bg'),
    ('Хатидже', 'Тачева', 'hatidje.tacheva@school11b.bg'),
    ('Хатидже', 'Бекир', 'hatidje.bekir@school11b.bg'),
    ('Хюсеин', 'Кьоров', 'huseyin.kyorov@school11b.bg'),
    ('Шерифе', 'Мехмед', 'sherife.mehmed@school11b.bg');

-- Проверка на вмъкнатите данни
SELECT 
    id,
    CONCAT(first_name, ' ', last_name) AS full_name,
    email
FROM students 
ORDER BY id;

-- Брой на учениците
SELECT COUNT(*) AS total_students FROM students;
