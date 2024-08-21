CREATE TABLE IF NOT EXISTS `livros`(
    `ID` bigint(20) NOT NULL AUTO_INCREMENT,
    `Titulo` VARCHAR(100) NOT NULL,
    `Autor` VARCHAR(100) NOT NULL,
    `Genero` VARCHAR(100) NOT NULL,
    `DataPublicacao` DATE NOT NULL,
    `UF` VARCHAR(100) NOT NULL,
    `Editora` VARCHAR(100) NOT NULL,
    `Quantidade` BIGINT(100) NOT NULL,
    `Valor` Float(6,2) NOT NULL,
    PRIMARY KEY(`ID`)
)