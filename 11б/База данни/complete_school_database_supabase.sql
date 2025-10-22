-- Пълен SQL файл за база данни на училище - Supabase версия
-- Включва: създаване на таблици, seed данни, select заявки и връзки
-- Адаптирано за PostgreSQL/Supabase

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ==============================================
-- 1. СЪЗДАВАНЕ НА ТАБЛИЦИ
-- ==============================================

-- Таблица за класове
CREATE TABLE IF NOT EXISTS classes (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    class_name TEXT NOT NULL UNIQUE,
    class_teacher TEXT,
    created_at TIMESTAMPTZ DEFAULT NOW()
);

-- Таблица за предмети
CREATE TABLE IF NOT EXISTS subjects (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    subject_name TEXT NOT NULL,
    subject_code TEXT UNIQUE,
    credits INTEGER DEFAULT 0,
    created_at TIMESTAMPTZ DEFAULT NOW()
);

-- Таблица за ученици
CREATE TABLE IF NOT EXISTS students (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email TEXT UNIQUE,
    class_id UUID,
    student_number TEXT UNIQUE,
    birth_date DATE,
    phone TEXT,
    address TEXT,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    FOREIGN KEY (class_id) REFERENCES classes(id) ON DELETE SET NULL
);

-- Таблица за оценки
CREATE TABLE IF NOT EXISTS grades (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    student_id UUID NOT NULL,
    subject_id UUID NOT NULL,
    grade_value NUMERIC(3,2) NOT NULL CHECK (grade_value >= 2.00 AND grade_value <= 6.00),
    grade_type TEXT DEFAULT 'тест' CHECK (grade_type IN ('тест', 'контролна', 'устен', 'курсова', 'изпит')),
    exam_date DATE,
    teacher_notes TEXT,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    FOREIGN KEY (student_id) REFERENCES students(id) ON DELETE CASCADE,
    FOREIGN KEY (subject_id) REFERENCES subjects(id) ON DELETE CASCADE
);

-- Таблица за присъствия
CREATE TABLE IF NOT EXISTS attendance (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    student_id UUID NOT NULL,
    subject_id UUID NOT NULL,
    attendance_date DATE NOT NULL,
    status TEXT DEFAULT 'присъства' CHECK (status IN ('присъства', 'отсъства', 'оправдано отсъствие')),
    notes TEXT,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    FOREIGN KEY (student_id) REFERENCES students(id) ON DELETE CASCADE,
    FOREIGN KEY (subject_id) REFERENCES subjects(id) ON DELETE CASCADE
);

-- Enable Row Level Security (RLS)
ALTER TABLE classes ENABLE ROW LEVEL SECURITY;
ALTER TABLE subjects ENABLE ROW LEVEL SECURITY;
ALTER TABLE students ENABLE ROW LEVEL SECURITY;
ALTER TABLE grades ENABLE ROW LEVEL SECURITY;
ALTER TABLE attendance ENABLE ROW LEVEL SECURITY;

-- Create RLS policies (allow all for now - adjust as needed)
CREATE POLICY "Allow all operations on classes" ON classes FOR ALL USING (true);
CREATE POLICY "Allow all operations on subjects" ON subjects FOR ALL USING (true);
CREATE POLICY "Allow all operations on students" ON students FOR ALL USING (true);
CREATE POLICY "Allow all operations on grades" ON grades FOR ALL USING (true);
CREATE POLICY "Allow all operations on attendance" ON attendance FOR ALL USING (true);

-- ==============================================
-- 2. SEED ДАННИ
-- ==============================================

-- Вмъкване на класове
INSERT INTO classes (class_name, class_teacher) VALUES
('11б', 'Мария Петрова'),
('11а', 'Иван Димитров'),
('12а', 'Анна Стоянова'),
('12б', 'Петър Георгиев');

-- Вмъкване на предмети
INSERT INTO subjects (subject_name, subject_code, credits) VALUES
('Математика', 'MATH', 4),
('Български език и литература', 'BEL', 3),
('Английски език', 'ENG', 3),
('Информатика', 'INF', 4),
('Физика', 'PHY', 3),
('Химия', 'CHEM', 3),
('История', 'HIST', 2),
('География', 'GEO', 2),
('База данни', 'DB', 4),
('Програмиране', 'PROG', 4);

