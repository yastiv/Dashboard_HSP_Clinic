# Dashboard_HSP_Clinic
PLEASE DOWNLOAD THE FOLLOWING IN VISUAL STUDIOS 2019:
GOOGLE API CALENDAR
MATERIAL DESIGN
MYSQL

CREATE THE DATABASES:

CREATE TABLE users (
id bigint NOT NULL AUTO_INCREMENT,
first_name varchar(45) NOT NULL,
last_name varchar(45) NOT NULL,
student_number varchar(13) NOT NULL,
email varchar(50) NOT NULL,
password varchar(80) NOT NULL,
phone_number varchar(13) NOT NULL,
allergies varchar(255) DEFAULT NULL,
address varchar(255) DEFAULT NULL,
gender char(1) DEFAULT NULL,
DOB date DEFAULT NULL,
validated int NOT NULL DEFAULT 0,
PRIMARY KEY (id),
UNIQUE KEY id (id),
UNIQUE KEY email (email),
UNIQUE KEY student_number (student_number)
) ENGINE=InnoDB AUTO_INCREMENT=96 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE diagnosis ( diagnosis_id int NOT NULL AUTO_INCREMENT, diagnosis_type varchar(50) DEFAULT NULL, PRIMARY KEY (diagnosis_id), UNIQUE KEY diagnosis_type_UNIQUE (diagnosis_type) ) ENGINE=InnoDB AUTO_INCREMENT=987654324 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE medicine ( medicine_id int NOT NULL AUTO_INCREMENT, medicine_name varchar(50) DEFAULT NULL, stock_amount int DEFAULT NULL, measurement varchar(30) DEFAULT NULL, expiry_date date DEFAULT NULL, batch_number varchar(20) DEFAULT NULL, PRIMARY KEY (medicine_id) ) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE treatment ( treatment_id int NOT NULL AUTO_INCREMENT, student_number varchar(10) NOT NULL, curr_date date DEFAULT NULL, diagnosis_type varchar(50) DEFAULT NULL, treatment_notes varchar(100) DEFAULT NULL, medicine_name varchar(50) DEFAULT NULL, amount_dispensed int DEFAULT NULL, measurement varchar(30) DEFAULT NULL, next_appointment date DEFAULT NULL, issued_by varchar(50) DEFAULT NULL, PRIMARY KEY (treatment_id) ) ENGINE=InnoDB AUTO_INCREMENT=1987654324 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CONNECT THE VISUAL STUDIO TO THE DATABASE.
