INSERT INTO accounts
    (account_id, account_name, account_mail, account_password_hash, account_password_salt, account_create_date, account_change_date)
SELECT 1, 'administrator', 'administrator', '"fPEKB0lhNRvAsfO5lwhIh1EL+a0psLqk4UxUWCiQNmGCXWdD7cwAxInJLql53KJFmgcxaDTOaDjsTbcmovsp+g=="', '"mETerjGvZrr6uiU3"', Now(), Now()
WHERE
    NOT EXISTS (
        SELECT account_id FROM accounts WHERE account_id = 1
    );