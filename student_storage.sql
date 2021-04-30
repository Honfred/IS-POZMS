-- phpMyAdmin SQL Dump
-- version 3.4.6
-- http://www.phpmyadmin.net
--
-- Хост: localhost:3306
-- Время создания: Апр 30 2021 г., 13:54
-- Версия сервера: 5.6.27
-- Версия PHP: 5.5.20-pl0-gentoo

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `student_storage`
--

-- --------------------------------------------------------

--
-- Структура таблицы `departments`
--

CREATE TABLE IF NOT EXISTS `departments` (
  `id` int(64) NOT NULL AUTO_INCREMENT,
  `code` int(64) NOT NULL,
  `name` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `code` (`code`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Дамп данных таблицы `departments`
--

INSERT INTO `departments` (`id`, `code`, `name`) VALUES
(1, 100, 'Отдел труда и заработной платы'),
(2, 200, 'Отдел кадров');

-- --------------------------------------------------------

--
-- Структура таблицы `materials`
--

CREATE TABLE IF NOT EXISTS `materials` (
  `id` int(64) NOT NULL AUTO_INCREMENT,
  `articul` int(64) NOT NULL,
  `name` varchar(256) NOT NULL,
  `count` int(64) NOT NULL,
  `min_count` int(64) NOT NULL,
  `organization` int(64) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `articul` (`articul`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Дамп данных таблицы `materials`
--

INSERT INTO `materials` (`id`, `articul`, `name`, `count`, `min_count`, `organization`) VALUES
(1, 46855345, 'гайка m6', 260, 1000, 1),
(2, 2412165, 'Молоток', 10, 5, 1),
(3, 531561654, 'Щетка-сметка', 20, 5, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `organizations`
--

CREATE TABLE IF NOT EXISTS `organizations` (
  `id` int(64) NOT NULL AUTO_INCREMENT,
  `name` varchar(256) NOT NULL,
  `phone` int(128) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `name` (`name`(255))
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=10 ;

--
-- Дамп данных таблицы `organizations`
--

INSERT INTO `organizations` (`id`, `name`, `phone`) VALUES
(1, 'ООО "Рога и копыта"', 7253535),
(4, 'ывапыврыпр', 1452525),
(5, 'фыфвфыв', 4785696),
(6, 'фывафвафвыа', 1485149149),
(7, 'ывпаыпывпрв', 453453453),
(8, 'ыапыркрр', 1475358732),
(9, 'ОАО "Полёт"', 2147483647);

-- --------------------------------------------------------

--
-- Структура таблицы `records`
--

CREATE TABLE IF NOT EXISTS `records` (
  `id` int(64) NOT NULL AUTO_INCREMENT,
  `materials` int(64) NOT NULL,
  `org_in` int(64) DEFAULT NULL,
  `dep_to` int(64) DEFAULT NULL,
  `date_time` datetime NOT NULL,
  `in_out_count` int(64) NOT NULL,
  `in_out` varchar(6) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `materials` (`materials`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ;

--
-- Дамп данных таблицы `records`
--

INSERT INTO `records` (`id`, `materials`, `org_in`, `dep_to`, `date_time`, `in_out_count`, `in_out`) VALUES
(1, 1, 1, NULL, '2021-04-23 00:00:00', 30, 'Пришло'),
(2, 1, NULL, 2, '2021-04-26 09:25:41', 10, 'Ушло'),
(3, 1, 1, NULL, '2021-04-30 09:20:57', 10, 'Пришло'),
(4, 1, NULL, 5, '2021-04-30 13:22:25', 25, 'Пришло'),
(5, 1, NULL, 5, '2021-04-30 13:23:16', 25, 'Пришло'),
(6, 1, NULL, 5, '2021-04-30 13:24:37', 25, 'Пришло'),
(8, 3, 1, NULL, '2021-04-30 13:45:54', 25, 'Пришло'),
(9, 2, NULL, 2, '2021-04-30 13:47:47', 25, 'Ушло'),
(10, 2, 9, NULL, '2021-04-30 13:48:14', 25, 'Пришло');

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `id` int(64) NOT NULL AUTO_INCREMENT,
  `fio` varchar(256) NOT NULL,
  `password` varchar(64) NOT NULL,
  `department` int(11) NOT NULL,
  `phone` int(64) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `department` (`department`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `fio`, `password`, `department`, `phone`) VALUES
(1, 'Иванов Иван Иванович', '123', 1, 0),
(2, 'Алинова Света Николаевна', '202cb962ac59075b964b07152d234b70', 2, 0),
(3, 'Кандратьев Алексей Иванович', '', 1, NULL),
(4, 'Вершикова Юлия Николаевна', '', 2, NULL),
(5, 'Горохин Пётр Афонасиевич', '68053af2923e00204c3ca7c6a3150cf7', 2, 656457458);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
