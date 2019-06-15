use FUNKOBD

insert into tb_usuarios (nomusu, pass, tipusu, correo, direcenvio, tarjeta, estado) values
('Alejo', 'BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413',
 'ADMIN', 'Alejo@com', 'Calle 1', '123456789', 'A');

insert into tb_categorias (nomcate, descripcion, estado) 
values ('PELICULAS', 'TEMATICAS DE PELICULAS', 'A'),
	   ('ANIMES', 'SERIES ANIMADAS JAPONESAS', 'A'),
	   ('SERIES', 'SERIES DE CADENA TELEVISA', 'A');

insert into tb_productos(codbar, nomprodu, idcate, entrada, precio, caracte, descripcion, imagen, estado)
	   VALUES('1234567898765','Shota Aizawa', 2, 10, 50.00,'My Hero Academia','Pop! Animation','PRO00001.png', 'A'),
			 ('1234786579484','Tsuyu', 2, 10, 50.00,'My Hero Academia','Pop! Animation','PRO00002.png', 'A'),
			 ('2345876984312','Deku(Training)', 2, 10, 50.00,'My Hero Academia','Pop! Animation','PRO00003.png', 'A'),
			 ('1235432346789','All Might(Weakened)', 2, 10, 50.00,'My Hero Academia','Pop! Animation','PRO00004.png', 'A'),
			 ('9087651234567','Thanos and Iron Spider', 1, 10, 100.00,'Infinity War','Marvel Avengers','PRO00005.png', 'A'),
			 ('1809862637848','Ghost', 1, 10, 150.00,'Ant-Man & The Wasp','Pop! Marvel','PRO00006.png', 'A'),
			 ('1209856453525','Captain America (Red & Blue)', 1, 10, 50.00,'Avengers','Hikari XS Marvel','PRO00007.png', 'A'),
			 ('1234675465345','Hela', 1, 10, 76.00,'Thor Ragnarok','Rock Candy Marvel','PRO00008.png', 'A'),
			 ('1245321786955','Deadpool with Swords', 1, 10, 25.00,'Deadpool','Pocket Pop! Keychain Marvel','PRO00009.png', 'A'),
			 ('3456232363647','Wonder Woman & Batman', 1, 10, 75.00,'Justice League','Vynl DC Comics','PRO00010.png', 'A'),
			 ('1245764346543','Spider-Man', 1, 10, 45.00,'Spider-Man','Hikari XS Marvel ','PRO00011.png', 'A'),
			 ('3464767657334','Deadpool & Scooter', 1, 10, 90.00,'Deadpool','Pop! Rides Marvel','PRO00012.png', 'A'),
			 ('1234556546545','Clown Deadpool', 1, 10, 45.00,'Deadpool','Pop! Marvel','PRO00013.png', 'A'),
			 ('4465333222111','Bedtime Deadpool', 1, 10, 45.00,'Deadpool','Pop! Marvel','PRO00014.png', 'A'),
			 ('4445423232333','Bed_Deadpool', 1, 10, 34.00,'Deadpool','Pop! Marvel','PRO00015.png', 'A'),
			 ('7865544423222','Gladiator Hulk', 1, 10, 34.00,'Thor Ragnarok','Wobbier Marvel','PRO00016.png', 'A'),
			 ('1244545657544','Thanos', 1, 10, 39.00,'Infinity War','Hero Plushies Marvel Avengers','PRO00017.jpg', 'A'),
			 ('1234354565555','Hulkbuster', 1, 10, 130.00,'Infinity War','Hero Plushies Marvel Avengers','PRO00018.jpg', 'A'),
			 ('2323344356566','Groot', 1, 10, 96.00,'Infinity War','Hero Plushies Avengers','PRO00019.jpg', 'A'),
			 ('4377888888555','Corvus Glaive', 1, 10, 70.00,'Infinity War','Dorbz Marvel Avengers','PRO00020.png', 'A'),
			 ('1234567898765','Thanos', 1, 10, 58.00,'Infinity War','Dorbz Marvel Avengers','PRO00021.png', 'A'),
			 ('1234567898765','Edony Maw', 1, 10, 20.00,'Infinity War','Dorbz Marvel Avengers','PRO00022.png', 'A'),
			 ('1234567898765','Captain America', 1, 10, 22.00,'Avengers','Hikari XS Marvel','PRO00023.jpg', 'A'),
			 ('1234567898765','Negasonic Teenage Warhead', 3, 10, 43.00,'X-Men Series','Pop! Marvel X-MEN ','PRO00024.png', 'A'),
			 ('1234567898765','Venom (Red & White)', 1, 10, 88.00,'Venom','Hikari XS Marvel','PRO00025.jpg', 'A'),
			 ('0839283423590','Ghost Rider', 2, 10, 50.00,'Llavero Marvel','Pocket Pop!','PRO00026.png', 'A'),
			 ('0022173680171','The joker', 2, 10, 50.00,'Hikari XS','DC Universe','PRO00027.png', 'A'),
			 ('1229903224087','Howard the duck', 2, 10, 50.00,'Contest of Champions','Pop! Games Marvel','PRO00028.png', 'A'),
			 ('3442501937596','Guillotine', 2, 10, 50.00,'Contest of Champions','Pop! Games Marvel','PRO00029.png', 'A'),
			 ('9935017277275','King Groot', 2, 10, 50.00,'Contest of Champions','Pop! Games Marvel','PRO00030.png', 'A'),
			 ('8785166090714','Godzilla', 2, 10, 50.00,'Mystery','Pop! Minis','PRO00031.png', 'A'),
			 ('1525870034965','Harry Potter S3', 2, 10, 50.00,'Mystery','Pop! Minis Box','PRO00032.png', 'A'),
			 ('6416874544482','Cuphead', 2, 10, 50.00,'Mystery','Pop! Minis Blind Box','PRO00033.png', 'A'),
	         ('8263774650506','Incredibles 2', 2, 10, 50.00,'Mystery','Pop! Minis Blind Box','PRO00034.png', 'A'),
	         ('4659563673799','Star Wars', 2, 10, 50.00,'Mystery','Pop! Minis Blind Box','PRO00035.png', 'A'),
	         ('2250952561280','Llavero Star Wars', 2, 10, 50.00,'Mystery','Pop! Minis Blind Bag Plush','PRO00036.png', 'A'),
	         ('5575200945811','Rick and Morty', 2, 10, 50.00,'Mystery','Pop! Mini','PRO00037.png', 'A'),
	         ('8764694949192','Deadpool', 2, 10, 50.00,'Mystery','Pop! Mini Blind Box','PRO00038.png', 'A'),
	         ('9962137465078','Llavero Star Wars - The last Jedi', 2, 10, 50.00,'Mystery','Mini Blind Bag Plush','PRO00039.png', 'A'),
			 ('1831326155900','Nickelodeon', 2, 10, 50.00,'Mini','Blind Bag Plush','PRO00040.png', 'A'),
	         ('3036054924271','Avengers infinity War', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00041.png', 'A'),
             ('9724666575535','The Walking Dead', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00042.png', 'A'),
             ('6085687392840','Spider-Man', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00043.png', 'A'),
             ('9338407177726','X-Men', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00044.png', 'A'),
             ('0342885481578','Gears Of War', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00045.png', 'A'),
             ('0630272710630','WWE Series', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00046.png', 'A'),
             ('4335731361916','Suicide Squad', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00047.png', 'A'),
             ('5561544302699','Bethesda All Stars', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00048.png', 'A'),
             ('8738626947271','Game of Thrones', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00049.png', 'A'),
             ('0498675682994','Warcraft', 2, 10, 50.00,'Mystery','Mini Blind Box','PRO00050.png', 'A');

select * from  tb_productos
select * from  tb_categorias

ALTER SEQUENCE Cod_Prod_Seq 
RESTART WITH 1
INCREMENT BY 1
NO CYCLE
NO CACHE
GO

DBCC CHECKIDENT (tb_categorias, RESEED,0)

delete from tb_productos
delete  from tb_categorias