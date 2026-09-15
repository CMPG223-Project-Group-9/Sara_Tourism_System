-- ============================================================================
-- SARA Tourism Booking System
-- MySQL Database Script (Aligned with Project Data Model Image)
-- Group 9 - CMPG213 Project 3
-- ============================================================================

-- ----------------------------------------------------------------------------
-- 1. DATABASE
-- ----------------------------------------------------------------------------
DROP DATABASE IF EXISTS sara_tourism;
CREATE DATABASE sara_tourism CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE sara_tourism;

-- ----------------------------------------------------------------------------
-- 2. TABLES (Updated to match the schema image fields, data types, and names)
-- ----------------------------------------------------------------------------

-- ---------- TOURIST ----------
CREATE TABLE Tourist (
    Tourist_ID      INT AUTO_INCREMENT PRIMARY KEY,
    Surname         VARCHAR(50)  NOT NULL,
    Name            VARCHAR(50)  NOT NULL,
    Email           VARCHAR(100) NOT NULL,
    `Password`        VARCHAR(255) NOT NULL,
    Date_Of_Birth   DATE         NOT NULL,
    CONSTRAINT UQ_Tourist_Email UNIQUE (Email)
) ENGINE=InnoDB;

-- ---------- ACTIVITY ----------
CREATE TABLE Activity (
    Activity_ID         INT AUTO_INCREMENT PRIMARY KEY,
    Activity_Name       VARCHAR(50)    NOT NULL,
    Description         VARCHAR(100)   NULL,
    Duration            INT            NOT NULL,
    Price_Per_Person    DECIMAL(10,2)  NOT NULL
) ENGINE=InnoDB;

-- ---------- TOUR GUIDE ----------
CREATE TABLE Tour_Guide (
    Tour_Guide_ID   INT AUTO_INCREMENT PRIMARY KEY,
    Name            VARCHAR(20)  NOT NULL,
    Specialization  VARCHAR(50)  NOT NULL
) ENGINE=InnoDB;

-- ---------- ADMIN ----------
-- Note: Retained from original script for management functionality
CREATE TABLE Admin (
    AdminID         INT AUTO_INCREMENT PRIMARY KEY,
    AdminName       VARCHAR(100) NOT NULL,
    Email           VARCHAR(100) NOT NULL,
    `Password`    VARCHAR(255) NOT NULL,
    CONSTRAINT UQ_Admin_Email UNIQUE (Email)
) ENGINE=InnoDB;

-- ---------- BOOKING ----------
CREATE TABLE Booking (
    Booking_ID      INT AUTO_INCREMENT PRIMARY KEY,
    Tourist_ID      INT            NOT NULL,
    Activity_ID     INT            NOT NULL,
    Tour_Guide_ID   INT            NULL,
    Booking_Date    DATE           NOT NULL,
    Start_Time      TIME           NOT NULL,
    End_Time        TIME           NOT NULL,
    Number_of_People INT           NOT NULL,
    Payment_Status  VARCHAR(20)    NOT NULL DEFAULT 'Pending',
    TotalAmount     DECIMAL(10,2)  NOT NULL DEFAULT 0,        -- Retained for financial balance calculations
    CONSTRAINT FK_Booking_Tourist   FOREIGN KEY (Tourist_ID)    REFERENCES Tourist(Tourist_ID)       ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT FK_Booking_Activity  FOREIGN KEY (Activity_ID)   REFERENCES Activity(Activity_ID)     ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT FK_Booking_TourGuide FOREIGN KEY (Tour_Guide_ID) REFERENCES Tour_Guide(Tour_Guide_ID) ON DELETE SET NULL  ON UPDATE CASCADE,
    CONSTRAINT CHK_Booking_People CHECK (Number_of_People > 0),
    CONSTRAINT CHK_Booking_Times CHECK (End_Time > Start_Time),
	CONSTRAINT CHK_Booking_PaymentStatus CHECK ( Payment_Status IN ('Pending','Successful','Failed','Refunded'))
) ENGINE=InnoDB;

-- ---------- TOUR REVIEW ----------
CREATE TABLE Tour_Review (
    Review_ID       INT AUTO_INCREMENT PRIMARY KEY,
    Booking_ID      INT           NOT NULL,
    Rating          INT           NOT NULL,
    Comment         VARCHAR(100)  NULL,
    CONSTRAINT UQ_Review_Booking UNIQUE (Booking_ID),
    CONSTRAINT FK_Review_Booking FOREIGN KEY (Booking_ID) REFERENCES Booking(Booking_ID) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT CHK_Review_Rating CHECK (Rating BETWEEN 1 AND 5)
) ENGINE=InnoDB;

