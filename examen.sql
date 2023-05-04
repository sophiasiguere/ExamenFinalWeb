create database Examenfinal;
use Examenfinal;
CREATE TABLE bicicletas (
  id INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(50) NOT NULL,
  imagen VARCHAR(100) NOT NULL,
  tipo VARCHAR(20) NOT NULL,
  marca VARCHAR(50) NOT NULL,
  tamano INT NOT NULL,
  cantidad_platos INT NOT NULL,
  cantidad_pinones INT NOT NULL,
  PRIMARY KEY (id)
);
CREATE TABLE `usuarios` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
);

INSERT INTO usuarios (username, password) VALUES ('sophia', 'sophia');
