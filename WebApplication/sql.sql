CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Album" (
    "Id" bigserial NOT NULL,
    "Cover" text NULL,
    "Title" text NULL,
    CONSTRAINT "PK_Album" PRIMARY KEY ("Id")
);

CREATE TABLE "Feedbacks" (
    "Id" bigserial NOT NULL,
    "Name" text NULL,
    "Email" text NULL,
    "CreateAt" timestamp without time zone NOT NULL,
    "Content" text NOT NULL,
    CONSTRAINT "PK_Feedbacks" PRIMARY KEY ("Id")
);

CREATE TABLE "VideoDetail" (
    "Id" bigserial NOT NULL,
    "Height" integer NOT NULL,
    "Width" integer NOT NULL,
    "Duration" text NULL,
    CONSTRAINT "PK_VideoDetail" PRIMARY KEY ("Id")
);

CREATE TABLE "Videos" (
    "Id" bigserial NOT NULL,
    "AlbumId" bigint NULL,
    "Cover" text NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "Tags" text[] NULL,
    "Thumbnail" text NULL,
    "Title" character varying(100) NOT NULL,
    "UpdatedAt" timestamp without time zone NOT NULL,
    "Url" character varying(512) NOT NULL,
    "VoteDown" integer NOT NULL,
    "VoteUp" integer NOT NULL,
    "WatchedCount" integer NOT NULL,
    "VideoDetailId" bigint NULL,
    CONSTRAINT "PK_Videos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Videos_Album_AlbumId" FOREIGN KEY ("AlbumId") REFERENCES "Album" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Videos_VideoDetail_VideoDetailId" FOREIGN KEY ("VideoDetailId") REFERENCES "VideoDetail" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Videos_AlbumId" ON "Videos" ("AlbumId");

CREATE INDEX "IX_Videos_VideoDetailId" ON "Videos" ("VideoDetailId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190222123855_Initial', '2.2.2-servicing-10034');


