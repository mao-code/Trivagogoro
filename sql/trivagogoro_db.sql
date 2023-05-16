-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- 主機： localhost
-- 產生時間： 2023 年 05 月 16 日 13:45
-- 伺服器版本： 10.4.21-MariaDB
-- PHP 版本： 7.4.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫: `trivagogoro_db`
--

-- --------------------------------------------------------

--
-- 資料表結構 `Archives`
--

CREATE TABLE `Archives` (
  `id` int(11) NOT NULL COMMENT 'PK',
  `foodWallPostId` int(11) NOT NULL,
  `userId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- 資料表結構 `Comment`
--

CREATE TABLE `Comment` (
  `id` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `foodWallPostId` int(11) NOT NULL,
  `content` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- 資料表結構 `Compose`
--

CREATE TABLE `Compose` (
  `id` int(11) NOT NULL,
  `foodMapId` int(11) NOT NULL,
  `favoriteId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- 資料表結構 `Favorite`
--

CREATE TABLE `Favorite` (
  `id` int(11) NOT NULL COMMENT 'PK',
  `userId` int(11) NOT NULL,
  `restaurantId` int(11) NOT NULL,
  `selfRating` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 傾印資料表的資料 `Favorite`
--

INSERT INTO `Favorite` (`id`, `userId`, `restaurantId`, `selfRating`) VALUES
(4, 4, 379, 5),
(5, 4, 380, 5),
(6, 4, 397, 5),
(7, 4, 390, 5),
(8, 4, 385, 5);

-- --------------------------------------------------------

--
-- 資料表結構 `Follows`
--

CREATE TABLE `Follows` (
  `id` int(11) NOT NULL COMMENT 'PK',
  `followerId` int(11) NOT NULL,
  `followingId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- 資料表結構 `FoodMap`
--

CREATE TABLE `FoodMap` (
  `id` int(11) NOT NULL COMMENT 'FK',
  `userId` int(11) NOT NULL,
  `name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- 資料表結構 `FoodWallPost`
--

CREATE TABLE `FoodWallPost` (
  `id` int(11) NOT NULL COMMENT 'PK',
  `description` varchar(200) NOT NULL,
  `title` varchar(100) NOT NULL,
  `archivedNum` int(11) NOT NULL DEFAULT 0,
  `type` varchar(50) NOT NULL COMMENT '1 for rest, 2 for foodmap',
  `sourceId` int(11) NOT NULL,
  `userId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 傾印資料表的資料 `FoodWallPost`
--

INSERT INTO `FoodWallPost` (`id`, `description`, `title`, `archivedNum`, `type`, `sourceId`, `userId`) VALUES
(2, 'I love it!', 'The Chips 信義店', 0, '1', 7, 4),
(3, 'Good choice!', '烤師傅烤肉飯', 0, '1', 4, 4),
(4, 'good tacos!', 'Macho Tacos 瑪丘墨式餅舖', 0, '1', 5, 4);

-- --------------------------------------------------------

--
-- 資料表結構 `Restaurant`
--

CREATE TABLE `Restaurant` (
  `id` int(11) NOT NULL COMMENT 'PK',
  `name` varchar(100) NOT NULL,
  `lng` float NOT NULL,
  `lat` float NOT NULL,
  `address` varchar(100) NOT NULL,
  `placeId` varchar(100) NOT NULL,
  `priceLevel` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 傾印資料表的資料 `Restaurant`
--

INSERT INTO `Restaurant` (`id`, `name`, `lng`, `lat`, `address`, `placeId`, `priceLevel`) VALUES
(368, '隨意鳥地方101觀景餐廳', 121.565, 25.0336, '信義區信義路五段7號85F', 'ChIJ1S3Z6barQjQRUnd4BwJ6NkY', 4),
(369, '豬跳舞小餐館', 121.556, 25.0395, '大安區光復南路290巷48號', 'ChIJif-sI8arQjQR8xPQsGhwaqQ', 2),
(370, '探索廚房', 121.568, 25.0381, '信義區松仁路38號', 'ChIJewvXULqrQjQR6zpjEk5Nj0E', 3),
(371, '欣葉日本料理信義店 | 謝師宴專案開跑，平日15人用餐招待2人！', 121.567, 25.0361, '信義區松壽路11號5樓', 'ChIJPxnPnrCrQjQRmEwUNrIViu0', 3),
(372, '泰市場 Spice Market', 121.566, 25.0395, '信義區松高路11號6F', 'ChIJuQeY1LurQjQRrnGuucsl6ls', 3),
(373, '太和殿鴛鴦麻辣火鍋', 121.556, 25.0333, '大安區信義路四段315號', 'ChIJ_z7E28urQjQRAWHwki-BPDE', 3),
(374, '勞瑞斯牛肋排餐廳', 121.568, 25.0396, '信義區松仁路28號6樓', 'ChIJjwfWf7CrQjQR8HEILkaBigk', 4),
(375, '彭園台北館', 121.573, 25.0412, '信義區忠孝東路五段297號5樓、6樓', 'ChIJTzI5aaOrQjQRBH36kxcPJnE', 0),
(376, '平價快炒', 121.554, 25.0403, '大安區忠孝東路四段216巷19弄10號', 'ChIJ8YzB5MWrQjQR0_qQu8vD7rI', 1),
(377, 'Woolloomooloo', 121.558, 25.0333, '信義區信義路四段379號', 'ChIJlxI4ismrQjQRnmAHnozDIug', 2),
(378, '陶板屋 台北光復南店', 121.557, 25.0396, 'B1, No. 286號, 光复南路, 大安区', 'ChIJT1w6Ay6qQjQRGGGPvH4mtwk', 2),
(379, '烤師傅烤肉飯', 121.569, 25.0425, '信義區永吉路30巷151弄2-1號', 'ChIJjWPIjLyrQjQRYisxunkQoKU', 1),
(380, 'Macho Tacos 瑪丘墨式餅舖', 121.554, 25.0404, '大安區延吉街126巷3號', 'ChIJk9KTC8arQjQRjnNY8f7CNhw', 2),
(381, 'Mo-Mo-Paradise', 121.568, 25.0353, '信義區松壽路22號2F', 'ChIJTW0EhbCrQjQRbXvETrjnOJI', 2),
(382, '馬辣頂級麻辣鴛鴦火鍋 信義店', 121.568, 25.0355, '信義區松壽路22號3樓', 'ChIJ9cRPhLCrQjQRqMgpsbFekQE', 2),
(383, 'The Kitchen Table 西餐廳', 121.566, 25.0406, '10樓, No. 10號, 忠孝東路五段, 信義區', 'ChIJwd1oV7mrQjQRyk2tnFtxTO4', 4),
(384, '丸亀製麵 新光三越台北信義A8店 烏龍麵餐廳', 121.567, 25.0384, '信義區松高路12號B2', 'ChIJoX9YNLqrQjQR-MWv7zDgkN8', 2),
(385, '馬友友印度廚房 Mayur Indian Kitchen, MIK-1', 121.562, 25.0368, '信義區基隆路一段350-5號', 'ChIJ2Tb8kberQjQR6VzLwafa4jI', 2),
(386, '陳根找茶', 121.566, 25.0284, '信義區莊敬路391巷7號', 'ChIJ21GKfrOrQjQRBOFpikvZPkQ', 2),
(387, '都一處(仁愛店)', 121.561, 25.0372, '信義區仁愛路四段506號', 'ChIJvctP8rerQjQRGfwXMTJiCt4', 3),
(388, 'Chaurau Sukiyaki Restaurant', 121.555, 25.0427, 'No. 26, Lane 131, Yanji Street, Da’an District', 'ChIJY2-nlMarQjQRS5idGUMiMHA', 2),
(389, 'Good Cho\'s', 121.562, 25.0312, 'No. 54號, Songqin Street, Xinyi District', 'ChIJUztrxbWrQjQRGsdUosiD8LQ', 2),
(390, 'The Chips 信義店', 121.566, 25.0353, '5 樓, No. 12號, Songshou Road, Xinyi District', 'ChIJHd7EPMarQjQRNjXqec_9Cys', 2),
(391, 'BELLINI Pasta Pasta 台北信義威秀店', 121.567, 25.0352, 'No. 20號, Songshou Road, Xinyi District', 'ChIJH3SPl7CrQjQRaflQ7dF8dek', 2),
(392, 'Don Show Buri', 121.567, 25.0353, '2樓, No. 20號, Songshou Road, Xinyi District', 'ChIJH3SPl7CrQjQRyTODuYSxOy0', 2),
(393, 'My Humble House', 121.568, 25.0382, '2F, No. 38號, Songren Road, Xinyi District', 'ChIJewvXULqrQjQRGoqJRNRDAP0', 4),
(394, 'Chao Ping Ji', 121.567, 25.0397, '6F, No. 19號, Songgao Road, Xinyi District', 'ChIJDY5gdMWrQjQRI8UAUhM4tkQ', 3),
(395, 'Shin Yeh Mitsukoshi Xinyi Place A9 Restaurant', 121.567, 25.0367, '8樓, No. 9號, Songshou Road, Xinyi District', 'ChIJrSZypLCrQjQRdNWuZqKdwqM', 3),
(396, 'Ramen Kagetsu Arashi', 121.565, 25.0408, '號B2, No. 8, Section 5 of Zhongxiao East RoadSection 5 of Zhongxiao East Road, Xinyi District', 'ChIJRWWPl7CrQjQRBnkit6tZTBM', 2),
(397, 'Toast Chat', 121.556, 25.0394, 'No. 58, Lane 290, Guangfu South Road, Da’an District', 'ChIJASafGMarQjQRtDMLDBSxPL4', 2),
(398, 'YEN Taipei', 121.566, 25.0406, '31樓, No. 10號, Section 5 of Zhongxiao East RoadSection 5 of Zhongxiao East Road, Xinyi District', 'ChIJwd1oV7mrQjQR2ozp9uHcBGw', 3),
(399, 'Very Thai Restaurant', 121.568, 25.0355, 'No. 22號, Songshou Road, Xinyi District', 'ChIJj9lPhLCrQjQRJeduZ4avFRU', 2),
(400, '滇味廚房', 121.569, 25.0414, '號1樓, No. 2, Alley 178, Lane 30, Yongji Road, Xinyi District', 'ChIJkWE9hLyrQjQRQ97gEXyZCDo', 1),
(401, 'Mitsui cuisine M', 121.566, 25.0386, 'No. 1, Songzhi Road, Xinyi District', 'ChIJzeoLKbqrQjQRESSI-FKAiAM', 3),
(402, 'Fushun House Restaurant', 121.558, 25.0355, '1樓, No. 2號, Lane 443, Guangfu South Road, Xinyi District', 'ChIJC6NEscmrQjQRQl2MnHQAtB0', 2),
(403, '武鼎越豐', 121.569, 25.0418, 'No. 2號, Alley 167, Lane 30, Yongji Road, Xinyi District', 'ChIJLdVhiLyrQjQRZF22uh1818M', 2),
(404, 'LEANG BAN JIA Korean Barbecue', 121.567, 25.0364, '6樓, No. 9, Songshou Road, Xinyi District', 'ChIJKSJypLCrQjQR4zs1EAIqa1c', 3),
(405, 'JOYCE EAST', 121.569, 25.0324, 'No. 128號, Section 5, Xinyi Road, Xinyi District', 'ChIJk-t4sLGrQjQRG0XNTYo1jgg', 0),
(406, '南村小吃店', 121.568, 25.0285, 'No. 14, Alley 8, Lane 423, Zhuangjing Road, Xinyi District', 'ChIJ_7LKp7OrQjQRZksUCxcoeSM', 2),
(407, 'Sung Kitchen', 121.565, 25.0418, 'No. 14, Lane 15, Section 5, Zhongxiao East Road, Xinyi District', 'ChIJw76WVbmrQjQRPc_t8gM-6x8', 2);

-- --------------------------------------------------------

--
-- 資料表結構 `User`
--

CREATE TABLE `User` (
  `id` int(11) NOT NULL COMMENT 'PK',
  `name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 傾印資料表的資料 `User`
--

INSERT INTO `User` (`id`, `name`) VALUES
(4, 'Test4'),
(5, 'TT'),
(6, 'test'),
(7, 'test'),
(8, 'MaoXun');

-- --------------------------------------------------------

--
-- 資料表結構 `UserCredential`
--

CREATE TABLE `UserCredential` (
  `id` int(11) NOT NULL COMMENT 'PK',
  `userId` int(11) NOT NULL,
  `account` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `salt` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 傾印資料表的資料 `UserCredential`
--

INSERT INTO `UserCredential` (`id`, `userId`, `account`, `password`, `salt`) VALUES
(2, 4, 'test', '$2a$12$Cc2ePkB4dvlXjz5bE6XQjul0kx/kWDNO5s7ZU6G0JGorZzVPRY4Pu', '$2a$12$Cc2ePkB4dvlXjz5bE6XQju'),
(3, 5, 'tt', '$2a$12$AkZlf4QBJRdGbgFOFrmqQO67rTVhKDyLbH5xOOmxsMh3veagVYqUa', '$2a$12$AkZlf4QBJRdGbgFOFrmqQO'),
(4, 8, 'a22908352@gmail.com', '$2a$12$X4vXgalkoKCkOCvL0EXvk.Rni2aQYUpAYcAUvJoBXPdON0NOcLXra', '$2a$12$X4vXgalkoKCkOCvL0EXvk.');

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `Archives`
--
ALTER TABLE `Archives`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ufk` (`userId`),
  ADD KEY `foodWallPostFK` (`foodWallPostId`);

--
-- 資料表索引 `Favorite`
--
ALTER TABLE `Favorite`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userFK` (`userId`),
  ADD KEY `restaurantFK` (`restaurantId`);

--
-- 資料表索引 `Follows`
--
ALTER TABLE `Follows`
  ADD PRIMARY KEY (`id`),
  ADD KEY `followerFK` (`followerId`),
  ADD KEY `followingFK` (`followingId`);

--
-- 資料表索引 `FoodMap`
--
ALTER TABLE `FoodMap`
  ADD PRIMARY KEY (`id`);

--
-- 資料表索引 `FoodWallPost`
--
ALTER TABLE `FoodWallPost`
  ADD PRIMARY KEY (`id`),
  ADD KEY `post_userFK` (`userId`);

--
-- 資料表索引 `Restaurant`
--
ALTER TABLE `Restaurant`
  ADD PRIMARY KEY (`id`);

--
-- 資料表索引 `User`
--
ALTER TABLE `User`
  ADD PRIMARY KEY (`id`);

--
-- 資料表索引 `UserCredential`
--
ALTER TABLE `UserCredential`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `password` (`password`),
  ADD UNIQUE KEY `account` (`account`),
  ADD KEY `userIdFK` (`userId`);

--
-- 在傾印的資料表使用自動遞增(AUTO_INCREMENT)
--

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `Archives`
--
ALTER TABLE `Archives`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PK';

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `Favorite`
--
ALTER TABLE `Favorite`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PK', AUTO_INCREMENT=9;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `Follows`
--
ALTER TABLE `Follows`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PK';

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `FoodMap`
--
ALTER TABLE `FoodMap`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'FK';

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `FoodWallPost`
--
ALTER TABLE `FoodWallPost`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PK', AUTO_INCREMENT=5;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `Restaurant`
--
ALTER TABLE `Restaurant`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PK', AUTO_INCREMENT=408;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `User`
--
ALTER TABLE `User`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PK', AUTO_INCREMENT=9;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `UserCredential`
--
ALTER TABLE `UserCredential`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PK', AUTO_INCREMENT=5;

--
-- 已傾印資料表的限制式
--

--
-- 資料表的限制式 `Archives`
--
ALTER TABLE `Archives`
  ADD CONSTRAINT `foodWallPostFK` FOREIGN KEY (`foodWallPostId`) REFERENCES `FoodWallPost` (`id`),
  ADD CONSTRAINT `ufk` FOREIGN KEY (`userId`) REFERENCES `User` (`id`);

--
-- 資料表的限制式 `Favorite`
--
ALTER TABLE `Favorite`
  ADD CONSTRAINT `restaurantFK` FOREIGN KEY (`restaurantId`) REFERENCES `Restaurant` (`id`),
  ADD CONSTRAINT `userFK` FOREIGN KEY (`userId`) REFERENCES `User` (`id`);

--
-- 資料表的限制式 `Follows`
--
ALTER TABLE `Follows`
  ADD CONSTRAINT `followerFK` FOREIGN KEY (`followerId`) REFERENCES `User` (`id`),
  ADD CONSTRAINT `followingFK` FOREIGN KEY (`followingId`) REFERENCES `User` (`id`);

--
-- 資料表的限制式 `FoodWallPost`
--
ALTER TABLE `FoodWallPost`
  ADD CONSTRAINT `post_userFK` FOREIGN KEY (`userId`) REFERENCES `User` (`id`);

--
-- 資料表的限制式 `UserCredential`
--
ALTER TABLE `UserCredential`
  ADD CONSTRAINT `userIdFK` FOREIGN KEY (`userId`) REFERENCES `User` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
