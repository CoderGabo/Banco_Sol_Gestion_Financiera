-- ==========================================
-- Banco Sol - Sistema de Gestion Financiera
-- Datos de prueba
-- ==========================================

INSERT INTO "Users"
("Name","Email","Password","CreatedAt")
VALUES
('Gabriel Diaz','gabdihu@gmail.com','HASH_AQUI',NOW()),
('Jose Miguel Tordoya','algo@gmail.com','HASH_AQUI',NOW()),
('Jose Armando Vargas','esteesmicorreo@gmail.com','HASH_AQUI',NOW()),
('Wanda Sandoval','nada@gmail.com','HASH_AQUI',NOW()),
('Tatiana Clavijo','yoquienmas@gmail.com','HASH_AQUI',NOW());

INSERT INTO "Incomes"
("UserId","Amount","Currency","Description","Source","ReceivedAt","CreatedAt")
VALUES

-- Gabriel
(1,5000.00,'BOB','Salario diciembre','Salary','2025-12-01',NOW()),
(1,120.00,'USD','Proyecto Landing Page','Freelance','2025-12-08',NOW()),
(1,800.00,'BOB','Bono anual','Bonus','2025-12-15',NOW()),

-- Jose Miguel
(2,3000.00,'BOB','Salario diciembre','Salary','2025-12-01',NOW()),
(2,200.00,'USD','Venta de laptop','Sale','2025-12-05',NOW()),
(2,450.00,'USD','Proyecto API','Freelance','2025-12-20',NOW()),

-- Jose Armando
(3,2500.00,'BOB','Salario noviembre','Salary','2025-11-01',NOW()),
(3,150.00,'USD','Desarrollo Backend','Freelance','2025-11-20',NOW()),
(3,600.00,'BOB','Venta de monitor','Sale','2025-12-10',NOW()),
(3,75.00,'USD','Consultoria','Consulting','2025-12-18',NOW());