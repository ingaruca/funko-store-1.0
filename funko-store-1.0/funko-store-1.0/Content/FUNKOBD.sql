Use Master
go

if exists(Select * from sys.databases  Where name='FUNKOBD')
Begin
	Drop Database FUNKOBD
End
go

CREATE DATABASE FUNKOBD;
GO

USE FUNKOBD;
GO

--drop sequence Cod_Prod_Seq
CREATE SEQUENCE Cod_Prod_Seq 
AS smallint
START WITH 1
INCREMENT BY 1
NO CYCLE
NO CACHE
GO

--drop sequence Cod_USU_Seq
CREATE SEQUENCE Cod_USU_Seq 
AS smallint
START WITH 1
INCREMENT BY 1
NO CYCLE
NO CACHE
GO

--DROP TABLE tb_usuarios
CREATE TABLE tb_usuarios
(
idusu char(8) NOT NULL DEFAULT 'USU' + RIGHT('0000' + CAST(NEXT VALUE FOR Cod_USU_Seq AS varchar), 5),
nomusu varchar(30) not null,
pass nvarchar(500) not null,
tipusu varchar(50) not null,
correo varchar(100),
direcenvio varchar(200),
tarjeta varchar(16),
estado char(1) NOT NULL,
PRIMARY KEY (idusu)
)
GO
/*
insert into tb_usuarios (nomusu,pass,tipusu,estado) values('Alejo','123','ADMIN','A')
select * from tb_usuarios
*/
--truncate table tb_categorias
--drop table tb_categorias
CREATE TABLE tb_categorias
(
idcate int IDENTITY(1,1) NOT NULL,
nomcate varchar(50) NOT NULL,
descripcion text,
estado char(1) NOT NULL,
PRIMARY KEY (idcate)
)
GO

--select * from tb_categorias
--insert into tb_categorias(nomcate,descripcion,estado) values('ANIMO','AIUDA','A')

--drop table tb_productos
--truncate table tb_productos
CREATE TABLE tb_productos
(
idprodu char(8) NOT NULL DEFAULT 'PRO' + RIGHT('0000' + CAST(NEXT VALUE FOR Cod_Prod_Seq AS varchar), 5),
codbar varchar(13) NOT NULL,
nomprodu varchar(100) not null,
idcate int not null,
entrada int default 0,
salida int default 0,
precio decimal(15,2) not null,
caracte varchar(200) not null,
descripcion text,
imagen varchar(50),
estado char(1) NOT NULL,
PRIMARY KEY (idprodu),
FOREIGN KEY (idcate) REFERENCES tb_categorias (idcate)
)
go
--select * from tb_productos
/*
insert into tb_productos (codbar,nomprodu,idcate,precio,caracte,descripcion,imagen,estado) 
VALUES ('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00001.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00002.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00003.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00004.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00005.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00006.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00007.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00008.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00009.jpg','A'),
('1234567891234','Hulk',1,100.99,'es verde','hombre verde','PRO00010.jpg','A')
go
*/
--drop table tb_pedido
--truncate table tb_pedido
create table tb_pedido
(
idpedido int IDENTITY(1001,1) not null,
fecpedido smalldatetime CONSTRAINT datetimenow DEFAULT GETDATE(),
idusu char(8) NOT NULL,
total decimal(15,2) default 0.00 NOT NULL,
estado char(10) NOT NULL,
PRIMARY KEY (idpedido),
FOREIGN KEY (idusu) REFERENCES tb_usuarios (idusu)
)
go

--insert into tb_pedido(idusu,total,estado) values('USU00001',1000.50,'PENDIENTE')
--go
/*select * from tb_pedido
go
select * from tb_detapedido
go*/
--drop table tb_detapedido
create table tb_detapedido
(
idpedido int NOT NULL,
idprodu char(8) NOT NULL,
precio decimal(15,2) default 0.00 NOT NULL,
cantidad int default 0 NOT NULL,
foreign key(idpedido) references tb_pedido (idpedido)
on update cascade,
foreign key(idprodu) references tb_productos (idprodu)
on update cascade
)
go

-- PROCEDIMIENTOS ALMACENADOS
--drop proc Autogenera
Create proc Autogenera
as 
	select iif(max(idpedido)+1 is null,1001,max(idpedido)+1) from tb_pedido
	
go

----------------------------------

create procedure detalleProducto
@idprodu char(8)
as
select p.idprodu, p.codbar, p.nomprodu, p.idcate, c.nomcate, p.entrada, p.salida, 
	   p.precio, p.caracte, p.descripcion, imagen, p.estado
