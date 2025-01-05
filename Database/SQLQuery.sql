-- Create Tables
CREATE TABLE Questions (
    QuestionId INT IDENTITY(1,1) PRIMARY KEY,
    QuestionText NVARCHAR(500) NOT NULL,
    QuestionType VARCHAR(20) NOT NULL, -- SingleChoice, MultiChoice, Text
    IsRequired BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

CREATE TABLE Options (
    OptionId INT IDENTITY(1,1) PRIMARY KEY,
    QuestionId INT FOREIGN KEY REFERENCES Questions(QuestionId),
    OptionText NVARCHAR(200) NOT NULL
);

CREATE TABLE UserResponses (
    ResponseId INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(128),  -- If using ASP.NET Identity
    QuestionId INT FOREIGN KEY REFERENCES Questions(QuestionId),
    Answer NVARCHAR(MAX),
    SubmittedDate DATETIME DEFAULT GETDATE()
);