-- Вмъкване на ученици от 11б клас
INSERT INTO students (first_name, last_name, email, class_id, student_number, birth_date, phone) VALUES
('Ава', 'Ахмед', 'ava.ahmed@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б001', '2006-03-15', '0888123456'),
('Айлин', 'Ибрахим', 'aylin.ibrahim@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б002', '2006-07-22', '0888123457'),
('Ахмед', 'Кехайов', 'ahmed.kehajov@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б003', '2006-01-10', '0888123458'),
('Ахмед', 'Робев', 'ahmed.robev@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б004', '2006-05-18', '0888123459'),
('Ибрахим', 'Бичков', 'ibrahim.bichkov@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б005', '2006-09-03', '0888123460'),
('Исмаил', 'Шериф', 'ismail.sherif@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б006', '2006-11-12', '0888123461'),
('Исме', 'Мустафа', 'isme.mustafa@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б007', '2006-04-25', '0888123462'),
('Муса', 'Кьоров', 'musa.kyorov@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б008', '2006-08-14', '0888123463'),
('Муса', 'Сабри', 'musa.sabri@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б009', '2006-12-01', '0888123464'),
('Мустафа', 'Шериф', 'mustafa.sherif@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б010', '2006-06-08', '0888123465'),
('Риза', 'Терзи', 'riza.terzi@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б011', '2006-02-20', '0888123466'),
('Сабие', 'Авдикова', 'sabie.avdikova@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б012', '2006-10-17', '0888123467'),
('Сабит', 'Айредин', 'sabit.ayredin@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б013', '2006-03-30', '0888123468'),
('Сабит', 'Саабит', 'sabit.saabit@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б014', '2006-07-05', '0888123469'),
('Сабиха', 'Бозева', 'sabiha.bozeva@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б015', '2006-01-28', '0888123470'),
('Сабри', 'Мухтарски', 'sabri.muhtarski@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б016', '2006-05-12', '0888123471'),
('Сайде', 'Бекир', 'sayde.bekir@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б017', '2006-09-19', '0888123472'),
('Салих', 'Мехмед', 'salih.mehmed@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б018', '2006-11-26', '0888123473'),
('Фатиме', 'Кимова', 'fatime.kimova@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б019', '2006-04-02', '0888123474'),
('Фатиме', 'Гугурева', 'fatime.gugureva@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б020', '2006-08-09', '0888123475'),
('Хатидже', 'Багренска', 'hatidje.bagrenska@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б021', '2006-12-16', '0888123476'),
('Хатидже', 'Тачева', 'hatidje.tacheva@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б022', '2006-06-23', '0888123477'),
('Хатидже', 'Бекир', 'hatidje.bekir@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б023', '2006-02-07', '0888123478'),
('Хюсеин', 'Кьоров', 'huseyin.kyorov@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б024', '2006-10-14', '0888123479'),
('Шерифе', 'Мехмед', 'sherife.mehmed@school11b.bg', (SELECT id FROM classes WHERE class_name = '11б'), '11Б025', '2006-03-21', '0888123480');

-- Вмъкване на примерни оценки
INSERT INTO grades (student_id, subject_id, grade_value, grade_type, exam_date) VALUES
((SELECT id FROM students WHERE student_number = '11Б001'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), 5.50, 'тест', '2024-01-15'),
((SELECT id FROM students WHERE student_number = '11Б001'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), 4.75, 'контролна', '2024-01-20'),
((SELECT id FROM students WHERE student_number = '11Б001'), (SELECT id FROM subjects WHERE subject_code = 'DB'), 6.00, 'устен', '2024-01-25'),
((SELECT id FROM students WHERE student_number = '11Б002'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), 4.25, 'тест', '2024-01-15'),
((SELECT id FROM students WHERE student_number = '11Б002'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), 5.00, 'контролна', '2024-01-20'),
((SELECT id FROM students WHERE student_number = '11Б002'), (SELECT id FROM subjects WHERE subject_code = 'DB'), 5.50, 'устен', '2024-01-25'),
((SELECT id FROM students WHERE student_number = '11Б003'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), 3.75, 'тест', '2024-01-15'),
((SELECT id FROM students WHERE student_number = '11Б003'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), 4.50, 'контролна', '2024-01-20'),
((SELECT id FROM students WHERE student_number = '11Б003'), (SELECT id FROM subjects WHERE subject_code = 'DB'), 4.25, 'устен', '2024-01-25'),
((SELECT id FROM students WHERE student_number = '11Б004'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), 5.75, 'тест', '2024-01-15'),
((SELECT id FROM students WHERE student_number = '11Б004'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), 5.25, 'контролна', '2024-01-20'),
((SELECT id FROM students WHERE student_number = '11Б004'), (SELECT id FROM subjects WHERE subject_code = 'DB'), 5.75, 'устен', '2024-01-25'),
((SELECT id FROM students WHERE student_number = '11Б005'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), 4.00, 'тест', '2024-01-15'),
((SELECT id FROM students WHERE student_number = '11Б005'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), 4.75, 'контролна', '2024-01-20'),
((SELECT id FROM students WHERE student_number = '11Б005'), (SELECT id FROM subjects WHERE subject_code = 'DB'), 4.50, 'устен', '2024-01-25');