from tb_productos p join tb_categorias c on p.idcate = c.idcate where p.idprodu = @idprodu

go

/*buscar producto*/
create procedure buscarProducto
@idprodu char(8)
as
select * from tb_productos where idprodu = @idprodu
go

/*eliminar de productos*/
create procedure eliminarProducto
@idprodu char(8)
as
delete tb_productos where idprodu=@idprodu
go

/*Insert de productos*/
create procedure insertarProducto
@codbar varchar(13),
@nomprodu varchar(100),
@idcate int,
@entrada int,
@salida int,
@precio decimal(15,2),
@caracte varchar(200),
@descripcion text,
@imagen varchar(50),
@estado char(1)
as
insert into tb_productos (codbar, nomprodu, idcate, entrada, salida, precio, caracte, descripcion, imagen, estado)
values (@codbar, @nomprodu, @idcate, @entrada, @salida, @precio, @caracte, @descripcion, @imagen, @estado)
go

/*modificar de productos*/
create procedure modificarProducto
@idprodu char(8),
@codbar varchar(13),
@nomprodu varchar(100),
@idcate int,
@entrada int,
@salida int,
@precio decimal(15,2),
@caracte varchar(200),
@descripcion text,
@imagen varchar(50),
@estado char(1)
as
update tb_productos set codbar=@codbar, nomprodu=@nomprodu, idcate=@idcate, entrada=@entrada, salida=@salida, 
						precio=@precio, caracte=@caracte, descripcion=@descripcion, imagen=@imagen, estado=@estado
					where idprodu=@idprodu
go

/*listar productos*/
create procedure listarProductos
as
select p.idprodu, p.codbar, nomprodu, p.idcate, c.nomcate, p.entrada, p.salida, 
	   p.precio, p.caracte, p.descripcion, imagen, p.estado
from tb_productos p join tb_categorias c on p.idcate = c.idcate
go

/*LISTAR CATEGORIA*/
CREATE PROCEDURE listarCategorias
AS
SELECT * FROM tb_categorias
GO

/*ELIMINAR CATEGORIA*/
CREATE PROCEDURE eliminarCategoria
@idcate int
AS
DELETE tb_categorias WHERE idcate=@idcate
GO

/*INSERTAR CATEGORIA*/
CREATE PROCEDURE insertarCategoria
@nomcate varchar(50),
@descripcion text,
@estado char(1)
AS
INSERT INTO tb_categorias(nomcate, descripcion, estado)
	   VALUES(@nomcate, @descripcion, @estado)
GO

/*MODIFICAR CATEGORIA*/
CREATE PROCEDURE modificarCategoria
@idcate int,
@nomcate varchar(50),
@descripcion text,
@estado char(1)
AS
UPDATE tb_categorias SET nomcate=@nomcate, descripcion=@descripcion, estado=@estado
		             WHERE idcate=@idcate
GO

/*LISTAR USUARIO*/
CREATE PROCEDURE listarUsuarios
AS
SELECT * FROM tb_usuarios
GO

/*ELIMINAR USUARIO*/
CREATE PROCEDURE eliminarUsuario
@idusu char(8)
AS
DELETE tb_usuarios WHERE idusu=@idusu
GO

/*INSERTAR USUARIO*/
CREATE PROCEDURE insertarUsuario
@nomusu varchar(30),
@pass nvarchar(500),
@tipusu varchar(50),
@correo varchar(100),
@direcenvio nvarchar(200),
@tarjeta varchar(16),
@estado char(1)
AS
INSERT INTO tb_usuarios(nomusu, pass, tipusu, correo, direcenvio, tarjeta, estado)
	   VALUES(@nomusu, @pass, @tipusu, @direcenvio, @correo, @tarjeta, @estado)
GO

/*MODIFICAR USUARIO*/
CREATE PROCEDURE modificarUsuario
@idusu char(8),
@nomusu varchar(30),
@pass nvarchar(500),
@tipusu varchar(50),
@correo varchar(100),
@direcenvio nvarchar(200),
@tarjeta varchar(16),
@estado char(1)
AS
UPDATE tb_usuarios SET nomusu=@nomusu, pass=@pass, tipusu=@tipusu, correo=@correo,
					   direcenvio=@direcenvio, tarjeta=@tarjeta, estado=@estado
		           WHERE idusu=@idusu
GO

/*DETALLE USUARIO*/
CREATE PROCEDURE detalleUsuario
@idusu char(8)
AS
SELECT * FROM tb_usuarios WHERE idusu=@idusu
GO
