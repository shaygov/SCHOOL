-- Create sample schema and seed data for JOIN demos
CREATE DATABASE IF NOT EXISTS school_db CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE school_db;

DROP TABLE IF EXISTS StudentGroups;
DROP TABLE IF EXISTS `Groups`;
DROP TABLE IF EXISTS Students;

CREATE TABLE Students (
  StudentId INT AUTO_INCREMENT PRIMARY KEY,
  EGN CHAR(10) UNIQUE,
  FullName VARCHAR(100) NOT NULL
);

CREATE TABLE `Groups` (
  GroupId INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(100) NOT NULL
);

CREATE TABLE StudentGroups (
  StudentId INT NOT NULL,
  GroupId INT NOT NULL,
  JoinedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (StudentId, GroupId),
  CONSTRAINT fk_sg_student FOREIGN KEY (StudentId) REFERENCES Students(StudentId) ON DELETE CASCADE,
  CONSTRAINT fk_sg_group FOREIGN KEY (GroupId) REFERENCES `Groups`(GroupId) ON DELETE CASCADE
);

INSERT INTO Students (EGN, FullName) VALUES
('0101010001','Айше Б. Дагон'),
('0101010002','Айше Т. Пастрамарска'),
('0101010003','Анифе И. Кусарова'),
('0101010004','Атидже И. Ходжова'),
('0101010005','Атидже И. Тупева'),
('0101010006','Джемиле М. Хасан'),
('0101010007','Джемиле С. Али'),
('0101010008','Зарифка М. Хахньова'),
('0101010009','Муртаза М. Клечов'),
('0101010010','Муса И. Шехов'),
('0101010011','Муса И. Кимов'),
('0101010012','Муса С. Аянски'),
('0101010013','Муса Х. Абдула'),
('0101010014','Мустафа А. Аянски'),
('0101010015','Найме И. Бекир'),
('0101010016','Сабит А. Али'),
('0101010017','Сайде И. Зекрия'),
('0101010018','Хасан М. Кавунски'),
('0101010019','Юсуф И. Звездьов');

INSERT INTO `Groups` (Name) VALUES
('Math'),
('Informatics'),
('English');

-- Relations (Maria без група за LEFT JOIN демонстрация)
INSERT INTO StudentGroups (StudentId, GroupId) VALUES
((SELECT StudentId FROM Students WHERE FullName='Айше Б. Дагон'), (SELECT GroupId FROM `Groups` WHERE Name='Math')),
((SELECT StudentId FROM Students WHERE FullName='Айше Б. Дагон'), (SELECT GroupId FROM `Groups` WHERE Name='Informatics')),
((SELECT StudentId FROM Students WHERE FullName='Хасан М. Кавунски'), (SELECT GroupId FROM `Groups` WHERE Name='English'));