-- Вмъкване на примерни присъствия
INSERT INTO attendance (student_id, subject_id, attendance_date, status) VALUES
((SELECT id FROM students WHERE student_number = '11Б001'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), '2024-01-15', 'присъства'),
((SELECT id FROM students WHERE student_number = '11Б001'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), '2024-01-16', 'присъства'),
((SELECT id FROM students WHERE student_number = '11Б001'), (SELECT id FROM subjects WHERE subject_code = 'DB'), '2024-01-17', 'присъства'),
((SELECT id FROM students WHERE student_number = '11Б002'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), '2024-01-15', 'присъства'),
((SELECT id FROM students WHERE student_number = '11Б002'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), '2024-01-16', 'отсъства'),
((SELECT id FROM students WHERE student_number = '11Б002'), (SELECT id FROM subjects WHERE subject_code = 'DB'), '2024-01-17', 'присъства'),
((SELECT id FROM students WHERE student_number = '11Б003'), (SELECT id FROM subjects WHERE subject_code = 'MATH'), '2024-01-15', 'присъства'),
((SELECT id FROM students WHERE student_number = '11Б003'), (SELECT id FROM subjects WHERE subject_code = 'BEL'), '2024-01-16', 'присъства'),
((SELECT id FROM students WHERE student_number = '11Б003'), (SELECT id FROM subjects WHERE subject_code = 'DB'), '2024-01-17', 'оправдано отсъствие');

-- ==============================================
-- 3. SELECT ЗАЯВКИ
-- ==============================================

-- Всички ученици с пълни имена
SELECT 
    id,
    CONCAT(first_name, ' ', last_name) AS full_name,
    email,
    student_number,
    birth_date
FROM students 
ORDER BY last_name, first_name;

-- Ученици от 11б клас с класния учител
SELECT 
    s.first_name,
    s.last_name,
    s.student_number,
    c.class_name,
    c.class_teacher
FROM students s
JOIN classes c ON s.class_id = c.id
WHERE c.class_name = '11б'
ORDER BY s.last_name;

-- Средни оценки по предмети
SELECT 
    sub.subject_name,
    AVG(g.grade_value) AS average_grade,
    COUNT(g.id) AS total_grades
FROM subjects sub
LEFT JOIN grades g ON sub.id = g.subject_id
GROUP BY sub.id, sub.subject_name
ORDER BY average_grade DESC;

-- Успеваемост на учениците
SELECT 
    CONCAT(s.first_name, ' ', s.last_name) AS student_name,
    sub.subject_name,
    AVG(g.grade_value) AS average_grade,
    COUNT(g.id) AS total_grades
FROM students s
JOIN grades g ON s.id = g.student_id
JOIN subjects sub ON g.subject_id = sub.id
GROUP BY s.id, s.first_name, s.last_name, sub.id, sub.subject_name
ORDER BY s.last_name, sub.subject_name;

-- Присъствия по дати
SELECT 
    a.attendance_date,
    sub.subject_name,
    COUNT(CASE WHEN a.status = 'присъства' THEN 1 END) AS present_count,
    COUNT(CASE WHEN a.status = 'отсъства' THEN 1 END) AS absent_count,
    COUNT(CASE WHEN a.status = 'оправдано отсъствие' THEN 1 END) AS excused_count
FROM attendance a
JOIN subjects sub ON a.subject_id = sub.id
GROUP BY a.attendance_date, sub.subject_name
ORDER BY a.attendance_date DESC;

-- Най-добрите ученици по среден успех
SELECT 
    CONCAT(s.first_name, ' ', s.last_name) AS student_name,
    s.student_number,
    ROUND(AVG(g.grade_value), 2) AS average_grade,
    COUNT(g.id) AS total_grades
FROM students s
JOIN grades g ON s.id = g.student_id
GROUP BY s.id, s.first_name, s.last_name, s.student_number
HAVING COUNT(g.id) >= 3
ORDER BY average_grade DESC
LIMIT 10;

-- Статистика по класове
SELECT 
    c.class_name,
    c.class_teacher,
    COUNT(s.id) AS student_count,
    COUNT(DISTINCT g.subject_id) AS subjects_with_grades
FROM classes c
LEFT JOIN students s ON c.id = s.class_id
LEFT JOIN grades g ON s.id = g.student_id
GROUP BY c.id, c.class_name, c.class_teacher
ORDER BY c.class_name;

-- ==============================================
-- 4. ДОПЪЛНИТЕЛНИ ПОЛЕЗНИ ЗАЯВКИ
-- ==============================================

-- Ученици без оценки
SELECT 
    CONCAT(s.first_name, ' ', s.last_name) AS student_name,
    s.student_number,
    s.email
FROM students s
LEFT JOIN grades g ON s.id = g.student_id
WHERE g.id IS NULL
ORDER BY s.last_name;

