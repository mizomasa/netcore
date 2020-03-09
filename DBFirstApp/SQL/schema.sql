
CREATE TABLE t_human (
	id INTEGER NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	sex INTEGER NOT NULL,
	birthday DATETIME NOT NULL,
	CreatedBy varchar(200),
	CreatedOn DATETIME,
	UpdatedBy varchar(200),
	UpdatedOn DATETIME,
	CONSTRAINT t_human_PK PRIMARY KEY (id)
);

CREATE TABLE t_employee (
    employee_id varchar(200) NOT NULL,
	mail_address varchar(200) NOT NULL,
	human_id INTEGER NOT NULL,
	CreatedBy varchar(200),
	CreatedOn DATETIME,
	UpdatedBy varchar(200),
	UpdatedOn DATETIME,
	CONSTRAINT t_employee_PK PRIMARY KEY (employee_id)
);


CREATE TABLE t_family (
    employee_id varchar(200) NOT NULL,
	human_id INTEGER NOT NULL,
    relation_ship INTEGER NOT NULL,
	CreatedBy varchar(200),
	CreatedOn DATETIME,
	UpdatedBy varchar(200),
	UpdatedOn DATETIME,
	CONSTRAINT t_family_PK PRIMARY KEY (employee_id,human_id)
);

CREATE TABLE m_demartment(
    id INTEGER NOT NULL,
    name VARCHAR(100) NOT NULL,
	CreatedBy varchar(200),
	CreatedOn DATETIME,
	UpdatedBy varchar(200),
	UpdatedOn DATETIME,
	CONSTRAINT m_demartment_PK PRIMARY KEY (id)
);


CREATE TABLE t_departmentHistory(
    human_id INTEGER NOT NULL,
    departmnet_id INTEGER NOT NULL,
    start_date DateTime Not Null,    
    end_date DateTime,
	CreatedBy varchar(200),
	CreatedOn DATETIME,
	UpdatedBy varchar(200),
	UpdatedOn DATETIME,
	CONSTRAINT t_departmentHistory_PK PRIMARY KEY (human_id,departmnet_id)
);


CREATE TABLE t_notification(
	notification_id INTEGER NOT NULL,
    title varchar(100) not null,
    contents varchar(1000) not null,
	CreatedBy varchar(200),
	CreatedOn DATETIME,
	UpdatedBy varchar(200),
	UpdatedOn DATETIME,
	CONSTRAINT t_notification_PK PRIMARY KEY (notification_id)
);


CREATE TABLE t_notificationTarget(
	notification_id INTEGER NOT NULL,
    employee_id varchar(100) not null,
    confirm_date datetime ,
	CreatedBy varchar(200),
	CreatedOn DATETIME,
	UpdatedBy varchar(200),
	UpdatedOn DATETIME,
	CONSTRAINT t_notificationTarget_PK PRIMARY KEY (notification_id,employee_id)
);