-- ----------------------------------------------------------------------------
-- 3. INDEXES
-- ----------------------------------------------------------------------------
CREATE INDEX IX_Booking_Date        ON Booking(Booking_Date);
CREATE INDEX IX_Booking_Payment_Status      ON Booking(Payment_Status);
CREATE INDEX IX_Booking_TourGuide   ON Booking(Tour_Guide_ID);
CREATE INDEX IX_Booking_Tourist     ON Booking(Tourist_ID);


-- ============================================================================
-- 4. STORED PROCEDURES - TOURIST
-- ============================================================================
DELIMITER $$

CREATE PROCEDURE sp_Tourist_Insert (
    IN p_Surname VARCHAR(50), IN p_Name VARCHAR(50), IN p_Email VARCHAR(100),
    IN p_Password VARCHAR(255), IN p_Date_Of_Birth DATE,
    OUT p_NewTouristID INT
)
BEGIN
    INSERT INTO Tourist (Surname, Name, Email, `Password`, Date_Of_Birth)
    VALUES (p_Surname, p_Name, p_Email, p_Password, p_Date_Of_Birth);
    SET p_NewTouristID = LAST_INSERT_ID();
END $$

CREATE PROCEDURE sp_Tourist_Update (
    IN p_TouristID INT, IN p_Surname VARCHAR(50), IN p_Name VARCHAR(50),
    IN p_Email VARCHAR(100), IN p_Date_Of_Birth DATE
)
BEGIN
    UPDATE Tourist
    SET Surname = p_Surname, Name = p_Name, Email = p_Email, Date_Of_Birth = p_Date_Of_Birth
    WHERE Tourist_ID = p_TouristID;
END $$

CREATE PROCEDURE sp_Tourist_Delete (IN p_TouristID INT)
BEGIN
    
    DELETE FROM Tourist WHERE Tourist_ID = p_TouristID;
END $$

CREATE PROCEDURE sp_Tourist_GetById (IN p_TouristID INT)
BEGIN
    SELECT * FROM Tourist WHERE Tourist_ID = p_TouristID;
END $$

CREATE PROCEDURE sp_Tourist_GetAll ()
BEGIN
    SELECT * FROM Tourist ORDER BY Surname, Name;
END $$

CREATE PROCEDURE sp_Tourist_Login (IN p_Email VARCHAR(100))
BEGIN
    SELECT Tourist_ID, Name, Surname, Email, `Password`
    FROM Tourist
    WHERE Email = p_Email;
END $$

DELIMITER ;

-- ============================================================================
-- 5. STORED PROCEDURES - ACTIVITY
-- ============================================================================
DELIMITER $$

CREATE PROCEDURE sp_Activity_Insert (
    IN p_Activity_Name VARCHAR(100), IN p_Description VARCHAR(255),
    IN p_Duration INT, IN p_Price_Per_Person DECIMAL(10,2),
    OUT p_NewActivity_ID INT
)
BEGIN
    INSERT INTO Activity (Activity_Name, Description, Duration, Price_Per_Person)
    VALUES (p_Activity_Name, p_Description, p_Duration, p_Price_Per_Person);
    SET p_NewActivity_ID = LAST_INSERT_ID();
END $$

CREATE PROCEDURE sp_Activity_Update (
    IN p_Activity_ID INT, IN p_Activity_Name VARCHAR(100), IN p_Description VARCHAR(255),
    IN p_Duration INT, IN p_Price_Per_Person DECIMAL(10,2)
)
BEGIN
    UPDATE Activity
    SET Activity_Name = p_Activity_Name, Description = p_Description,
        Duration = p_Duration, Price_Per_Person = p_Price_Per_Person
    WHERE Activity_ID = p_Activity_ID;
END $$

CREATE PROCEDURE sp_Activity_Delete (IN p_Activity_ID INT)
BEGIN
    DELETE FROM Activity WHERE Activity_ID = p_Activity_ID;
END $$

CREATE PROCEDURE sp_Activity_GetById (IN p_Activity_ID INT)
BEGIN
    SELECT * FROM Activity WHERE Activity_ID = p_Activity_ID;
END $$

CREATE PROCEDURE sp_Activity_GetAll ()
BEGIN
    SELECT * FROM Activity ORDER BY Activity_Name;
END $$



DELIMITER ;

-- ============================================================================
-- 6. STORED PROCEDURES - TOUR GUIDE
-- ============================================================================
DELIMITER $$

