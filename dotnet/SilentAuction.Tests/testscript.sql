--BEGIN TRAN

USE final_capstone


--empty database

Delete from item_category
Delete from category
Delete from item
Delete from auction


--Dummy Data--

INSERT INTO auction (start_time, end_time) VALUES ('2020-08-15T09:00:00', '2020-08-16T09:00:00');
Declare @auctionID as int = @@IDENTITY
INSERT INTO item (auction_id, title, subtitle, pic, starting_bid) 
	VALUES (@auctionID, 'Josh''s Waterbottle', 'This waterbottle was drunk from by the one and only Josh Tucholski.', 
	'https://images.unsplash.com/photo-1523362628745-0c100150b504?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1493&q=80', 
	150);
declare @item1 as int = @@Identity

INSERT INTO item (auction_id, title, subtitle, pic, starting_bid) 
	VALUES (@auctionID, 'Clyde''s Special Catnip', 'A secret blend of herbs, spices, and proteins that every cat will love.', 
	'https://i.insider.com/5b2d07195e48eca9028b458d?width=750&format=jpeg&auto=webp', 
	10);
declare @item2 as int = @@Identity

INSERT INTO item (auction_id, title, subtitle, pic, starting_bid) 
	VALUES (@auctionID, 'Hotel Stay at the beach for 2', 'One night with a lovely ocean view', 
	'https://www.simplemost.com/wp-content/uploads/2016/08/beach-vacation-e1470663653924.jpeg',
	150);
declare @item3 as int = @@Identity

INSERT INTO item (auction_id, title, subtitle, pic, starting_bid) 
	VALUES (@auctionID, 'Baseball signed by Omar Vizquel', 'Signed by the world''s greatest shortstop', 
	'https://i1.wp.com/www.cooperstowncred.com/wp-content/uploads/2018/12/OMAR-VIZQUEL-CHUCK-CROW.png?fit=922%2C644&ssl=1',
	250);
declare @item4 as int = @@Identity

INSERT INTO item (auction_id, title, subtitle, pic, starting_bid) 
	VALUES (@auctionID, 'Fruit Basket', 'You may not eat it all, but it looks nice', 
	'https://www.bachatagifts.com/124-thickbox_default/fruit-basket-big-delivered-in-dominican-republic.jpg',
	10);
declare @item5 as int = @@Identity

INSERT INTO category (name) VALUES ('Celebrity');
Declare @category1 as int = @@IDENTITY
INSERT INTO category (name) VALUES ('Sports Memorabilia');
Declare @category2 as int = @@IDENTITY
INSERT INTO category (name) VALUES ('Animals');
Declare @category3 as int = @@IDENTITY
INSERT INTO category (name) VALUES ('Relaxation');
Declare @category4 as int = @@IDENTITY
INSERT INTO category (name) VALUES ('Horcrux');
Declare @category5 as int = @@IDENTITY
INSERT INTO category (name) VALUES ('Vacation');
Declare @category6 as int = @@IDENTITY
INSERT INTO category (name) VALUES ('Food');
Declare @category7 as int = @@IDENTITY

INSERT INTO item_category (item_id, category_id) VALUES (@item1, @category1);
INSERT INTO item_category (item_id, category_id) VALUES (@item1, @category2);
INSERT INTO item_category (item_id, category_id) VALUES (@item1, @category5);

INSERT INTO item_category (item_id, category_id) VALUES (@item2, @category1);
INSERT INTO item_category (item_id, category_id) VALUES (@item2, @category4);

INSERT INTO item_category (item_id, category_id) VALUES (@item3, @category6);

INSERT INTO item_category (item_id, category_id) VALUES (@item4, @category1);
INSERT INTO item_category (item_id, category_id) VALUES (@item4, @category2);

INSERT INTO item_category (item_id, category_id) VALUES (@item5, @category7);
INSERT INTO item_category (item_id, category_id) VALUES (@item5, @category4);


--ROLLBACK TRAN