CREATE TABLE "user" (
	id UUID NOT NULL PRIMARY KEY,
	"key" TEXT NOT NULL,
	email TEXT NOT NULL,
	given_name TEXT NOT NULL,
	family_name TEXT NOT NULL,
	password_hash TEXT NOT NULL,
	password_salt BYTEA NOT NULL,
	created_by_id UUID NOT NULL,
	created_on TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_by_id UUID,
	updated_on TIMESTAMP WITH TIME ZONE
);

CREATE TABLE account_verification (
	"key" TEXT NOT NULL PRIMARY KEY,
	account_verification_token TEXT NOT NULL,
	account_verification_token_expiry TIMESTAMP WITH TIME ZONE,
	is_verified BOOLEAN,
	"user_id" UUID,
	created_by_id UUID NOT NULL,
	created_on TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_by_id UUID,
	updated_on TIMESTAMP WITH TIME ZONE
);

ALTER TABLE "user" ADD CONSTRAINT uq_user_email UNIQUE (email);
ALTER TABLE account_verification ADD CONSTRAINT fk_accountverification_userid_user_id  FOREIGN KEY (user_id) REFERENCES "user" (id);
ALTER TABLE account_verification ADD CONSTRAINT fk_accountverification_createdbyid_user_id  FOREIGN KEY (created_by_id) REFERENCES "user" (id);
ALTER TABLE account_verification ADD CONSTRAINT fk_accountverification_updatedbyid_user_id  FOREIGN KEY (updated_by_id) REFERENCES "user" (id);


INSERT INTO "user" (id, "key", email, given_name, family_name, password_hash, password_salt, created_by_id, created_on, updated_by_id, updated_on)
VALUES ('77c6bc21-4ae9-425d-aa9c-5f4290070ed0', 'system_user', 'system@swimmingpool.com', 'System', 'User', 'aRe0exdxHbhPHE9yxWmiYapPUGSFG15KWnX8517J+eM=', decode('0742b4969bebb3b9e5cf233a1bdfaf1c', 'hex'), '77c6bc21-4ae9-425d-aa9c-5f4290070ed0', current_timestamp, null, null)
ON CONFLICT DO NOTHING;

INSERT INTO account_verification ("key", account_verification_token, account_verification_token_expiry, is_verified, "user_id", created_by_id, created_on, updated_by_id, updated_on)
VALUES ('system_user_verification', 'alwaysverified', current_timestamp, TRUE, '77c6bc21-4ae9-425d-aa9c-5f4290070ed0', '77c6bc21-4ae9-425d-aa9c-5f4290070ed0', current_timestamp, null, null)
ON CONFLICT DO NOTHING;