CREATE PROCEDURE sp_TourGuide_Insert (
    IN p_Name VARCHAR(20), IN p_Specialization VARCHAR(50),
    OUT p_NewTourGuideID INT
)
BEGIN
    INSERT INTO Tour_Guide (Name, Specialization) VALUES (p_Name, p_Specialization);
    SET p_NewTourGuideID = LAST_INSERT_ID();
END $$

CREATE PROCEDURE sp_TourGuide_Update (
    IN p_TourGuideID INT, IN p_Name VARCHAR(20), IN p_Specialization VARCHAR(50)
)
BEGIN
    UPDATE Tour_Guide SET Name = p_Name, Specialization = p_Specialization
    WHERE Tour_Guide_ID = p_TourGuideID;
END $$

CREATE PROCEDURE sp_TourGuide_Delete (IN p_TourGuideID INT)
BEGIN
    DELETE FROM Tour_Guide WHERE Tour_Guide_ID = p_TourGuideID;
END $$

CREATE PROCEDURE sp_TourGuide_GetById (IN p_TourGuideID INT)
BEGIN
    SELECT * FROM Tour_Guide WHERE Tour_Guide_ID = p_TourGuideID;
END $$

CREATE PROCEDURE sp_TourGuide_GetAll ()
BEGIN
    SELECT * FROM Tour_Guide ORDER BY Name;
END $$

CREATE PROCEDURE sp_TourGuide_GetAvailable (
    IN p_BookingDate DATE, IN p_StartTime TIME, IN p_EndTime TIME
)
BEGIN
    SELECT g.*
    FROM Tour_Guide g
    WHERE g.Tour_Guide_ID NOT IN (
        SELECT b.Tour_Guide_ID
        FROM Booking b
        WHERE b.Booking_Date = p_BookingDate
        AND b.Tour_Guide_ID IS NOT NULL
		AND b.Payment_Status = 'Successful'
        AND (p_StartTime < b.End_Time AND p_EndTime > b.Start_Time)
      )
    ORDER BY g.Name;
END $$

DELIMITER ;

-- ============================================================================
-- 7. STORED PROCEDURES - ADMIN
-- ============================================================================
DELIMITER $$



CREATE PROCEDURE sp_Admin_Update (
    IN p_AdminID INT, IN p_AdminName VARCHAR(100), IN p_Email VARCHAR(100)
)
BEGIN
    UPDATE Admin SET AdminName = p_AdminName, Email = p_Email WHERE AdminID = p_AdminID;
END $$



CREATE PROCEDURE sp_Admin_Login (IN p_Email VARCHAR(100))
BEGIN
    SELECT AdminID, AdminName, Email, `Password` FROM Admin WHERE Email = p_Email;
END $$

DELIMITER ;

-- ============================================================================
-- 8. STORED PROCEDURES - BOOKING
-- ============================================================================
DELIMITER $$


CREATE PROCEDURE sp_Booking_Insert (
    IN p_Tourist_ID INT, IN p_Activity_ID INT, IN p_Tour_Guide_ID INT,
    IN p_Booking_Date DATE, IN p_Start_Time TIME, IN p_End_Time TIME,
    IN p_Number_of_People INT, IN p_Payment_Status VARCHAR(20),
    OUT p_NewBookingID INT
)
BEGIN
	DECLARE v_Price_Per_Person DECIMAL(10,2);
	DECLARE v_TotalAmount DECIMAL(10,2);
	SELECT Price_Per_Person INTO v_Price_Per_Person FROM Activity WHERE Activity_ID  = p_Activity_ID;
	SET v_TotalAmount = v_Price_Per_Person * p_Number_of_People;
	
    INSERT INTO Booking (Tourist_ID, Activity_ID, Tour_Guide_ID, Booking_Date,
                          Start_Time, End_Time, Number_of_People, Payment_Status, TotalAmount)
    VALUES (p_Tourist_ID, p_Activity_ID, p_Tour_Guide_ID, p_Booking_Date,
            p_Start_Time, p_End_Time, p_Number_of_People, p_Payment_Status, v_TotalAmount);
    SET p_NewBookingID = LAST_INSERT_ID();
END $$

CREATE PROCEDURE sp_Booking_Update (
    IN p_BookingID INT, IN p_Booking_Date DATE, IN p_Start_Time TIME, IN p_End_Time TIME,
    IN p_Number_of_People INT
)
BEGIN
	DECLARE v_Activity_ID INT;
	DECLARE v_Price_Per_Person DECIMAL(10,2);
	DECLARE v_TotalAmount DECIMAL(10,2);
	
	SELECT Activity_ID INTO v_Activity_ID FROM Booking WHERE Booking_ID = p_BookingID;
	
	SELECT Price_Per_Person INTO v_Price_Per_Person FROM Activity WHERE Activity_ID = v_Activity_ID;
	
	SET v_TotalAmount = v_Price_Per_Person * p_Number_of_People;
	
    UPDATE Booking
    SET Booking_Date = p_Booking_Date, Start_Time = p_Start_Time, End_Time = p_End_Time,
        Number_of_People = p_Number_of_People, TotalAmount = v_TotalAmount
    WHERE Booking_ID = p_BookingID;
