-- Banco já existente, criado antes da migration '20261009222528_AdicionarTagsEImagemRegistro':
-- roda isso contra `sistema_registros` pra ficar em dia sem precisar de `dotnet ef database update`.
-- Idempotente (seguro rodar de novo).

ALTER TABLE `registros` ADD COLUMN IF NOT EXISTS `tags` longtext CHARACTER SET utf8mb4 NULL;
ALTER TABLE `registros` ADD COLUMN IF NOT EXISTS `imagem_url` longtext CHARACTER SET utf8mb4 NULL;

INSERT IGNORE INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261009222528_AdicionarTagsEImagemRegistro', '9.0.0');
