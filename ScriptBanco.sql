create database Banco
go
use Banco
create table Contas
(id uniqueidentifier DEFAULT NEWID() PRIMARY KEY,
saldo decimal,
chequeEspecial decimal,
situacao bit,
dsDescricao varchar(max),
dtCriacao datetime NOT NULL)
go
create table Transferencias
(
id uniqueidentifier DEFAULT NEWID() PRIMARY KEY,
idContaOrigem uniqueidentifier foreign key references Contas(id),
idContaDestino uniqueidentifier foreign key references Contas(id),
valor decimal NOT NULL,
dtTransf datetime NOT NULL,
situacao smallint,
erro varchar(max)
)
go

INSERT INTO Contas (saldo, chequeEspecial, situacao, dsDescricao, dtCriacao)
VALUES
    (1500.75, 300.00, 1, 'Conta A', GETDATE()),
    (800.50, 100.00, 1, 'Conta B', GETDATE()),
    (2000.00, 500.00, 1, 'Conta C', GETDATE()),
    (1200.25, 400.00, 1, 'Conta D', GETDATE()),
    (600.75, 150.00, 1, 'Conta E', GETDATE()),
    (950.50, 200.00, 1, 'Conta F', GETDATE()),
    (1800.00, 300.00, 1, 'Conta G', GETDATE()),
    (300.25, 50.00, 1, 'Conta H', GETDATE()),
    (1600.75, 200.00, 1, 'Conta I', GETDATE()),
    (1000.50, 100.00, 1, 'Conta J', GETDATE());
INSERT INTO Transferencias (idContaOrigem, idContaDestino, valor, dtTransf, situacao, erro)
VALUES
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta A'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta B'),
     300.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta C'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta D'),
     500.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta E'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta F'),
     150.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta G'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta H'),
     300.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta I'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta J'),
     200.00, GETDATE(), 1, NULL),
	 ((SELECT id FROM Contas WHERE dsDescricao = 'Conta B'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta C'),
     100.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta D'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta E'),
     200.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta F'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta G'),
     50.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta H'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta I'),
     100.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta J'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta A'),
     150.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta C'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta D'),
     300.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta E'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta F'),
     50.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta G'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta H'),
     200.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta I'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta J'),
     75.00, GETDATE(), 1, NULL),
    ((SELECT id FROM Contas WHERE dsDescricao = 'Conta A'),
     (SELECT id FROM Contas WHERE dsDescricao = 'Conta B'),
     120.00, GETDATE(), 1, NULL);