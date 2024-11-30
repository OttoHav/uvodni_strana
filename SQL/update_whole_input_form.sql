-- Aktualizace existujících dat v tabulce Votes
UPDATE voteparam
SET
    namevote = @namevote,
    companyvote = @firma,
    placevote = @misto,
    managervote = @vedouci,
    manageremail = @email,
    auditorvote = @auditor,
    auditoremail = @emailauditor
WHERE
    id = @id;
