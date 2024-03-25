DO $$
DECLARE 
	administrator_account_id integer := 0;

BEGIN
	INSERT INTO accounts
    	(account_name, account_mail, account_password_hash, account_password_salt, account_create_date, account_change_date)
		SELECT 'administrator', 'administrator', '"fPEKB0lhNRvAsfO5lwhIh1EL+a0psLqk4UxUWCiQNmGCXWdD7cwAxInJLql53KJFmgcxaDTOaDjsTbcmovsp+g=="', '"mETerjGvZrr6uiU3"', Now(), Now()
		WHERE NOT EXISTS (
        	SELECT account_id FROM accounts WHERE account_name = 'administrator'
		)    
RETURNING account_id INTO administrator_account_id;

END $$