END $$

CREATE PROCEDURE sp_Booking_AllocateGuide (
    IN p_BookingID INT, IN p_Tour_Guide_ID INT
)
BEGIN
    UPDATE Booking
    SET Tour_Guide_ID = p_Tour_Guide_ID
    WHERE Booking_ID = p_BookingID
		AND Payment_Status = 'Successful';
		
	IF ROW_COUNT() = 0 THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Guide can only be allocated to a successfully paid booking';
	END IF;
END $$


CREATE PROCEDURE sp_Booking_Cancel (IN p_BookingID INT)
BEGIN
    UPDATE Booking SET Payment_Status = 'Refunded' WHERE Booking_ID = p_BookingID AND Payment_Status = 'Successful';
	
END $$



CREATE PROCEDURE sp_Booking_GetAll ()
BEGIN
    SELECT b.*, t.Name AS TouristName, t.Surname AS TouristSurname,
           a.Activity_Name, g.Name AS GuideName
    FROM Booking b
    JOIN Tourist t   ON b.Tourist_ID = t.Tourist_ID
    JOIN Activity a  ON b.Activity_ID = a.Activity_ID
    LEFT JOIN Tour_Guide g ON b.Tour_Guide_ID = g.Tour_Guide_ID
    ORDER BY b.Booking_Date DESC, b.Start_Time;
END $$

CREATE PROCEDURE sp_Booking_GetByTourist (IN p_TouristID INT)
BEGIN
    SELECT b.*, a.Activity_Name, g.Name AS GuideName
    FROM Booking b
    JOIN Activity a ON b.Activity_ID = a.Activity_ID
    LEFT JOIN Tour_Guide g ON b.Tour_Guide_ID = g.Tour_Guide_ID
    WHERE b.Tourist_ID = p_TouristID
    ORDER BY b.Booking_Date DESC;
END $$

DELIMITER ;



-- ============================================================================
-- 10. STORED PROCEDURES - TOUR REVIEW
-- ============================================================================
DELIMITER $$

CREATE PROCEDURE sp_TourReview_Insert (
    IN p_Booking_ID INT, IN p_Rating INT, IN p_Comment VARCHAR(100),
    OUT p_NewReviewID INT
)
BEGIN
    DECLARE v_PaymentStatus VARCHAR(20);
	DECLARE v_BookingDate DATE;
	DECLARE v_EndTime TIME;
	
    SELECT Payment_Status, Booking_Date, End_Time
	INTO v_PaymentStatus, v_BookingDate, v_EndTime
	FROM Booking
	WHERE Booking_ID = p_Booking_ID;

    IF v_PaymentStatus <> 'Successful' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Only successfully paid bookings can be reviewed';
    ELSEIF
		v_BookingDate > CURDATE()
		OR (v_BookingDate = CURDATE() AND v_EndTime > CURTIME())
	THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'A review can only be submitted after the tour has been completed';
	ELSE	
        INSERT INTO Tour_Review (Booking_ID, Rating, Comment)
        VALUES (p_Booking_ID, p_Rating, p_Comment);
        SET p_NewReviewID = LAST_INSERT_ID();
    END IF;
END $$

CREATE PROCEDURE sp_TourReview_Update (
    IN p_ReviewID INT, IN p_Rating INT, IN p_Comment VARCHAR(100)
)
BEGIN
    UPDATE Tour_Review SET Rating = p_Rating, Comment = p_Comment WHERE Review_ID = p_ReviewID;
END $$

CREATE PROCEDURE sp_TourReview_Delete (IN p_ReviewID INT)
BEGIN
    DELETE FROM Tour_Review WHERE Review_ID = p_ReviewID;
END $$

CREATE PROCEDURE sp_TourReview_GetAll ()
BEGIN
    SELECT r.*, b.Booking_Date, a.Activity_Name, g.Name AS GuideName
    FROM Tour_Review r
    JOIN Booking b  ON r.Booking_ID = b.Booking_ID
    JOIN Activity a ON b.Activity_ID = a.Activity_ID
    LEFT JOIN Tour_Guide g ON b.Tour_Guide_ID = g.Tour_Guide_ID
    ORDER BY r.Review_ID DESC;
END $$

DELIMITER ;

