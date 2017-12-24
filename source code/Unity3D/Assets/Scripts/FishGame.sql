/*
 Navicat MySQL Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50718
 Source Host           : localhost
 Source Database       : FishGame

 Target Server Type    : MySQL
 Target Server Version : 50718
 File Encoding         : utf-8

 Date: 06/19/2017 20:23:35 PM
*/

SET NAMES utf8;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
--  Table structure for `Fish`
-- ----------------------------
DROP TABLE IF EXISTS `Fish`;
CREATE TABLE `Fish` (
  `level` int(10) unsigned NOT NULL,
  `force` float DEFAULT NULL,
  `patience` int(10) unsigned DEFAULT NULL,
  `sensitivity` float DEFAULT NULL,
  `depth` int(11) DEFAULT NULL,
  PRIMARY KEY (`level`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Records of `Fish`
-- ----------------------------
BEGIN;
INSERT INTO `Fish` VALUES ('1', '4', '15', '5', '30'), ('2', '6', '10', '4', '35'), ('3', '8', '8', '3', '50');
COMMIT;

-- ----------------------------
--  Table structure for `Scene`
-- ----------------------------
DROP TABLE IF EXISTS `Scene`;
CREATE TABLE `Scene` (
  `level` int(10) unsigned NOT NULL,
  `score` int(11) DEFAULT NULL,
  PRIMARY KEY (`level`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Records of `Scene`
-- ----------------------------
BEGIN;
INSERT INTO `Scene` VALUES ('1', '15'), ('2', '20'), ('3', '25');
COMMIT;

-- ----------------------------
--  Table structure for `User`
-- ----------------------------
DROP TABLE IF EXISTS `User`;
CREATE TABLE `User` (
  `name` varchar(255) NOT NULL,
  `level` int(10) unsigned DEFAULT NULL,
  `score` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

SET FOREIGN_KEY_CHECKS = 1;
