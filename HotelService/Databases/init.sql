--
-- PostgreSQL database dump
--

\restrict hhp9aUSh09gImizBDMpLgtbxZjCvg4IcoPv8xcCpglulzJUT4AUSybl3sbO61lC

-- Dumped from database version 16.11 (Ubuntu 16.11-0ubuntu0.24.04.1)
-- Dumped by pg_dump version 16.11 (Ubuntu 16.11-0ubuntu0.24.04.1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: HotelComments; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."HotelComments" (
    "Id" uuid NOT NULL,
    "HotelId" uuid NOT NULL,
    "PersonId" text NOT NULL,
    "Message" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."HotelComments" OWNER TO pauldor;

--
-- Name: HotelPhotos; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."HotelPhotos" (
    "Id" uuid NOT NULL,
    "HotelId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Key" text NOT NULL,
    "Type" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."HotelPhotos" OWNER TO pauldor;

--
-- Name: HotelRatings; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."HotelRatings" (
    "Id" uuid NOT NULL,
    "HotelId" uuid NOT NULL,
    "PersonId" text NOT NULL,
    "Value" smallint NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."HotelRatings" OWNER TO pauldor;

--
-- Name: HotelRooms; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."HotelRooms" (
    "Id" uuid NOT NULL,
    "HotelId" uuid NOT NULL,
    "NumberOfResidents" smallint NOT NULL,
    "Description" text NOT NULL
);


ALTER TABLE public."HotelRooms" OWNER TO pauldor;

--
-- Name: Hotels; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."Hotels" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Country" text NOT NULL,
    "City" text NOT NULL,
    "Stars" smallint NOT NULL,
    "Description" text NOT NULL,
    "RealRating" numeric NOT NULL,
    "ShownRating" numeric NOT NULL
);


ALTER TABLE public."Hotels" OWNER TO pauldor;

--
-- Name: Persons; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."Persons" (
    "Id" text NOT NULL,
    "Name" text NOT NULL
);


ALTER TABLE public."Persons" OWNER TO pauldor;

--
-- Name: RoomPhotos; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."RoomPhotos" (
    "Id" uuid NOT NULL,
    "RoomId" uuid NOT NULL,
    "HotelId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Key" text NOT NULL,
    "Type" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "HotelRoomId" uuid
);


ALTER TABLE public."RoomPhotos" OWNER TO pauldor;

--
-- Name: RoomStates; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."RoomStates" (
    "Id" uuid NOT NULL,
    "RoomId" uuid NOT NULL,
    "HotelId" uuid NOT NULL,
    "PersonId" text NOT NULL,
    "StartDate" date NOT NULL,
    "EndDate" date NOT NULL,
    "HotelRoomId" uuid
);


ALTER TABLE public."RoomStates" OWNER TO pauldor;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: pauldor
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO pauldor;

--
-- Data for Name: HotelComments; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."HotelComments" ("Id", "HotelId", "PersonId", "Message", "CreatedAt") FROM stdin;
\.


--
-- Data for Name: HotelPhotos; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."HotelPhotos" ("Id", "HotelId", "Name", "Key", "Type", "CreatedAt") FROM stdin;
\.


--
-- Data for Name: HotelRatings; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."HotelRatings" ("Id", "HotelId", "PersonId", "Value", "CreatedAt") FROM stdin;
\.


--
-- Data for Name: HotelRooms; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."HotelRooms" ("Id", "HotelId", "NumberOfResidents", "Description") FROM stdin;
\.


--
-- Data for Name: Hotels; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."Hotels" ("Id", "Name", "Country", "City", "Stars", "Description", "RealRating", "ShownRating") FROM stdin;
019b551c-a6f1-77de-b70d-226f587304bf	FiveStars	Russia	Penza	4	Super hotel mega	0	0
\.


--
-- Data for Name: Persons; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."Persons" ("Id", "Name") FROM stdin;
af2b3c8b-39a3-40e5-a4dc-198d4241517f	User1
6cc714ac-a5fd-469e-b898-efd6667aaf39	User2
6abfb7f8-6572-43ea-aa82-2f6073e78194	User3
567a1546-0241-4a6a-bd10-bbe6a3260867	User4
97a564f1-9af0-4bdf-beeb-bed1dd745b9a	User44
\.


--
-- Data for Name: RoomPhotos; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."RoomPhotos" ("Id", "RoomId", "HotelId", "Name", "Key", "Type", "CreatedAt", "HotelRoomId") FROM stdin;
\.


--
-- Data for Name: RoomStates; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."RoomStates" ("Id", "RoomId", "HotelId", "PersonId", "StartDate", "EndDate", "HotelRoomId") FROM stdin;
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: pauldor
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20251224142916_FirstMigration	10.0.1
\.


--
-- Name: HotelComments PK_HotelComments; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."HotelComments"
    ADD CONSTRAINT "PK_HotelComments" PRIMARY KEY ("Id");


--
-- Name: HotelPhotos PK_HotelPhotos; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."HotelPhotos"
    ADD CONSTRAINT "PK_HotelPhotos" PRIMARY KEY ("Id");


--
-- Name: HotelRatings PK_HotelRatings; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."HotelRatings"
    ADD CONSTRAINT "PK_HotelRatings" PRIMARY KEY ("Id");


--
-- Name: HotelRooms PK_HotelRooms; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."HotelRooms"
    ADD CONSTRAINT "PK_HotelRooms" PRIMARY KEY ("Id");


--
-- Name: Hotels PK_Hotels; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."Hotels"
    ADD CONSTRAINT "PK_Hotels" PRIMARY KEY ("Id");


--
-- Name: Persons PK_Persons; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."Persons"
    ADD CONSTRAINT "PK_Persons" PRIMARY KEY ("Id");


--
-- Name: RoomPhotos PK_RoomPhotos; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."RoomPhotos"
    ADD CONSTRAINT "PK_RoomPhotos" PRIMARY KEY ("Id");


--
-- Name: RoomStates PK_RoomStates; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."RoomStates"
    ADD CONSTRAINT "PK_RoomStates" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_HotelComments_HotelId; Type: INDEX; Schema: public; Owner: pauldor
--

CREATE INDEX "IX_HotelComments_HotelId" ON public."HotelComments" USING btree ("HotelId");


--
-- Name: IX_HotelPhotos_HotelId; Type: INDEX; Schema: public; Owner: pauldor
--

CREATE INDEX "IX_HotelPhotos_HotelId" ON public."HotelPhotos" USING btree ("HotelId");


--
-- Name: IX_HotelRooms_HotelId; Type: INDEX; Schema: public; Owner: pauldor
--

CREATE INDEX "IX_HotelRooms_HotelId" ON public."HotelRooms" USING btree ("HotelId");


--
-- Name: IX_RoomPhotos_HotelRoomId; Type: INDEX; Schema: public; Owner: pauldor
--

CREATE INDEX "IX_RoomPhotos_HotelRoomId" ON public."RoomPhotos" USING btree ("HotelRoomId");


--
-- Name: IX_RoomStates_HotelRoomId; Type: INDEX; Schema: public; Owner: pauldor
--

CREATE INDEX "IX_RoomStates_HotelRoomId" ON public."RoomStates" USING btree ("HotelRoomId");


--
-- Name: HotelComments FK_HotelComments_Hotels_HotelId; Type: FK CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."HotelComments"
    ADD CONSTRAINT "FK_HotelComments_Hotels_HotelId" FOREIGN KEY ("HotelId") REFERENCES public."Hotels"("Id") ON DELETE CASCADE;


--
-- Name: HotelPhotos FK_HotelPhotos_Hotels_HotelId; Type: FK CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."HotelPhotos"
    ADD CONSTRAINT "FK_HotelPhotos_Hotels_HotelId" FOREIGN KEY ("HotelId") REFERENCES public."Hotels"("Id") ON DELETE CASCADE;


--
-- Name: HotelRooms FK_HotelRooms_Hotels_HotelId; Type: FK CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."HotelRooms"
    ADD CONSTRAINT "FK_HotelRooms_Hotels_HotelId" FOREIGN KEY ("HotelId") REFERENCES public."Hotels"("Id") ON DELETE CASCADE;


--
-- Name: RoomPhotos FK_RoomPhotos_HotelRooms_HotelRoomId; Type: FK CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."RoomPhotos"
    ADD CONSTRAINT "FK_RoomPhotos_HotelRooms_HotelRoomId" FOREIGN KEY ("HotelRoomId") REFERENCES public."HotelRooms"("Id");


--
-- Name: RoomStates FK_RoomStates_HotelRooms_HotelRoomId; Type: FK CONSTRAINT; Schema: public; Owner: pauldor
--

ALTER TABLE ONLY public."RoomStates"
    ADD CONSTRAINT "FK_RoomStates_HotelRooms_HotelRoomId" FOREIGN KEY ("HotelRoomId") REFERENCES public."HotelRooms"("Id");


--
-- PostgreSQL database dump complete
--

\unrestrict hhp9aUSh09gImizBDMpLgtbxZjCvg4IcoPv8xcCpglulzJUT4AUSybl3sbO61lC