-- ============================================================================
-- 11. STORED PROCEDURES - REPORTS
-- ============================================================================
DELIMITER $$

CREATE PROCEDURE sp_Report_ToursPerGuide (
    IN p_StartDate DATE, IN p_EndDate DATE
)
BEGIN
    SELECT g.Tour_Guide_ID, g.Name AS GuideName,
           COUNT(b.Booking_ID) AS NumberOfTours
    FROM Tour_Guide g
    LEFT JOIN Booking b
           ON b.Tour_Guide_ID = g.Tour_Guide_ID
          AND b.Payment_Status = 'Successful'
          AND b.Booking_Date BETWEEN p_StartDate AND p_EndDate
		  AND ( b.Booking_Date < CURDATE() OR (b.Booking_Date = CURDATE() AND b.End_Time <= CURTIME() ) )
    GROUP BY g.Tour_Guide_ID, g.Name
    ORDER BY NumberOfTours DESC;
END $$





DELIMITER ;

-- ============================================================================
-- 12. SEED / SAMPLE DATA
-- ============================================================================
INSERT INTO Admin (AdminName, Email, `Password`) VALUES
('Lerato Redelinghuys', 'admin@saratourism.co.za', 'Admin123!');

INSERT INTO Tourist (Surname, Name, Email, `Password`, Date_Of_Birth) VALUES
('Van der Merwe','Johan','johan.vdm@gmail.com','pass5678','1988-07-22'),
('Nkosi','Thabo','thabo@gmail.com','securePwd1','1995-11-05'),
('Mokoena','Lerato','lerato@example.com','letMeIn99','2000-09-18'),
('Botha','Pieter','p.b@example.com','bothaPass','1979-04-12'),
('Smith','Sarah','sarah.smith@example.com','smith2024','1992-01-30'),
('Machaka', 'Faith', 'faith@example.com', 'user1234', '2001-03-14');

INSERT INTO Tour_Guide (Name, Specialization) VALUES
('Sipho', 'Wildlife tours'),
('Themba','Wildlife tours'),
('Kagiso','Wildlife tours'),
('Anelisa', 'Cultural heritage'),
('Zanele','Cultural heritage'),
('David','Adventure hikes'),
('Nomvula','Adventure hikes'),
('Harry', 'Adventure hikes');

INSERT INTO Activity (Activity_Name, Description, Duration, Price_Per_Person) VALUES
('Kruger Safari', 'Game drive at sunrise',240, 850.00),
('Waterberg Trail', 'Half-day hike',180, 350.00),
('Canyon Tour', 'Scenic canyon viewpoint',300, 600.00);


INSERT INTO Booking (Tourist_ID,Activity_ID,Tour_Guide_ID,Booking_Date,Start_Time,End_Time,Number_of_People,Payment_Status,TotalAmount) VALUES
(1,1,1,'2026-07-10','06:00:00','10:00:00',2,'Successful',1700.00),
(2,2,6,'2026-07-15','08:00:00','11:00:00',4,'Successful',1400.00),
(3,3,4,'2026-07-20','09:00:00','14:00:00',3,'Successful',1800.00),
(4,1,2,'2026-07-28','06:00:00','10:00:00',2,'Successful',1700.00),

(5,1,1, '2026-07-10','06:00:00','10:00:00',3,'Successful',2550.00),
(6,2,7,'2026-09-10','07:30:00','10:30:00',2,'Successful',700.00),
(1,3,5,'2026-09-12','08:00:00','13:00:00',4,'Successful',2400.00),

(2,1,NULL,'2026-09-18','06:00:00','10:00:00',2,'Pending',1700.00),
(3,2,NULL,'2026-09-20','08:30:00','11:30:00',1,'Pending',350.00),
(4,3,4,'2026-09-25','09:00:00','14:00:00',2,'Pending',1200.00),

(5,2,NULL,'2026-08-01','08:00:00','11:00:00',2,'Refunded',700.00),
(6,3,NULL,'2026-08-15','09:00:00','14:00:00',1,'Failed',600.00);



INSERT INTO Tour_Review(Booking_ID,Rating,Comment) VALUES
(1,5,'Incredible sunrise game drive! Sipho was knowledgeable and spotted lions'),
(2,4,'Great scenic hike'),
(3,5,'The views were breathtaking!'),
(4,3,'Saw plenty of wildlife but the morniing was a bit rushed');

-- Example execution mapping updated table references
CALL sp_Booking_Insert(1, 1, NULL, CURDATE() + INTERVAL 7 DAY, '06:00:00', '10:00:00', 2, 'Pending',@newBookingId);
SELECT @newBookingId AS SampleBookingID;