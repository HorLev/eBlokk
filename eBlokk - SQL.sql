-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Máj 18. 13:33
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `eblokk`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `blokkok`
--

CREATE TABLE `blokkok` (
  `blk_id` int(11) NOT NULL,
  `fhk_qr` varchar(30) NOT NULL,
  `adatok` varchar(255) NOT NULL,
  `vasar_dt` date NOT NULL,
  `vasar_ido` time NOT NULL,
  `vasar_hely` varchar(100) NOT NULL,
  `uzlet` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `blokkok`
--

INSERT INTO `blokkok` (`blk_id`, `fhk_qr`, `adatok`, `vasar_dt`, `vasar_ido`, `vasar_hely`, `uzlet`) VALUES
(1, 'QR001', 'Tej 1L: 200 Ft, 2 db; Kenyér 500g: 150 Ft, 1 db; Sajt 354g: 800 Ft, 1 db', '2024-11-01', '10:30:00', 'Budapest, Árpád híd', 'Tesco'),
(2, 'QR001', 'Coca-Cola 1.5L: 300 Ft, 2 db; Chips 150g: 500 Ft, 3 db', '2024-11-01', '10:45:00', 'Budapest, Rákóczi tér', 'Spar'),
(3, 'QR002', 'Alma 1kg: 250 Ft, 5 db; Banán 1kg: 300 Ft, 3 db; Narancs 1kg: 400 Ft, 4 db', '2024-11-01', '11:00:00', 'Budapest, Kálvin tér', 'Lidl'),
(4, 'QR002', 'Csokoládé 90g: 400 Ft, 2 db; Keksz 200g: 300 Ft, 1 db', '2024-11-02', '09:15:00', 'Debrecen, Piac tér', 'Penny'),
(5, 'QR003', 'Tojás 10db: 500 Ft, 1 db; Liszt 1kg: 300 Ft, 2 db; Cukor 500g: 250 Ft, 1 db', '2024-11-02', '09:45:00', 'Szeged, Fő tér', 'Aldi'),
(6, 'QR003', 'Kávé 250g: 600 Ft, 1 db; Tej 1L: 200 Ft, 3 db; Méz 300g: 400 Ft, 1 db', '2024-11-02', '10:00:00', 'Budapest, Nyugati tér', 'Tesco'),
(7, 'QR004', 'Pasta 500g: 500 Ft, 2 db; Paradicsom 1kg: 400 Ft, 5 db; Hagyma 1kg: 150 Ft, 3 db', '2024-11-03', '08:30:00', 'Budapest, Rákóczi tér', 'Spar'),
(8, 'QR004', 'Sajt 200g: 800 Ft, 1 db; Kenyér 500g: 150 Ft, 2 db; Sonka 150g: 900 Ft, 1 db', '2024-11-03', '11:30:00', 'Budapest, Kálvin tér', 'Lidl'),
(9, 'QR005', 'Alma 500g: 250 Ft, 4 db; Banán 500g: 300 Ft, 3 db; Szőlő 500g: 350 Ft, 2 db', '2024-11-03', '12:00:00', 'Debrecen, Piac tér', 'Penny'),
(10, 'QR005', 'Üdítő 1.5L: 300 Ft, 6 db; Chips 200g: 500 Ft, 2 db', '2024-11-04', '14:00:00', 'Budapest, Fő tér', 'Aldi'),
(11, 'QR006', 'Kenyér 750g: 150 Ft, 2 db; Tej 1L: 200 Ft, 1 db; Sajt 300g: 800 Ft, 1 db', '2024-11-04', '14:15:00', 'Budapest, Rákóczi tér', 'Tesco'),
(12, 'QR006', 'Coca-Cola 1.5L: 300 Ft, 3 db; Sajt 200g: 800 Ft, 2 db; Kenyér 500g: 150 Ft, 1 db', '2024-11-04', '14:45:00', 'Budapest, Kálvin tér', 'Lidl'),
(13, 'QR007', 'Tojás 6db: 500 Ft, 1 db; Liszt 1kg: 300 Ft, 2 db; Zöldség Mix 500g: 400 Ft, 3 db', '2024-11-05', '09:00:00', 'Debrecen, Piac tér', 'Penny'),
(14, 'QR007', 'Csokoládé 200g: 400 Ft, 2 db; Tej 1L: 200 Ft, 1 db; Keksz 150g: 300 Ft, 3 db', '2024-11-05', '09:30:00', 'Szeged, Fő tér', 'Aldi'),
(15, 'QR008', 'Zöldség Mix 500g: 400 Ft, 5 db; Gyümölcs Mix 1kg: 300 Ft, 10 db; Kávé 100g: 600 Ft, 2 db', '2024-11-05', '10:00:00', 'Budapest, Nyugati tér', 'Tesco'),
(16, 'QR008', 'Kenyér 250g: 150 Ft, 1 db; Tej 1L: 200 Ft, 2 db; Tojás 10db: 500 Ft, 1 db', '2024-11-06', '11:00:00', 'Budapest, Rákóczi tér', 'Spar'),
(17, 'QR009', 'Sajt 100g: 800 Ft, 1 db; Üdítő 1.5L: 300 Ft, 2 db; Chips 150g: 500 Ft, 1 db', '2024-11-06', '11:30:00', 'Budapest, Kálvin tér', 'Lidl'),
(18, 'QR009', 'Pasta 1kg: 500 Ft, 3 db; Cukor 500g: 250 Ft, 2 db; Narancs 1kg: 400 Ft, 4 db', '2024-11-06', '12:00:00', 'Debrecen, Piac tér', 'Penny'),
(19, 'QR010', 'Alma 1kg: 250 Ft, 2 db; Kenyér 500g: 150 Ft, 3 db; Cukor 1kg: 250 Ft, 1 db', '2024-11-07', '09:00:00', 'Szeged, Fő tér', 'Aldi'),
(20, 'QR010', 'Kávé 200g: 600 Ft, 1 db; Tej 1L: 200 Ft, 1 db; Sajt 354g: 800 Ft, 1 db; Banán 500g: 300 Ft, 2 db', '2024-11-07', '09:30:00', 'Budapest, Árpád híd', 'Tesco');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `qr` varchar(100) NOT NULL,
  `nev` varchar(100) NOT NULL,
  `felh_nev` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `jelszo` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `felhasznalok`
--

INSERT INTO `felhasznalok` (`qr`, `nev`, `felh_nev`, `email`, `jelszo`) VALUES
('QR001', 'Kiss János', 'kiss.janos', 'janos.kiss@example.com', 'jelszo1'),
('QR002', 'Nagy Anna', 'nagy.anna', 'anna.nagy@example.com', 'jelszo2'),
('QR003', 'Varga Péter', 'varga.peter', 'peter.varga@example.com', 'jelszo3'),
('QR004', 'Tóth Gábor', 'toth.gabor', 'gabor.toth@example.com', 'jelszo4'),
('QR005', 'Szabó László', 'szabo.laszlo', 'laszlo.szabo@example.com', 'jelszo5'),
('QR006', 'Kovács Éva', 'kovacs.eva', 'eva.kovacs@example.com', 'jelszo6'),
('QR007', 'Fekete Tamás', 'fekete.tamas', 'tamas.fekete@example.com', 'jelszo7'),
('QR008', 'Horváth Zsuzsa', 'horvath.zsuzsa', 'zsuzsa.horvath@example.com', 'jelszo8'),
('QR009', 'Papp Péter', 'papp.peter', 'peter.papp@example.com', 'jelszo9'),
('QR010', 'Molnár Júlia', 'molnar.julia', 'julia.molnar@example.com', 'jelszo10'),
('QR011', 'Nagy Lajos', 'nagylali', 'nagylali@example.com', 'a8cc02c870635f72470d0a689ca16faff8958cfe845dc7a82f78de62b8d19ae7'),
('QR012', 'Horváth Levente', 'horlev', 'horlev@example.com', '0f7ea2f7c16c85be025e7620cd52df274bf6741bcfbde4b2b69858473a989383');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rendelkezesek`
--

CREATE TABLE `rendelkezesek` (
  `blk_id` int(20) NOT NULL,
  `fhk_qr` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `rendelkezesek`
--

INSERT INTO `rendelkezesek` (`blk_id`, `fhk_qr`) VALUES
(1, 'QR002'),
(1, 'QR003'),
(2, 'QR001'),
(3, 'QR003'),
(4, 'QR001'),
(5, 'QR002'),
(6, 'QR003'),
(7, 'QR001'),
(8, 'QR002'),
(9, 'QR003'),
(10, 'QR001'),
(11, 'QR004'),
(12, 'QR005'),
(13, 'QR006'),
(14, 'QR007'),
(15, 'QR008'),
(16, 'QR009'),
(17, 'QR010'),
(18, 'QR004'),
(19, 'QR005'),
(20, 'QR006');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `blokkok`
--
ALTER TABLE `blokkok`
  ADD PRIMARY KEY (`blk_id`),
  ADD KEY `qr` (`fhk_qr`);

--
-- A tábla indexei `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`qr`);

--
-- A tábla indexei `rendelkezesek`
--
ALTER TABLE `rendelkezesek`
  ADD PRIMARY KEY (`blk_id`,`fhk_qr`),
  ADD KEY `qr` (`fhk_qr`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `blokkok`
--
ALTER TABLE `blokkok`
  MODIFY `blk_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `blokkok`
--
ALTER TABLE `blokkok`
  ADD CONSTRAINT `blokkok_ibfk_1` FOREIGN KEY (`fhk_qr`) REFERENCES `felhasznalok` (`qr`);

--
-- Megkötések a táblához `rendelkezesek`
--
ALTER TABLE `rendelkezesek`
  ADD CONSTRAINT `rendelkezesek_ibfk_1` FOREIGN KEY (`fhk_qr`) REFERENCES `felhasznalok` (`qr`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `rendelkezesek_ibfk_2` FOREIGN KEY (`blk_id`) REFERENCES `blokkok` (`blk_id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
