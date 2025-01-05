DBCC CHECKIDENT ('Questions', RESEED, 0);  -- This will reset the identity seed to 1.
DBCC CHECKIDENT ('Options', RESEED, 0);  -- This will reset the identity seed to 1.
DBCC CHECKIDENT ('UserResponses', RESEED, 0);  -- This will reset the identity seed to 1.

use SurvyTask;
Go

-- Insert Questions
INSERT INTO Questions (QuestionText, QuestionType)
VALUES 
('Which programming language do you primarily use?', 'SingleChoice'),
('Which programming language do you find easiest to learn?', 'SingleChoice'),
('What are the important factors when choosing a programming language for a new project?', 'Text'),
('What type of programming do you do most often?', 'SingleChoice'),
('Which programming language do you think has the best performance?', 'SingleChoice'),
('Which programming language do you prefer for backend development?', 'SingleChoice'),
('Which language do you prefer for front-end development?', 'SingleChoice'),
('Which of the following do you find more difficult when learning a programming language?', 'SingleChoice'),
('Which of these programming languages would you like to learn next?', 'SingleChoice');

-- Insert Options for Question 1: Which programming language do you primarily use?
DECLARE @Question1Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'Which programming language do you primarily use?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question1Id, 'Python'),
(@Question1Id, 'JavaScript'),
(@Question1Id, 'Java'),
(@Question1Id, 'C#'),
(@Question1Id, 'C++'),
(@Question1Id, 'Ruby'),
(@Question1Id, 'Go'),
(@Question1Id, 'TypeScript'),
(@Question1Id, 'PHP'),
(@Question1Id, 'Other (Please specify)');


-- Insert Options for Question 3: Which programming language do you find easiest to learn?
DECLARE @Question3Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'Which programming language do you find easiest to learn?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question3Id, 'Python'),
(@Question3Id, 'JavaScript'),
(@Question3Id, 'Ruby'),
(@Question3Id, 'Scratch (or block-based languages)'),
(@Question3Id, 'Java'),
(@Question3Id, 'Swift'),
(@Question3Id, 'PHP'),
(@Question3Id, 'C++'),
(@Question3Id, 'Go'),
(@Question3Id, 'Other (Please specify)');

-- Insert Options for Question 4: What type of programming do you do most often?
DECLARE @Question4Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'What type of programming do you do most often?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question4Id, 'Web Development'),
(@Question4Id, 'Mobile App Development'),
(@Question4Id, 'Systems Programming'),
(@Question4Id, 'Game Development'),
(@Question4Id, 'Data Science / Machine Learning'),
(@Question4Id, 'DevOps / Automation'),
(@Question4Id, 'Embedded Systems'),
(@Question4Id, 'Other (Please specify)');

-- Insert Options for Question 5: Which programming language do you think has the best performance?
DECLARE @Question5Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'Which programming language do you think has the best performance?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question5Id, 'C++'),
(@Question5Id, 'C'),
(@Question5Id, 'Rust'),
(@Question5Id, 'Go'),
(@Question5Id, 'Java'),
(@Question5Id, 'Python'),
(@Question5Id, 'JavaScript'),
(@Question5Id, 'Other (Please specify)');

-- Insert Options for Question 6: Which programming language do you prefer for backend development?
DECLARE @Question6Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'Which programming language do you prefer for backend development?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question6Id, 'Node.js (JavaScript)'),
(@Question6Id, 'Python (Django, Flask)'),
(@Question6Id, 'Java (Spring)'),
(@Question6Id, 'C# (.NET)'),
(@Question6Id, 'Ruby (Ruby on Rails)'),
(@Question6Id, 'PHP'),
(@Question6Id, 'Go'),
(@Question6Id, 'Other (Please specify)');

-- Insert Options for Question 7: Which language do you prefer for front-end development?
DECLARE @Question7Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'Which language do you prefer for front-end development?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question7Id, 'JavaScript'),
(@Question7Id, 'TypeScript'),
(@Question7Id, 'HTML/CSS (for simple websites)'),
(@Question7Id, 'Dart (Flutter)'),
(@Question7Id, 'Kotlin (for Android development)'),
(@Question7Id, 'Swift (for iOS development)'),
(@Question7Id, 'Other (Please specify)');

-- Insert Options for Question 8: How important are the following factors when choosing a programming language for a new project?
-- This is a ranking question, so no options to insert here (will be handled differently in the system).

-- Insert Options for Question 9: Which of the following do you find more difficult when learning a programming language?
DECLARE @Question9Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'Which of the following do you find more difficult when learning a programming language?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question9Id, 'Syntax'),
(@Question9Id, 'Debugging and troubleshooting'),
(@Question9Id, 'Memory management'),
(@Question9Id, 'Concurrency / Parallelism'),
(@Question9Id, 'Handling dependencies'),
(@Question9Id, 'Learning the development tools (IDEs, etc.)'),
(@Question9Id, 'Other (Please specify)');

-- Insert Options for Question 10: Which of these programming languages would you like to learn next?
DECLARE @Question10Id INT = (SELECT QuestionId FROM Questions WHERE QuestionText = 'Which of these programming languages would you like to learn next?');
INSERT INTO Options (QuestionId, OptionText) VALUES 
(@Question10Id, 'Python'),
(@Question10Id, 'JavaScript'),
(@Question10Id, 'Rust'),
(@Question10Id, 'Go'),
(@Question10Id, 'Kotlin'),
(@Question10Id, 'Swift'),
(@Question10Id, 'TypeScript'),
(@Question10Id, 'C++'),
(@Question10Id, 'Julia'),
(@Question10Id, 'Other (Please specify)');