--Begin tran
USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
DECLARE @startTime datetime; SET @startTime = '2020-08-10T09:00:00'
DECLARE @endTime datetime; SET @endTime = '2020-08-14T10:08:00'
--the above variables cannot be set into a check constraint

CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	name varchar(50) NOT NULL, 
	address varchar(200) NOT NULL,
	phone_number varchar(15) NOT NULL,
	contact_times varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE auction (
	auction_id int IDENTITY(1, 1) NOT NULL,
	--org_name varchar(50) NOT NULL,
	start_time datetime NOT NULL,
	end_time datetime NOT NULL
	CONSTRAINT PK_auction_id PRIMARY KEY (auction_id)
)

INSERT INTO auction (start_time, end_time) VALUES (@startTime, @endTime);

CREATE TABLE item (
	item_id int IDENTITY(1, 1) NOT NULL,
	donor varchar(100) NOT NULL,
	auction_id int NOT NULL,
	title varchar(100) NOT NULL,
	subtitle varchar(240) NOT NULL,
	description varchar(1000) NOT NULL,
	pic varchar(200) NOT NULL,
	starting_bid decimal NOT NULL,
	--is_sold bit
	CONSTRAINT PK_item PRIMARY KEY (item_id),
	CONSTRAINT FK_item_auction FOREIGN KEY (auction_id) REFERENCES auction(auction_id)
)

CREATE TABLE category (
	category_id int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	name varchar(100) NOT NULL,
)

CREATE TABLE item_category (
	item_id int NOT NULL,
	category_id int NOT NULL,
	CONSTRAINT pk_item_category PRIMARY KEY (item_id, category_id),
	CONSTRAINT fk_item_category_item FOREIGN KEY (item_id) REFERENCES item(item_id),
	CONSTRAINT fk_item_category_category FOREIGN KEY (category_id) REFERENCES category(category_id)
)
Create Table bid (
	bid_id int IDENTITY Not NULL,
	item_id int NOT NULL,
	user_id int NOT NULL,
	amount decimal NOT NUll,
	time_placed datetime NOT NULL,
	CONSTRAINT pk_bid PRIMARY KEY (bid_id),
	CONSTRAINT fk_bid_item FOREIGN KEY (item_id) References item(item_id),
	CONSTRAINT fk_bid_user FOREIGN KEY (user_id) References users(user_id),
	CHECK (time_placed >= '2020-08-10T09:00:00' and time_placed <= '2020-08-14T10:08:00')
	)



--populate default data
INSERT INTO users (username, name, address, phone_number, contact_times, password_hash, salt, user_role) VALUES ('user', 'Tom Hanks', '123 Dummydata Ave. North Canton OH, 44720', '330-867-5309', 'Evenings', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, name, address, phone_number, contact_times, password_hash, salt, user_role) VALUES ('admin', 'Daniel Data', '135 Dummydata Ave. North Canton OH, 44720', '330-876-5903', 'Evenings', 'mAedat2HHvUYPW7tzPmaz7oxjSI=', 'MGPhTggCRKc=','admin');
INSERT INTO users (username, name, address, phone_number, contact_times, password_hash, salt, user_role) VALUES ('bill', 'Daniel Data', '135 Dummydata Ave. North Canton OH, 44720', '330-876-5903', 'Evenings', 'mAedat2HHvUYPW7tzPmaz7oxjSI=', 'MGPhTggCRKc=','user');
INSERT INTO users (username, name, address, phone_number, contact_times, password_hash, salt, user_role) VALUES ('george', 'Daniel Data', '135 Dummydata Ave. North Canton OH, 44720', '330-876-5903', 'Evenings', 'mAedat2HHvUYPW7tzPmaz7oxjSI=', 'MGPhTggCRKc=','user');
GO

--Dummy Data--

INSERT INTO category (name) VALUES ('Celebrity');
INSERT INTO category (name) VALUES ('Sports Memorabilia');
INSERT INTO category (name) VALUES ('Musical Instrument');
INSERT INTO category (name) VALUES ('Clothes');
INSERT INTO category (name) VALUES ('Animals');
INSERT INTO category (name) VALUES ('Relaxation');
INSERT INTO category (name) VALUES ('Vacation');
INSERT INTO category (name) VALUES ('Food');
INSERT INTO category (name) VALUES ('Entertainment Memorabilia');
INSERT INTO category (name) VALUES ('Electronics');
INSERT INTO category (name) VALUES ('Shoes');
INSERT INTO category (name) VALUES ('Sporting Goods');
INSERT INTO category (name) VALUES ('Motors');
INSERT INTO category (name) VALUES ('Toys');
INSERT INTO category (name) VALUES ('Hobbies');
INSERT INTO category (name) VALUES ('Exercise');
INSERT INTO category (name) VALUES ('Tickets & Experiences');


INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Marty Mordarski', 1, 'Josh''s Waterbottle', 'This waterbottle was drunk from by the one and only Josh Tucholski', 
	'https://i.imgur.com/uVQERGW.png', 
	150, 'Quench your thirst and learn to code! This waterbottle is infused with electrolytes and electrobytes! You will naturally get better at coding, simply by using this waterbottle. Tall. Hollow. A cap to close it. Yes, this waterbottle truly has it all.');
	

INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Amy Cave', 1, 'Clyde''s Special Catnip', 'A secret blend of herbs, spices, and proteins that every cat will love.', 
	'https://i.imgur.com/ijv1ZpX.png', 
	10, 'Clyde has been around for a long time (in cat years) and he knows good catnip when he smells it! This catnip will make your cat soar, and go places a cat can only go while under the strong effects that only Clyde''s Special Catnip can provide. (Note, do not let your cat operate heavy machinery under the effects of catnip. On second thought, never let your cat operate heavy machinery)');


INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Mike Morel', 1, 'Hotel Stay at the Beach for Two', 'One night with a lovely ocean view in Myrtle Beach', 
	'https://i.imgur.com/kkw3OkM.png',
	150, 'Can''t travel internationally because of COVID-19? Has a trip to Africa been taken off this summer''s itinerary? This beachfront Myrtle Beach property offers suites with a kitchen. A large outdoor pool is featured. Myrtle Beach Boardwalk Promenade shops and restaurants are 2 minutes’ walk away.');


INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('José Mesa', 1, 'Baseball signed by Omar Vizquel', 'Signed by the world''s greatest shortstop', 
	'https://i.imgur.com/IxU9PSA.png',
	250, 'When José Mesa blew the save for Cleveland in Game 7 of the 1997 World Series, it started a chain of events that would end with him getting traded within a year and beginning a very, very long feud with former teammate and friend Omar Vizquel. Before the feud began, José Mesa snagged a signed ball from Omar Vizquel, and now, it can be yours.');


INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Carmen Miranda', 1, 'Fruit Basket', 'You may not eat it all, but it looks nice', 
	'https://i.imgur.com/nqNjfwu.png',
	10, 'I''m a Chiquita Banana and I''m here to say: there is a lot of fruit in this basket! Pineapple, plums, apples, grapes, kiwi, mango, grapes, oranges, pears... Got fruit? You will soon, if you snag this delicious fruit basket!');

INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Chauncey Leopardi', 1, 'Squints'' Hat', 'The iconic hat that Michael ''Squints'' Palledorous wore in the film The Sandlot', 
	'https://i.imgur.com/AuevVUs.jpg',
	10, 'Michael ''Squints'' Palledorous is a smart aleck kid who wears glasses with thick black frames. Squints wears a plain black hat. He was portrayed by Chauncey Leopardi. You could own this hat for-ev-ver. For-ev-ver. For-ev-ver.');

INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Arthur C. Clarke', 1, 'HAL 9000 Replica', 'HAL 9000 is a fictional artificial intelligence character', 
	'https://i.imgur.com/L5n3KDi.png',
	10, '"I''m afraid I can''t do that Dave." This is a replica of the HAL 9000 from the film 2001: A Space Odyssey. As long as your superiors don''t order you to disconnect HAL, you have nothing to worry about.');

INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Stanley Yelnats IV', 1, 'Clyde "Sweet Feet" Livingston''s Shoes', 'These are the shoes modeled after the shoes from the 2003 film Holes', 
	'https://i.imgur.com/J6UL3Yp.png',
	10, '"Sweet Feet" Livingston was a "Major League" baseball player for the Texas Rangers. He got his name from having a foot fungus that made his feet smell bad. Rest assured, these were not actually worn by fictional character Clyde Livingston, and smell just fine! Note: If you steal these shoes from the auction, you may be forced to attend Camp Green Lake');

INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Dooley Wilson', 1, 'Piano from ''Casablanca''', 'The painted upright piano that adorned Rick''s Cafe in the classic movie "Casablanca"', 
	'https://i.imgur.com/fPQ0RP6.png',
	10, 'The orange piano -- on which Sam famously plays "As Time Goes By" at the request of his one-time love Ilsa from the film ''Casablanca.'' The piano featured prominently in the Oscar-winning 1942 romantic drama, with leading man Humphrey Bogart using it as a hiding place for the letters of transit that ultimately secure his former lover''s safe passage to the United States. Last sold at auction for $3.4 million, own it today!');

INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Buddy Love', 1, 'Sweat Shout: An Aerobic Workout', 'Richard Simmons brings us yet another aerobic workout to dance and shout to and work off all that bodyfat', 
	'https://i.imgur.com/fHOWyxY.png',
	10, '"People are still grasping for the miracle, and unfortunately, there are no miracles, if you do not exercise for a year, and let your body remain dormant, then you will gain weight." This is what Richard Simmons has to say on finding a weight-loss strategy that works for you, and look no further than this classic VHS!');

INSERT INTO item (donor, auction_id, title, subtitle, pic, starting_bid, description) 
	VALUES ('Bob Elk', 1, 'John Deere Ground Force Tractor with Trailer', 'Peg Perego''s rideable tractor perfect for the young one in your family', 
	'https://i.imgur.com/oBPDRVP.png',
	10, '"It ain''t much, but it''s honest work." Teach the little one in your life how to plow the fields. The best way to build grit and character, is a childhood spent on the farm. 2 speeds plus reverse (2¼ & 4½ mph). Accelerator pedal with automatic brakes. Farm tractor wheels provide traction on grass, dirt, pavement, or gravel. Working FM radio! (Child not included)');

--categories
INSERT INTO item_category (item_id, category_id) VALUES (1, 1);
INSERT INTO item_category (item_id, category_id) VALUES (1, 8);

INSERT INTO item_category (item_id, category_id) VALUES (2, 5);

INSERT INTO item_category (item_id, category_id) VALUES (3, 6);
INSERT INTO item_category (item_id, category_id) VALUES (3, 7);
INSERT INTO item_category (item_id, category_id) VALUES (3, 17);

INSERT INTO item_category (item_id, category_id) VALUES (4, 2);
INSERT INTO item_category (item_id, category_id) VALUES (4, 12);

INSERT INTO item_category (item_id, category_id) VALUES (5, 8);

INSERT INTO item_category (item_id, category_id) VALUES (6, 1);
INSERT INTO item_category (item_id, category_id) VALUES (6, 4);
INSERT INTO item_category (item_id, category_id) VALUES (6, 9);

INSERT INTO item_category (item_id, category_id) VALUES (7, 9);
INSERT INTO item_category (item_id, category_id) VALUES (7, 10);

INSERT INTO item_category (item_id, category_id) VALUES (8, 1);
INSERT INTO item_category (item_id, category_id) VALUES (8, 9);
INSERT INTO item_category (item_id, category_id) VALUES (8, 11);

INSERT INTO item_category (item_id, category_id) VALUES (9, 3);
INSERT INTO item_category (item_id, category_id) VALUES (9, 9);
INSERT INTO item_category (item_id, category_id) VALUES (9, 15);

INSERT INTO item_category (item_id, category_id) VALUES (10, 1);
INSERT INTO item_category (item_id, category_id) VALUES (10, 12);
INSERT INTO item_category (item_id, category_id) VALUES (10, 16);

INSERT INTO item_category (item_id, category_id) VALUES (11, 10);
INSERT INTO item_category (item_id, category_id) VALUES (11, 13);
INSERT INTO item_category (item_id, category_id) VALUES (11, 14);

--bids
-- 11 items

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (1, 1, 150.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (1, 2, 155.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (1, 1, 160.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (1, 2, 170.00, '2020-08-13T09:03:00' )


INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (2, 1, 10.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (2, 2, 11.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (2, 1, 15.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (2, 2, 20.00, '2020-08-13T09:03:00' )
																					   

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (3, 1, 150.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (3, 2, 160.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (3, 1, 161.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (3, 2, 170.00, '2020-08-13T09:03:00' )


INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (4, 1, 250.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (4, 2, 255.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (4, 1, 265.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (4, 3, 270.00, '2020-08-13T09:03:00' )

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (5, 4, 20.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (5, 3, 25.00, '2020-08-13T09:01:00' )

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (6, 2, 400.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (6, 4, 450.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (6, 2, 500.00, '2020-08-13T09:03:00' )

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (7, 3, 1000.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (7, 2, 1200.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (7, 4, 1225.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (7, 2, 1250.00, '2020-08-13T09:03:00' )

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (8, 3, 750.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (8, 2, 800.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (8, 1, 900.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (8, 3, 1000.00, '2020-08-13T09:03:00' )

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (9, 4, 7500.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (9, 1, 8000.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (9, 4, 9000.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (9, 3, 10000.00, '2020-08-13T09:03:00' )

INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (11, 3, 25.00, '2020-08-13T09:03:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (11, 1, 30.00, '2020-08-13T09:00:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (11, 3, 35.00, '2020-08-13T09:01:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (11, 2, 45.00, '2020-08-13T09:02:00' )
INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (11, 1, 50.00, '2020-08-13T09:03:00' )

																


Go
CREATE PROCEDURE GetAllItems
AS Begin
	Select * From Item order by title; 

	Select * From item_category IC 
	JOIN Category C on IC.category_id = c.category_id;

	SELECT  * From bid 
	JOIN item on bid.item_id = item.item_id
	JOIN users on users.user_id = bid.user_id Order by amount desc
End

Go
CREATE PROCEDURE EndForUser
@userID int
As Begin
select * from bid 
	join (select item_id, Max(amount) as winningBid from bid group by item_id) as topBids
	on bid.item_id = topBids.item_id and bid.amount = topBids.winningBid
	Join item on bid.item_id = item.item_id
	Where user_id = @userID
End
		--1 wins 11
	--2 wins 1,2,3,6,7
	--3 wins 4,5,8,9
	--4 wins none

