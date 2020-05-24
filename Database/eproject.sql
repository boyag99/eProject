-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 23, 2020 at 11:52 AM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.4.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `eproject`
--

-- --------------------------------------------------------

--
-- Table structure for table `abouts`
--

CREATE TABLE `abouts` (
  `AboutId` int(11) NOT NULL,
  `Image` longtext DEFAULT NULL,
  `Name` longtext DEFAULT NULL,
  `Description` longtext DEFAULT NULL,
  `Slogan` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `abouts`
--

INSERT INTO `abouts` (`AboutId`, `Image`, `Name`, `Description`, `Slogan`) VALUES
(1, 'nguyennhathoangha.jpg', 'Nguyen Nhat Hoang Ha', 'Member', '“if it were easy everybody would do it.'),
(2, 'trinhhaohiep.jpg', 'Trinh Hao Hiep', 'Member', '“I can actually do this!'),
(3, 'phankhactrieu.jpg', 'Phan Khac Trieu', 'Member', '“Failure teaches me.'),
(4, 'tranquangtuyen.jpg', 'Tran Quang Tuyen', 'Member', '“My work matters.'),
(5, 'todaoviethoang.jpg', 'To Dao Viet Hoang', 'Leader', ' “Other people’s success inspires me');

-- --------------------------------------------------------

--
-- Table structure for table `addresses`
--

CREATE TABLE `addresses` (
  `AddressId` char(36) NOT NULL,
  `StreetAddress` varchar(256) DEFAULT NULL,
  `PostalCode` varchar(256) DEFAULT NULL,
  `City` varchar(256) DEFAULT NULL,
  `County` varchar(256) DEFAULT NULL,
  `Country` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `addresses`
--

INSERT INTO `addresses` (`AddressId`, `StreetAddress`, `PostalCode`, `City`, `County`, `Country`) VALUES
('08d7fef7-3df4-4961-8368-76a975c01deb', '123 Pham Hung', '80000', 'Ho Chi Minh', 'Binh Chanh', 'Vietnam'),
('08d7fef7-3e36-4ada-80b5-4fcdeb076d84', '123 Pham Hung', '80000', 'Ho Chi Minh', 'Binh Chanh', 'Vietnam'),
('08d7fef7-3e46-45e8-8bcb-43524be7c875', '123 Pham Hung', '80000', 'Ho Chi Minh', 'Binh Chanh', 'Vietnam'),
('08d7fef7-a360-4585-87e7-1c1707eaac87', '21321', '213', '123', '123', 'India'),
('08d7fefb-3b56-4939-8470-ec05a7a58b59', NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `auctionhistories`
--

CREATE TABLE `auctionhistories` (
  `AuctionHistoryId` int(11) NOT NULL,
  `Bid` double NOT NULL,
  `UserId` varchar(255) DEFAULT NULL,
  `ProductId` int(11) NOT NULL,
  `Created_At` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `auctionhistories`
--

INSERT INTO `auctionhistories` (`AuctionHistoryId`, `Bid`, `UserId`, `ProductId`, `Created_At`) VALUES
(1, 60000, '6e52da13-55d9-44a3-b243-6beec74f0a1c', 8, '2020-05-23 16:32:44.552733'),
(2, 60000, '6e52da13-55d9-44a3-b243-6beec74f0a1c', 9, '2020-05-23 16:33:40.042068'),
(3, 100000, '6e52da13-55d9-44a3-b243-6beec74f0a1c', 8, '2020-05-23 16:41:51.148962');

-- --------------------------------------------------------

--
-- Table structure for table `blogs`
--

CREATE TABLE `blogs` (
  `BlogId` int(11) NOT NULL,
  `Title` longtext DEFAULT NULL,
  `Photo` longtext DEFAULT NULL,
  `PostedDate` datetime(6) NOT NULL,
  `UserId` varchar(255) DEFAULT NULL,
  `Description` longtext NOT NULL,
  `Content` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `blogs`
--

INSERT INTO `blogs` (`BlogId`, `Title`, `Photo`, `PostedDate`, `UserId`, `Description`, `Content`) VALUES
(1, 'DIFFERENCE BETWEEN TEMPERA', 'blog1.jpg', '2020-05-23 15:56:55.519928', '0f1872c3-ce03-406d-8334-76970bb4c5e4', 'Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition...', 'Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition it is opposed to oil painting, with its lower, dimmed and less shiny...'),
(2, 'USE OF VARNISH IN PAINTINGS', 'blog2.jpg', '2020-05-23 15:56:55.520678', '0f1872c3-ce03-406d-8334-76970bb4c5e4', 'Originally, the varnish used for the paintings was a resin dissolved in oil. It was ...', 'Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition it is opposed to oil painting, with its lower, dimmed and less shiny...'),
(3, 'USE OF GOLD IN PAINTING', 'blog3.jpg', '2020-05-23 15:56:55.520680', '0f1872c3-ce03-406d-8334-76970bb4c5e4', 'The use of gold in painting is linked to the recognition of the role of art of ...', 'Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition it is opposed to oil painting, with its lower, dimmed and less shiny...'),
(4, 'THE ART OF PAINTING AND DREAMS', 'blog4.jpg', '2020-05-23 15:56:55.520681', '0f1872c3-ce03-406d-8334-76970bb4c5e4', 'It is impressive that painting, which is the most sensual of the arts, is also...', 'It is impressive that painting, which is the most sensual of the arts, is also the most metaphysical. For color, of course, but not only for that'),
(5, 'WHY COPIES, REPRODUCTIONS AND FAKES PAINTINGS?', 'blog5.jpg', '2020-05-23 15:56:55.520682', '0f1872c3-ce03-406d-8334-76970bb4c5e4', 'It usually happens that the original paintings produced by the artist could ...', 'WHY COPIES, REPRODUCTIONS AND FAKES PAINTINGS?');

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `CategoryId` int(11) NOT NULL,
  `CategoryName` varchar(255) DEFAULT NULL,
  `Status` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`CategoryId`, `CategoryName`, `Status`) VALUES
(1, 'Painting', 1),
(2, 'Photography', 1),
(3, 'Sculpture', 1);

-- --------------------------------------------------------

--
-- Table structure for table `contactadmins`
--

CREATE TABLE `contactadmins` (
  `ContactAdminId` int(11) NOT NULL,
  `Message` longtext DEFAULT NULL,
  `Status` int(11) NOT NULL,
  `UserId` varchar(255) DEFAULT NULL,
  `Created_At` datetime(6) NOT NULL,
  `Updated_At` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `contacts`
--

CREATE TABLE `contacts` (
  `Id` int(11) NOT NULL,
  `Address` longtext DEFAULT NULL,
  `Telephone` double NOT NULL,
  `Fax` double NOT NULL,
  `Email` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `contacts`
--

INSERT INTO `contacts` (`Id`, `Address`, `Telephone`, `Fax`, `Email`) VALUES
(1, NULL, 0, 0, 'llf80083@bcaoo.com');

-- --------------------------------------------------------

--
-- Table structure for table `gateways`
--

CREATE TABLE `gateways` (
  `GatewayId` int(11) NOT NULL,
  `BankName` longtext DEFAULT NULL,
  `AccountNumber` longtext DEFAULT NULL,
  `AccountName` longtext DEFAULT NULL,
  `Branch` longtext DEFAULT NULL,
  `Message` longtext DEFAULT NULL,
  `Logo` longtext DEFAULT NULL,
  `Status` tinyint(1) NOT NULL,
  `Created_At` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `generalsettings`
--

CREATE TABLE `generalsettings` (
  `GeneralId` int(11) NOT NULL,
  `RegistrationArtistCost` double NOT NULL,
  `ShippingCost` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `generalsettings`
--

INSERT INTO `generalsettings` (`GeneralId`, `RegistrationArtistCost`, `ShippingCost`) VALUES
(1, 5, 5);

-- --------------------------------------------------------

--
-- Table structure for table `invoicedetails`
--

CREATE TABLE `invoicedetails` (
  `InvoiceId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `Price` double NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Note` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `invoicedetails`
--

INSERT INTO `invoicedetails` (`InvoiceId`, `ProductId`, `Price`, `Quantity`, `Note`) VALUES
(1, 1, 3500, 1, NULL),
(2, 1, 3500, 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `invoices`
--

CREATE TABLE `invoices` (
  `Id` int(11) NOT NULL,
  `Name` longtext DEFAULT NULL,
  `Created` datetime(6) NOT NULL,
  `Status` int(11) NOT NULL,
  `SellerId` varchar(255) DEFAULT NULL,
  `BuyerId` varchar(255) DEFAULT NULL,
  `ShippingAddressId` int(11) DEFAULT NULL,
  `UserId` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `invoices`
--

INSERT INTO `invoices` (`Id`, `Name`, `Created`, `Status`, `SellerId`, `BuyerId`, `ShippingAddressId`, `UserId`) VALUES
(1, 'Invoice Online', '2020-05-23 16:02:21.996083', 1, '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', '440ad23e-97e5-40fa-8d58-96c123600e27', NULL, NULL),
(2, 'Invoice Online', '2020-05-23 16:26:20.679325', 1, '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', '6e52da13-55d9-44a3-b243-6beec74f0a1c', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `photos`
--

CREATE TABLE `photos` (
  `PhotoId` int(11) NOT NULL,
  `Name` longtext DEFAULT NULL,
  `ProductId` int(11) NOT NULL,
  `Status` tinyint(1) NOT NULL,
  `Featured` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `photos`
--

INSERT INTO `photos` (`PhotoId`, `Name`, `ProductId`, `Status`, `Featured`) VALUES
(1, 'AConstant.png', 1, 1, 1),
(2, 'ForeseeingNothing.png', 2, 1, 1),
(3, 'IdleHands.png', 3, 1, 1),
(4, 'IfHeWasntHandsome.png', 4, 1, 1),
(5, 'InSearchofWhatOnceWas.png', 5, 1, 1),
(6, 'Badain.png', 6, 1, 1),
(7, 'Blafellsa.png', 7, 1, 1),
(8, 'Fleswichbay.png', 8, 1, 1),
(9, 'Haifoss.png', 9, 1, 1),
(10, 'KMuffarfjoll.png', 10, 1, 1),
(11, 'LastCall.png', 11, 1, 1),
(12, 'MerryWoodsman.png', 12, 1, 1),
(13, 'OhDear.png', 13, 1, 1),
(14, 'SpeedDating.png', 14, 1, 1),
(15, 'WeCanDream.png', 15, 1, 1),
(16, 'painting4.png', 16, 1, 1),
(17, 'painting14.png', 17, 1, 1),
(18, 'KMuffarfjoll.png', 18, 1, 1),
(19, 'painting4.png', 19, 1, 1),
(20, 'Fleswichbay.png', 20, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `porfolios`
--

CREATE TABLE `porfolios` (
  `PorfolioId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `UserId` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductId` int(11) NOT NULL,
  `Name` longtext DEFAULT NULL,
  `Price` double NOT NULL,
  `SalePrice` double NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Hot` int(11) NOT NULL,
  `FromDate` datetime(6) NOT NULL,
  `ToDate` datetime(6) NOT NULL,
  `UserId` varchar(255) DEFAULT NULL,
  `CategoryId` int(11) NOT NULL,
  `Description` longtext DEFAULT NULL,
  `Status` tinyint(1) NOT NULL,
  `Featured` tinyint(1) NOT NULL,
  `Auction` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ProductId`, `Name`, `Price`, `SalePrice`, `Quantity`, `Hot`, `FromDate`, `ToDate`, `UserId`, `CategoryId`, `Description`, `Status`, `Featured`, `Auction`) VALUES
(1, 'A Constant', 3500, 100, 1, 0, '2020-05-23 15:56:55.299882', '2020-05-23 15:56:55.298562', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 1, 'Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.', 1, 1, 0),
(2, 'Foreseeing Nothing', 12500, 100, 1, 0, '2020-05-23 15:56:55.300898', '2020-05-23 15:56:55.300895', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 1, 'Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.', 1, 1, 0),
(3, 'Idle Hands', 12500, 100, 1, 0, '2020-05-23 15:56:55.300900', '2020-05-23 15:56:55.300899', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 1, 'Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.', 1, 1, 0),
(4, 'If He Wasn\'t', 12500, 0, 1, 0, '2020-05-23 15:56:55.300901', '2020-05-23 15:56:55.300901', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 1, 'Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.', 1, 1, 0),
(5, 'What Once Was', 12500, 0, 1, 0, '2020-05-23 15:56:55.300903', '2020-05-23 15:56:55.300902', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 1, 'Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.', 1, 1, 0),
(6, 'Badain', 30000, 0, 1, 0, '2020-05-23 15:56:55.300906', '2020-05-23 15:56:55.300905', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 2, 'American photographer Aaron Young won first place in the Landscape Photographer of the Year category for four photos taken in the Badain Jaran desert in Iceland.', 1, 1, 0),
(7, 'Bláfellsá', 30000, 30000, 1, 0, '2020-05-23 15:56:55.300908', '2020-05-23 15:56:55.300907', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 2, 'The International Landscape Photographer of the Year contest has just announced its 2019 winners, and their pics are the perfect reminders of just how diverse and beautiful our Mother Earth really is.', 1, 1, 1),
(8, 'Fleswich bay', 30000, 100000, 1, 12, '2020-05-23 15:56:55.300910', '2020-05-23 15:56:55.300909', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 2, 'The International Landscape Photographer of the Year contest has just announced its 2019 winners, and their pics are the perfect reminders of just how diverse and beautiful our Mother Earth really is.', 1, 1, 1),
(9, 'Haifoss', 30000, 60000, 1, 5, '2020-05-23 15:56:55.300911', '2020-05-30 15:56:55.300911', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 2, 'Ershov believes that a picture is beautiful when hung on a wall, so he focuses on creating special photographs like large-sized wall paintings.', 1, 1, 1),
(10, 'K Muffarfjoll', 30000, 30000, 1, 0, '2020-05-23 15:56:55.300914', '2020-05-23 15:56:55.300913', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 2, 'This Russia. He has been to Iceland 15 times and took 10 years to complete the first book of his career.', 1, 1, 1),
(11, 'Last Call', 1800, 1800, 1, 0, '2020-05-23 15:56:55.300915', '2020-05-23 15:56:55.300915', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 3, 'Assorted plastics, acrylic paint', 1, 1, 1),
(12, 'Merry Woodsman', 1500, 1500, 1, 0, '2020-05-23 15:56:55.300917', '2020-05-23 15:56:55.300916', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 3, 'Assorted plastics, acrylic paint', 1, 1, 1),
(13, 'Oh Dear', 1500, 1500, 1, 0, '2020-05-23 15:56:55.300919', '2020-05-23 15:56:55.300918', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 3, 'Assorted plastics, acrylic paint', 1, 0, 1),
(14, 'Speed Dating', 1500, 1500, 1, 0, '2020-05-23 15:56:55.300921', '2020-05-23 15:56:55.300920', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 3, 'Assorted plastics, acrylic paint', 1, 0, 1),
(15, 'We Can Dream', 1500, 1500, 1, 0, '2020-05-23 15:56:55.300922', '2020-05-23 15:56:55.300921', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 3, 'Assorted plastics, acrylic paint', 1, 0, 1),
(16, 'Beauty and beast', 1500, 200, 1, 0, '2020-05-23 15:56:55.300924', '2020-05-23 15:56:55.300923', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 3, 'Assorted plastics, acrylic paint', 1, 0, 0),
(17, 'Lion King', 1500, 200, 1, 0, '2020-05-23 15:56:55.300926', '2020-05-23 15:56:55.300924', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 1, 'Assorted plastics, acrylic paint', 1, 0, 0),
(18, 'Private', 1500, 200, 1, 0, '2020-05-23 15:56:55.300928', '2020-05-23 15:56:55.300928', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 2, 'Assorted plastics, acrylic paint', 1, 0, 0),
(19, 'One and many', 1500, 200, 1, 0, '2020-05-23 15:56:55.300930', '2020-05-23 15:56:55.300929', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 1, 'Assorted plastics, acrylic paint', 1, 1, 0),
(20, 'Alpha', 1500, 200, 1, 0, '2020-05-23 15:56:55.300962', '2020-05-23 15:56:55.300930', '4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 2, 'Assorted plastics, acrylic paint', 1, 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `reviews`
--

CREATE TABLE `reviews` (
  `ReviewId` int(11) NOT NULL,
  `Message` longtext DEFAULT NULL,
  `Status` int(11) NOT NULL,
  `UserId` varchar(255) DEFAULT NULL,
  `ProductId` int(11) NOT NULL,
  `Created_At` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `roleclaims`
--

CREATE TABLE `roleclaims` (
  `Id` int(11) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('6902ee5d-946a-4530-a6d1-cb628d83eb39', 'Artist', 'ARTIST', '14bb9790-af25-404b-9e75-c2697656cf38'),
('7bf2d9ac-0b46-4262-89d5-92ce930a9eed', 'Admin', 'ADMIN', '94e6d563-a31e-421b-aa43-3591a5d36ee8'),
('df6410b7-844f-4792-86ac-f058fe8badb1', 'Customer', 'CUSTOMER', '2f204282-f6b6-4ced-89c5-8c55c12f7516');

-- --------------------------------------------------------

--
-- Table structure for table `shippingaddresses`
--

CREATE TABLE `shippingaddresses` (
  `ShippingAddressId` int(11) NOT NULL,
  `FirstName` longtext DEFAULT NULL,
  `LastName` longtext DEFAULT NULL,
  `StreetAddress` longtext DEFAULT NULL,
  `Country` longtext DEFAULT NULL,
  `City` longtext DEFAULT NULL,
  `PostalCode` longtext DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `Address` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `slideshows`
--

CREATE TABLE `slideshows` (
  `Id` int(11) NOT NULL,
  `Name` longtext DEFAULT NULL,
  `Status` tinyint(1) NOT NULL,
  `Title` longtext DEFAULT NULL,
  `Description` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `slideshows`
--

INSERT INTO `slideshows` (`Id`, `Name`, `Status`, `Title`, `Description`) VALUES
(1, 'banner1.jpg', 1, 'Title SlideShow', '50%'),
(2, 'banner2.jpg', 1, 'Title SlideShow', '10%'),
(3, 'banner3.jpg', 1, 'Title SlideShow', '20%');

-- --------------------------------------------------------

--
-- Table structure for table `userclaims`
--

CREATE TABLE `userclaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `userlogins`
--

CREATE TABLE `userlogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `userroles`
--

CREATE TABLE `userroles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `userroles`
--

INSERT INTO `userroles` (`UserId`, `RoleId`) VALUES
('0f1872c3-ce03-406d-8334-76970bb4c5e4', '7bf2d9ac-0b46-4262-89d5-92ce930a9eed'),
('440ad23e-97e5-40fa-8d58-96c123600e27', '6902ee5d-946a-4530-a6d1-cb628d83eb39'),
('4bfaaf50-52a1-4aff-9c07-66bb62838fe4', '6902ee5d-946a-4530-a6d1-cb628d83eb39'),
('6e52da13-55d9-44a3-b243-6beec74f0a1c', 'df6410b7-844f-4792-86ac-f058fe8badb1'),
('e48af6a0-987e-4345-92a5-030929186964', 'df6410b7-844f-4792-86ac-f058fe8badb1');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `Gender` int(11) NOT NULL,
  `DateOfBirthDay` datetime(6) NOT NULL,
  `ProfileImage` varchar(255) DEFAULT NULL,
  `Biography` varchar(255) DEFAULT NULL,
  `Exhibition` varchar(255) DEFAULT NULL,
  `Balance` double NOT NULL,
  `AddressId` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`, `FirstName`, `LastName`, `Gender`, `DateOfBirthDay`, `ProfileImage`, `Biography`, `Exhibition`, `Balance`, `AddressId`) VALUES
('0f1872c3-ce03-406d-8334-76970bb4c5e4', 'admin@gmail.com', 'ADMIN@GMAIL.COM', 'admin@gmail.com', 'ADMIN@GMAIL.COM', 1, 'AQAAAAEAACcQAAAAEOIE6nkm7PJdAYSfS3kXEbQty5nJVjOfEmsoKOde+U/mQ7wTexEH47Zgf8pOJ5T4ng==', 'NOE22FIGHAWQC25HS6PUO6U7D7VS6O5X', 'f8e60385-ac5e-4f46-a84a-5952ca687abd', '0909090909', 0, 0, NULL, 1, 0, 'Trinh Hao', 'Hiep', 0, '1999-07-26 00:00:00.000000', NULL, NULL, NULL, 0, '08d7fef7-3e46-45e8-8bcb-43524be7c875'),
('440ad23e-97e5-40fa-8d58-96c123600e27', 'llf80083@bcaoo.com', 'LLF80083@BCAOO.COM', 'llf80083@bcaoo.com', 'LLF80083@BCAOO.COM', 1, 'AQAAAAEAACcQAAAAENoIWKA4LkdvDXDvUWSmz6DKkac6mJjdWfKUKm4U69RBlkrvLCAwNOMZBhAlICFzIw==', 'YJNDJTTXBPO2XYWQFNXG7QHMQKTWBFFS', 'd57742b2-a52b-4884-84ac-ebd77a35d731', '0976718316', 0, 0, NULL, 1, 0, 'Ma', 'Ara', 0, '2020-05-23 15:59:44.510000', NULL, '123', '123', 9995, '08d7fef7-a360-4585-87e7-1c1707eaac87'),
('4bfaaf50-52a1-4aff-9c07-66bb62838fe4', 'artist@gmail.com', 'ARTIST@GMAIL.COM', 'artist@gmail.com', 'ARTIST@GMAIL.COM', 1, 'AQAAAAEAACcQAAAAEIm3gjO4lSvFYfutHB14z4YSyxEhc9sseDvEiv/ghIKk9jzN/DHE6ySYfYPMNTTkyQ==', 'OZQJIW6AR6UDO6X3WMIUKLK37VXH6N2S', 'db66b6da-2065-450f-80e5-8a852f074805', '0909090909', 0, 0, NULL, 1, 0, 'Tran Quang', 'Tuyen', 0, '1999-07-26 00:00:00.000000', NULL, NULL, NULL, 0, '08d7fef7-3e36-4ada-80b5-4fcdeb076d84'),
('6e52da13-55d9-44a3-b243-6beec74f0a1c', 'tuyentran8121997@gmail.com', 'TUYENTRAN8121997@GMAIL.COM', 'tuyentran8121997@gmail.com', 'TUYENTRAN8121997@GMAIL.COM', 1, 'AQAAAAEAACcQAAAAEOteLrIpMZkAj3MkXbvAAMR8ih3eRYtsj4Qo0NQiLee3oQJEUC0HBcy43r8Y2HxFGQ==', 'FSTZ2RWG6APN2AGZMP5YCIW3ZKHJPCDS', '31be36bb-f5f1-4431-8bd4-e739433ecdf6', '0976718316', 0, 0, NULL, 1, 0, 'Tuyen', 'Tran Quang', 0, '2020-05-23 16:25:28.136000', NULL, NULL, NULL, 0, '08d7fefb-3b56-4939-8470-ec05a7a58b59'),
('e48af6a0-987e-4345-92a5-030929186964', 'customer@gmail.com', 'CUSTOMER@GMAIL.COM', 'customer@gmail.com', 'CUSTOMER@GMAIL.COM', 1, 'AQAAAAEAACcQAAAAEBuWt3ug7Fa9gy7/sx3mhfGBUwJz1ICsXgjt36Q64bNHi42Ipn3doBMMffe5Mxk8xg==', '35EVK5OLMG3NWOC3LJYRA2KEJKYQSHSL', 'ef8b6be8-95bd-4faa-8b57-444a105e26eb', '0909090909', 0, 0, NULL, 1, 0, 'To Dao', 'Viet Hoang', 0, '1999-07-26 00:00:00.000000', NULL, NULL, NULL, 0, '08d7fef7-3df4-4961-8368-76a975c01deb');

-- --------------------------------------------------------

--
-- Table structure for table `usertokens`
--

CREATE TABLE `usertokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `warehouseaddresses`
--

CREATE TABLE `warehouseaddresses` (
  `WareHouseId` int(11) NOT NULL,
  `CompanyName` longtext DEFAULT NULL,
  `StreetAddress` longtext DEFAULT NULL,
  `City` longtext DEFAULT NULL,
  `Country` longtext DEFAULT NULL,
  `PostalCode` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `Email` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `warehouseaddresses`
--

INSERT INTO `warehouseaddresses` (`WareHouseId`, `CompanyName`, `StreetAddress`, `City`, `Country`, `PostalCode`, `PhoneNumber`, `Email`) VALUES
(1, 'Heaven Art', '590 CMT8, District 3', 'HCMC', 'Vietnam', '70000', '0909123456', 'admin@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `wishlists`
--

CREATE TABLE `wishlists` (
  `WishListId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `UserId` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `abouts`
--
ALTER TABLE `abouts`
  ADD PRIMARY KEY (`AboutId`);

--
-- Indexes for table `addresses`
--
ALTER TABLE `addresses`
  ADD PRIMARY KEY (`AddressId`);

--
-- Indexes for table `auctionhistories`
--
ALTER TABLE `auctionhistories`
  ADD PRIMARY KEY (`AuctionHistoryId`),
  ADD KEY `IX_AuctionHistories_ProductId` (`ProductId`),
  ADD KEY `IX_AuctionHistories_UserId` (`UserId`);

--
-- Indexes for table `blogs`
--
ALTER TABLE `blogs`
  ADD PRIMARY KEY (`BlogId`),
  ADD KEY `IX_Blogs_UserId` (`UserId`);

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`CategoryId`);

--
-- Indexes for table `contactadmins`
--
ALTER TABLE `contactadmins`
  ADD PRIMARY KEY (`ContactAdminId`),
  ADD KEY `IX_ContactAdmins_UserId` (`UserId`);

--
-- Indexes for table `contacts`
--
ALTER TABLE `contacts`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `gateways`
--
ALTER TABLE `gateways`
  ADD PRIMARY KEY (`GatewayId`);

--
-- Indexes for table `generalsettings`
--
ALTER TABLE `generalsettings`
  ADD PRIMARY KEY (`GeneralId`);

--
-- Indexes for table `invoicedetails`
--
ALTER TABLE `invoicedetails`
  ADD PRIMARY KEY (`InvoiceId`,`ProductId`),
  ADD KEY `IX_InvoiceDetails_ProductId` (`ProductId`);

--
-- Indexes for table `invoices`
--
ALTER TABLE `invoices`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Invoices_ShippingAddressId` (`ShippingAddressId`),
  ADD KEY `IX_Invoices_UserId` (`UserId`);

--
-- Indexes for table `photos`
--
ALTER TABLE `photos`
  ADD PRIMARY KEY (`PhotoId`),
  ADD KEY `IX_Photos_ProductId` (`ProductId`);

--
-- Indexes for table `porfolios`
--
ALTER TABLE `porfolios`
  ADD PRIMARY KEY (`PorfolioId`),
  ADD KEY `IX_Porfolios_ProductId` (`ProductId`),
  ADD KEY `IX_Porfolios_UserId` (`UserId`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductId`),
  ADD KEY `IX_Products_CategoryId` (`CategoryId`),
  ADD KEY `IX_Products_UserId` (`UserId`);

--
-- Indexes for table `reviews`
--
ALTER TABLE `reviews`
  ADD PRIMARY KEY (`ReviewId`),
  ADD KEY `IX_Reviews_ProductId` (`ProductId`),
  ADD KEY `IX_Reviews_UserId` (`UserId`);

--
-- Indexes for table `roleclaims`
--
ALTER TABLE `roleclaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_RoleClaims_RoleId` (`RoleId`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Indexes for table `shippingaddresses`
--
ALTER TABLE `shippingaddresses`
  ADD PRIMARY KEY (`ShippingAddressId`);

--
-- Indexes for table `slideshows`
--
ALTER TABLE `slideshows`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `userclaims`
--
ALTER TABLE `userclaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_UserClaims_UserId` (`UserId`);

--
-- Indexes for table `userlogins`
--
ALTER TABLE `userlogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_UserLogins_UserId` (`UserId`);

--
-- Indexes for table `userroles`
--
ALTER TABLE `userroles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_UserRoles_RoleId` (`RoleId`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `IX_Users_AddressId` (`AddressId`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Indexes for table `usertokens`
--
ALTER TABLE `usertokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Indexes for table `warehouseaddresses`
--
ALTER TABLE `warehouseaddresses`
  ADD PRIMARY KEY (`WareHouseId`);

--
-- Indexes for table `wishlists`
--
ALTER TABLE `wishlists`
  ADD PRIMARY KEY (`WishListId`),
  ADD KEY `IX_WishLists_ProductId` (`ProductId`),
  ADD KEY `IX_WishLists_UserId` (`UserId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `abouts`
--
ALTER TABLE `abouts`
  MODIFY `AboutId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `auctionhistories`
--
ALTER TABLE `auctionhistories`
  MODIFY `AuctionHistoryId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `blogs`
--
ALTER TABLE `blogs`
  MODIFY `BlogId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `CategoryId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `contactadmins`
--
ALTER TABLE `contactadmins`
  MODIFY `ContactAdminId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `contacts`
--
ALTER TABLE `contacts`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `gateways`
--
ALTER TABLE `gateways`
  MODIFY `GatewayId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `generalsettings`
--
ALTER TABLE `generalsettings`
  MODIFY `GeneralId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `invoices`
--
ALTER TABLE `invoices`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `photos`
--
ALTER TABLE `photos`
  MODIFY `PhotoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `porfolios`
--
ALTER TABLE `porfolios`
  MODIFY `PorfolioId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `reviews`
--
ALTER TABLE `reviews`
  MODIFY `ReviewId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `roleclaims`
--
ALTER TABLE `roleclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `shippingaddresses`
--
ALTER TABLE `shippingaddresses`
  MODIFY `ShippingAddressId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `slideshows`
--
ALTER TABLE `slideshows`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `userclaims`
--
ALTER TABLE `userclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `warehouseaddresses`
--
ALTER TABLE `warehouseaddresses`
  MODIFY `WareHouseId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `wishlists`
--
ALTER TABLE `wishlists`
  MODIFY `WishListId` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `auctionhistories`
--
ALTER TABLE `auctionhistories`
  ADD CONSTRAINT `FK_AuctionHistories_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AuctionHistories_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `blogs`
--
ALTER TABLE `blogs`
  ADD CONSTRAINT `FK_Blogs_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `contactadmins`
--
ALTER TABLE `contactadmins`
  ADD CONSTRAINT `FK_ContactAdmins_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `invoicedetails`
--
ALTER TABLE `invoicedetails`
  ADD CONSTRAINT `FK_InvoiceDetail_Invoice` FOREIGN KEY (`InvoiceId`) REFERENCES `invoices` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_InvoiceDetails_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductId`) ON DELETE CASCADE;

--
-- Constraints for table `invoices`
--
ALTER TABLE `invoices`
  ADD CONSTRAINT `FK_Invoices_ShippingAddresses_ShippingAddressId` FOREIGN KEY (`ShippingAddressId`) REFERENCES `shippingaddresses` (`ShippingAddressId`),
  ADD CONSTRAINT `FK_Invoices_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `photos`
--
ALTER TABLE `photos`
  ADD CONSTRAINT `FK_Product_Photo` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductId`) ON DELETE CASCADE;

--
-- Constraints for table `porfolios`
--
ALTER TABLE `porfolios`
  ADD CONSTRAINT `FK_Porfolios_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Porfolios_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `FK_Category_Product` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`CategoryId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Products_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `reviews`
--
ALTER TABLE `reviews`
  ADD CONSTRAINT `FK_Reviews_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Reviews_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `roleclaims`
--
ALTER TABLE `roleclaims`
  ADD CONSTRAINT `FK_RoleClaims_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `userclaims`
--
ALTER TABLE `userclaims`
  ADD CONSTRAINT `FK_UserClaims_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `userlogins`
--
ALTER TABLE `userlogins`
  ADD CONSTRAINT `FK_UserLogins_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `userroles`
--
ALTER TABLE `userroles`
  ADD CONSTRAINT `FK_UserRoles_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_UserRoles_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `FK_Users_Addresses_AddressId` FOREIGN KEY (`AddressId`) REFERENCES `addresses` (`AddressId`) ON DELETE CASCADE;

--
-- Constraints for table `usertokens`
--
ALTER TABLE `usertokens`
  ADD CONSTRAINT `FK_UserTokens_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `wishlists`
--
ALTER TABLE `wishlists`
  ADD CONSTRAINT `FK_WishLists_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_WishLists_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