-- Предмети без оценки
SELECT 
    sub.subject_name,
    sub.subject_code,
    sub.credits
FROM subjects sub
LEFT JOIN grades g ON sub.id = g.subject_id
WHERE g.id IS NULL
ORDER BY sub.subject_name;

-- Месечна статистика на присъствията
SELECT 
    EXTRACT(YEAR FROM a.attendance_date) AS year,
    EXTRACT(MONTH FROM a.attendance_date) AS month,
    COUNT(CASE WHEN a.status = 'присъства' THEN 1 END) AS present_days,
    COUNT(CASE WHEN a.status = 'отсъства' THEN 1 END) AS absent_days,
    ROUND(COUNT(CASE WHEN a.status = 'присъства' THEN 1 END) * 100.0 / COUNT(*), 2) AS attendance_percentage
FROM attendance a
GROUP BY EXTRACT(YEAR FROM a.attendance_date), EXTRACT(MONTH FROM a.attendance_date)
ORDER BY year DESC, month DESC;

-- ==============================================
-- 5. ИНДЕКСИ ЗА ПОДОБРЯВАНЕ НА ПРОИЗВОДИТЕЛНОСТТА
-- ==============================================

CREATE INDEX idx_students_class_id ON students(class_id);
CREATE INDEX idx_students_email ON students(email);
CREATE INDEX idx_students_student_number ON students(student_number);
CREATE INDEX idx_students_last_name ON students(last_name);
CREATE INDEX idx_grades_student_id ON grades(student_id);
CREATE INDEX idx_grades_subject_id ON grades(subject_id);
CREATE INDEX idx_grades_exam_date ON grades(exam_date);
CREATE INDEX idx_grades_grade_value ON grades(grade_value);
CREATE INDEX idx_attendance_student_id ON attendance(student_id);
CREATE INDEX idx_attendance_subject_id ON attendance(subject_id);
CREATE INDEX idx_attendance_date ON attendance(attendance_date);
CREATE INDEX idx_attendance_status ON attendance(status);

-- ==============================================
-- 6. ПРОВЕРКА НА ДАННИТЕ
-- ==============================================

-- Общ брой записи в таблиците
SELECT 'classes' AS table_name, COUNT(*) AS record_count FROM classes
UNION ALL
SELECT 'subjects', COUNT(*) FROM subjects
UNION ALL
SELECT 'students', COUNT(*) FROM students
UNION ALL
SELECT 'grades', COUNT(*) FROM grades
UNION ALL
SELECT 'attendance', COUNT(*) FROM attendance;

-- Последни 5 добавени ученика
SELECT 
    CONCAT(first_name, ' ', last_name) AS student_name,
    student_number,
    created_at
FROM students 
ORDER BY created_at DESC 
LIMIT 5;

-- ==============================================
-- 7. SUPABASE СПЕЦИФИЧНИ ФУНКЦИИ
-- ==============================================

-- Сортиране с RLS (само собствените записи)
-- Пример: SELECT * FROM students WHERE id = auth.uid() ORDER BY last_name;

-- Full Text Search за търсене на ученици
CREATE INDEX idx_students_fulltext ON students USING gin(to_tsvector('bulgarian', first_name || ' ' || last_name));

-- Пример за FTS търсене:
-- SELECT * FROM students WHERE to_tsvector('bulgarian', first_name || ' ' || last_name) @@ plainto_tsquery('bulgarian', 'Ахмед');

-- JSON колона за допълнителни данни (ако е нужно)
-- ALTER TABLE students ADD COLUMN metadata JSONB;
-- CREATE INDEX idx_students_metadata ON students USING gin(metadata);

-- ==============================================
-- 8. ПРИМЕРИ ЗА СОРТИРАНЕ (за урока)
-- ==============================================

-- Сортиране по фамилия (български ред)
SELECT first_name, last_name, student_number
FROM students 
ORDER BY last_name COLLATE "bg_BG.utf8" ASC;

-- Сортиране по среден успех
SELECT 
    CONCAT(s.first_name, ' ', s.last_name) AS student_name,
    ROUND(AVG(g.grade_value), 2) AS average_grade
FROM students s
JOIN grades g ON s.id = g.student_id
GROUP BY s.id, s.first_name, s.last_name
ORDER BY average_grade DESC NULLS LAST;

-- Сортиране по дата на създаване (най-нови първи)
SELECT first_name, last_name, created_at
FROM students
ORDER BY created_at DESC;

-- Сортиране с множество ключове
SELECT 
    c.class_name,
    CONCAT(s.first_name, ' ', s.last_name) AS student_name,
    s.student_number
FROM students s
JOIN classes c ON s.class_id = c.id
ORDER BY c.class_name ASC, s.last_name ASC, s.first_name ASC;
